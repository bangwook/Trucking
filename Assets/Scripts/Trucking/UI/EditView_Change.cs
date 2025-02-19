using System.Collections.Generic;
using System.Linq;
using DatasTypes;
using DG.Tweening;
using EnhancedUI.EnhancedScroller;
using Trucking.Ad;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.CellView;
using Trucking.UI.Craft;
using Trucking.UI.Guide;
using Trucking.UI.Popup;
using UniRx;
using UnityEngine;

namespace Trucking.UI
{
    public class EditView_Change : MonoBehaviour, IEnhancedScrollerDelegate
    {
        public EnhancedScroller scroller;
        public EditCellView targetTruckView;

        private List<EditCellData> _data = new List<EditCellData>();
        private int selectedCellIndex;
        private Truck targetTruck;
        private CompositeDisposable disposable = new CompositeDisposable();
        [HideInInspector] private Truck selectedTruck;

        private void Start()
        {
            scroller.Delegate = this;
            scroller.cellViewVisibilityChanged = CellViewVisibilityChanged;
        }

        public void Show(EditCellData targetData)
        {
            GameManager.Instance.fsm.PushState(GameManager.Instance.changeTruckState);

            gameObject.SetActive(true);
            GetComponent<RectTransform>().anchoredPosition =
                new Vector2(-600, GetComponent<RectTransform>().anchoredPosition.y);
            GetComponent<RectTransform>().DOAnchorPosX(0, 0.3f);

            targetTruck = targetData.truck;
            targetTruckView.SetData(targetData);
            targetTruckView.HideAllButton();

            MakeList();
            EditView.Instance.SelectTruckLane(targetTruck);
        }

        public void MakeList()
        {
            selectedCellIndex = -1;
            scroller.ClearAll();
            _data.Clear();

            foreach (var tr in GameManager.Instance.trucks)
            {
                if (tr != targetTruck
                    && tr.currentStation.Value == null
                    && !tr.model.hasRoute.Value
                )
                {
                    EditCellData changeCell = new EditCellData();
                    changeCell.truck = tr;
                    changeCell.type = EditCellData.EditCellType.TRUCK;
                    changeCell.targetTruck = targetTruck;

                    _data.Add(changeCell);
                }
            }

//            // addTruck cell
//            EditCellData cellAddTruck = new EditCellData();
//            cellAddTruck.type = EditCellData.EditCellType.NEW_TRUCK;
//            _data.Add(cellAddTruck);

            foreach (var tr in GameManager.Instance.trucks)
            {
                if (tr != targetTruck
                    && tr.currentStation.Value != null
                    && tr.model.hasRoute.Value
                    && tr.model.state.Value == TruckModel.State.Wait
                )
                {
                    EditCellData changeCell = new EditCellData();
                    changeCell.truck = tr;
                    changeCell.type = EditCellData.EditCellType.TRUCK;
                    changeCell.targetTruck = targetTruck;

                    _data.Add(changeCell);
                }
            }

            foreach (var tr in GameManager.Instance.trucks)
            {
                if (tr != targetTruck
                    && tr.currentStation.Value != null
                    && tr.model.hasRoute.Value
                    && tr.model.state.Value == TruckModel.State.Move
                    && tr.pathStation.Count > 0
                )
                {
                    EditCellData changeCell = new EditCellData();
                    changeCell.truck = tr;
                    changeCell.type = EditCellData.EditCellType.TRUCK;
                    changeCell.targetTruck = targetTruck;
                    changeCell.targetCity = tr.pathStation.Last();
                    _data.Add(changeCell);

                    tr.model.state.Pairwise().Subscribe(value =>
                    {
                        if (value.Previous == TruckModel.State.Move
                            && value.Current == TruckModel.State.Wait)
                        {
                            changeCell.targetCity = null;
                            scroller.ReloadData(scroller.NormalizedScrollPosition);
                        }
                    }).AddTo(disposable);
                }
            }

            scroller.ReloadData();
        }

        private void OnDisable()
        {
            disposable.Clear();
        }


        public void Close()
        {
            EditView.Instance.SelectTruckLane(targetTruck);

            GetComponent<RectTransform>().DOAnchorPosX(-600, 0.3f).OnComplete(() =>
            {
                gameObject.SetActive(false);

//                GameManager.Instance.fsm.PopState();
            });
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
            GameManager.Instance.SetBoostScene(selectedTruck, () =>
            {
                selectedTruck.SetBoost(type);

                Observable.NextFrame().Subscribe(_ =>
                {
                    RefreshScroll();
                    //CheckSelectedTruckCompleteCargos();
                }).AddTo(this);
            });
        }

