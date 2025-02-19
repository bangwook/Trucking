using System.Linq;
using DatasTypes;
using EnhancedUI.EnhancedScroller;
using Trucking.Common;
using Trucking.UI.Popup;
using UnityEngine;

namespace Trucking.UI.Shop
{
    public class ShopView_Coin : MonoBehaviour, IEnhancedScrollerDelegate
    {
        public EnhancedScroller scroller;
        public EnhancedScrollerCellView cellViewPrefab;
        public float coinExchangeRatio;

        public void Show()
        {
            gameObject.SetActive(true);

            LevelMag lvMag = Datas.levelMag.ToArray()
                .FirstOrDefault(x => x.lv == UserDataManager.Instance.data.lv.Value);

            if (lvMag != null)
            {
                coinExchangeRatio = lvMag.coinshop_mag;
            }
            else
            {
                coinExchangeRatio = Datas.levelMag.ToArray().Last().coinshop_mag;
            }

            if (scroller.Delegate == null)
            {
                scroller.Delegate = this;
                scroller.cellViewVisibilityChanged = CellViewVisibilityChanged;
            }

            scroller.ReloadData();
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void OnClickConfirm(ShopCellView_Coin cell)
        {
            if (UserDataManager.Instance.UseCash(cell.data.price_count))
            {
                AudioManager.Instance.PlaySound("sfx_button_main");

                Popup_Reward.Instance.Show(cell.coinValue, 0, 0, () =>
                {
                    AudioManager.Instance.PlaySound("sfx_shop_coin_get");
                    scroller.RefreshActiveCellViews();
                    UserDataManager.Instance.SaveData();
                });
            }
            else
            {
                AudioManager.Instance.PlaySound("sfx_require");
            }
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return Datas.coinShopData.Length;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 300;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            ShopCellView_Coin cellView = scroller.GetCellView(cellViewPrefab) as ShopCellView_Coin;
            cellView.name = "Cell " + dataIndex;

            return cellView;
        }

        private void CellViewVisibilityChanged(EnhancedScrollerCellView cellView)
        {
            ShopCellView_Coin view = cellView as ShopCellView_Coin;

            if (cellView.active)
            {
                view.SetData(Datas.coinShopData[cellView.dataIndex],
                    OnClickConfirm);
            }
        }
    }
}