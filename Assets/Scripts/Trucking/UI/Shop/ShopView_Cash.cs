using System.Collections.Generic;
using DatasTypes;
using EnhancedUI.EnhancedScroller;
using Trucking.Ad;
using Trucking.Common;
using Trucking.Iap;
using Trucking.Manager;
using Trucking.UI.Popup;
using UniRx;
using UnityEngine;

namespace Trucking.UI.Shop
{
    public class ShopView_Cash : MonoBehaviour, IEnhancedScrollerDelegate
    {
        public EnhancedScroller scroller;
        public EnhancedScrollerCellView cellViewPrefab;

        readonly List<IAPData> datas = new List<IAPData>();

        public void Show()
        {
            gameObject.SetActive(true);

            if (scroller.Delegate == null)
            {
                scroller.Delegate = this;
                scroller.cellViewVisibilityChanged = CellViewVisibilityChanged;
            }

            datas.Clear();

            if (FreeCashManager.Instance.model.hasStart.Value)
            {
                datas.Insert(0, new IAPData(Utilities.GetStringByData(20085), null, Datas.rewardData.cash, "",
                    1, 0,
                    1, 20085, 0));
            }

            for (int i = 0; i < Datas.iAPData.Length; i++)
            {
                if (Datas.iAPData[i].item_type.type == RewardData.eType.cash)
                {
                    datas.Add(Datas.iAPData[i]);
                    Debug.Log($"Iap : {Datas.iAPData[i].name}");
                }
            }

            scroller.ReloadData();
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void OnClickConfirm(ShopCellView_Cash cell)
        {
            AudioManager.Instance.PlaySound("sfx_button_main");

            if (cell.dataIndex == 0)
            {
                if (AdManager.Instance.IsLoadedReward.Value)
                {
                    Popup_Loading.Instance.Show();
                    AdManager.Instance.ShowRewardLoad(AdUnit.Free_Cash)
                        .Subscribe(
                            result =>
                            {
                                GameManager.Instance.fsm.PopState();

                                if (result == AdResult.Success)
                                {
                                    Popup_Reward.Instance.Show(0, 1, 0, () => { UserDataManager.Instance.SaveData(); });
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
            }
            else
            {
                IapManager.Instance.Purchase(cell.data.iap_id.id);
            }
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
            ShopCellView_Cash cellView = scroller.GetCellView(cellViewPrefab) as ShopCellView_Cash;
            cellView.name = "Cell " + dataIndex;

            return cellView;
        }

        private void CellViewVisibilityChanged(EnhancedScrollerCellView cellView)
        {
            ShopCellView_Cash view = cellView as ShopCellView_Cash;

            if (cellView.active)
            {
                view.SetData(datas[cellView.dataIndex],
                    OnClickConfirm);
            }
        }
    }
}