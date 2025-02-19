using System;
using System.Linq;
using DatasTypes;
using TMPro;
using Trucking.Common;
using Trucking.UI.Guide;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Trucking.UI.Popup
{
    public class Popup_LevelUp : Popup_Base<Popup_LevelUp>
    {
        public Button btnClaim;

        public TextMeshProUGUI txtLv;
        public TextMeshProUGUI[] txtRewardCount;
        public ParticleSystem particle;

        public Image[] imgRewards;

        private int lastRouteMax;
        private int lastAreaOpen;
        private int lastGuide;
        private int unLockArrIndex;

        private void Start()
        {
            btnClaim.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    GameManager.Instance.fsm.PopState();

                    LevelData levelData = Datas.levelData.ToArray()
                        .FirstOrDefault(x => x.level == UserDataManager.Instance.data.lv.Value);

                    Popup_Reward.Instance.Show(levelData.reward_gold, levelData.reward_cash, 0,
                        () =>
                        {
                            unLockArrIndex = 0;
                            CheckUnlockContents(() => { UserDataManager.Instance.CheckExp(); });
                        });
                })
                .AddTo(this);
        }

        public override void Show()
        {
            base.Show();

            if (lastGuide == 0)
            {
                lastGuide = UserDataManager.Instance.data.lv.Value;
            }

            FBAnalytics.FBAnalytics.LogLevelUpEvent(UserDataManager.Instance.data.lv.Value,
                GameManager.Instance.trucks.Count(x => x.model.hasRoute.Value),
                GameManager.Instance.trucks.Count,
                GameManager.Instance.trucks.Where(x => x.model.hasRoute.Value).Sum(x => x.MaxWeight.Value),
                GameManager.Instance.roads.Count(x => x.model.isOpen.Value),
                GameManager.Instance.cities.Count(x => x.IsOpen()),
                GameManager.Instance.cities.Count(x => x.IsOpen() && x.model.level.Value == 0),
                GameManager.Instance.cities.Count(x => x.model.level.Value == 1),
                GameManager.Instance.cities.Count(x => x.model.level.Value == 2),
                GameManager.Instance.cities.Count(x => x.model.level.Value == 3),
                GameManager.Instance.cities.Count(x => x.model.level.Value == 4),
                GameManager.Instance.cities.Count(x => x.model.level.Value == 5),
                GameManager.Instance.cities.Count(x => x.model.level.Value == 6));

            AudioManager.Instance.PlaySound("sfx_level");

            txtLv.text = UserDataManager.Instance.data.lv.Value.ToString();

            LevelData levelData = Datas.levelData.ToArray()
                .FirstOrDefault(x => x.level == UserDataManager.Instance.data.lv.Value);

            imgRewards[0].transform.parent.parent.gameObject.SetActive(levelData.reward_gold > 0);
            txtRewardCount[0].text = Utilities.GetNumberKKK(levelData.reward_gold);
            imgRewards[0].sprite = GameManager.Instance.GetRewardImage(RewardData.eType.gold);

            imgRewards[1].transform.parent.parent.gameObject.SetActive(levelData.reward_cash > 0);
            txtRewardCount[1].text = Utilities.GetNumberKKK(levelData.reward_cash);
            imgRewards[1].sprite = GameManager.Instance.GetRewardImage(RewardData.eType.cash);

            UserDataManager.Instance.data.levelUpReward.Value = false;

            particle.gameObject.SetActive(true);
            particle.Stop();
            particle.Clear();
            particle.Play();
        }

        public override void BackKey()
        {
            btnClaim.onClick.Invoke();
        }

//        void CheckNewArea()
//        {
//            if (lastGuide > UserDataManager.Instance.data.lv.Value)
//            {
//                lastGuide = UserDataManager.Instance.data.lv.Value;
//            }
//
//            int openAreaLv = UserDataManager.Instance.GetOpenAreaLV(UserDataManager.Instance.data.cloudOpen.Value + 1);
//
//            if (openAreaLv > 0 && UserDataManager.Instance.data.lv.Value >= openAreaLv)
//            {
//                UserDataManager.Instance.data.cloudOpen.Value++;
//                MissionManager.Instance.AddValue(QuestData.eType.map_open, 1);
//                UserDataManager.Instance.SaveData();
//            }
//
//            lastGuide = UserDataManager.Instance.data.lv.Value;
//        }

        void CheckUnlockContents(Action onClear)
        {
            /*
                         *  1: 딜리버리(D-1 리텐션) 이벤트
                            2: 데일리 미션
                            3: 레벨 미션
                            4: 시즌 이벤트(현재 unlock값 미사용)
                            5: 리뷰 팝업
                            6: 상점 트럭 추가
                         */
            LevelData levelData = Datas.levelData.ToArray()
                .FirstOrDefault(x => x.level == UserDataManager.Instance.data.lv.Value);

            if (levelData != null && unLockArrIndex < levelData.unlock.Length)
            {
                switch (levelData.unlock[unLockArrIndex])
                {
                    case LevelData.eUnlock.none:
                        onClear.Invoke();
                        break;
                    case LevelData.eUnlock.review:
                        Popup_Guide_Review.Instance.Show(onClear.Invoke);
                        break;
                    default:
                        Popup_ContentsUnlock.Instance.Show(levelData.unlock[unLockArrIndex], () =>
                        {
                            unLockArrIndex++;
                            CheckUnlockContents(onClear);
                        });
                        break;
                }
            }
            else
            {
                onClear.Invoke();
            }
        }
    }
}