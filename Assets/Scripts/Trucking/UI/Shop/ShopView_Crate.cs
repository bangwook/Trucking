using System.Collections.Generic;
using DatasTypes;
using EnhancedUI.EnhancedScroller;
using Trucking.Common;
using Trucking.Iap;
using Trucking.Model;
using UnityEngine;

namespace Trucking.UI.Shop
{
    public class ShopView_Crate : MonoBehaviour, IEnhancedScrollerDelegate
    {
        public EnhancedScroller scroller;
        public EnhancedScrollerCellView cellViewPrefab_s;
        public EnhancedScrollerCellView cellViewPrefab_n;

        List<IAPData> datas = new List<IAPData>();
        
        public void Show()
        {
            gameObject.SetActive(true);
            
            if (scroller.Delegate == null)
            {
                for (int i = 0; i < Datas.iAPData.Length; i++)
                {
                    if (Datas.iAPData[i].item_type.type == RewardData.eType.crate)
                    {
                        datas.Add(Datas.iAPData[i]);
                        Debug.Log($"Iap : {Datas.iAPData[i].name}");
                    }
                }

                scroller.Delegate = this;
                scroller.cellViewVisibilityChanged = CellViewVisibilityChanged;
            }
            
            scroller.ReloadData();
        }
        
        public void Close()
        {
            gameObject.SetActive(false);
        }
        
        public void OnClickConfirm(ShopCellView_Crate cell)
        {
            AudioManager.Instance.PlaySound("sfx_button_main");
            IapManager.Instance.Purchase(cell.data.iap_id.id);
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return datas.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 300;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            int crateIndex = RewardModel.GetIndex(RewardData.eType.crate, datas[dataIndex].reward_id);

            ShopCellView_Crate cellView;
            
            if (crateIndex == 0)
            {
                cellView = scroller.GetCellView(cellViewPrefab_n) as ShopCellView_Crate;    
            }
            else
            {
                cellView = scroller.GetCellView(cellViewPrefab_s) as ShopCellView_Crate;
            }
            
            cellView.name = "Cell " + dataIndex;
            return cellView;
        }
        
        private void CellViewVisibilityChanged(EnhancedScrollerCellView cellView)
        {
            ShopCellView_Crate view = cellView as ShopCellView_Crate;
                
            if (cellView.active)
            {
                view.SetData(datas[cellView.dataIndex], 
                    OnClickConfirm);
            }
        }
    }
}