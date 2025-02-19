/*
 * Created on 2019년 3월 13일 수요일
 * Creator jhlee5@cookapps.com
 * Copyright (c) 2019 CookAppsPlay
 */

using System.Collections.Generic;
using Facebook.Unity;
using UniRx;
using UnityEngine;

namespace Trucking.FBAnalytics
{
    public static class FBAnalytics
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void RuntimeInit()
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                FB.Init(OnInit);
            }

            Observable.EveryApplicationPause()
                .Subscribe(pauseStatus =>
                {
                    if (!pauseStatus)
                    {
                        if (FB.IsInitialized)
                        {
                            FB.ActivateApp();
                        }
                        else
                        {
                            FB.Init(OnInit);
                        }
                    }
                });

            void OnInit()
            {
                FB.ActivateApp();
            }
        }

        /// <summary>
        /// 결제 로그
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="packagename"></param>
        /// <param name="level"></param>
        /// <param name="pre_purchase"></param>
        public static void LogPurchase(float amount, string currency, string packagename, int level, bool pre_purchase)
        {
            var parameters = new Dictionary<string, object>();
            parameters["packagename"] = packagename;
            parameters["level"] = level;
            parameters["pre_purchase"] = pre_purchase ? 1 : 0;
            FB.LogPurchase(amount, currency, parameters);
        }

        /// <summary>
        /// TutorialEvent
        /// </summary>
        /// <param name="step"></param>
        /// <param name="complete"></param>
        public static void LogTutorialEvent(int step, bool complete)
        {
            var parameters = new Dictionary<string, object>();
            parameters["step"] = step;
            parameters["complete"] = complete ? 1 : 0;
            FB.LogAppEvent("Tutorial", null, parameters);
        }

        public static void LogLevelUpEvent(int level,
            int route_count,
            int truck_count,
            int cargo_count,
            int road_count,
            //int map_number,
            int city_count_all,
            int city_count_lv1,
            int city_count_lv2 = 0,
            int city_count_lv3 = 0,
            int city_count_lv4 = 0,
            int city_count_lv5 = 0,
            int city_count_lv6 = 0,
            int city_count_lv7 = 0)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["route_count"] = route_count;
            parameters["truck_count"] = truck_count;
            parameters["cargo_count"] = cargo_count;
            parameters["road_count"] = road_count;
            //parameters["map_number"] = map_number;
            parameters["city_count_all"] = city_count_all;
            parameters["city_count_lv1"] = city_count_lv1;
            parameters["city_count_lv2"] = city_count_lv2;
            parameters["city_count_lv3"] = city_count_lv3;
            parameters["city_count_lv4"] = city_count_lv4;
            parameters["city_count_lv5"] = city_count_lv5;
            parameters["city_count_lv6"] = city_count_lv6;
            parameters["city_count_lv7"] = city_count_lv7;

            FB.LogAppEvent("LevelUp", null, parameters);
        }

        public static void LogCreateRouteEvent(int level, int route_count, int truck_count)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["route_count"] = route_count;
            parameters["truck_count"] = truck_count;
            FB.LogAppEvent("CreateRoute", null, parameters);
        }

        public static void LogTruckBoostEvent(int level, int truck_count, string method)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["truck_count"] = truck_count;
            parameters["method"] = method;
            FB.LogAppEvent("TruckBoost", null, parameters);
        }

        public static void LogJobListRefAdEvent(int level, string city, int result)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["city"] = city;
            parameters["result"] = result;
            FB.LogAppEvent("JobListRefAd", null, parameters);
        }

        public static void LogLuckyBoxEvent(int level, bool pre_purchase)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["pre_purchase"] = pre_purchase ? 1 : 0;
            FB.LogAppEvent("LuckyBox", null, parameters);
        }

        public static void LogEventPackEvent(int level, bool pre_purchase, string pack_type)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["pre_purchase"] = pre_purchase ? 1 : 0;
            parameters["pack_type"] = pack_type;

            FB.LogAppEvent("EventPack", null, parameters);
        }

        public static void LogUpgradeCityEvent(int level, string city, int city_level, string purchaseType)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["city"] = city;
            parameters["city_level"] = city_level;
            parameters["purchaseType"] = purchaseType;

            FB.LogAppEvent("UpgradeCity", null, parameters);
        }

        public static void LogUpgradeTruckEvent(int level, int truck_id, int truck_level, string purchaseType)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["truck_id"] = truck_id;
            parameters["truck_level"] = truck_level;
            parameters["purchaseType"] = purchaseType;

            FB.LogAppEvent("UpgradeTruck", null, parameters);
        }

        public static void LogBuyTruckEvent(int level, int truck_id, int truck_count, int route_count)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["truck_id"] = truck_id;
            parameters["truck_count"] = truck_count;
            parameters["route_count"] = route_count;

            FB.LogAppEvent("BuyTruck", null, parameters);
        }


        public static void LogWatchAdEvent(int level, string location)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["location"] = location;

            FB.LogAppEvent("WatchAd", null, parameters);
        }

        public static void LogSendTruckEvent(int level, string city_from, string city_to, int job_count,
            long reward_coin, long reward_cash, long reward_exp, int truck_id)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["city_from"] = city_from;
            parameters["city_to"] = city_to;
            parameters["job_count"] = job_count;
            parameters["reward_coin"] = reward_coin;
            parameters["reward_cash"] = reward_cash;
