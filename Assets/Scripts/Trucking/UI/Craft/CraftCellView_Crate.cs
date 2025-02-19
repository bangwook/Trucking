using DatasTypes;
using EnhancedUI.EnhancedScroller;
using TMPro;
using Trucking.Common;
using Trucking.UI.Popup;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Craft
{
    public class CraftCellView_Crate : EnhancedScrollerCellView
    {
        public Button btnOpen;
        public GameObject grayOpen;
        
        public TextMeshProUGUI txtName;
        public TextMeshProUGUI txtCrateCount;
        public TextMeshProUGUI txtCrateCount2;
        public TextMeshProUGUI txtCash;
        public TextMeshProUGUI txtDes;
        public Transform trsIcon;
        public GameObject noti;
        
        [HideInInspector] public Crate data;
        private CompositeDisposable _disposable = new CompositeDisposable();

        private void Start()
        {
            btnOpen.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (UserDataManager.Instance.data.crate[dataIndex].Value > 0
                        && UserDataManager.Instance.UseCash(data.price_count))
                    {
                        AudioManager.Instance.PlaySound("sfx_button_main");
                        UserDataManager.Instance.UseCrate(dataIndex, 1);
                        FBAnalytics.FBAnalytics.LogOpenCrateEvent(UserDataManager.Instance.data.lv.Value,
                            UserDataManager.Instance.data.cash.Value,
                            dataIndex == 0 ? 1 : 0, 
                            dataIndex == 1 ? 1 : 0,
                            UserDataManager.Instance.data.crate[0].Value,
                            UserDataManager.Instance.data.crate[1].Value);
                        Popup_RandomBoxOpenEffect.Instance.ShowCrateBox(data);
                    }
                    else
                    {
                        AudioManager.Instance.PlaySound("sfx_require");
                    }
                })
                .AddTo(this);
        }
        
        private void OnDisable()
        {
            UserDataManager.Instance.crateNoti[dataIndex].Value = false;
            _disposable.Clear();
        }
        
        public void SetData(Crate _data)
        {
            data = _data;
            txtName.text = Utilities.GetStringByData(data.title_string);
            txtDes.text = Utilities.GetStringByData(data.des_string);
            txtCash.text = data.price_count.ToString();
            
            for (int i = 0; i < trsIcon.childCount; i++)
            {
                trsIcon.GetChild(i).gameObject.SetActive(data.icon - 1 == i);
            }

            _disposable.Clear();
            
            UserDataManager.Instance.data.crate[dataIndex].Subscribe(crate =>
                {
                    txtCrateCount.text = crate.ToString();
                    txtCrateCount2.text = crate.ToString();
                })
                .AddTo(_disposable);

            UserDataManager.Instance.crateNoti[dataIndex].Subscribe(hasNoti =>
            {
                noti.SetActive(hasNoti);    
            }).AddTo(_disposable);

            UserDataManager.Instance.data.cash.Subscribe(cash =>
            {
                txtCash.color = cash >= data.price_count ? Utilities.GetColorByHtmlString("464646") : Color.red;
            }).AddTo(_disposable);
            
            Observable.CombineLatest(UserDataManager.Instance.data.crate[dataIndex],
                UserDataManager.Instance.data.cash,
                (crate, cash) => crate < 1 || cash < data.price_count).Subscribe(value =>
            {
                grayOpen.SetActive(value);
            }).AddTo(_disposable);
        }
    }
}