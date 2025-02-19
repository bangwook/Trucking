using Trucking.Ad;
using Trucking.Common;
using UniRx;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_FreeCash : Popup_Base<Popup_FreeCash>
    {
        public Button btnClaim;

        private void Start()
        {
            btnClaim.OnClickAsObservable()
                .Subscribe(_ =>
                {
//                    if (!FreeCashManager.Instance.CanNext())
//                    {
//                        AudioManager.Instance.PlaySound("sfx_require");
//                        return;
//                    }

                    AudioManager.Instance.PlaySound("sfx_button_main");
                    Popup_Loading.Instance.Show();
                    AdManager.Instance.ShowRewardLoad(AdUnit.Free_Cash)
                        .Subscribe(
                            result =>
                            {
                                GameManager.Instance.fsm.PopState();

                                if (result == AdResult.Success)
                                {
                                    Popup_Reward.Instance.Show(0, 1, 0, () =>
                                    {
                                        UserDataManager.Instance.SaveData();

//                                    if (FreeCashManager.Instance.SetNext())
//                                    {
//                                        Popup_FreeCash_Reward.Instance.Show();
//                                    }
                                    });
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
                })
                .AddTo(this);
        }

        public override void Show()
        {
            base.Show();
//            txtLeftTime.gameObject.SetActive(false);
            btnClaim.gameObject.SetActive(true);

//            txtCashRewardCount.text = Utilities.GetThousandCommaText(FreeCashManager.Instance.GetReward().count.Value);
//            txtCashRewardCountSmall.text = Utilities.GetThousandCommaText(FreeCashManager.Instance.GetFinalReward().count.Value);
//            Update();
        }

//        private void Update()
//        {
//            btnClaim.gameObject.GetComponent<UIEffect>().enabled = false;
//            txtCollect.gameObject.SetActive(true);
//            txtLeftTime.gameObject.SetActive(!FreeCashManager.Instance.CanNext());
//            
//            if (FreeCashManager.Instance.model.hasMission.Value)
//            {
//                txtDesc.text = Utilities.GetStringByData(30060);
//                txtLeftTime.text =
//                    Utilities.GetTimeStringShort(FreeCashManager.Instance.model.delayTime.Value - DateTime.Now);
//
//            }
//            else
//            {
//                txtDesc.text = Utilities.GetStringByData(30061);
//                txtLeftTime.text =
//                    Utilities.GetTimeStringShort(FreeCashManager.Instance.model.nextTime.Value - DateTime.Now);
//            }
//
//
//            txtLeftTimeSmall.gameObject.SetActive(FreeCashManager.Instance.model.hasMission.Value);
//            
//            if (FreeCashManager.Instance.model.hasMission.Value)
//            {
//                txtLeftTimeSmall.text = Utilities.GetTimeStringShort(FreeCashManager.Instance.model.nextTime.Value - DateTime.Now);    
//            }
//
//            for (int i = 0; i < arrCheck.Length; i++)
//            {
//                arrCheck[i].SetActive(i < FreeCashManager.Instance.model.step.Value);
//            }
//        }
    }
}