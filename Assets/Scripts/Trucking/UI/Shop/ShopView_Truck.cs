//using System.Collections.Generic;
//using System.Linq;
//using DatasTypes;
//using DG.Tweening;
//using EnhancedUI.EnhancedScroller;
//using Trucking.Common;
//using Trucking.Model;
//using Trucking.UI.Popup;
//using UniRx;
//using UnityEngine;
//
//namespace Trucking.UI.Shop
//{
//    public class ShopView_Truck : MonoBehaviour, IEnhancedScrollerDelegate
//    {
//        public EnhancedScroller scroller;
//        public EnhancedScrollerCellView truckCellViewPrefab;
//
//        private List<TruckData> shopDatas;
//        private int nearIndex = -1;
//
//        private void Awake()
//        {
//            
//        }
//
//        public void Show()
//        {
//            gameObject.SetActive(true);
//
//
//            if (shopDatas == null)
//            {
//                shopDatas = Datas.truckData.ToArray().Where(x => x.shop > 0).ToList();
//                scroller.Delegate = this;
//                scroller.cellViewVisibilityChanged = CellViewVisibilityChanged;
////                scroller.scrollerTweeningChanged = ScrollerTweeningChangedDelegate;
//            }
//            
//            if (nearIndex < 0
//                || UserDataManager.Instance.data.shopAlert.Value)
//            {
//                TruckData nearData = shopDatas.OrderBy(x => Mathf.Abs(UserDataManager.Instance.data.lv.Value - x.require_lv))
//                    .ThenBy(x => x.id)
//                    .FirstOrDefault();
//                nearIndex = shopDatas.IndexOf(nearData);
//                scroller.ReloadData();
//                scroller.JumpToDataIndex(nearIndex);
//            }
//            else
//            {
//                scroller.ReloadData(scroller.NormalizedScrollPosition);
////                SetAnimation();
//            }
//
////            SetAnimation();
//        }
//
//        public void Close()
//        {
//            gameObject.SetActive(false);
//        }
//        
//        void SetAnimation()
//        {
//            for (int i = scroller.StartCellViewIndex; i < shopDatas.Count; i++)
//            {
//                EnhancedScrollerCellView cellView = scroller.GetCellViewAtDataIndex(i);
//
//                if (cellView != null)
//                {
//                    cellView.transform.GetChild(0).localScale = new Vector3(0, 1, 1);
//                    cellView.transform.GetChild(0).DOScaleX(1, 0.3f)
//                        .SetDelay((i - scroller.StartCellViewIndex) * 0.05f);
//                }
//            }
//        }
//
//
//        public void OnClickSell(ShopCellView_Truck cell)
//        {            
//            Truck truck = GameManager.Instance.trucks.FirstOrDefault(x => x.data.id == cell.data.id);
//
//            if (truck != null)
//            {
//                AudioManager.Instance.PlaySound("sfx_button_main");
//                Popup_TruckInformation.Instance.ShowSell(truck, () =>
////                Popup_TruckClaim.Instance.ShowSell(truck, () =>
//                {
//                    UserDataManager.Instance.AddGold(truck.data.resell_gold[truck.model.upgradeLv.Value - 1]);
//                    GameManager.Instance.trucks.Remove(truck);
//                    UserDataManager.Instance.data.truckData.Remove(truck.model);
//                    Observable.NextFrame().Subscribe(unit =>
//                    {
//                        Utilities.RemoveObject(truck.gameObject);
//                    }).AddTo(this);
//                    
//                    cell.RefreshData();
//                });
//            }
//        }
//        
////        public void OnClickGoTruck(ShopCellView_Truck cell)
////        {            
////            Truck truck = GameManager.Instance.trucks.FirstOrDefault(x => x.data.id == cell.data.id);
////
////            if (truck != null)
////            {
////                AudioManager.Instance.PlaySound("sfx_button_main");
////
////                while (GameManager.Instance.fsm.GetCurrentState() != GameManager.Instance.worldMapState)
////                {
////                    GameManager.Instance.fsm.PopState();    
////                }
////                
////                HQView.Instance.Show(truck);                
////            }
////        }
//
//        
//        public void OnClickBuy(ShopCellView_Truck cell)
//        {
//            scroller.RefreshActiveCellViews();
//
//            if (UserDataManager.Instance.data.lv.Value >= cell.data.require_lv)
//            {
//                int truckCount = GameManager.Instance.trucks.Count(x => x.data.id == cell.data.id);
//
//                if (truckCount == 0)
//                {
//                    if (UserDataManager.Instance.UseResource(cell.data.gold[0], 
//                            cell.data.cash[0],
//                            cell.data.voucher[0]))
//                    {
//                        Truck newTruck = Truck.AddNewTruck(cell.data.id, true);
//                        UserDataManager.Instance.SaveData();
//                        
//                        FBAnalytics.FBAnalytics.LogBuyTruckEvent(UserDataManager.Instance.data.lv.Value, 
//                            cell.data.id, 
//                            GameManager.Instance.trucks.Count, 
//                            GameManager.Instance.trucks.Count(x => x.model.hasRoute.Value));
//
//                        Popup_TruckInformation.Instance.ShowClaim(newTruck, cell.RefreshData);
////                        Popup_TruckClaim.Instance.ShowClaim(newTruck, cell.RefreshData);
//
//                    }
//                    else
//                    {
//                        AudioManager.Instance.PlaySound("sfx_require");
//                        cell.btnBuy.transform.DOShakePosition(0.3f, 3);
//                    }
//                }
//                else
//                {
//                    AudioManager.Instance.PlaySound("sfx_require");
//                    cell.btnBuy.transform.DOShakePosition(0.3f, 3);
//                }
//            }
//            else
//            {
//                AudioManager.Instance.PlaySound("sfx_require");
//                cell.btnBuy.transform.DOShakePosition(0.3f, 3);
//            }
//        }
//
//        public int GetNumberOfCells(EnhancedScroller scroller)
//        {
//            return shopDatas.Count;
//        }
//
//        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
//        {
//            return 300;
//        }
//
//        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
//        {
//            ShopCellView_Truck cellView = scroller.GetCellView(truckCellViewPrefab) as ShopCellView_Truck;
//            cellView.name = "Cell " + dataIndex;
//            cellView.gameObject.SetActive(true);
//            return cellView;
//        }
//        
//        private void CellViewVisibilityChanged(EnhancedScrollerCellView cellView)
//        {
//            // cast the cell view to our custom view
//            ShopCellView_Truck view = cellView as ShopCellView_Truck;
//
//            // if the cell is active, we set its data, 
//            // otherwise we will clear the image back to 
//            // its default state
//            if (cellView.active)
//            {
//                cellView.gameObject.SetActive(true);
//                view.SetData(shopDatas[cellView.dataIndex], 
//                    OnClickBuy, 
//                    OnClickSell
//                    );
//            }
//                
//        }
//
////        void ScrollerTweeningChangedDelegate(EnhancedScroller scroller, bool tweening)
////        {
////            SetAnimation();
////        }
//    }
//    
//}