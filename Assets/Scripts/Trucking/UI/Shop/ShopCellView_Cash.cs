using System;
using EnhancedUI.EnhancedScroller;
using TMPro;
using Trucking.Ad;
using Trucking.Common;
using Trucking.Iap;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Trucking.UI.Shop
{
    public class ShopCellView_Cash : EnhancedScrollerCellView
    {
        public Button btnConfirm;
        public GameObject btnConfirmGray;
        public GameObject video;
        public GameObject video2;
        public TextMeshProUGUI txtName;
        public TextMeshProUGUI txtValue;
        public TextMeshProUGUI txtPrice;
        public GameObject imgRibon_Best;
        public GameObject imgRibon_Popular;
        public Image imgCheckBack;
        public DatasTypes.IAPData data;
        public Transform trsIcon;

        private Action<ShopCellView_Cash> OnClickConfirm;
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

        public void SetData(DatasTypes.IAPData _data,
            Action<ShopCellView_Cash> _onClickConfirm)
        {
            disposable.Clear();

            data = _data;
            OnClickConfirm = _onClickConfirm;

            txtName.text = Utilities.GetStringByData(data.string_name);
            txtValue.text = "x " + Utilities.GetThousandCommaText(data.count);

            for (int i = 0; i < trsIcon.childCount; i++)
            {
                trsIcon.GetChild(i).gameObject.SetActive(data.icon - 1 == i);
            }

            imgRibon_Best.SetActive(false);
            imgRibon_Popular.SetActive(false);
            imgCheckBack.enabled = false;

            if (dataIndex == 1)
            {
                imgRibon_Popular.SetActive(true);
                imgCheckBack.enabled = true;
            }
            else if (dataIndex == 2)
            {
                imgRibon_Best.SetActive(true);
                imgCheckBack.enabled = true;
            }

            video2.SetActive(data.iap_id == null);

            if (data.iap_id != null)
            {
                btnConfirmGray.SetActive(false);
                txtPrice.gameObject.SetActive(true);
                video.SetActive(false);
                IapManager.Instance.ObsLocalizedPrice(data.iap_id.id)
                    .Subscribe(txt => { txtPrice.text = txt; }).AddTo(disposable);
            }
            else
            {
                txtPrice.text = Utilities.GetStringByData(20084);

                AdManager.Instance.IsLoadedReward.Subscribe(load =>
                {
                    txtPrice.gameObject.SetActive(!load);
                    video.SetActive(load);
                    btnConfirmGray.SetActive(!load);
                }).AddTo(this);
            }
        }
    }
}