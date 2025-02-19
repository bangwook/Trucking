//using System;
//using System.Collections.Generic;
//using System.Linq;
//using DatasTypes;
//using Trucking.Common;
//using UniRx;
//using UnityEngine;
//
//namespace Trucking.Manager
//{
//    public class EventTruckManager : Singleton<EventTruckManager>
//    {
//        public EventTruckShopModel model;
//
//        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
//
//        public EventTruckManager()
//        {
//        }
//
//        public void SetModel(EventTruckShopModel _model)
//        {
//            model = _model;
//            
//            if (model == null)
//            {
//                model = new EventTruckShopModel();
//                
//                for (int i = 0; i < Datas.eventPackData.Length; i++)
//                {
//                    model.count.Add(0);
//                }
//                
//                UserDataManager.Instance.data.eventTruckShopData.Value = model;
//            }
//            
//            if (model.count.Count < Datas.eventPackData.Length)
//            {
//                for (int i = 0; i < Datas.eventPackData.Length - model.count.Count; i++)
//                {
//                    model.count.Add(0);
//                }
//            }
//
////            // test
////            model.hasEvent.Value = true;
////            model.endTime.Value = DateTime.Now.AddHours(-2);
////            model.nextShowTime.Value = DateTime.Now.AddDays(4);
//
//            if (model.endTime.Value > DateTime.Now)
//            {
//                Subscribe();
//            }
//            else
//            {
//                CheckEvent();
//            }
//        }
//
//        void Subscribe()
//        {
//            _compositeDisposable.Clear();
//
//            if (model.endTime.Value > DateTime.Now && model.hasEvent.Value)
//            {
//                UnirxExtension.DateTimer(model.endTime.Value).Subscribe(_ =>
//                {
//                    model.hasEvent.Value = false;
//                }).AddTo(_compositeDisposable);
//            }
//
//            if (model.nextShowTime.Value > DateTime.Now)
//            {
//                UnirxExtension.DateTimer(model.nextShowTime.Value).Subscribe(_ =>
//                {
//                    CheckEvent();
//                }).AddTo(_compositeDisposable);
//            }
//        }
//
//        public EventPackData FindData()
//        {
//            List<EventPackData> eventPackDataList = new List<EventPackData>();
//            
//            // 1회 구매 제한
//            for (int i = 0; i < Datas.eventPackData.Length; i++)
//            {
//                if (model.count[i] < 1)
//                {
//                    eventPackDataList.Add(Datas.eventPackData[i]);
//                }
//            }
//            // 유저 레벨 구간
//            eventPackDataList = Datas.eventPackData.ToArray()
//                .Where(x => x.start_lv <= UserDataManager.Instance.data.lv.Value
//                                       && x.end_level >= UserDataManager.Instance.data.lv.Value
//                                       && UserDataManager.Instance.data.truckData.FirstOrDefault(tr => tr.dataID == x.truck_id) == null)
//                .ToList();
//           
//            if (eventPackDataList.Count == 1)
//            {
//                EventPackData selectEventPackData = eventPackDataList[0];
//
//                return selectEventPackData;
//            }
//            
//            if (eventPackDataList.Count > 1)
//            {
//                int prevEventIndex = model.eventIndex.Value;
//                int randIndex;
//                
//                do
//                {
//                    randIndex = UnityEngine.Random.Range(0, eventPackDataList.Count);
//                } while (prevEventIndex == randIndex);
//                
//                return eventPackDataList[randIndex];
//            }
//
//            return null;
//        }
//
//        public void CheckEvent()
//        {
//            EventPackData findEventPackData = FindData();
//
//            if (findEventPackData != null)
//            {
//                // start
//                Debug.Log("EventTruck Start!");
//                model.eventIndex.Value = Array.IndexOf(Datas.eventPackData.ToArray(), findEventPackData);
//                model.endTime.Value = DateTime.Now.AddHours(48);
//                model.nextShowTime.Value = DateTime.Now.AddHours(UnityEngine.Random.Range(120, 168));
//                model.hasEvent.Value = true;
//                Subscribe();
//            }
//            else
//            {
//                model.hasEvent.Value = false;
//            } 
//        }
//    }
//    
//    public class EventTruckShopModel
//    {
//        public ReactiveProperty<DateTime> endTime = new ReactiveProperty<DateTime>(DateTime.MinValue);
//        public ReactiveCollection<int> count = new ReactiveCollection<int>();
//        public ReactiveProperty<int> eventIndex = new ReactiveProperty<int>(-1);
//        public ReactiveProperty<DateTime> nextShowTime = new ReactiveProperty<DateTime>(DateTime.MinValue);
//        public ReactiveProperty<bool> hasEvent = new ReactiveProperty<bool>();
//    }
//
//}