        void RefreshScroll()
        {
            for (int i = scroller.StartCellViewIndex; i < _data.Count; i++)
            {
                EnhancedScrollerCellView cellView = scroller.GetCellViewAtDataIndex(i);
                EditCellView view = cellView as EditCellView;

                if (view != null)
                {
                    OnCellRefresh(view);
                }
            }
        }

        void CheckSelectedTruckCompleteCargos()
        {
            if (selectedTruck != null)
            {
                if (selectedTruck.model.state.Value == TruckModel.State.Wait
                    && selectedTruck.completeCargos.Count > 0)
                {
                    Popup_Guide_RouteReward.Instance.Show(() =>
                    {
                        UIRewardManager.Instance.Show(selectedTruck.currentStation.Value);
                    });
                }
            }
        }

        public void OnCellRefresh(EditCellView cell)
        {
            cell.HideAllButton();

            switch (cell.data.type)
            {
                case EditCellData.EditCellType.TRUCK:
                    cell.goMaxSpec.SetActive(cell.data.truck.model.state.Value == TruckModel.State.Wait);

                    if (cell.dataIndex == selectedCellIndex)
                    {
                        selectedTruck = cell.data.truck;

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
                            cell.btnConfirm.gameObject.SetActive(true);
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
            AudioManager.Instance.PlaySound("sfx_button_main");

            selectedCellIndex = cell.dataIndex;

            if (cell.data.type == EditCellData.EditCellType.NEW_TRUCK)
            {
                // new truck
                CraftView.Instance.Show(CraftView.Type.Pieces);
                UIToastMassage.Instance.Hide();
            }
            else
            {
                RefreshScroll();

                if (cell.data.truck.model.hasRoute.Value)
                {
                    EditView.Instance.SelectTruckLane(cell.data.truck, targetTruck);
                }
                else
                {
                    EditView.Instance.SelectTruckLane(targetTruck);
                }

                if (cell.data.truck.model.state.Value == TruckModel.State.Move)
                {
                    UIToastMassage.Instance.Show(20061, true);
                }
                else
                {
                    UIToastMassage.Instance.Show(30047, true);
                    CheckSelectedTruckCompleteCargos();
                }
            }
        }

        public void OnClickTargetTruckCell(EditCellView cell)
        {
            EditView.Instance.SelectTruckLane(cell.data.truck);
        }

        public void OnClickConfirm(EditCellView cell)
        {
            AudioManager.Instance.PlaySound("sfx_button_main");

            List<Road> roads = GameManager.Instance.roads.ToList().FindAll(x => x.truck.Value == targetTruck);
            Truck truck = _data[selectedCellIndex].truck;

            if (cell.data.truck.model.hasRoute.Value)
            {
                Popup_Guide_SwapTruck.Instance.Show(() =>
                {
                    City swapCity = truck.currentStation.Value;
                    truck.currentStation.Value = targetTruck.currentStation.Value;
                    targetTruck.currentStation.Value = swapCity;

                    truck.model.hasRoute.Value = true;
//                    int swapColor = truck.model.colorIndex.Value;
//                    truck.model.colorIndex.Value = targetTruck.model.colorIndex.Value;
//                    targetTruck.model.colorIndex.Value = swapColor;

                    truck.cargos.Clear();
                    targetTruck.cargos.Clear();

                    //targetTruck.currentStation.Value = null;
                    //targetTruck.model.hasRoute.Value = false;

                    List<Road> swapRoads = GameManager.Instance.roads.ToList().FindAll(x => x.truck.Value == truck);

                    foreach (var road in roads)
                    {
                        road.truck.Value = truck;
                    }

                    foreach (var road in swapRoads)
                    {
                        road.truck.Value = targetTruck;
                    }

                    GameManager.Instance.fsm.PopState();
                    EditView.Instance.MakeList();
                    EditView.Instance.SelectTruckCell(truck);
                    UserDataManager.Instance.SaveData();
                });
            }
            else
            {
                truck.currentStation.Value = targetTruck.currentStation.Value;
                truck.model.hasRoute.Value = true;
                truck.model.colorIndex.Value = targetTruck.model.colorIndex.Value;
                targetTruck.cargos.Clear();
                targetTruck.currentStation.Value = null;
                targetTruck.model.hasRoute.Value = false;

                foreach (var road in roads)
                {
                    road.truck.Value = truck;
                }

                GameManager.Instance.fsm.PopState();
                EditView.Instance.MakeList();
                EditView.Instance.SelectTruckCell(truck);
                UserDataManager.Instance.SaveData();
            }
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _data.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            if (_data[dataIndex].truck?.model.state.Value == TruckModel.State.Move)
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
                view.SetData(_data[cellView.dataIndex],
                    OnCellRefresh,
                    OnClickItem,
                    OnClickConfirm,
                    null,
                    null,
                    null,
                    OnClickBoost);
                OnCellRefresh(view);
            }
        }
    }
}