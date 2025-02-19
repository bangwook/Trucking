using System;
using DatasTypes;
using Trucking.Common;
using Trucking.Manager;
using Trucking.Model;
using UniRx;

namespace Trucking.UI.Mission
{
    public class MissionManager : Singleton<MissionManager>
    {
//        public float GetLevelMagCount()
//        {
//            for (int i = 0; i < Datas.levelMag.Length; i++)
//            {
//                if (UserDataManager.Instance.data.lv.Value >= Datas.levelMag[i].lv)
//                {
//                    return Datas.levelMag[i].count;
//                }
//            }
//
//            return Datas.levelMag.ToArray().Last().count;
//        }

        public float GetLevelMagCount(QuestData.eType qid, int lv = 0)
        {
            LevelMag lvMag = UserDataManager.Instance.GetLevelMag(lv);

            float countMag = 1;

            switch (QuestManager.GetCountMagType(qid))
            {
                case 1:
                    countMag = lvMag.count_1;
                    break;
                case 2:
                    countMag = lvMag.count_2;
                    break;
                case 3:
                    countMag = lvMag.count_3;
                    break;
                case 4:
                    countMag = lvMag.count_4;
                    break;
            }

            return countMag;
        }


        public long CalcLevelMagReward(RewardData.eType type, float count, int lv = 0)
        {
            LevelMag levelMag = UserDataManager.Instance.GetLevelMag(lv);

            if (type == RewardData.eType.random_box)
            {
                return (long) Math.Ceiling(count * levelMag.reward_box);
            }

            if (type == RewardData.eType.cash)
            {
                return (long) Math.Ceiling(count * levelMag.reward_cash);
            }

            if (type == RewardData.eType.exp)
            {
                return (long) Math.Ceiling(count * levelMag.reward_xp);
            }

            if (type == RewardData.eType.gold)
            {
                return (long) Math.Ceiling(count * levelMag.reward_gold);
            }


            return 1;
        }


        public void AddValue(QuestData.eType eType, long value)
        {
            foreach (var missionModel in LevelMissionManager.Instance.model.models)
            {
                foreach (var quest in missionModel.listQuestModel)
                {
                    quest.AddValue(eType, value);
                }
            }

            foreach (var achievementCellModel in AchievementManager.Instance.model.model)
            {
                QuestModel quest = achievementCellModel.quest.Value;
                quest.AddValue(eType, value);
            }

            GuideQuestManager.Instance.model.questModel.Value?.AddValue(eType, value);
        }

        public void SetValue(QuestData.eType eType, long value)
        {
            foreach (var missionModel in LevelMissionManager.Instance.model.models)
            {
                foreach (var quest in missionModel.listQuestModel)
                {
                    quest.SetValue(eType, value);
                }
            }

            foreach (var achievementCellModel in AchievementManager.Instance.model.model)
            {
                QuestModel quest = achievementCellModel.quest.Value;
                quest.SetValue(eType, value);
            }

            GuideQuestManager.Instance.model.questModel.Value?.SetValue(eType, value);
        }

        public void AddValue(QuestData.eType eType, RewardData.eType type, long value)
        {
            foreach (var missionModel in LevelMissionManager.Instance.model.models)
            {
                foreach (var quest in missionModel.listQuestModel)
                {
                    quest.AddValue(eType, type, value);
                }
            }

//            if (ConceptMissionManager.Instance.model.hasMission.Value)
//            {
//                foreach (var quest in ConceptMissionManager.Instance.model.model.Value.listQuestModel)
//                {
//                    quest.AddValue(eType, type, value);
//                }
//            }

            foreach (var achievementCellModel in AchievementManager.Instance.model.model)
            {
                QuestModel quest = achievementCellModel.quest.Value;
                quest.AddValue(eType, type, value);
            }

            NewDailyMissionManager.Instance.AddValue(eType, value);
            GuideQuestManager.Instance.model.questModel.Value?.AddValue(eType, type, value);
        }

        public void AddValue(QuestData.eType eType, Cargo cargo, long value)
        {
            foreach (var missionModel in LevelMissionManager.Instance.model.models)
            {
                foreach (var quest in missionModel.listQuestModel)
                {
                    quest.AddValue(eType, cargo.model.id.Value, value);
                }
            }

//            if (ConceptMissionManager.Instance.model.hasMission.Value)
//            {
//                foreach (var quest in ConceptMissionManager.Instance.model.model.Value.listQuestModel)
//                {
//                    quest.AddValue(eType, cargo.model.id.Value, value);
//                }
//
//            }

            foreach (var achievementCellModel in AchievementManager.Instance.model.model)
            {
                QuestModel quest = achievementCellModel.quest.Value;
                quest.AddValue(eType, cargo.model.id.Value, value);
            }

            GuideQuestManager.Instance.model.questModel.Value?.AddValue(eType, cargo.model.id.Value, value);
        }

        public void AddValue(QuestData.eType eType, City city, long value)
        {
            foreach (var missionModel in LevelMissionManager.Instance.model.models)
            {
                foreach (var quest in missionModel.listQuestModel)
                {
                    quest.AddValue(eType, city, value);
                }
            }

//            if (ConceptMissionManager.Instance.model.hasMission.Value)
//            {
//                foreach (var quest in ConceptMissionManager.Instance.model.model.Value.listQuestModel)
//                {
//                    if (ConceptMissionManager.Instance.city.Value == city)
//                    {
//                        quest.AddValue(eType, city, value);
//                    }
//                }
//            }

            GuideQuestManager.Instance.model.questModel.Value?.AddValue(eType, city, value);
        }

        public void AddValue(QuestData.eType eType, City city, RewardData.eType type, long value)
        {
//            if (ConceptMissionManager.Instance.model.hasMission.Value)
//            {
//                foreach (var quest in ConceptMissionManager.Instance.model.model.Value.listQuestModel)
//                {
//                    if (ConceptMissionManager.Instance.city.Value == city)
//                    {
//                        quest.AddValue(eType, type, value);
//                    }
//                }
//            }
        }

//        
        public void AddValue(QuestData.eType eType, City city, Cargo cargo, long value)
        {
//            if (ConceptMissionManager.Instance.model.hasMission.Value)
//            {
//                foreach (var quest in ConceptMissionManager.Instance.model.model.Value.listQuestModel)
//                {
//                    if (ConceptMissionManager.Instance.city.Value == city)
//                    {
//                        quest.AddValue(eType, cargo.model.id.Value, value);
//                    }
//                }
//            }

            if (NewDailyMissionManager.Instance.model.hasMission.Value
                && NewDailyMissionManager.Instance.city.Value == city)
            {
                QuestModel questModel = NewDailyMissionManager.Instance.model.questModel.Value;
                questModel.AddValue(eType, cargo.model.id.Value, value);
            }
        }
    }

    public class MissionBaseModel
    {
        public ReactiveCollection<QuestModel> listQuestModel = new ReactiveCollection<QuestModel>();
        public ReactiveProperty<int> mid = new IntReactiveProperty();


        public bool IsSuccess()
        {
            foreach (var quest in listQuestModel)
            {
                if (quest.count.Value < quest.max.Value)
                {
                    return false;
                }
            }

            return true;
        }

        public void SetSuccess()
        {
            foreach (var quest in listQuestModel)
            {
                quest.count.Value = quest.max.Value;
            }
        }
    }
}