using System.Linq;
using DatasTypes;
using Trucking.Common;
using UniRx;

namespace Trucking.Manager
{
    public class FreeCashManager : Singleton<FreeCashManager>
    {
        public FreeCashModel model;

        private CompositeDisposable disposable = new CompositeDisposable();
//        private CompositeDisposable _disposableNextTime = new CompositeDisposable();
//        private ReactiveProperty<long> obsNextMissionTime = new ReactiveProperty<long>();

        public void SetModel(FreeCashModel _model)
        {
            model = _model;

            if (model == null)
            {
                model = new FreeCashModel();
                UserDataManager.Instance.data.freeCashData.Value = model;
            }


            LevelData levelData = Datas.levelData.ToArray()
                .FirstOrDefault(x => x.unlock.ToList().FindIndex(y => y == LevelData.eUnlock.freeCash) >= 0);

            if (levelData != null)
            {
                int startLevel = levelData.level;

                UserDataManager.Instance.data.lv.Subscribe(lv =>
                {
                    if (lv >= startLevel)
                    {
                        if (!model.hasStart.Value)
                        {
                            model.hasStart.Value = true;
                        }
                    }
                }).AddTo(disposable);
            }


//            _compositeDisposable.Clear();
//
//            if (model.nextTime.Value <= DateTime.Now)
//            {
//                Reset();    
//            }
//            else
//            {
//                SetNextTime();
//            }
//                       
//            Observable.CombineLatest(model.hasMission, 
//                    UserDataManager.Instance.data.hasTutorial,
//                    obsNextMissionTime,
//                    (mission, isTutorial, timer) =>
//                    {
//                        if (isTutorial)
//                        {
//                            return false;
//                        }
//
//                        if (model.nextTime.Value <= DateTime.Now)
//                        {
//                            return true;
//                        }
//                        
//                        return false;
//                    })
//                .Subscribe(value =>
//                {
//                    if (value)
//                    {
//                        Reset();
//                    }
//                }).AddTo(_compositeDisposable);
//
//            model.step.Subscribe(step =>
//            {
//                if (step >= Datas.freeCashData[0].ad_reward_count.Length)
//                {
//                    model.hasMission.Value = false;
//                }
//                else
//                {
//                    model.delayTime.Value = DateTime.Now.AddMinutes(Datas.freeCashData[0].ad_delay[model.step.Value]);
//                    
//                    if (LunarConsoleVariables.isFreeCashFast && step > 0)
//                    {
//                        model.delayTime.Value = DateTime.Now.AddSeconds(2);    
//                    }
//                    
//                }
//            }).AddTo(_compositeDisposable);
        }

//        public void Reset()
//        {
//            Debug.Log($"FreeCashManager Create");
//
//            model.hasMission.Value = true;
//            model.step.Value = 0;
//            
//            SetNextTime();
//            
//            if (LunarConsoleVariables.isFreeCashFast)
//            {
//                SetNextTime_Test();
//            }
//        }
//
//        void SetNextTime()
//        {
//            _disposableNextTime.Clear();
//            
//            DateTime nextMissionTime =
//                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 0, 0);
//
//            if (DateTime.Now > nextMissionTime)
//            {
//                nextMissionTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 0, 0)
//                    .AddDays(1);
//            }
//
//            model.nextTime.Value = nextMissionTime;
//                
//            UnirxExtension.DateTimer(model.nextTime.Value).Subscribe(value => obsNextMissionTime.Value = value)
//                .AddTo(_disposableNextTime);
//        }
//        
//        void SetNextTime_Test()
//        {
//            _disposableNextTime.Clear();
//            
//            model.nextTime.Value = DateTime.Now.AddMinutes(1);
//                
//            UnirxExtension.DateTimer(model.nextTime.Value).Subscribe(value => obsNextMissionTime.Value = value)
//                .AddTo(_disposableNextTime);
//        }
//
//
//        public RewardModel GetReward()
//        {
//            long count;
//
//            if (model.step.Value >= Datas.freeCashData[0].ad_reward_count.Length)
//            {
//                count = Datas.freeCashData[0].ad_reward_count[0];
//            }
//            else
//            {
//                count = Datas.freeCashData[0].ad_reward_count[model.step.Value];
//            }
//            
//            RewardModel rewardModel = new RewardModel(RewardData.eType.cash, count);
//            
//            return rewardModel;
//        }
//        
//        public RewardModel GetFinalReward()
//        {
//            RewardModel rewardModel = new RewardModel(RewardData.eType.cash, Datas.freeCashData[0].event_reward_count);
//            
//            return rewardModel;
//        }
//
//        public bool SetNext()
//        {
//            model.step.Value++;
//            
//            if (model.step.Value < Datas.freeCashData[0].ad_reward_count.Length)
//            {
//                return false;
//            }
//
//            return true;
//        }
//
//        public bool CanNext()
//        {
//            return model.hasMission.Value
//                   && model.delayTime.Value < DateTime.Now;
//        }
//
//        public DateTime GetNextTime()
//        {
//            if (model.hasMission.Value)
//            {
//                return model.delayTime.Value;
//            }
//            
//            return model.nextTime.Value;
//        }
    }

    public class FreeCashModel
    {
        public ReactiveProperty<bool> hasStart = new ReactiveProperty<bool>();
//        public ReactiveProperty<int> step = new ReactiveProperty<int>();
//        public ReactiveProperty<bool> hasMission = new ReactiveProperty<bool>();
//        public ReactiveProperty<DateTime> nextTime = new ReactiveProperty<DateTime>(DateTime.MinValue);
//        public ReactiveProperty<DateTime> delayTime = new ReactiveProperty<DateTime>(DateTime.MinValue);
    }
}