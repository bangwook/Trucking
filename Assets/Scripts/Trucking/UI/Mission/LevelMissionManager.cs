using System;
using System.Collections.Generic;
using System.Linq;
using DatasTypes;
using Newtonsoft.Json.Utilities;
using Trucking.Common;
using Trucking.Model;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Trucking.UI.Mission
{
    public class LevelMissionManager : Singleton<LevelMissionManager>
    {
        public LevelMissionModel model;

        private List<LevelMissionData> randomMissions = new List<LevelMissionData>();
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private CompositeDisposable _compositeDisposableMission = new CompositeDisposable();
        private int totalRate;

        public LevelMissionData GetRandomData()
        {
            if (randomMissions.Count == 0)
            {
                int index = 0;
                int counter = 0;

                totalRate = Datas.levelMissionData.ToArray().Sum(x => x.rate);

                for (int i = 0; i < totalRate; i++)
                {
                    if (Datas.levelMissionData[index].rate > 0)
                    {
                        randomMissions.Add(Datas.levelMissionData[index]);

                        counter++;

                        if (counter >= Datas.levelMissionData[index].rate)
                        {
                            counter = 0;
                            index++;
                        }
                    }
                    else
                    {
                        counter = 0;
                        index++;
                        i--;
                    }
                }
            }

            return randomMissions[Random.Range(0, totalRate)];
        }


        void Subscribe()
        {
            _compositeDisposableMission.Clear();

            model.hasMission.Subscribe(has =>
            {
                if (!has && model.isSuccess.Value)
                {
                    model.isSuccess.Value = false;
                    model.models.Clear();
                    model.startTime.Value = DateTime.Now.AddMinutes(GetDelayTime_min());
                    SetNextMission(DateTime.Now.AddMinutes(GetDelayTime_min()));
                }
            }).AddTo(_compositeDisposable);

            foreach (var model in model.models)
            {
                foreach (var quest in model.listQuestModel)
                {
                    quest.count.Subscribe(_ => { this.model.isSuccess.Value = this.model.IsSuccess(); })
                        .AddTo(_compositeDisposableMission);
                }
            }
        }

        public void SetModel(LevelMissionModel _mission)
        {
            model = _mission;

            if (model == null)
            {
                model = new LevelMissionModel();
                UserDataManager.Instance.data.levelMissionData.Value = model;
            }

            Subscribe();

            LevelData levelData = Datas.levelData.ToArray()
                .FirstOrDefault(x => x.unlock.ToList().FindIndex(y => y == LevelData.eUnlock.level) >= 0);

            if (levelData != null)
            {
                int startLevel = levelData.level;

                UserDataManager.Instance.data.lv.Subscribe(lv =>
                {
                    if (model != null
                        && !model.hasMission.Value
                        && LevelCheck()
                        && lv >= startLevel)
                    {
                        SetNextMission(model.startTime.Value);
                    }
                }).AddTo(_compositeDisposable);
            }
        }

        public void Create()
        {
            Debug.Log($"LevelMissionManager Create");
            List<MissionBaseModel> baseModels =
                new List<MissionBaseModel>();

            if (model.isFirst.Value)
            {
                for (int i = 0; i < 3; i++)
                {
                    LevelMissionData lvMissionData = Datas.levelMissionData.ToArray()
                        .FirstOrDefault(x => x.id == Datas.levelMission[0].start_id[i]);
                    MissionBaseModel levelMissionBaseModel =
                        new MissionBaseModel();

                    List<QuestModel> listQuest = QuestModel.Make(lvMissionData);
                    levelMissionBaseModel.listQuestModel.Clear();
                    levelMissionBaseModel.listQuestModel.AddRange(listQuest);
                    levelMissionBaseModel.mid.Value = lvMissionData.id;

                    baseModels.Add(levelMissionBaseModel);
                }

                model.isFirst.Value = false;
                model.rewardIndex.Value =
                    RewardModel.GetIndex(RewardData.eType.random_box, Datas.levelMission[0].start_reward);
            }
            else
            {
                List<LevelMissionData> containList = new List<LevelMissionData>();

                for (int i = 0; i < 3; i++)
                {
                    LevelMissionData lvMissionData;

                    do
                    {
                        lvMissionData = GetRandomData();
                    } while (containList.Contains(lvMissionData));

                    containList.Add(lvMissionData);

                    MissionBaseModel levelMissionBaseModel =
                        new MissionBaseModel();

                    List<QuestModel> listQuest = QuestModel.Make(lvMissionData);
                    levelMissionBaseModel.listQuestModel.Clear();
                    levelMissionBaseModel.listQuestModel.AddRange(listQuest);
                    levelMissionBaseModel.mid.Value = lvMissionData.id;

                    baseModels.Add(levelMissionBaseModel);
                }

                model.rewardIndex.Value = RewardModel.GetIndex(RewardData.eType.random_box, GetRewardId());
            }

            model.isSuccess.Value = false;
            model.hasMission.Value = true;
            model.SetModels(baseModels);

            Subscribe();
        }

        void SetNextMission(DateTime time)
        {
            Debug.Log($"LevelMissionManager SetNextMission : {time}");

            if (time.Millisecond < 0)
            {
                Create();
            }
            else
            {
                UnirxExtension.DateTimer(time).Subscribe(_ => { Create(); })
                    .AddTo(_compositeDisposableMission);
            }
        }

        int GetDelayTime_min()
        {
            foreach (LevelMission data in Datas.levelMission)
            {
                if (UserDataManager.Instance.data.lv.Value >= data.lv)
                {
                    return data.delay_time;
                }
            }

            return Datas.levelMission.ToArray().Last().delay_time;
        }

        bool LevelCheck()
        {
            foreach (LevelMission data in Datas.levelMission)
            {
                if (UserDataManager.Instance.data.lv.Value >= data.lv)
                {
                    return true;
                }
            }

            return false;
        }


        string GetRewardId()
        {
            int count = Datas.levelMission[0].reward_rate.Length;
            int random100 = Random.Range(0, 100);
            int rate = 0;

            for (int i = 0; i < count; i++)
            {
                rate += Datas.levelMission[0].reward_rate[i];

                if (random100 < rate)
                {
                    return Datas.levelMission[0].reward_id[i];
                }
            }

            return Datas.levelMission[0].reward_id[0];
        }
    }


    public class LevelMissionModel
    {
        public ReactiveCollection<MissionBaseModel> models = new ReactiveCollection<MissionBaseModel>();
        public ReactiveProperty<bool> hasMission = new BoolReactiveProperty();
        public ReactiveProperty<DateTime> startTime = new ReactiveProperty<DateTime>();
        public ReactiveProperty<bool> isSuccess = new BoolReactiveProperty();
        public ReactiveProperty<int> rewardIndex = new ReactiveProperty<int>();
        public ReactiveProperty<bool> isFirst = new BoolReactiveProperty(true);
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void SetModels(List<MissionBaseModel> _models)
        {
            _compositeDisposable.Clear();
            models.Clear();

            for (int i = 0; i < 3; i++)
            {
                models.Add(_models[i]);
            }
        }


        public bool IsSuccess()
        {
            foreach (var model in models)
            {
                if (!model.IsSuccess())
                {
                    return false;
                }
            }

            return true;
        }
    }
}