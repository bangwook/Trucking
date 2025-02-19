//using TMPro;
//using Trucking.Common;
//using Trucking.Manager;
//using UniRx;
//using UnityEngine.UI;
//
//namespace Trucking.UI.Popup
//{
//    public class Popup_FreeCash_Reward : Popup_Base<Popup_FreeCash_Reward>
//    {
//        public Button btnClaim;
//        public TextMeshProUGUI txtCashRewardCount;
//
//        private void Start()
//        {
//            btnClaim.OnClickAsObservable()
//                .Subscribe(_ =>
//                {   
//                    AudioManager.Instance.PlaySound("sfx_button_main");
//                    
//                    GameManager.Instance.fsm.PopState();
//                    Popup_Reward.Instance.Show(FreeCashManager.Instance.GetFinalReward(), () =>
//                    {
//                        UserDataManager.Instance.SaveData();
//                    });
//                })
//                .AddTo(this);
//        }
//        
//        public override void Show()
//        {
//            base.Show();
//            txtCashRewardCount.text = Utilities.GetThousandCommaText(FreeCashManager.Instance.GetFinalReward().count.Value);
//        }
//    }
//}