//        parameters["reward_exp"] = reward_exp;
            parameters["truck_id"] = truck_id;

            FB.LogAppEvent("SendTruck", null, parameters);
        }

        public static void LogArriveTruckEvent(int level, string city, int job_count, long reward_coin,
            long reward_cash, long reward_exp, int truck_id)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["city"] = city;
            parameters["job_count"] = job_count;
            parameters["reward_coin"] = reward_coin;
            parameters["reward_cash"] = reward_cash;
//        parameters["reward_exp"] = reward_exp;
            parameters["truck_id"] = truck_id;

            FB.LogAppEvent("ArriveTruck", null, parameters);
        }

        public static void LogDeliveryRewardEvent(int level, string city, int job_count, long reward_coin,
            long reward_cash, long reward_exp, long reward_material_1, long reward_material_2, long reward_material_3,
            long reward_material_4)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["city"] = city;
            parameters["job_count"] = job_count;
            parameters["reward_coin"] = reward_coin;
            parameters["reward_cash"] = reward_cash;
//        parameters["reward_exp"] = reward_exp;

            parameters["reward_material_1"] = reward_material_1;
            parameters["reward_material_2"] = reward_material_2;
            parameters["reward_material_3"] = reward_material_3;
            parameters["reward_material_4"] = reward_material_4;

            FB.LogAppEvent("DeliveryReward", null, parameters);
        }

        public static void LogFlyObljectEvent(string reward)
        {
            var parameters = new Dictionary<string, object>();
            parameters["reward"] = reward;

            FB.LogAppEvent("FlyOblject", null, parameters);
        }

        public static void LogTutorialSkipEvent(int step)
        {
            var parameters = new Dictionary<string, object>();
            parameters["step"] = step;

            FB.LogAppEvent("TutorialSkip", null, parameters);
        }

        public static void LogFreeCashButtonEvent(int level)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;

            FB.LogAppEvent("FreeCashButton", null, parameters);
        }

        public static void LogNPDSEvent(int level, int step)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["step"] = step;

            FB.LogAppEvent("NPDS", null, parameters);
        }

        public static void LogNPDSButtonEvent(int level)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;

            FB.LogAppEvent("NPDSButton", null, parameters);
        }

        public static void LogTruckJobEvent(string location)
        {
            var parameters = new Dictionary<string, object>();
            parameters["location"] = location;

            FB.LogAppEvent("TruckJob", null, parameters);
        }

        public static void LogDailyRewardsButtonEvent(int level)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;

            FB.LogAppEvent("DeliveryReward", null, parameters);
        }

        public static void LogDailyRewardsEvent(int level, int step)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["step"] = step;

            FB.LogAppEvent("DailyRewards", null, parameters);
        }

        public static void LogClickObjectEvent(int level, string location, string reward)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["location"] = level;
            parameters["reward"] = reward;

            FB.LogAppEvent("ClickObject", null, parameters);
        }

        public static void LogPushNotificationEvent(int level, string type)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["type"] = type;

            FB.LogAppEvent("PushNotification", null, parameters);
        }

        public static void LogGuideQuestEvent(int level, int step)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["step"] = step;

            FB.LogAppEvent("GuideQuest", null, parameters);
        }

        public static void LogOpenCrateEvent(int level, long cash, int normal_crate, int special_crate,
            int normal_count, int special_count)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["cash"] = cash;
            parameters["normal_crate"] = normal_crate;
            parameters["special_crate"] = special_crate;
            parameters["normal_count"] = normal_count;
            parameters["special_count"] = special_count;

            FB.LogAppEvent("OpenCrate", null, parameters);
        }

        public static void LogPieceCollectEvent(int level, int truck_id)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["truck_id"] = truck_id;

            FB.LogAppEvent("PieceCollect", null, parameters);
        }

        public static void LogPartsCraftEvent(int level, int shoplevel, string city)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["shoplevel"] = shoplevel;
            parameters["city"] = city;

            FB.LogAppEvent("PartsCraft", null, parameters);
        }

        public static void LogHintClickEvent(string type)
        {
            var parameters = new Dictionary<string, object>();
            parameters["type"] = type;

            FB.LogAppEvent("HintClick", null, parameters);
        }

        public static void LogCityClickEvent(int level, string city)
        {
            var parameters = new Dictionary<string, object>();
            parameters["level"] = level;
            parameters["city"] = city;

            FB.LogAppEvent("CityClick", null, parameters);
        }
    }
}