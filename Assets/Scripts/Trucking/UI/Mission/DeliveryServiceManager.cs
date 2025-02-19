using System;
using System.Linq;
using DatasTypes;
using Trucking.Common;
using Trucking.Manager;
using Trucking.Model;
using UniRx;

namespace Trucking.UI.Mission
{
    public class DeliveryServiceManager : Singleton<DeliveryServiceManager>
    {
        public DeliveryServiceModel model;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private CompositeDisposable _disposableDelivery = new CompositeDisposable();


        public void SetModel(DeliveryServiceModel _model)
        {
            model = _model;

            if (model == null)
            {
                model = new DeliveryServiceModel();
                UserDataManager.Instance.data.deliveryData.Value = model;
            }

            Observable.CombineLatest(model.isMove,
                    model.hasEvent,
                    (move, hasEvent) =>
                    {
                        if (!hasEvent && model.targetIndex.Value == 4 && model.rewardIndex.Value >= 5)
                        {
                            return true;
                        }

                        return move;
                    })
                .Subscribe(value =>
                {
                    if (value)
                    {
                        UnirxExtension.DateTimer(model.endTime.Value).Subscribe(time =>
                        {
                            if (model.isMove.Value)
                            {
                                model.isMove.Value = false;
                                model.hasReward.Value = true;
                            }
                            else if (!model.hasEvent.Value)
                            {
                                model.hasReward.Value = true;
                                model.targetIndex.Value = 0;
                                model.rewardIndex.Value = 0;
                                model.hasEvent.Value = true;
                            }

                            _disposableDelivery.Clear();
                        }).AddTo(_disposableDelivery);
                    }
                }).AddTo(_compositeDisposable);

            LevelData levelData = Datas.levelData.ToArray()
                .FirstOrDefault(x => x.unlock.ToList().FindIndex(y => y == LevelData.eUnlock.delivery) >= 0);

            if (levelData != null)
            {
                int startLevel = levelData.level;

                UserDataManager.Instance.data.lv.Subscribe(lv =>
                {
                    if (lv >= startLevel)
                    {
                        if (!model.startEvent.Value)
                        {
                            model.hasEvent.Value = true;
                            model.startEvent.Value = true;
                        }
                    }
                }).AddTo(_compositeDisposable);
            }
        }

        public void SetNext()
        {
            model.rewardIndex.Value++;
            model.hasReward.Value = false;
            model.startTime.Value = DateTime.Now;
            model.endTime.Value = DateTime.Now.Add(GetTime());


            if (model.targetIndex.Value < 4)
            {
                if (LunarConsoleVariables.isDeliveryHack)
                {
                    model.endTime.Value = DateTime.Now.AddSeconds(2);
                }

                model.targetIndex.Value++;
                model.isMove.Value = true;
            }
            else
            {
                if (LunarConsoleVariables.isDeliveryHack)
                {
                    model.endTime.Value = DateTime.Now.AddSeconds(10);
                }

                model.hasEvent.Value = false;
            }

            UserDataManager.Instance.SaveData();
        }

        public RewardModel GetReward(int index)
        {
            RewardModel rewardModel = new RewardModel(
                Datas.days5EventData[0].type[index].type,
                Datas.days5EventData[0].count[index],
                Datas.days5EventData[0].type_index[index]);

            return rewardModel;
        }

        public TimeSpan GetTime()
        {
            return TimeSpan.FromMinutes(Datas.days5EventData[0].delay_min);
        }

        public string GetCityName(int index)
        {
            if (index >= 0
                && index < Datas.days5EventData[0].city_id.Length)
            {
                return Datas.cityData.ToArray().FirstOrDefault(x => x.id == Datas.days5EventData[0].city_id[index])
                    .name;
            }

            return null;
        }
    }

    public class DeliveryServiceModel
    {
        public ReactiveProperty<int> targetIndex = new IntReactiveProperty();
        public ReactiveProperty<DateTime> startTime = new ReactiveProperty<DateTime>(DateTime.MinValue);
        public ReactiveProperty<DateTime> endTime = new ReactiveProperty<DateTime>(DateTime.MinValue);
        public ReactiveProperty<int> rewardIndex = new IntReactiveProperty(-1);
        public ReactiveProperty<bool> isMove = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> hasReward = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> hasEvent = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> startEvent = new ReactiveProperty<bool>(false);
    }
}