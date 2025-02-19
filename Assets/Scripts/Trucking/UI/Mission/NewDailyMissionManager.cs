using System;
using System.Collections.Generic;
using System.Linq;
using DatasTypes;
using Trucking.Common;
using Trucking.Manager;
using Trucking.Model;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Trucking.UI.Mission
{
    public class NewDailyMissionManager : Singleton<NewDailyMissionManager>
    {
        public NewDailyMissionModel model;
        public ReactiveProperty<bool> isNew = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> hasReward = new ReactiveProperty<bool>();
        public ReactiveProperty<City> city = new ReactiveProperty<City>();

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private CompositeDisposable _disposableNextTime = new CompositeDisposable();
        private CompositeDisposable _disposableQuest = new CompositeDisposable();

        ReactiveProperty<long> obsNextMissionTime = new ReactiveProperty<long>();

        public void SetModel(NewDailyMissionModel _model)
        {
            model = _model;

            if (model == null)
            {
                model = new NewDailyMissionModel();
                UserDataManager.Instance.data.newDailyMissionData.Value = model;
            }

            _compositeDisposable.Clear();

            LevelData levelData = Datas.levelData.ToArray()
                .FirstOrDefault(x => x.unlock.ToList().FindIndex(y => y == LevelData.eUnlock.daily) >= 0);

            if (levelData != null)
            {
                int startLevel = levelData.level;

                if (UserDataManager.Instance.data.lv.Value >= startLevel)
                {
                    if (!model.questModel.Value.IsSuccess())
                    {
                        if (model.nextTime.Value <= DateTime.Now)
                        {
                            MakeMission();
                        }
                        else
                        {
                            SetNextTime();
                        }
                    }
                }

                model.city_id.Subscribe(city_id =>
                {
                    city.Value = GameManager.Instance.cities.Find(x => x.data.id == city_id);
                }).AddTo(_compositeDisposable);

                model.questModel.Subscribe(quest =>
                {
                    _disposableQuest.Clear();
                    if (quest != null)
                    {
                        quest?.ObsChanged.Subscribe(q => { hasReward.Value = q.IsSuccess(); }).AddTo(_disposableQuest);
                    }
                    else
                    {
                        hasReward.Value = false;
                    }
                }).AddTo(_compositeDisposable);


                Observable.CombineLatest(UserDataManager.Instance.data.lv,
                        hasReward,
                        obsNextMissionTime,
                        (userLv, reward, timer) =>
                        {
                            if (userLv < startLevel || reward)
                            {
                                return false;
                            }

                            if (model.nextTime.Value <= DateTime.Now)
                            {
                                return true;
                            }

                            return false;
                        })
                    .Subscribe(value =>
                    {
                        if (value)
                        {
                            MakeMission();
                        }
                    }).AddTo(_compositeDisposable);
            }
        }

        public void MakeMission()
        {
            Debug.Log($"NewDailyMissionManager Create");

            City _city = GetEventCity();

            if (_city == null)
            {
                SetClear();
            }
            else
            {
                int random = Random.RandomRange(0, Datas.newDailyMission.Length);
                QuestModel quest = QuestModel.Make(Datas.newDailyMission[random]);
                model.SetQuestModel(quest);
                model.hasMission.Value = true;
                model.data_id.Value = random;
                model.city_id.Value = GetEventCity().data.id;
                isNew.Value = true;
            }

            SetNextTime();
        }

        void SetNextTime()
        {
            _disposableNextTime.Clear();

            DateTime nextTime =
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 0, 0);

            if (DateTime.Now.Hour > 7)
            {
                nextTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 0, 0).AddDays(1);
            }

            model.nextTime.Value = nextTime;

            UnirxExtension.DateTimer(model.nextTime.Value).Subscribe(value => obsNextMissionTime.Value = value)
                .AddTo(_disposableNextTime);

            if (LunarConsoleVariables.isNewDailyMsiion)
            {
                SetNextTime_Test();
            }
        }

        void SetNextTime_Test()
        {
            _disposableNextTime.Clear();

            model.nextTime.Value = DateTime.Now.AddMinutes(1);

            UnirxExtension.DateTimer(model.nextTime.Value).Subscribe(value => obsNextMissionTime.Value = value)
                .AddTo(_disposableNextTime);
        }

        public RewardModel GetReward()
        {
            NewDailyMission misson = Datas.newDailyMission[model.data_id.Value];
            RewardModel rewardModel = new RewardModel(misson.reward_type.type, 1, misson.reward_id);
            return rewardModel;
        }

        City GetEventCity()
        {
            City eventCity = null;

            List<City> cities = GameManager.Instance.cities.FindAll(x => x.IsOpen() && !x.IsMega());

            if (cities.Count > 0)
            {
                eventCity = cities[Random.RandomRange(0, cities.Count)];
            }

            return eventCity;
        }

        public int GetCargoId()
        {
            return model.questModel.Value.fid.Value;
        }

        public void SetClear()
        {
            model.city_id.Value = 0;
            model.hasMission.Value = false;
            model.questModel.Value = null;
            UserDataManager.Instance.SaveData();
        }

        public void AddValue(QuestData.eType eType, long value)
        {
            QuestModel quest = model.questModel.Value;

            if (quest != null
                && quest.qid.Value == eType
                && quest.fid.Value == (int) FactorTypeData.eType.none
                && quest.count.Value < quest.max.Value)
            {
                quest.AddValue(eType, value);
            }
        }

        public bool IsMissionCargo(Cargo cargo)
        {
            if (model.hasMission.Value)
            {
                if (!model.questModel.Value.IsSuccess()
                    && model.questModel.Value.IsMissionCargo(cargo, city.Value))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class NewDailyMissionModel
    {
        public ReactiveProperty<QuestModel> questModel = new ReactiveProperty<QuestModel>();
        public ReactiveProperty<bool> hasMission = new ReactiveProperty<bool>();
        public ReactiveProperty<int> city_id = new ReactiveProperty<int>();
        public ReactiveProperty<DateTime> nextTime = new ReactiveProperty<DateTime>(DateTime.MinValue);
        public ReactiveProperty<int> data_id = new ReactiveProperty<int>();

        public void SetQuestModel(QuestModel _model)
        {
            questModel.Value = _model;
        }
    }
}