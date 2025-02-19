//using System;
//using System.Collections.Generic;
//using DatasTypes;
//using Trucking.Common;
//using Trucking.Model;
//using UniRx;
//using Random = UnityEngine.Random;
//
//namespace Trucking.Manager
//{
//    public class LuckyBoxManager : Singleton<LuckyBoxManager>
//    {
//        public ReactiveProperty<bool> hasEvent = new BoolReactiveProperty();
//        public ReactiveProperty<LuckyBoxData> data = new ReactiveProperty<LuckyBoxData>();
//                
//        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
//        private CompositeDisposable _disposableTimer = new CompositeDisposable();
//
//        public LuckyBoxManager()
//        {
//            _compositeDisposable.Clear();
//            
//            data.Subscribe(_data =>
//            {
//                hasEvent.Value = _data != null;
//            }).AddTo(_compositeDisposable);
//
//            Init();
//        }
//
//        public void Init()
//        {
//            _disposableTimer.Clear();
//            
//            LuckyBoxData luckyBoxData = FindEvent();
//            
//            if (luckyBoxData == null)
//            {
//                _disposableTimer.Clear();
//
//                LuckyBoxData nearData = FindNearEvent();
//                data.Value = nearData;
//
//                UnirxExtension.DateTimer(data.Value.start_date).Subscribe(_ =>
//                {
//                    Init();
//                }).AddTo(_disposableTimer);
//            }
//            else
//            {
//                data.Value = luckyBoxData;
//                
//                UnirxExtension.DateTimer(data.Value.end_date).Subscribe(_ =>
//                {
//                    Init();
//                }).AddTo(_disposableTimer);
//
//            }
//            
//            
//        }
//
//        LuckyBoxData MakeData(LuckyBoxData luckyBoxData)
//        {   
//            DateTime start = luckyBoxData.start_date;
//            DateTime end = luckyBoxData.end_date;
//
//            DateTime startTime = new DateTime(DateTime.Now.Year, start.Month, start.Day, start.Hour, start.Minute, start.Second);
//            DateTime endTime = new DateTime(DateTime.Now.Year, end.Month, end.Day, end.Hour, end.Minute, end.Second);
//                
//            if (endTime < DateTime.Now)
//            {
//                startTime = new DateTime(DateTime.Now.Year + 1, start.Month, start.Day, start.Hour, start.Minute, start.Second);
//                endTime = new DateTime(DateTime.Now.Year + 1, end.Month, end.Day, end.Hour, end.Minute, end.Second);
//            }
//
//            if (startTime > endTime)
//            {
//                endTime = new DateTime(endTime.Year + 1, end.Month, end.Day, end.Hour, end.Minute, end.Second);
//            }
//            
//            LuckyBoxData newData = new LuckyBoxData(
//                luckyBoxData.index,
//                startTime, 
//                endTime,
//                luckyBoxData.price_cash,
//                luckyBoxData.reward_type,
//                luckyBoxData.reward_index,
//                luckyBoxData.reward_count,
//                luckyBoxData.reward_rate);
//
//            return newData;
//        }
//
//        LuckyBoxData FindEvent()
//        {
//            foreach (LuckyBoxData luckyBoxData in Datas.luckyBoxData)
//            {
//                LuckyBoxData newData = MakeData(luckyBoxData);
//                                
//                if (newData.start_date < DateTime.Now && DateTime.Now < newData.end_date)
//                {
//                    return newData;
//                }
//            }
//
//            return null;
//        }
//        
//        LuckyBoxData FindNearEvent()
//        {
//            TimeSpan timeSpan = TimeSpan.MaxValue;
//            LuckyBoxData nearData = Datas.luckyBoxData[0];
//            
//            foreach (LuckyBoxData luckyBoxData in Datas.luckyBoxData)
//            {
//                LuckyBoxData newData = MakeData(luckyBoxData);
//
//                if (newData.start_date - DateTime.Now < timeSpan)
//                {
//                    nearData = newData;
//                    timeSpan = newData.start_date - DateTime.Now;
//                }
//            }
//
//            return nearData;
//        }
//
//        public RewardModel GetRewardModel()
//        {
//            int total = 0;
//            int resultIndex = 0;
//            
//            for (int i = 0; i < data.Value.reward_rate.Length; i++)
//            {
//                total += data.Value.reward_rate[i];
//            }
//            
//            int random = Random.Range(0, total);
//            int rate = 0;
//            
//            for (int i = 0; i < data.Value.reward_rate.Length; i++)
//            {
//                rate += data.Value.reward_rate[i];
//                
//                if (random < rate)
//                {
//                    resultIndex = i;
//                    break;
//                }
//            }
//            
//            RewardModel rewardModel = new RewardModel(data.Value.reward_type[resultIndex].type,
//                data.Value.reward_count[resultIndex]);
//
//            return rewardModel;
//        }
//        
//        public List<RewardModel> GetRewardModels()
//        {
//            int total = 0;
//            int resultIndex = 0;
//            
//            for (int i = 0; i < data.Value.reward_rate.Length; i++)
//            {
//                total += data.Value.reward_rate[i];
//            }
//
//            List<RewardModel> rewardModels = new List<RewardModel>();
//            List<int> indexList = new List<int>();
//
//            for (int j = 0; j < 3; j++)
//            {
//                int random = Random.Range(0, total);
//                int rate = 0;
//                
//                for (int i = 0; i < data.Value.reward_rate.Length; i++)
//                {
//                    if (indexList.Contains(i))
//                    {
//                        continue;
//                    }
//                    
//                    rate += data.Value.reward_rate[i];
//                
//                    if (random < rate)
//                    {
//                        resultIndex = i;
//                        break;
//                    }
//                }
//            
//                indexList.Add(resultIndex);
//                total -= data.Value.reward_rate[resultIndex];
//                RewardModel rewardModel = new RewardModel(data.Value.reward_type[resultIndex].type,
//                    data.Value.reward_count[resultIndex], data.Value.reward_index[resultIndex]);
//                rewardModels.Add(rewardModel);
//            }
//            
//            return rewardModels;
//        }
//
//        
//
//
//    }
//}

