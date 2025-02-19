using DG.Tweening;
using TMPro;
using Trucking.Ad;
using Trucking.Common;
using Trucking.Manager;
using Trucking.Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_TouchObject : Popup_Base<Popup_TouchObject>
    {
        public Button btnClaim;
        public Button btnClaimX2;
        public Button btnPrepare;
        public TextMeshProUGUI txtRewardCount;
        public Image imgRewardType;

        private RewardModel rewardModel;

        private void Start()
        {
            btnClaim.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    GetReward();
                })
                .AddTo(this);

            btnClaimX2.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");

                    Popup_Loading.Instance.Show();

                    AdManager.Instance.ShowRewardLoad(AdUnit.Airplane_Cash)
                        .Subscribe(result =>
                        {
                            GameManager.Instance.fsm.PopState();

                            if (result == AdResult.Success)
                            {
                                GetReward(true);
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

            btnPrepare.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_require");
                    btnPrepare.transform.DOShakePosition(0.3f, 3);
                }).AddTo(this);

            AdManager.Instance.IsLoadedReward.Subscribe(load =>
            {
                Debug.Log($"Popup_TouchObject IsLoadedReward : {load}");
                btnPrepare.gameObject.SetActive(!load);
                btnClaimX2.gameObject.SetActive(load);
            }).AddTo(this);
        }

        public override void BackKey()
        {
            btnClaim.onClick.Invoke();
        }

        public void Show(Vector3 pos)
        {
            rewardModel = TouchObjectManager_Plane.Instance.GetReward();

            base.Show();
            imgRewardType.sprite = GameManager.Instance.GetRewardImage(rewardModel);
            txtRewardCount.text = Utilities.GetThousandCommaText(rewardModel.count.Value);
        }

        void GetReward(bool isDouble = false)
        {
            if (isDouble)
            {
                rewardModel.count.Value *= 2;
            }

            GameManager.Instance.fsm.PopState();

            TouchObjectManager_Plane.Instance.AddCashLimit(isDouble);
            Popup_Reward.Instance.Show(rewardModel);
        }
    }
}