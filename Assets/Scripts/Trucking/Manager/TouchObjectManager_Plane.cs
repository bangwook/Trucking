using System;
using DatasTypes;
using Trucking.Model;
using Trucking.Common;
using Trucking.UI.Mission;
using UniRx;
using Random = UnityEngine.Random;

namespace Trucking.Manager
{
    public class TouchObjectManager_Plane : Singleton<TouchObjectManager_Plane>
    {
        public TouchObject_PlaneModel model;

        public ReactiveProperty<bool> isShow = new ReactiveProperty<bool>();

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private CompositeDisposable _disposableIntaval = new CompositeDisposable();
        private RewardModel rewardModel;
        private CompositeDisposable _disposableNextTime = new CompositeDisposable();
        private bool isFirst = true;

        public void SetModel(TouchObject_PlaneModel _model)
        {
            model = _model;

            if (model == null)
            {
                model = new TouchObject_PlaneModel();
                UserDataManager.Instance.data.planeObjectData.Value = model;
            }

            _compositeDisposable.Clear();

            Observable.CombineLatest(
                    isShow,
                    UserDataManager.Instance.data.hasTutorial,
                    (show, isTutorial) =>
                    {
                        if (!isTutorial && !show)
                        {
                            return true;
                        }

                        return false;
                    })
                .Subscribe(value =>
                {
                    if (value)
                    {
                        _disposableIntaval.Clear();

                        if (isFirst)
                        {
                            rewardModel = MakeRewardModel();
                            isShow.Value = true;
                            isFirst = false;
                        }
                        else
                        {
                            int intavalSec = Random.Range(Datas.touchObjectData.plane.delay_min,
                                Datas.touchObjectData.plane.delay_max);

                            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(intavalSec)).Subscribe(_ =>
                            {
                                if (!isShow.Value)
                                {
                                    rewardModel = MakeRewardModel();
                                    isShow.Value = true;
                                }
                            }).AddTo(_disposableIntaval);
                        }
                    }
                }).AddTo(_compositeDisposable);

            if (model.nextTime.Value <= DateTime.Now)
            {
                Reset();
            }
            else
            {
                SetNextTime();
            }
        }

        public void Reset()
        {
            model.cashCount.Value = 0;

            SetNextTime();
        }

        public void AddCashLimit(bool isDouble)
        {
            if (rewardModel.type.Value == RewardData.eType.cash)
            {
                if (isDouble)
                {
                    model.cashCount.Value += (int) rewardModel.count.Value;
                }

                model.cashCount.Value += (int) rewardModel.count.Value;
            }
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

            if (LunarConsoleVariables.isPlaneCash)
            {
                SetNextTime_Test();
            }

            UnirxExtension.DateTimer(model.nextTime.Value).Subscribe(value => { Reset(); })
                .AddTo(_disposableNextTime);
        }

        void SetNextTime_Test()
        {
            model.nextTime.Value = DateTime.Now.AddMinutes(1);
        }

        public RewardModel GetReward()
        {
            return rewardModel;
        }

        public RewardModel MakeRewardModel()
        {
            int total = 0;
            int resultIndex = 0;


            for (int i = 0; i < Datas.touchObjectData.plane.reward_rate.Length; i++)
            {
                if (Datas.touchObjectData.plane.reward_limit[i] < 0
                    || model.cashCount.Value < Datas.touchObjectData.plane.reward_limit[i])
                {
                    total += Datas.touchObjectData.plane.reward_rate[i];
                }
            }

            int random = Random.Range(0, total);
            int rate = 0;

            //test 
            if (LunarConsoleVariables.isPlaneCash)
            {
                random = 99;
            }

            for (int i = 0; i < Datas.touchObjectData.plane.reward_rate.Length; i++)
            {
                if (Datas.touchObjectData.plane.reward_limit[i] < 0
                    || model.cashCount.Value < Datas.touchObjectData.plane.reward_limit[i])
                {
                    rate += Datas.touchObjectData.plane.reward_rate[i];

                    if (random < rate)
                    {
                        resultIndex = i;
                        break;
                    }
                }
            }

            RewardModel rewardModel = new RewardModel(Datas.touchObjectData.plane.reward_type[resultIndex].type,
                MissionManager.Instance.CalcLevelMagReward(
                    Datas.touchObjectData.plane.reward_type[resultIndex].type,
                    Datas.touchObjectData.plane.reward_count[resultIndex] * 0.2f));

            return rewardModel;
        }
    }

    public class TouchObject_PlaneModel
    {
        public ReactiveProperty<int> cashCount = new ReactiveProperty<int>(0);
        public ReactiveProperty<DateTime> nextTime = new ReactiveProperty<DateTime>(DateTime.MinValue);
    }
}