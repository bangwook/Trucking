using System;
using System.Linq;
using DatasTypes;
using Newtonsoft.Json.Utilities;
using Trucking.Common;
using Trucking.Manager;
using Trucking.Model;
using UniRx;

namespace Trucking.UI.Mission
{
    public class OperationManager : Singleton<OperationManager>
    {
        public OperationManagerModel model;

        private CompositeDisposable disposable = new CompositeDisposable();

        public void SetModel(OperationManagerModel _model)
        {
            model = _model;

            if (model == null)
            {
                model = new OperationManagerModel();
                UserDataManager.Instance.data.operationData.Value = model;
            }

            model.crate_delay.Subscribe(delay =>
            {
                if (delay)
                {
                    UnirxExtension.DateTimer(model.endTime.Value)
                        .Subscribe(_ =>
                        {
                            model.crate_delay.Value = false;
                            model.rewardIndex.Value = 0;
                        })
                        .AddTo(disposable);
                }
            }).AddTo(disposable);

            LevelData levelData = Datas.levelData.ToArray()
                .FirstOrDefault(x => x.unlock.ToList().FindIndex(y => y == LevelData.eUnlock.operation) >= 0);


            if (levelData != null)
            {
                int startLevel = levelData.level;

                UserDataManager.Instance.data.lv.Subscribe(lv =>
                {
                    if (lv >= startLevel)
                    {
                        if (!model.startEvent.Value)
                        {
                            model.startEvent.Value = true;
                        }
                    }
                }).AddTo(disposable);
            }
        }

        public void SetNext()
        {
            if (model.endTime.Value < DateTime.Now)
            {
                model.rewardIndex.Value++;
            }

            UserDataManager.Instance.SaveData();
        }

        public void CollectBonus()
        {
            if (model.rewardIndex.Value >= 3)
            {
                model.endTime.Value = DateTime.Now.Add(GetTime());

                if (LunarConsoleVariables.isDeliveryHack)
                {
                    model.endTime.Value = DateTime.Now.AddSeconds(10);
                }

                model.crate_delay.Value = true;

                UserDataManager.Instance.SaveData();
            }
        }

        public RewardModel GetReward()
        {
            RewardModel rewardModel = new RewardModel(RewardData.eType.booster, 30, "speed");

            return rewardModel;
        }

        public RewardModel GetBonusReward()
        {
            RewardModel rewardModel = new RewardModel(RewardData.eType.crate, 1, 1);

            return rewardModel;
        }

        public TimeSpan GetTime()
        {
            return TimeSpan.FromMinutes(1440);
        }
    }

    public class OperationManagerModel
    {
        public ReactiveProperty<int> rewardIndex = new IntReactiveProperty();
        public ReactiveProperty<DateTime> endTime = new ReactiveProperty<DateTime>(DateTime.MinValue);
        public ReactiveProperty<bool> crate_delay = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> startEvent = new ReactiveProperty<bool>();
    }
}