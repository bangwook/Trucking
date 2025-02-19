using System;
using System.Collections.Generic;
using Coffee.UIExtensions;
using DatasTypes;
using TMPro;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.Mission;
using UniRx;
using UnityEngine.UI;


namespace Trucking.UI.Popup
{
    public class Popup_NewDailyMission : Popup_Base<Popup_NewDailyMission>
    {
        public Button btnClaim;
        public TextMeshProUGUI txtTitle;
        public TextMeshProUGUI txtCity;
        public TextMeshProUGUI txtSliderCount;
        public TextMeshProUGUI txtQuest;
        public Image imgQuest;
        public Slider slider;

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private CompositeDisposable _disposableTimer = new CompositeDisposable();

        private void Start()
        {
            btnClaim.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    GameManager.Instance.fsm.PopState();

                    AudioManager.Instance.PlaySound("sfx_resource_get");

                    if (NewDailyMissionManager.Instance.hasReward.Value)
                    {
                        RewardModel rewardModel = NewDailyMissionManager.Instance.GetReward();

                        if (rewardModel.IsRandomBox())
                        {
                            List<RewardModel> listRewardModels = new List<RewardModel>();
                            listRewardModels.Add(rewardModel);

                            Popup_RandomBoxOpenEffect.Instance.Show(listRewardModels, () =>
                            {
                                MissionManager.Instance.AddValue(QuestData.eType.random_box, 1);
                                NewDailyMissionManager.Instance.SetClear();
                            });
                        }
                        else
                        {
                            Popup_Reward.Instance.Show(rewardModel,
                                () => { NewDailyMissionManager.Instance.SetClear(); });
                        }
                    }
                })
                .AddTo(this);
        }


        public override void Show()
        {
            base.Show();

            _compositeDisposable.Clear();
            gameObject.SetActive(true);

            txtTitle.text = Utilities.GetStringByData(10005);

            if (NewDailyMissionManager.Instance.model.questModel.Value != null)
            {
                NewDailyMissionManager.Instance.model.questModel.Value.ObsChanged.Subscribe(model =>
                {
                    long count = Utilities.LongClamp(model.count.Value, 0, model.max.Value);
                    txtSliderCount.text =
                        $"{Utilities.GetNumberKKK(count)} / {Utilities.GetNumberKKK(model.max.Value)}";
                    slider.maxValue = model.max.Value;
                    slider.value = count;
                    txtQuest.text = model.GetDescription(NewDailyMissionManager.Instance.city.Value);
                    imgQuest.sprite = GameManager.Instance.GetQuestIcon(model);
                }).AddTo(_compositeDisposable);
            }

            NewDailyMissionManager.Instance.hasReward.Subscribe(success =>
            {
                btnClaim.GetComponent<UIEffect>().enabled = !success;
                btnClaim.GetComponent<Button>().enabled = success;
            }).AddTo(_compositeDisposable);

            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (NewDailyMissionManager.Instance.model.nextTime.Value > DateTime.Now
                    && !NewDailyMissionManager.Instance.model.questModel.Value.IsSuccess())
                {
                    txtCity.text =
                        Utilities.GetTimeStringShort(
                            NewDailyMissionManager.Instance.model.nextTime.Value - DateTime.Now);
                }
                else
                {
                    txtCity.text = NewDailyMissionManager.Instance.city.Value.name;
                }
            }).AddTo(_disposableTimer);
        }

        public override void Close()
        {
            base.Close();
            _compositeDisposable.Clear();
            _disposableTimer.Clear();
        }
    }
}