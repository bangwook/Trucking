using System;
using Newtonsoft.Json.Utilities;
using TMPro;
using Trucking.Ad;
using Trucking.Common;
using Trucking.UI.Mission;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_OperationManager : Popup_Base<Popup_OperationManager>
    {
        public Button btnAd;
        public GameObject btnAdGray;
        public GameObject video;
        public Button btnCollect;
        public TextMeshProUGUI txtBoosterTime;
        public TextMeshProUGUI txtBonusDes;
        public TextMeshProUGUI txtNextTime;
        public Slider slider;
        public Transform trsCheckBox;
        int buffIndex;

        private CompositeDisposable disposable = new CompositeDisposable();

        private void Start()
        {
            btnAd.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (AdManager.Instance.IsLoadedReward.Value)
                    {
                        AudioManager.Instance.PlaySound("sfx_button_main");
                        Popup_Loading.Instance.Show();
                        AdManager.Instance.ShowRewardLoad(AdUnit.Time_Boost)
                            .Subscribe(
                                result =>
                                {
                                    GameManager.Instance.fsm.PopState();

                                    if (result == AdResult.Success)
                                    {
                                        AudioManager.Instance.PlaySound("sfx_resource_get");
                                        UserDataManager.Instance.AddBooster(buffIndex, 30);

                                        TimeSpan timeSpan =
                                            UserDataManager.Instance.data.boosterShopData[buffIndex] - DateTime.Now;

                                        if (timeSpan > TimeSpan.FromHours(4))
                                        {
                                            UserDataManager.Instance.data.boosterShopData[buffIndex] =
                                                DateTime.Now.AddHours(4);
                                        }

                                        OperationManager.Instance.SetNext();
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
                    else
                    {
                        AudioManager.Instance.PlaySound("sfx_require");
                    }
                })
                .AddTo(this);

            btnCollect.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");

                    Popup_Reward.Instance.Show(OperationManager.Instance.GetBonusReward(),
                        () => { OperationManager.Instance.CollectBonus(); });
                })
                .AddTo(this);

            AdManager.Instance.IsLoadedReward.Subscribe(load =>
            {
                btnAdGray.SetActive(!load);
                video.SetActive(load);
            }).AddTo(this);

            Observable.CombineLatest(
                    OperationManager.Instance.model.rewardIndex,
                    OperationManager.Instance.model.crate_delay,
                    (index, delay) => (index, delay))
                .Subscribe(value =>
                {
                    for (int i = 0; i < trsCheckBox.childCount; i++)
                    {
                        trsCheckBox.GetChild(i).GetChild(0).gameObject.SetActive(i < value.Item1);
                    }

                    trsCheckBox.gameObject.SetActive(!value.Item2);
                    btnCollect.gameObject.SetActive(value.Item1 >= trsCheckBox.childCount && !value.Item2);
                    txtBonusDes.text = Utilities.GetStringByData(!value.Item2 ? 20155 : 20156);
                    txtNextTime.gameObject.SetActive(value.Item2);
                }).AddTo(disposable);

            slider.maxValue = (float) TimeSpan.FromHours(4).TotalSeconds;
            buffIndex = Datas.buffData.ToArray().IndexOf(x => x.id == Datas.buffData.speed.id);

            Observable.EveryUpdate().Subscribe(_ =>
            {
                TimeSpan timeSpan = UserDataManager.Instance.data.boosterShopData[buffIndex] - DateTime.Now;

                if (timeSpan.TotalMilliseconds > 0)
                {
                    txtBoosterTime.text = Utilities.GetTimeString(timeSpan);
                }
                else
                {
                    txtBoosterTime.text = Utilities.GetTimeString(TimeSpan.FromMilliseconds(0));
                }

                slider.value = (float) timeSpan.TotalSeconds;

                txtNextTime.text =
                    Utilities.GetTimeString(OperationManager.Instance.model.endTime.Value - DateTime.Now);
            }).AddTo(disposable);
        }
    }
}