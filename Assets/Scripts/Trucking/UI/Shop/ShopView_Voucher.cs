using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Shop
{
    public class ShopView_Voucher : MonoBehaviour
    {
        public TextMeshProUGUI txtGold;        
        public TextMeshProUGUI txtAmount;
        public Button btnMin;
        public Button btnMax;
        public Button btnPlus;
        public Button btnMinus;
        
        public Button btnExchange;
        public TextMeshProUGUI txtExchangeGold;

//        private ReactiveProperty<int> amount = new ReactiveProperty<int>();
//        private void Start()
//        {
//            Observable.CombineLatest(UserDataManager.Instance.data.gold, amount, (gold, am) => gold)
//                .Subscribe(gold =>
//                {
//                    txtAmount.text = Utilities.GetThousandCommaText(amount.Value);
//                    txtExchangeGold.text = Utilities.GetThousandCommaText(amount.Value * Datas.baseData[0].g_to_v_exchange);
//                    
//                    if (gold < amount.Value * Datas.baseData[0].g_to_v_exchange)
//                    {
//                        txtExchangeGold.color = Color.red;
//                    }
//                    else
//                    {
//                        txtExchangeGold.color = Color.white;
//                                  }
//                }).AddTo(this);
//           
//            btnMin.OnClickAsObservable()
//                .Subscribe(_ =>
//                {
//                    AudioManager.Instance.PlaySound("sfx_button_main");
//                    amount.Value = 1;
//                })
//                .AddTo(this);
//
//            btnMax.OnClickAsObservable()
//                .Subscribe(_ =>
//                {
//                    AudioManager.Instance.PlaySound("sfx_button_main");
//                    amount.Value = (int)(UserDataManager.Instance.data.gold.Value / Datas.baseData[0].g_to_v_exchange);
//                })
//                .AddTo(this);
//            
//            btnPlus.OnClickAsObservable()
//                .Subscribe(_ =>
//                {
//                    AudioManager.Instance.PlaySound("sfx_button_main");
//                    
//                    if (amount.Value < UserDataManager.Instance.data.gold.Value / Datas.baseData[0].g_to_v_exchange)
//                    {
//                        amount.Value++;
//                    }
//                })
//                .AddTo(this);
//            
//            btnMinus.OnClickAsObservable()
//                .Subscribe(_ =>
//                {
//                    AudioManager.Instance.PlaySound("sfx_button_main");
//                    
//                    if (amount.Value > 1)
//                    {
//                        amount.Value--;
//                    }
//
//                })
//                .AddTo(this);
//            
//            btnExchange.OnClickAsObservable()
//                .Subscribe(_ =>
//                {
//                    if (UserDataManager.Instance.data.gold.Value < amount.Value * Datas.baseData[0].g_to_v_exchange)
//                    {
//                        AudioManager.Instance.PlaySound("sfx_require");
//                        btnExchange.transform.DOShakePosition(0.3f, 3);
//                    }
//                    else
//                    {
//                        AudioManager.Instance.PlaySound("sfx_button_main");
//                    
//                        if (UserDataManager.Instance.UseGold(amount.Value *  Datas.baseData[0].g_to_v_exchange))
//                        {
//                            FBAnalytics.FBAnalytics.LogVoucherExchangeEvent(UserDataManager.Instance.data.lv.Value, amount.Value *  Datas.baseData[0].g_to_v_exchange);
//                            Popup_Reward.Instance.Show(0, 0, 0);
//                            amount.Value = 1;
//                        }    
//                    }
//                    
//                })
//                .AddTo(this);
//        }
//        
//        public void Show()
//        {
//            gameObject.SetActive(true);
//            amount.Value = 1;
//            txtGold.text = Utilities.GetThousandCommaText(Datas.baseData[0].g_to_v_exchange) + " =";
//            
//            transform.GetChild(0).localScale = new Vector3(0, 1, 1);
//            transform.GetChild(0).DOScaleX(1, 0.2f);
//        }
//        
//        public void Close()
//        {
//            gameObject.SetActive(false);
//            transform.GetChild(0).localScale = new Vector3(0, 1, 1);
//        }
    }
}