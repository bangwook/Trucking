using System;
using DatasTypes;
using TMPro;
using Trucking.Common;
using Trucking.Manager;
using Trucking.Model;
using Trucking.UI.Popup;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI
{
    public class UIGuideQuest : MonoSingleton<UIGuideQuest>
    {
        public GameObject goCharacter;
        public GameObject goHint;

        public Animator animator;

        public Button btnBlackPanel;
        public Button btnCharacter;
        public Button btnHint;
        public Button btnCollect;

        public TextMeshProUGUI txtQuest;
        public TextMeshProUGUI txtCharacter;
        public TextMeshProUGUI txtComplete;
        public TextMeshProUGUI txtReward;
        public TextMeshProUGUI txtRewardCount;
        public Image imgReward;

        private CompositeDisposable disposableCount = new CompositeDisposable();
        private CompositeDisposable disposableText = new CompositeDisposable();
        private string strText;
        private int typeCount;
        private GuideQuestData questData;

        public void Init()
        {
            btnBlackPanel.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (txtCharacter.text.Length < strText.Length)
                    {
                        txtCharacter.text = strText;
                        disposableText.Clear();
                    }
                    else
                    {
                        btnBlackPanel.gameObject.SetActive(false);
                        goCharacter.SetActive(false);
                        goHint.SetActive(true);
                        GuideQuestManager.Instance.model.isCharacterText.Value = false;
                    }
                })
                .AddTo(this);

            btnHint.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    Popup_GuideMain.Instance.Show(GuideQuestManager.Instance.GetData().guide_id);
                    FBAnalytics.FBAnalytics.LogHintClickEvent(GuideQuestManager.Instance.GetData().qid.id);
                })
                .AddTo(this);

            btnCollect.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    Popup_Reward.Instance.Show(GuideQuestManager.Instance.GetReward(), () =>
                    {
                        GuideQuestManager.Instance.SetNext();
                        Show();
                        FBAnalytics.FBAnalytics.LogGuideQuestEvent(UserDataManager.Instance.data.lv.Value,
                            GuideQuestManager.Instance.model.index.Value);
                    });
                })
                .AddTo(this);

            GuideQuestManager.Instance.model.isSuccess.Subscribe(success =>
            {
                btnHint.gameObject.SetActive(!success);
                btnCollect.gameObject.SetActive(success);
                txtComplete.gameObject.SetActive(success);
                txtReward.gameObject.SetActive(!success);
            }).AddTo(this);

            GuideQuestManager.Instance.model.questModel.Subscribe(qm =>
            {
                disposableCount.Clear();

                if (qm != null)
                {
                    questData = GuideQuestManager.Instance.GetData();

                    txtRewardCount.gameObject.SetActive(questData.reward_type.type != RewardData.eType.truck_id
                                                        && questData.reward_type.type != RewardData.eType.random_box);

                    txtRewardCount.text =
                        Utilities.GetThousandCommaText(questData.reward_count);
                    imgReward.sprite =
                        GameManager.Instance.GetRewardImage(questData.reward_type.type,
                            RewardModel.GetIndex(questData.reward_type.type,
                                questData.reward_index));

                    if (qm.max.Value > 1)
                    {
                        qm.count.Subscribe(count =>
                        {
                            count = (long) Mathf.Clamp(count, 0, qm.max.Value);
                            txtQuest.text =
                                $"{Utilities.GetStringByData(questData.string2)} ({count}/{qm.max})";
                        }).AddTo(disposableCount);
                    }
                    else
                    {
                        txtQuest.text = Utilities.GetStringByData(questData.string2);
                    }

                    UserDataManager.Instance.SaveData();
                }
            }).AddTo(this);

            Observable.CombineLatest(GuideQuestManager.Instance.model.hasMission,
                GuideQuestManager.Instance.model.isCharacterText,
                (hasMission, isCharacterText) => (hasMission, isCharacterText)).Subscribe(value =>
            {
                btnBlackPanel.gameObject.SetActive(value.Item1 && value.Item2);
                goCharacter.SetActive(value.Item2);
                goHint.SetActive(!value.Item2);

                if (value.Item2)
                {
                    animator.Play("popup_giuide_character", -1, 0);
                    btnBlackPanel.gameObject.SetActive(true);
                    strText = Utilities.GetStringByData(GuideQuestManager.Instance.GetData().string1);
                    typeCount = 0;
                    txtCharacter.text = "";

                    Observable.Interval(TimeSpan.FromSeconds(0.01f)).Subscribe(_ =>
                    {
                        if (typeCount < strText.Length)
                        {
                            txtCharacter.text += strText[typeCount++];
                        }
                        else
                        {
                            disposableText.Clear();
                        }
                    }).AddTo(disposableText);
                }
            }).AddTo(this);

            gameObject.SetActive(true);
        }

        public void Show()
        {
            if (GuideQuestManager.Instance.model.hasMission.Value
                && UIMain.Instance.buttonHQ.gameObject.activeSelf
                && GameManager.Instance.fsm.GetCurrentState() == GameManager.Instance.worldMapState)
            {
                gameObject.SetActive(true);
                goCharacter.SetActive(GuideQuestManager.Instance.model.isCharacterText.Value);
                goHint.SetActive(!GuideQuestManager.Instance.model.isCharacterText.Value);
                btnBlackPanel.gameObject.SetActive(GuideQuestManager.Instance.model.isCharacterText.Value);
            }
            else
            {
                Close();
            }
        }

        public void Close()
        {
            goCharacter.SetActive(false);
            goHint.SetActive(false);
            btnBlackPanel.gameObject.SetActive(false);
        }
    }
}