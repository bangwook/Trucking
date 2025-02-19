using System;
using EnhancedUI.EnhancedScroller;
using TMPro;
using Trucking.Common;
using Trucking.Iap;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Trucking.UI.Shop
{
    public class ShopCellView_Crate : EnhancedScrollerCellView
    {
        public Button btnConfirm;

        public TextMeshProUGUI txtName;
        public TextMeshProUGUI txtValue;
        public TextMeshProUGUI txtPrice;
        public DatasTypes.IAPData data;
        public Transform trsIcon;
        
        private Action<ShopCellView_Crate> OnClickConfirm;
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
            Action<ShopCellView_Crate> _onClickConfirm)
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

            IapManager.Instance.ObsLocalizedPrice(data.iap_id.id)
                .Subscribe(txt =>
                {
                    txtPrice.text = txt;
                }).AddTo(disposable);
        }
    }
}