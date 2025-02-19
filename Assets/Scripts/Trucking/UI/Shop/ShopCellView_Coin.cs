using System;
using EnhancedUI.EnhancedScroller;
using TMPro;
using Trucking.Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Trucking.UI.Shop
{
    public class ShopCellView_Coin : EnhancedScrollerCellView
    {
        public Button btnConfirm;
        public Image imgbtnConfirmGray;

        public TextMeshProUGUI txtName;
        public TextMeshProUGUI txtValue;
        public TextMeshProUGUI txtPrice;
        public GameObject imgRibon_Best;
        public GameObject imgRibon_Popular;
        public Image imgCheckBack;
        public DatasTypes.CoinShopData data;
        public Transform trsIcon;

        [HideInInspector] public long coinValue;
        private Action<ShopCellView_Coin> OnClickConfirm;

        private CompositeDisposable disposable = new CompositeDisposable();


        private void Start()
        {
            btnConfirm.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (OnClickConfirm != null)
                    {
                        OnClickConfirm(this);
                    }
                })
                .AddTo(this);

            disposable.AddTo(this);
        }

        private void OnDisable()
        {
            disposable.Clear();
        }


        public void SetData(DatasTypes.CoinShopData _data,
            Action<ShopCellView_Coin> _onClickConfirm
        )
        {
            disposable.Clear();

            data = _data;
            OnClickConfirm = _onClickConfirm;

            txtName.text = Utilities.GetStringByData(data.string_name);

            coinValue = (long) (data.price_count * ShopView.Instance.coinView.coinExchangeRatio *
                                (1 + data.bonus_percent / 100) * 1000) / 1000 * 1000;

            txtValue.text = "x " + Utilities.GetThousandCommaText(coinValue);
            txtPrice.text = Utilities.GetThousandCommaText(data.price_count);

            UserDataManager.Instance.data.cash.Subscribe(cash =>
            {
                txtPrice.color = cash < data.price_count ? Color.red : Color.white;
                imgbtnConfirmGray.gameObject.SetActive(cash < data.price_count);
            }).AddTo(disposable);

            for (int i = 0; i < trsIcon.childCount; i++)
            {
                trsIcon.GetChild(i).gameObject.SetActive(data.icon - 1 == i);
            }

            imgRibon_Best.SetActive(false);
            imgRibon_Popular.SetActive(false);
            imgCheckBack.enabled = false;

            if (dataIndex == 0)
            {
                imgRibon_Popular.SetActive(true);
                imgCheckBack.enabled = true;
            }
            else if (dataIndex == 1)
            {
                imgRibon_Best.SetActive(true);
                imgCheckBack.enabled = true;
            }
        }
    }
}