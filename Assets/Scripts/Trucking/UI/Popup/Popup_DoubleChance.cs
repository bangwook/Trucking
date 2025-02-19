using System;
using System.Collections.Generic;
using TMPro;
using Trucking.Ad;
using Trucking.Common;
using Trucking.Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_DoubleChance : Popup_Base<Popup_DoubleChance>
    {
        public Button btnClaim;
        public Button btnPrepare;

        public TextMeshProUGUI txtTitle;
        public TextMeshProUGUI txtLeftTime;
        public TextMeshProUGUI txtDesc;
        public GameObject rewardText;

        public TextMeshProUGUI[] txtRewardCount;
        public Image[] imgRewards;

        private Action<bool> onClickClose;

        private void Start()
        {
            disposableClose.Clear();

            btnCloseX.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    GameManager.Instance.fsm.PopState();
                    onClickClose?.Invoke(false);
                })
                .AddTo(this);

            btnClaim.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");

                    Popup_Loading.Instance.Show();
                    AdManager.Instance.ShowRewardLoad(AdUnit.Job_Complete)
                        .Subscribe(
                            result =>
                            {
                                GameManager.Instance.fsm.PopState();

                                if (result == AdResult.Success)
                                {
                                    GameManager.Instance.fsm.PopState();
                                    onClickClose?.Invoke(true);
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
                .Subscribe(_ => { AudioManager.Instance.PlaySound("sfx_button_cancle"); }).AddTo(this);

            AdManager.Instance.IsLoadedReward.Subscribe(load =>
            {
                btnPrepare.gameObject.SetActive(!load);
                btnClaim.gameObject.SetActive(load);
            }).AddTo(this);
        }

        public void Show(List<RewardModel> _listRewardModels, Action<bool> _onClickClose = null)
        {
            onClickClose = _onClickClose;
            base.Show();
            rewardText.SetActive(true);
            txtLeftTime.gameObject.SetActive(false);

            txtTitle.text = Utilities.GetStringByData(20086);
            txtDesc.text = Utilities.GetStringByData(30052);

            for (int i = 0; i < 3; i++)
            {
                imgRewards[i].transform.parent.parent.gameObject.SetActive(i < _listRewardModels.Count);

                if (i < _listRewardModels.Count)
                {
                    txtRewardCount[i].text = Utilities.GetNumberKKK(_listRewardModels[i].count.Value);
                    imgRewards[i].sprite = GameManager.Instance.GetRewardImage(_listRewardModels[i]);
                }
            }
        }
    }
}