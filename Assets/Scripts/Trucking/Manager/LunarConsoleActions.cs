using System.Linq;
using DatasTypes;
using I2.Loc;
using LunarConsolePlugin;
using Trucking.Ad;
using Trucking.Common;
using Trucking.UI;
using Trucking.UI.Mission;
using UnityEngine;

namespace Trucking.Manager
{
    public class LunarConsoleActions : MonoBehaviour
    {
        private void Start()
        {
            LunarConsole.RegisterAction("Level Mission Clear", LevelMissionClear);
            LunarConsole.RegisterAction("New Daily Mission Clear", NewDailyMissionClear);
            LunarConsole.RegisterAction("Achevement Clear", AchevementClear);
            LunarConsole.RegisterAction("Guide Quest Clear", GuideQuestClear);
            LunarConsole.RegisterAction("Guide Quest All Clear", GuideQuestAllClear);
            LunarConsole.RegisterAction("Add 100000 Cash", AddCash);
            LunarConsole.RegisterAction("Add 10000000 Gold", AddGold);
            LunarConsole.RegisterAction("Add Exp", AddExp);
            LunarConsole.RegisterAction("Add Material", AddMaterial);
            LunarConsole.RegisterAction("Add Parts", AddParts);
            LunarConsole.RegisterAction("Add Crate", AddCrate);
            LunarConsole.RegisterAction("Add Piece", AddPiece);
            LunarConsole.RegisterAction("Refresh Cargo", RefreshCargo);
            LunarConsole.RegisterAction("Remove All Resource", RemoveAllResource);
            LunarConsole.RegisterAction("Change Language", ChangeLanguage);

            LunarConsoleVariables.userLv.AddDelegate((value) =>
            {
                int lv = value;

                if (lv < 1)
                {
                    lv = 1;
                }
                else if (lv > Datas.levelData.ToArray().Last().level)
                {
                    lv = Datas.levelData.ToArray().Last().level;
                }

                UserDataManager.Instance.data.lv.Value = lv;
            });

            LunarConsoleVariables.isNewDailyMsiion.AddDelegate((value) =>
            {
                if (value)
                {
                    NewDailyMissionManager.Instance.MakeMission();
                }
                else
                {
                    NewDailyMissionManager.Instance.model.questModel.Value.count.Value =
                        NewDailyMissionManager.Instance.model.questModel.Value.max.Value;
                }
            });

            LunarConsoleVariables.isLevelMission.AddDelegate((value) =>
            {
                if (value)
                {
                    LevelMissionManager.Instance.Create();
                }
                else
                {
                    LevelMissionManager.Instance.model.hasMission.Value = false;
                }
            });


//            LunarConsoleVariables.isEventPack.AddDelegate((value) =>
//            {
//                if (value)
//                {
//                    if (UserDataManager.Instance.data.lv.Value < 3)
//                    {
//                        UserDataManager.Instance.data.lv.Value = 3;
//                    }
//
//                    EventTruckManager.Instance.CheckEvent();
//                }
//                else
//                {
//                    EventTruckManager.Instance.model.hasEvent.Value = false;
//                }
//            });

            LunarConsoleVariables.isADLoading.AddDelegate(
                value => { AdManager.Instance.IsLoadedReward.Value = !value; });

            LunarConsoleVariables.isTree.AddDelegate(
                value => { WorldMap.Instance.trsFoliage.gameObject.SetActive(value); });

            LunarConsoleVariables.isPlaneCash.AddDelegate(
                value => { UserDataManager.Instance.data.planeObjectData.Value.cashCount.Value = 0; });
        }

        void OnDestroy()
        {
            LunarConsole.UnregisterAllActions(this); // don't forget to unregister!
            LunarConsoleVariables.userLv.RemoveDelegates(this);
        }

        void LevelMissionClear()
        {
            AudioManager.Instance.PlaySound("sfx_button_main");

            foreach (var missionModel in LevelMissionManager.Instance.model.models)
            {
                foreach (var quest in missionModel.listQuestModel)
                {
                    quest.AddValue(quest.qid.Value, quest.max.Value);
                    quest.AddValue(quest.qid.Value, (RewardData.eType) quest.fid.Value, quest.max.Value);
                    quest.AddValue(quest.qid.Value, quest.fid.Value, quest.max.Value);
                    quest.AddValue(quest.qid.Value, GameManager.Instance.cities.Find(x => x.data.id == quest.fid.Value),
                        quest.max.Value);
                }
            }
        }

