using System;
using System.Collections.Generic;
using System.Linq;
using DatasTypes;
using DG.Tweening;
using Dreamteck.Splines;
using EnhancedUI.EnhancedScroller;
using Trucking.Ad;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.CellView;
using Trucking.UI.Popup;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI
{
    public class HQView : MonoSingleton<HQView>, IEnhancedScrollerDelegate
    {
        public RectTransform contents;
        public EnhancedScroller scroller;
        public ReactiveProperty<Truck> selectedTruck = new ReactiveProperty<Truck>();
        public Button btnScrollTop;
        public Button btnScrollBottom;

        public List<EditCellData> data;

        private int selectedCellIndex;
        private CompositeDisposable disposable = new CompositeDisposable();
        private CompositeDisposable disposableScroll = new CompositeDisposable();
        private CompositeDisposable disposableSelectedTruck = new CompositeDisposable();
        private Tweener tweener;
        private bool animation;

        private void Start()
        {
            disposable.AddTo(this);
            disposableScroll.AddTo(this);
            disposableSelectedTruck.AddTo(this);

            btnScrollTop.OnClickAsObservable().Subscribe(_ =>
            {
                scroller.JumpToDataIndex(0, 0, 0, true, EnhancedScroller.TweenType.immediate, 0,
                    () => { ScrollerScrollingChangedDelegate(scroller, true); });
            }).AddTo(this);

            btnScrollBottom.OnClickAsObservable().Subscribe(_ =>
            {
                scroller.JumpToDataIndex(data.Count - 1, 1, 1, true, EnhancedScroller.TweenType.immediate, 0,
                    () => { ScrollerScrollingChangedDelegate(scroller, true); });
            }).AddTo(this);

            selectedTruck.Pairwise().Subscribe(tr =>
            {
                if (tr.Previous != null)
                {
                    tr.Previous.isNew.Value = false;
                }

                SelectTruckLane(tr.Current);

                if (tr.Current != null)
                {
                    if (tr.Current.model.state.Value == TruckModel.State.Move)
                    {
                        HQView_CargoList.Instance.Show(tr.Current);
                        WorldMap.Instance.SetCamera(tr.Current.transform.position + new Vector3(-50, 0, 0));
                    }
                    else
                    {
                        HQView_CargoList.Instance.Close();

                        if (tr.Current.model.hasRoute.Value && tr.Current.currentStation.Value != null)
                        {
                            WorldMap.Instance.SetCamera(tr.Current.transform.position + new Vector3(-110, 0, 0));

                            foreach (var road in GameManager.Instance.roads)
                            {
                                road.SetColorRoadAni(road.truck.Value == tr.Current);
                            }
                        }
                    }
                }
            }).AddTo(this);

            selectedTruck.Subscribe(tr =>
            {
                disposableSelectedTruck.Clear();

                tr?.pathRoad.ObsSomeChanged().Subscribe(path =>
                {
                    if (path.Count == 0)
                    {
                        SelectTruckLane(tr);
                    }
                }).AddTo(disposableSelectedTruck);
            }).AddTo(this);
        }

        public void Show(Truck tr = null)
        {
            AudioManager.Instance.PlaySound("sfx_mode_truck");
            animation = true;
            gameObject.SetActive(true);
            GameManager.Instance.fsm.PushState(GameManager.Instance.hqState);

            if (scroller.Delegate == null)
            {
                scroller.Delegate = this;
                scroller.cellViewVisibilityChanged = CellViewVisibilityChanged;
                scroller.scrollerScrollingChanged = ScrollerScrollingChangedDelegate;
            }

            MakeList();
            SelectTruck(tr);

            if (tr != null)
            {
                SelectedCellScrollCenter();
            }
            else
            {
                ScrollerScrollingChangedDelegate(scroller, false);
            }
        }

        public void Close()
        {
            disposableScroll.Clear();
            selectedCellIndex = -1;
            selectedTruck.Value = null;
            disposable.Clear();
            ClearTruckLane();

            foreach (var truck in GameManager.Instance.trucks)
            {
                truck.isNew.Value = false;
            }

            HQView_CargoList.Instance.Close();
            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(0.3f))
                .Subscribe(_ => { gameObject.SetActive(false); })
                .AddTo(this);
        }

        void SetAnimation()
        {
            Debug.Log("SetAnimation==================================================================");
            for (int i = 0; i < data.Count; i++)
            {
                Debug.Log("truck : " + data[i].truck.name);
            }

            for (int i = scroller.StartDataIndex; i < scroller.EndDataIndex; i++)
            {
                EnhancedScrollerCellView cellView = scroller.GetCellViewAtDataIndex(i);

                if (cellView != null)
                {
                    EditCellView view = cellView as EditCellView;
                    Debug.Log("SetAnimation : " + view.data.truck.name);
                    cellView.transform.GetChild(0).localScale = new Vector3(1, 0, 1);
                    DOTween.Kill(cellView.transform.GetChild(0));
                    cellView.transform.GetChild(0).DOScaleY(1, 0.3f)
                        .SetDelay((i - scroller.StartDataIndex) * 0.05f);
                }
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

        public void SelectedCellScrollCenter()
        {
            int centerPos = (int) (scroller.GetComponent<RectTransform>().rect.height /
                                   GetCellViewSize(scroller, selectedCellIndex) / 2);

            scroller.JumpToDataIndex(selectedCellIndex < centerPos ? selectedCellIndex : selectedCellIndex - centerPos,
                0,
                0,
                true,
                EnhancedScroller.TweenType.immediate,
                0f,
                () => ScrollerScrollingChangedDelegate(scroller, false));
        }

        public void ShowTween(bool isShow, Action callback = null)
        {
            tweener?.Kill();

            if (isShow)
            {
                gameObject.SetActive(true);
                contents.anchoredPosition = new Vector2(-900, contents.anchoredPosition.y);
                tweener = contents.DOAnchorPosX(0, 0.3f).OnComplete(() =>
                {
                    tweener = null;
                    callback?.Invoke();
                });
            }
            else
            {
                tweener = contents.DOAnchorPosX(-900, 0.3f)
                    .OnComplete(() =>
                    {
                        tweener = null;
                        callback?.Invoke();
                    });
            }
        }

        public void MakeList()
        {
            scroller.ClearAll();
            disposable.Clear();
            selectedCellIndex = -1;

            List<EditCellData> dataList = new List<EditCellData>();

            foreach (var tr in GameManager.Instance.trucks)
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

                            if (selectedTruck.Value == cell.truck)
                            {
                                HQView_CargoList.Instance.Close();
                            }
                        }
                    }).AddTo(disposable);
                }

                dataList.Add(cell);
            }

            data = dataList
                .OrderByDescending(x => x.truck.model.hasRoute.Value)
                .ThenByDescending(x => x.truck.model.state.Value == TruckModel.State.Wait)
                .ThenByDescending(x => x.truck.model.state.Value == TruckModel.State.Move)
                .ThenBy(x => x.truck.GetLeftTime())
                .ThenBy(x => x.truck.GetMaxRefuelTime())
                .ThenByDescending(x => x.truck.GetCargo(x.truck.model.upgradeLv.Value))
                .ThenByDescending(x => x.truck.data.id)
                .ToList();


