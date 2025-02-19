using System;
using System.Collections.Generic;
using System.Linq;
using DatasTypes;
using DG.Tweening;
using EnhancedUI.EnhancedScroller;
using Trucking.Ad;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.CellView;
using Trucking.UI.Guide;
using Trucking.UI.Mission;
using Trucking.UI.Popup;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace Trucking.UI
{
    public class EditView : MonoSingleton<EditView>, IEnhancedScrollerDelegate
    {
        public GameObject editView;
        public EditView_Change editViewChange;
        public EditVeiw_Allocate editVeiwAllocate;
        public EditView_AddTruck editViewAddTruck;
        public EditView_Setting editViewSetting;
        public EnhancedScroller scroller;
        public GameObject movingTruckText;
        public Button btnScrollTop;
        public Button btnScrollBottom;

        [HideInInspector] public City selectCity;
        [HideInInspector] public ReactiveProperty<Truck> selectedTruck = new ReactiveProperty<Truck>();


        private List<EditCellData> data = new List<EditCellData>();
        private int selectedCellIndex;

        private Road selectRoad;
        private Tweener tweener;

        private CompositeDisposable disposableButton = new CompositeDisposable();
        private CompositeDisposable disposableSelectedTruck = new CompositeDisposable();

        private void Awake()
        {
            disposableButton.AddTo(this);

            btnScrollTop.OnClickAsObservable().Subscribe(_ =>
            {
                scroller.JumpToDataIndex(0);
                ScrollerScrollingChangedDelegate(scroller, true);
            }).AddTo(this);

            btnScrollBottom.OnClickAsObservable().Subscribe(_ =>
            {
                scroller.JumpToDataIndex(data.Count - 1, 1, 1);
                ScrollerScrollingChangedDelegate(scroller, true);
            }).AddTo(this);

            selectedTruck.Subscribe(tr =>
            {
                disposableSelectedTruck.Clear();
                tr?.model.state.Pairwise().Subscribe(state =>
                {
                    if (state.Previous == TruckModel.State.Move
                        && state.Current == TruckModel.State.Wait
                        && tr.completeCargos.Count > 0)
                    {
                        Popup_Guide_RouteReward.Instance.Show(() =>
                        {
                            UIRewardManager.Instance.Show(selectedTruck.Value.currentStation.Value);
                        });
                    }
                }).AddTo(disposableSelectedTruck);
            }).AddTo(this);
        }

        public void Show()
        {
            AudioManager.Instance.PlaySound("sfx_mode_routes");

            if (scroller.Delegate == null)
            {
                scroller.Delegate = this;
                scroller.cellViewVisibilityChanged = CellViewVisibilityChanged;
                scroller.scrollerScrollingChanged = ScrollerScrollingChangedDelegate;
            }

            gameObject.SetActive(true);
            editViewChange.gameObject.SetActive(false);
            editVeiwAllocate.gameObject.SetActive(false);
            editViewAddTruck.gameObject.SetActive(false);
            editViewSetting.gameObject.SetActive(false);

            GameManager.Instance.fsm.PushState(GameManager.Instance.mapEditState);
            MakeList();
            ShowTween(true);
            movingTruckText.SetActive(false);
            SelectTruckLane(null);
//            SetAnimation();
        }

        public void Close()
        {
            ResetView();
            selectedCellIndex = -1;
            movingTruckText.SetActive(false);
//            GameManager.Instance.fsm.PopState();
//            Popup_RouteColorSetting.Instance.Close();
            disposableButton.Clear();
            GetComponent<Animator>().Play("edit_view_message_out", -1, 0f);
            ShowTween(false, () =>
            {
                editView.SetActive(false);
                scroller.ClearAll();

                GameManager.Instance.ClearUIObject3D();
                GC.Collect();
            });
        }

        void SetAnimation()
        {
            for (int i = scroller.StartCellViewIndex; i < data.Count; i++)
            {
                EnhancedScrollerCellView cellView = scroller.GetCellViewAtDataIndex(i);

                if (cellView != null)
                {
                    cellView.transform.GetChild(0).localScale = new Vector3(1, 0, 1);
                    cellView.transform.GetChild(0).DOScaleY(1, 0.3f)
                        .SetDelay((i - scroller.StartCellViewIndex) * 0.05f);
                }
            }
        }


        public void ShowTween(bool isShow, Action callback = null)
        {
            tweener?.Kill();

            if (isShow)
            {
                editView.SetActive(true);
//                scroller.RefreshActiveCellViews();
                editView.GetComponent<RectTransform>().anchoredPosition = new Vector2(-600,
                    editView.GetComponent<RectTransform>().anchoredPosition.y);
                tweener = editView.GetComponent<RectTransform>().DOAnchorPosX(0, 0.3f).OnComplete(() => tweener = null);
            }
            else
            {
                tweener = editView.GetComponent<RectTransform>().DOAnchorPosX(-600, 0.3f)
                    .OnComplete(() =>
                    {
                        callback?.Invoke();
                        tweener = null;
                    });
            }
        }

        void ScrollerScrollingChangedDelegate(EnhancedScroller scroller, bool scrolling)
        {
            btnScrollTop.gameObject.SetActive(scroller.NormalizedScrollPosition > 0);

            float endPos = scroller.GetScrollPositionForCellViewIndex(data.Count - 1,
                EnhancedScroller.CellViewPositionEnum.After);

            btnScrollBottom.gameObject.SetActive(scroller.NormalizedScrollPosition < 1 &&
                                                 scroller.ScrollRectSize < endPos);
        }

        public void MakeList()
        {
            disposableButton.Clear();
            selectedCellIndex = -1;
            data.Clear();
            scroller.ClearAll();

            foreach (var tr in GameManager.Instance.trucks)
            {
                if (tr.model.hasRoute.Value)
                {
                    EditCellData cell = new EditCellData();
                    cell.truck = tr;
                    cell.type = EditCellData.EditCellType.TRUCK;

                    if (tr.model.state.Value == TruckModel.State.Move
                        && tr.pathStation.Count > 0)
                    {
                        cell.targetCity = tr.pathStation.Last();

                        tr.model.state.Pairwise().Subscribe(value =>
                        {
                            if (value.Previous == TruckModel.State.Move
                                && value.Current == TruckModel.State.Wait)
                            {
                                cell.targetCity = null;
                                scroller.ReloadData(scroller.NormalizedScrollPosition);
                            }
                        }).AddTo(disposableButton);
                    }

                    data.Add(cell);
                }
            }

            data = data
                .OrderByDescending(x => x.truck.model.state.Value == TruckModel.State.Wait)
                .ThenByDescending(x => x.truck.model.state.Value == TruckModel.State.Move)
                .ThenBy(x => x.truck.GetLeftTime())
                .ToList();

//            int count = GameManager.Instance.trucks.Count(x => x.model.hasRoute.Value);
//
//            for (int i = 0; i < UserDataManager.Instance.data.maxRoute.Value - count; i++)
//            {
//                // addTruck cell
//                EditCellData cellAddTruck = new EditCellData();
//                cellAddTruck.type = EditCellData.EditCellType.ADD_TRUCK;
//                data.Add(cellAddTruck);
//            }

            if (GameManager.Instance.GetRouteCountLevelData()?.route_price > 0)
            {
                // add route cell
                EditCellData cellRoute = new EditCellData();
                cellRoute.type = EditCellData.EditCellType.ADD_ROUTE;
                data.Add(cellRoute);
            }

            foreach (var tr in GameManager.Instance.trucks)
            {
                if (!tr.model.hasRoute.Value)
                {
                    EditCellData cell = new EditCellData();
                    cell.truck = tr;
                    cell.type = EditCellData.EditCellType.TRUCK;

                    data.Add(cell);
                }
            }

            scroller.ReloadData(scroller.NormalizedScrollPosition);
//            scroller.RefreshActiveCellViews();            

            Observable.NextFrame().Subscribe(_ => { ScrollerScrollingChangedDelegate(scroller, true); }).AddTo(this);
        }

        public void SelectRoadCollider(Road road)
        {
            if (road.truck.Value != null)
            {
                int dataIndex = data.FindIndex(x => x.truck == road.truck.Value);

                if (dataIndex >= 0)
                {
                    var cell = scroller.GetCellViewAtDataIndex(dataIndex) as EditCellView;
                    OnClickItem(cell);
                    scroller.JumpToDataIndex(selectedCellIndex, 0.5f, 0.5f);
                }
            }
        }

        public void SelectTruckLane(Truck _truck = null, Truck _truck2 = null, bool isFocus = false)
        {
            selectedTruck.Value = _truck;

            foreach (var city in GameManager.Instance.cities)
            {
                city.ui.SetEditState(UICity.EditStateEnum.normal);
            }

            foreach (var road in GameManager.Instance.roads)
            {
                road.SetColorRoadAni((_truck != null && road.truck.Value == _truck)
                                     || (_truck2 != null && road.truck.Value == _truck2));
                road.SetFocusCloseRoad(false);
                road.ui.Clear();
            }

            RefreshScroll();

            if (isFocus && _truck != null && _truck.model.hasRoute.Value)
            {
                WorldMap.Instance.SetCamera(_truck.transform.position + new Vector3(-190, 0, 0));
            }

            if (_truck != null
                && _truck.model.state.Value == TruckModel.State.Move)
            {
                UIToastMassage.Instance.Show(20061, true);
            }
            else if (_truck != null
                     && _truck.model.state.Value == TruckModel.State.Wait)
            {
                if (GameManager.Instance.fsm.GetCurrentState() == GameManager.Instance.mapEditState)
                {
                    if (_truck.model.hasRoute.Value)
                    {
                        UIToastMassage.Instance.Show(30040, true);
                    }
                    else
                    {
                        UIToastMassage.Instance.Show(30039, true);
                    }
                }

                if (GameManager.Instance.fsm.GetCurrentState() == GameManager.Instance.mapEditState)
                {
                    List<City> focusedCities = new List<City>();

                    foreach (var city in GameManager.Instance.cities)
                    {
                        if (city.HasRoad(_truck))
                        {
                            focusedCities.Add(city);
                            city.ui.SetEditState(UICity.EditStateEnum.select, selectedTruck.Value.color);
                        }
                    }

                    foreach (var city in focusedCities)
                    {
                        city.ui.ShowEditTruck(selectedTruck.Value != null
                                              && selectedTruck.Value.currentStation.Value == city);

                        foreach (var linkedRoad in city.roads)
                        {
                            if (linkedRoad.truck.Value != null &&
                                linkedRoad.truck.Value.model.state.Value == TruckModel.State.Move)
                            {
                                continue;
                            }

                            if (linkedRoad.truck.Value == null)
                            {
                                if (linkedRoad.from.HasCloudOpen()
                                    && linkedRoad.to.HasCloudOpen())
                                {
                                    linkedRoad.ui.SetPlus();
                                }

                                if (linkedRoad.ui.gameObject.activeSelf)
                                {
                                    if (!linkedRoad.model.isOpen.Value)
                                    {
                                        linkedRoad.SetFocusCloseRoad(true);
                                    }
                                }
                            }
                            else if (linkedRoad.truck.Value == selectedTruck.Value)
                            {
                                linkedRoad.ui.SetMinus();
                            }
                        }
                    }
                }
            }
        }

        void ResetSelectRoad()
        {
            selectRoad = null;
        }

        void ResetSelectCity()
        {
            if (selectCity != null
                && selectedTruck.Value != null
                && selectedTruck.Value.model.state.Value == TruckModel.State.Wait)
            {
                AudioManager.Instance.PlaySound("sfx_button_cancle");
//                UIToastMassage.Instance.Show(30040, true);
            }

            selectCity = null;
        }

        public void Button_Focus(City _city)
        {
            if (GameManager.Instance.fsm.GetCurrentState() == GameManager.Instance.settingTruckState)
            {
                return;
            }

            selectCity = _city;
            Debug.Log("Set selectCity : " + selectCity);
            SelectTruckLane(selectedTruck.Value);
//            UIToastMassage.Instance.Show(30041, true);

            AudioManager.Instance.PlaySound("sfx_button_main");


            if (GameManager.Instance.fsm.GetCurrentState() == GameManager.Instance.allocateTruckState)
            {
                UIToastMassage.Instance.Show(30070, true);
                selectCity.ui.ShowEditTruck(true);

                foreach (var ct in GameManager.Instance.cities)
                {
                    if (ct != selectCity)
                    {
                        ct.ui.SetEditState(UICity.EditStateEnum.normal);
                    }
                }
            }

            foreach (var linkedRoad in selectCity.roads)
            {
                if (linkedRoad.truck.Value != null)
                {
                    continue;
                }


//                if (linkedRoad.truck.Value != null 
//                    && linkedRoad.truck.Value.model.state.Value == TruckModel.State.Wait)
//                {
                if (linkedRoad.truck.Value != selectedTruck.Value)
                {
                    if (linkedRoad.from != selectCity
                        && linkedRoad.from.HasCloudOpen())
                    {
                        linkedRoad.ui.SetPlus();
                    }
                    else if (linkedRoad.to != selectCity
                             && linkedRoad.to.HasCloudOpen())
                    {
                        linkedRoad.ui.SetPlus();
                    }

                    if (linkedRoad.ui.gameObject.activeSelf)
                    {
                        if (!linkedRoad.model.isOpen.Value)
                        {
                            linkedRoad.SetFocusCloseRoad(true);
                        }
                    }
                }

//                    else if (linkedRoad.truck.Value == selectedTruck)
//                    {
//                        linkedRoad.ui.SetMinus();
//                    }
//                }
            }
        }

        public void Button_Plus(City city, City targetCity)
        {
            if (city == targetCity)
            {
                return;
            }

            Road road = GameManager.Instance.FindRoad(city, targetCity);
            int roadCount = GameManager.Instance.roads.Count(x => x.model.isOpen.Value);
            long roadCost = Datas.roadCostData.ToArray().FirstOrDefault(x => x.sequence == roadCount).road_cost;

            if (road.model.isOpen.Value)
            {
                roadCost = Datas.levelData[0].route_install_cost;
            }

            Debug.Log($"Button_Plus : {targetCity.name}");

            if (GameManager.Instance.fsm.GetCurrentState() == GameManager.Instance.allocateTruckState)
            {
                AudioManager.Instance.PlaySound("sfx_button_main");

                LevelData levelData = GameManager.Instance.GetRouteCountLevelData();
                long routePrice = levelData.route_price + roadCost;

                Popup_Common.Instance.Show(Utilities.GetStringByData(20146),
                        Utilities.GetStringByData(20147))
//                    .SetResource(RewardData.eType.gold, routePrice)
                    .SetLeft(Utilities.GetStringByData(20066))
                    .SetRightReward(RewardData.eType.gold, routePrice, () =>
                    {
                        if (UserDataManager.Instance.UseGold(routePrice))
                        {
                            AddRoad(routePrice);
                        }
                    });
            }
            else
            {
                if (road.model.isOpen.Value)
                {
                    if (road.truck.Value != null)
                    {
                        AudioManager.Instance.PlaySound("sfx_require");
                        UIToastMassage.Instance.Show(30026);
                    }
                    else
                    {
                        AudioManager.Instance.PlaySound("sfx_button_main");

                        Popup_Common.Instance.Show(Utilities.GetStringByData(31406),
                                Utilities.GetStringByData(30036))
                            .SetCenterReward(RewardData.eType.gold,
                                roadCost,
                                ReInstallRoad);
                    }
                }
                else
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");

                    Popup_Common.Instance.Show(Utilities.GetStringByData(20076),
                            Utilities.GetStringByData(30037))
                        .SetLeftReward(RewardData.eType.gold, roadCost, () =>
                        {
                            if (UserDataManager.Instance.UseGold(roadCost))
                            {
                                AddRoad(roadCost);
                            }
                        });
                }
            }

            void AddRoad(long cost)
            {
                AudioManager.Instance.PlaySound("sfx_mode_routes_road_expansion");
                MakeRoad(city, targetCity, road, selectedTruck.Value);
                road.isBuy = true;

                ShowToolSlider(road, -cost, () => { SelectTruckLane(selectedTruck.Value); });
            }

            void ReInstallRoad()
            {
                AudioManager.Instance.PlaySound("sfx_mode_routes_road_expansion");
                UIRewardManager.Instance.Show(road.ui.transform.position, -roadCost, 0, 0);
                MakeRoad(city, targetCity, road, selectedTruck.Value);
                ShowToolSlider(road, -roadCost, () => { SelectTruckLane(selectedTruck.Value); });
            }
        }

        public void Button_Minus(City city, City targetCity)
        {
            Road road = GameManager.Instance.FindRoad(city, targetCity);
            int roadCount = GameManager.Instance.roads.ToList().FindAll(x => x.truck.Value == road.truck.Value).Count;
            bool lastRoute = GameManager.Instance.trucks.Count(x => x.model.hasRoute.Value) <= 1;
            City truckCity = null;
            int linkedRoadCount = 0;

            if (road.truck.Value.currentStation.Value == city)
            {
                truckCity = city;
                linkedRoadCount = truckCity.roads.FindAll(x => x.truck.Value == road.truck.Value).Count;
            }
            else if (road.truck.Value.currentStation.Value == targetCity)
            {
                truckCity = targetCity;
                linkedRoadCount = truckCity.roads.FindAll(x => x.truck.Value == road.truck.Value).Count;
            }


            if (truckCity != null && linkedRoadCount < 2 && roadCount > 1)
            {
                Popup_Truck_Caution.Instance.Show();
            }
            else if (lastRoute && roadCount == 1)
            {
                Popup_Common.Instance.Show(Utilities.GetStringByData(20064),
                        Utilities.GetStringByData(30018), false)
                    .SetCenter(Utilities.GetStringByData(20078));
            }
            else
            {
                Popup_Common.Instance.Show(Utilities.GetStringByData(31405), Utilities.GetStringByData(30035))
                    .SetResource(RewardData.eType.gold, Datas.levelData[0].route_delete_refund)
                    .SetCenter(Utilities.GetStringByData(19905),
                        Popup_Common.ButtonColor.Green,
                        () =>
                        {
                            if (roadCount == 1)
                            {
                                LevelData levelData =
                                    GameManager.Instance.GetRouteCountLevelData(GameManager.Instance.RouteCount.Value);

                                if (levelData != null)
                                {
                                    Popup_Common.Instance.Show(Utilities.GetStringByData(30013),
                                            Utilities.GetStringByData(30014), false)
                                        .SetResource(RewardData.eType.gold,
                                            Datas.levelData[0].route_delete_refund + levelData.route_price)
                                        .SetLeft(Utilities.GetStringByData(19913))
                                        .SetRight(Utilities.GetStringByData(20152), Popup_Common.ButtonColor.Red, () =>
                                        {
                                            RemoveRoad(road);
                                            PulloutTruck();
                                            ResetView();
                                            UIRewardManager.Instance.Show(road.ui.transform.position,
                                                Datas.levelData[0].route_delete_refund + levelData.route_price, 0, 0);
                                            UserDataManager.Instance.SaveData();
                                            ShowToolSlider(road, Datas.levelData[0].route_delete_refund,
                                                () => { SelectTruckLane(); });
                                        });
                                }
                            }
                            else
                            {
                                RemoveRoad(road);
                                SelectTruckLane(selectedTruck.Value);
                                UIRewardManager.Instance.Show(road.ui.transform.position,
                                    Datas.levelData[0].route_delete_refund, 0,
                                    0);
                                UserDataManager.Instance.SaveData();
                                ShowToolSlider(road, Datas.levelData[0].route_delete_refund,
                                    () => { SelectTruckLane(selectedTruck.Value); });
                            }
                        });
            }
        }

        void PulloutTruck()
        {
            selectedTruck.Value.currentStation.Value = null;
            selectedTruck.Value.model.hasRoute.Value = false;
            selectedTruck.Value.cargos.Clear();
            selectedTruck.Value.completeCargos.Clear();
            MakeList();
            scroller.RefreshActiveCellViews();
            SelectTruckLane(null);
        }

        void RemoveRoad(Road road)
        {
            AudioManager.Instance.PlaySound("sfx_mode_routes_road_remove");
            road.truck.Value = null;
            road.model.truckBirthID.Value = 0;
        }

        public void ResetView()
        {
            selectedCellIndex = -1;

            ResetSelectRoad();
            ResetSelectCity();

            if (GameManager.Instance.fsm.GetCurrentState() == GameManager.Instance.mapEditState)
            {
                SelectTruckLane(null);
//                scroller.RefreshActiveCellViews();
            }
            else if (GameManager.Instance.fsm.GetCurrentState() == GameManager.Instance.allocateTruckState)
            {
                editVeiwAllocate.ShowDraggableCities();
            }
        }

        void MakeRoad(City city, City targetCity, Road road, Truck truck)
        {
            bool cityOpen = city.IsOpen();
            bool targetCityOpen = targetCity.IsOpen();

            road.truck.Value = truck;
            SelectTruckLane(truck);

            if (GameManager.Instance.fsm.GetCurrentState() == GameManager.Instance.allocateTruckState)
            {
                truck.currentStation.Value = city;
                truck.model.hasRoute.Value = true;
                truck.model.colorIndex.Value = UserDataManager.Instance.GetNextTruckColor();

                int routeCount = GameManager.Instance.trucks.Count(x => x.model.hasRoute.Value);
                FBAnalytics.FBAnalytics.LogCreateRouteEvent(UserDataManager.Instance.data.lv.Value, routeCount,
                    GameManager.Instance.trucks.Count);

                MakeList();
                SelectTruckCell(truck);
                GameManager.Instance.fsm.PopState();
                scroller.RefreshActiveCellViews();
            }

            if (!UserDataManager.Instance.data.hasTutorial.Value)
            {
                if (!cityOpen)
                {
                    city.RefreshAllCargo();
                }

                if (!targetCityOpen)
                {
                    targetCity.RefreshAllCargo();
                }
            }

            UserDataManager.Instance.SaveData();
        }

        public void OnCellRefresh(EditCellView cell)
        {
            if (cell == null)
            {
                return;
            }

            cell.HideAllButton();

            switch (cell.data.type)
            {
                case EditCellData.EditCellType.TRUCK:
                    cell.goMaxSpec.SetActive(true);

//                    if (!cell.data.truck.model.hasRoute.Value)
//                    {
//                        cell.truckName.text = Utilities.GetStringByData(20121);
//                    }

                    if (cell.dataIndex == selectedCellIndex && cell.data.truck.model.hasRoute.Value)
                    {
                        if (cell.data.truck.model.state.Value == TruckModel.State.Wait)
                        {
                            cell.SetButtonAni_Chnage_Setting();
                        }
                        else
                        {
                            cell.btnBooster.gameObject.SetActive(true);

                            if (cell.data.truck.isAdBooster.Value)
                            {
                                cell.goAdBooster.SetActive(true);
                            }
                        }

                        cell.btnTruck.GetComponent<RectTransform>().DOAnchorPosX(15, 0.3f);
                    }
                    else
                    {
                        cell.btnTruck.GetComponent<RectTransform>().DOAnchorPosX(0, 0.3f);
                    }

                    break;
                case EditCellData.EditCellType.ADD_ROUTE:
                    break;
//                case EditCellData.EditCellType.ADD_TRUCK:
//                    break;
                case EditCellData.EditCellType.NEW_TRUCK:
                    break;
            }
        }

        public void OnClickItem(EditCellView cell)
        {
            selectedCellIndex = cell.dataIndex;

            if (cell.data.type == EditCellData.EditCellType.TRUCK
                && cell.data.truck != null
                && cell.data.truck.model.state.Value == TruckModel.State.Move)
            {
                AudioManager.Instance.PlaySound("sfx_require");
            }
            else
            {
                AudioManager.Instance.PlaySound("sfx_button_main");
            }

            if (cell.data.type == EditCellData.EditCellType.TRUCK)
            {
                selectCity = cell.data.truck.currentStation.Value;
                SelectTruckLane(cell.data.truck, null, true);
                CheckSelectedTruckCompleteCargos();
            }
            else if (cell.data.type == EditCellData.EditCellType.ADD_ROUTE)
            {
                SelectTruckLane(null);
                int count = GameManager.Instance.trucks.Count(x => !x.model.hasRoute.Value);


                if (count > 0)
                {
                    LevelData levelData = GameManager.Instance.GetRouteCountLevelData();
                    int roadCount = GameManager.Instance.roads.Count(x => x.model.isOpen.Value);
                    long gold = Datas.roadCostData.ToArray().FirstOrDefault(x => x.sequence == roadCount).road_cost;

                    if (levelData != null)
                    {
                        if (UserDataManager.Instance.data.gold.Value >= (levelData.route_price + gold))
                        {
                            // add route cell
                            MakeList();
                            editViewAddTruck.Show();
                            editView.GetComponent<RectTransform>().DOAnchorPosX(-600, 0.3f);
                            UserDataManager.Instance.SaveData();
                        }
                        else
                        {
                            AudioManager.Instance.PlaySound("sfx_require");
                        }
                    }
                }
                else
                {
                    // need truck
                    Popup_NeedTruck.Instance.Show();
                }
            }
        }

        void CheckSelectedTruckCompleteCargos()
        {
            if (selectedTruck.Value != null)
            {
                if (selectedTruck.Value.model.state.Value == TruckModel.State.Wait
                    && selectedTruck.Value.completeCargos.Count > 0)
                {
                    Popup_Guide_RouteReward.Instance.Show(() =>
                    {
                        UIRewardManager.Instance.Show(selectedTruck.Value.currentStation.Value);
                    });
                }
            }
        }

        void RefreshScroll()
        {
            for (int i = scroller.StartCellViewIndex; i < data.Count; i++)
            {
                EnhancedScrollerCellView cellView = scroller.GetCellViewAtDataIndex(i);
                EditCellView view = cellView as EditCellView;

                if (view != null)
                {
                    OnCellRefresh(view);
                }
            }
        }

        void ShowToolSlider(Road road, long value, Action action = null)
        {
            road.ui.ShowToolSlider(value > 0, () => { action?.Invoke(); });
        }

        public void SelectTruckCell(Truck truck)
        {
            int index = 0;

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].truck == truck)
                {
                    index = i;
                    break;
                }
            }

            selectedCellIndex = index;
            SelectTruckLane(truck);
            scroller.RefreshActiveCellViews();
            UIToastMassage.Instance.Show(30041, true);
        }

        public void OnClickChange(EditCellView cell)
        {
            AudioManager.Instance.PlaySound("sfx_button_main");

            if (selectedTruck.Value.cargos.Count > 0)
            {
                Popup_Guide_ChangeTruck.Instance.Show(() =>
                {
                    editViewChange.Show(data[selectedCellIndex]);
                    ShowTween(false);
                });
            }
            else
            {
                editViewChange.Show(data[selectedCellIndex]);
                ShowTween(false);
            }
        }

        public void OnClickSetting(EditCellView cell)
        {
            AudioManager.Instance.PlaySound("sfx_button_main");
            ShowTween(false);
            editViewSetting.Show(data[selectedCellIndex]);
        }

        void OnClickBoost(EditCellView cell)
        {
            AudioManager.Instance.PlaySound("sfx_button_main");

            int cash = Mathf.CeilToInt(Datas.baseData[0].transit_skip * cell.data.truck.GetLeftTime() / 120);

            if (cash < 1)
            {
                cash = 1;
            }

            if (cell.data.truck.isAdBooster.Value && AdManager.Instance.IsLoadedReward.Value)
            {
                Popup_Common popup = Popup_Common.Instance
                    .Show(Utilities.GetStringByData(20053), Utilities.GetStringByData(20145))
                    .SetLeftReward(RewardData.eType.cash, cash, () =>
                    {
                        if (UserDataManager.Instance.UseCash(cash))
                        {
                            SetBoost(RewardData.eType.cash);
                        }
                    })
                    .SetAD(
                        () =>
                        {
                            if (AdManager.Instance.IsLoadedReward.Value)
                            {
                                Popup_Loading.Instance.Show();
                                AdManager.Instance.ShowRewardLoad(AdUnit.Delivery_Bosster)
                                    .Subscribe(
                                        result =>
                                        {
                                            GameManager.Instance.fsm.PopState();

                                            if (result == AdResult.Success)
                                            {
                                                SetBoost(RewardData.eType.none);
                                            }
                                            else if (result == AdResult.NoFill)
                                            {
                                                AudioManager.Instance.PlaySound("sfx_require");
                                                UIToastMassage.Instance.Show(30055);
                                            }
                                            else
                                            {
                                                AudioManager.Instance.PlaySound("sfx_require");
                                                UIToastMassage.Instance.Show(30056);
                                            }
                                        }).AddTo(this);
                            }
                            else
                            {
                                AudioManager.Instance.PlaySound("sfx_require");
                                UIToastMassage.Instance.Show(30056);
                            }
                        });
            }
            else
            {
                Popup_Common popup = Popup_Common.Instance.Show(Utilities.GetStringByData(20053),
                    Utilities.GetStringByData(20054));

                popup.SetCenterReward(RewardData.eType.cash, cash, () =>
                {
                    if (UserDataManager.Instance.UseCash(cash))
                    {
                        SetBoost(RewardData.eType.cash);
                    }
                });
            }
        }

        void SetBoost(RewardData.eType type)
        {
            GameManager.Instance.SetBoostScene(selectedTruck.Value, () =>
            {
                selectedTruck.Value.SetBoost(type);

                Debug.Log("Truck SetBoostScene 000000000");

                Observable.NextFrame().Subscribe(_ =>
                {
                    Debug.Log("Truck SetBoostScene 11111");
                    RefreshScroll();
                    //CheckSelectedTruckCompleteCargos();
                }).AddTo(this);
            });
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return data.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            if (data[dataIndex].truck?.model.state.Value == TruckModel.State.Move)
            {
                return 138;
            }

            return 104;
        }


        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            EditCellView cellView = scroller.GetCellView(GameManager.Instance.editCellViewPrefab) as EditCellView;
            cellView.name = "Cell " + dataIndex;

            return cellView;
        }

        private void CellViewVisibilityChanged(EnhancedScrollerCellView cellView)
        {
            // cast the cell view to our custom view
            EditCellView view = cellView as EditCellView;

            // if the cell is active, we set its data, 
            // otherwise we will clear the image back to 
            // its default state

            if (cellView.active)
            {
                view.SetData(data[cellView.dataIndex],
                    OnCellRefresh,
                    OnClickItem,
                    null,
                    null,
                    OnClickChange,
                    null,
                    OnClickBoost,
                    null,
                    OnClickSetting
                );

                OnCellRefresh(view);
            }
        }
    }
}