        void NewDailyMissionClear()
        {
            AudioManager.Instance.PlaySound("sfx_button_main");

            QuestModel quest = NewDailyMissionManager.Instance.model.questModel.Value;
            quest.AddValue(quest.qid.Value, quest.fid.Value, quest.max.Value);


//            QuestModel questModel = NewDailyMissionManager.Instance.model.questModel.Value;                
//            questModel.AddValue(eType, cargo.model.id.Value, value);
        }

        void GuideQuestClear()
        {
            AudioManager.Instance.PlaySound("sfx_button_main");
            QuestModel quest = GuideQuestManager.Instance.model.questModel.Value;
            quest?.AddValue(quest.qid.Value, quest.max.Value);
        }

        void GuideQuestAllClear()
        {
            AudioManager.Instance.PlaySound("sfx_button_main");
            GuideQuestManager.Instance.model.index.Value = Datas.guideQuestData.Length;
            QuestModel quest = GuideQuestManager.Instance.model.questModel.Value;
            quest?.AddValue(quest.qid.Value, quest.max.Value);
        }

        void AchevementClear()
        {
            AudioManager.Instance.PlaySound("sfx_button_main");

            foreach (var achievementCellModel in AchievementManager.Instance.model.model)
            {
                QuestModel quest = achievementCellModel.quest.Value;
                quest.AddValue(quest.qid.Value, quest.max.Value);
                quest.AddValue(quest.qid.Value, (RewardData.eType) quest.fid.Value, quest.max.Value);
            }


//            QuestModel quest = AchievementManager.Instance.model.model[1].quest.Value;
//            quest.AddValue(quest.qid.Value, quest.max.Value);
//            quest.AddValue(quest.qid.Value, (RewardData.eType)quest.fid.Value, quest.max.Value);
        }

        void AddCash()
        {
            AudioManager.Instance.PlaySound("sfx_button_main");
            UserDataManager.Instance.AddCash(100000);
        }

        void AddGold()
        {
            AudioManager.Instance.PlaySound("sfx_button_main");
            UserDataManager.Instance.AddGold(10000000);
        }


        void AddExp()
        {
            AudioManager.Instance.PlaySound("sfx_button_main");
            long max = UserDataManager.Instance.GetNextExp();

            UserDataManager.Instance.AddExp(max / 2);
        }

        void RefreshCargo()
        {
            AudioManager.Instance.PlaySound("sfx_button_main");

            if (GameManager.Instance.fsm.GetCurrentState() == GameManager.Instance.jobState)
            {
                JobView.Instance.city.Value.RefreshNotContractCargo();
            }
        }

        void AddMaterial()
        {
            for (int i = 0; i < UserDataManager.Instance.data.cityMaterials.Count; i++)
            {
                UserDataManager.Instance.data.cityMaterials[i].Value += 100;
            }
        }

        void AddParts()
        {
            for (int i = 0; i < UserDataManager.Instance.data.truckParts.Count; i++)
            {
                UserDataManager.Instance.data.truckParts[i].Value += 100;
            }
        }

        void AddCrate()
        {
            for (int i = 0; i < UserDataManager.Instance.data.crate.Count; i++)
            {
                UserDataManager.Instance.data.crate[i].Value += 100;
            }
        }

        void AddPiece()
        {
//            for (int i = 0; i < UserDataManager.Instance.data.truckPieces.Count; i++)
//            {
//                UserDataManager.Instance.data.truckPieces[i].Value += 5;
//            }

            int[] arr = {1, 3, 4, 7, 10};

            for (int i = 0; i < arr.Length; i++)
            {
                UserDataManager.Instance.data.truckPieces[arr[i]].Value += 5;
            }
        }

        void RemoveAllResource()
        {
            UserDataManager.Instance.data.gold.Value = 0;
            UserDataManager.Instance.data.cash.Value = 0;

            foreach (var crate in UserDataManager.Instance.data.crate)
            {
                crate.Value = 0;
            }

            foreach (var cityMaterial in UserDataManager.Instance.data.cityMaterials)
            {
                cityMaterial.Value = 0;
            }

            foreach (var truckPart in UserDataManager.Instance.data.truckParts)
            {
                truckPart.Value = 0;
            }

            foreach (var truckPiece in UserDataManager.Instance.data.truckPieces)
            {
                truckPiece.Value = 0;
            }
        }

        public void ChangeLanguage()
        {
            if (LocalizationManager.CurrentLanguage == "English")
            {
                LocalizationManager.CurrentLanguage = "Korean";
            }
            else if (LocalizationManager.HasLanguage("Korean"))
            {
                LocalizationManager.CurrentLanguage = "English";
            }
        }
    }
}