//            float newScrollPosition = scroller.GetScrollPositionForDataIndex(dataIndex, CellViewPositionEnum.Before) + offset;
//        // clamp the scroll position to a valid location
//        newScrollPosition = Mathf.Clamp(newScrollPosition, 0, GetScrollPositionForCellViewIndex(_cellViewSizeArray.Count - 1, CellViewPositionEnum.Before));
//            // if spacing is used, adjust the final position
//            // move back by the spacing if necessary
//            newScrollPosition = Mathf.Clamp(newScrollPosition - spacing, 0, GetScrollPositionForCellViewIndex(_cellViewSizeArray.Count - 1, CellViewPositionEnum.Before));


            scroller.ScrollPosition = 0;
            scroller.ReloadData();
        }

        public void SelectTruck(Truck tr)
        {
            if (tr != null)
            {
                EditCellData cellData = data.Find(x => x.truck == tr);
                selectedCellIndex = data.IndexOf(cellData);
                selectedTruck.Value = tr;
                HQView_CargoList.Instance.Show(tr);
                SelectTruckLane(tr);
                scroller.JumpToDataIndex(selectedCellIndex, 0.5f, 0.5f);

                var cell = scroller.GetCellViewAtDataIndex(selectedCellIndex) as EditCellView;
                OnCellRefresh(cell);
//                scroller.RefreshActiveCellViews();
            }
            else
            {
                foreach (var road in GameManager.Instance.roads)
                {
                    road.SetTruckColor(road.truck.Value != null, false);
                }
            }
        }

        void SelectTruckLane(Truck tr)
        {
            WorldMap.Instance.SetCusor(null);

            foreach (var road in GameManager.Instance.roads)
            {
                road.SetEditState(false);
                road.SetColorRoadAni(road.truck.Value == tr);
                road.SetTruckColor(road.truck.Value != null, false);
            }

            if (tr != null)
            {
                if (tr.model.state.Value == TruckModel.State.Move)
                {
                    foreach (var pathDirection in tr.pathRoad)
                    {
                        pathDirection.road.SetColorRoadAni(false);
                        pathDirection.road.trsStateColor.gameObject.SetActive(false);
                        pathDirection.road.SetMovingAnimation(true, pathDirection.isReverse);
                    }

                    WorldMap.Instance.SetCusor(tr.GetComponent<SplinePositioner>().targetObject.transform);
                }
                else if (tr.currentStation.Value != null)
                {
                    WorldMap.Instance.SetCusor(tr.currentStation.Value.transform);
                }
            }
        }

        void ClearTruckLane()
        {
            foreach (var city in GameManager.Instance.cities)
            {
                city.SetDepartSelect(false);
            }

            foreach (var road in GameManager.Instance.roads)
            {
                road.SetColorRoadAni(false);
                road.SetTruckColor(false);
            }
        }

        public void OnCellRefresh(EditCellView cell)
        {
            if (cell == null)
            {
                return;
            }

            cell.HideAllButton();

            cell.goMaxSpec.SetActive(false);
            cell.sliderFuel.gameObject.SetActive(cell.data.truck.model.state.Value == TruckModel.State.Wait);

            if (cell.dataIndex == selectedCellIndex)
            {
                if (cell.data.truck.model.state.Value == TruckModel.State.Move)
                {
                    if (!cell.data.truck.HasArrival())
                    {
                        cell.btnBooster.gameObject.SetActive(true);

                        if (cell.data.truck.isAdBooster.Value)
                        {
                            cell.goAdBooster.SetActive(true);
                        }
                    }
                }
                else
                {
                    bool hasRoute = cell.data.truck.model.hasRoute.Value;

                    cell.btnJob.gameObject.SetActive(hasRoute);
                    cell.btnSell.gameObject.SetActive(!hasRoute);

                    UnirxExtension.DateTimer(DateTime.Now.AddSeconds(0.1f)).Subscribe(_ =>
                    {
                        cell.btnUpgrade.gameObject.SetActive(true);
                    }).AddTo(this);
                }

                cell.btnTruck.GetComponent<RectTransform>().DOAnchorPosX(15, 0.3f);
            }
            else
            {
//                if (cell.data.truck.model.state.Value == TruckModel.State.Wait 
//                    && cell.data.truck.model.completeCargoModels.Count > 0)
//                {
//                    cell.btnComplete.gameObject.SetActive(true);    
//                }


                cell.btnTruck.GetComponent<RectTransform>().DOAnchorPosX(0, 0.3f);
            }
        }

        public void OnClickItem(EditCellView cell)
        {
            AudioManager.Instance.PlaySound("sfx_button_main");

            disposableScroll.Clear();
            selectedCellIndex = cell.dataIndex;
            selectedTruck.Value = cell.data.truck;
//            scroller.RefreshActiveCellViews();

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

                if (UserDataManager.Instance.data.hasTutorial.Value)
                {
                    popup.SetCenter(Utilities.GetStringByData(20097),
                        Popup_Common.ButtonColor.Blue,
                        () => { SetBoost(RewardData.eType.none); });
                }
                else
                {
                    popup.SetCenterReward(RewardData.eType.cash, cash, () =>
                    {
                        if (UserDataManager.Instance.UseCash(cash))
                        {
                            SetBoost(RewardData.eType.cash);
                        }
                    });
                }
            }
        }

        void SetBoost(RewardData.eType type)
        {
            GameManager.Instance.SetBoostScene(selectedTruck.Value, () =>
            {
                selectedTruck.Value.SetBoost(type);
                WorldMap.Instance.SetCamera(selectedTruck.Value.transform.position + new Vector3(-110, 0, 0));
                MakeList();
                SelectTruck(selectedTruck.Value);
            });
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
                }
            }
        }

        private void OnDisable()
        {
            disposable.Clear();
        }

        public void OnClickUpgrade(EditCellView cell)
        {
            AudioManager.Instance.PlaySound("sfx_button_main");
            Popup_TruckInformation.Instance.ShowUpgrade(cell.data.truck, cell.RefreshData);
        }

        public void OnClickSell(EditCellView cell)
        {
            AudioManager.Instance.PlaySound("sfx_button_main");
            Truck truck = cell.data.truck;

//            Popup_TruckClaim.Instance.ShowSell(truck, () =>
            Popup_TruckInformation.Instance.ShowSell(truck, () =>
            {
                AudioManager.Instance.PlaySound("sfx_button_main");

                UserDataManager.Instance.AddGold(truck.data.resell_gold[truck.model.upgradeLv.Value - 1]);
                GameManager.Instance.trucks.Remove(truck);
                UserDataManager.Instance.data.truckData.Remove(truck.model);
                MakeList();
                Observable.NextFrame().Subscribe(unit => { Utilities.RemoveObject(truck.gameObject); }).AddTo(this);
            });
        }

        public void OnClickJob(EditCellView cell)
        {
            if (cell.data.truck.model.completeCargoModels.Count > 0)
            {
                AudioManager.Instance.PlaySound("sfx_require");
                UIToastMassage.Instance.Show(string.Format(Utilities.GetStringByData(20103),
                    cell.data.truck.model.currentStation));
            }
            else
            {
                AudioManager.Instance.PlaySound("sfx_button_main");
                GameManager.Instance.fsm.PopState();
                FBAnalytics.FBAnalytics.LogTruckJobEvent("Truck List");
                JobView.Instance.Show(cell.data.truck.currentStation.Value, cell.data.truck);
            }
        }

        public void OnClickComplete(EditCellView cell)
        {
            AudioManager.Instance.PlaySound("sfx_button_main");
            WorldMap.Instance.SetCamera(cell.data.truck.currentStation.Value.transform.position);
            UIRewardManager.Instance.Show(cell.data.truck.currentStation.Value);
            cell.RefreshCellView();
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
                    OnClickUpgrade,
                    null,
                    null,
                    OnClickBoost,
                    OnClickSell,
                    null,
                    OnClickJob,
                    OnClickComplete);

                OnCellRefresh(view);
            }
        }
    }
}