using System;
using System.Linq;
using Trucking.Ad;
using Trucking.Common;
using Trucking.Model;
using Trucking.Notifications;
using Trucking.UI.Mission;
using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;

#elif UNITY_IOS
using Unity.Notifications.iOS;
#endif

namespace Trucking.Manager
{
    public class NotificationsManager : MonoSingleton<NotificationsManager>
    {
        private const string ChannelId = "Pocket Truck";
        private const string GroupId = "Message";

        public void Init()
        {
            if (GameNotificationsManager.Instance.Initialized)
            {
                return;
            }

            var channel1 = new GameNotificationChannel(ChannelId, GroupId, "Generic notifications");
            GameNotificationsManager.Instance.Initialize(channel1);
            GameNotificationsManager.Instance.CancelAllNotifications();
            GameNotificationsManager.Instance.DismissAllNotifications();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        public void CheckGetLastNotification()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            var notificationIntentData = AndroidNotificationCenter.GetLastNotificationIntent();

            if (notificationIntentData != null)
            {
                int id = notificationIntentData.Id;

                LogAppEvent(id);
            }
#elif UNITY_IOS && !UNITY_EDITOR
            var notificationIntentData = iOSNotificationCenter.GetLastNotification();
    
            if (notificationIntentData != null)
            {
                int id = Int32.Parse(notificationIntentData.Identifier);
                LogAppEvent(id);
            }
#elif UNITY_EDITOR
            FBAnalytics.FBAnalytics.LogPushNotificationEvent(UserDataManager.Instance.data.lv.Value, "Editor Test");
#endif
        }

        void LogAppEvent(int id)
        {
            if (id >= 100 && id < 200)
            {
                FBAnalytics.FBAnalytics.LogPushNotificationEvent(UserDataManager.Instance.data.lv.Value,
                    "Each JobComplete");
            }
            else if (id >= 200 && id < 300)
            {
                FBAnalytics.FBAnalytics.LogPushNotificationEvent(UserDataManager.Instance.data.lv.Value,
                    "Each Full Fuel");
            }
            else if (id == 300)
            {
                FBAnalytics.FBAnalytics.LogPushNotificationEvent(UserDataManager.Instance.data.lv.Value,
                    "All JobComlete");
            }
            else if (id == 400)
            {
                FBAnalytics.FBAnalytics.LogPushNotificationEvent(UserDataManager.Instance.data.lv.Value,
                    "All Full Fuel");
            }
            else if (id == 500)
            {
                FBAnalytics.FBAnalytics.LogPushNotificationEvent(UserDataManager.Instance.data.lv.Value, "New Event");
            }
            else if (id == 600)
            {
                FBAnalytics.FBAnalytics.LogPushNotificationEvent(UserDataManager.Instance.data.lv.Value,
                    "Event Delivery");
            }
            else
            {
                FBAnalytics.FBAnalytics.LogPushNotificationEvent(UserDataManager.Instance.data.lv.Value,
                    "Unknown Notification id : " + id);
            }
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (!GameNotificationsManager.Instance.Initialized)
            {
                Init();
                return;
            }

            GameNotificationsManager.Instance.CancelAllNotifications();
            GameNotificationsManager.Instance.DismissAllNotifications();

            if (pauseStatus && !AdManager.Instance.IsShowing)
            {
                SetNotifications();
            }
        }

        public void OnApplicationFocus(bool hasFocus)
        {
            if (!GameNotificationsManager.Instance.Initialized)
            {
                Init();
                return;
            }

            GameNotificationsManager.Instance.CancelAllNotifications();
            GameNotificationsManager.Instance.DismissAllNotifications();

            if (!hasFocus && !AdManager.Instance.IsShowing)
            {
                SetNotifications();
            }
        }

        void SetNotifications()
        {
            float allArriveTime = 0;
            double allFuelTime = 0;

            //int id, string title, string message, double afterSec
            for (int i = 0; i < GameManager.Instance.trucks.Count; i++)
            {
                Truck truck = GameManager.Instance.trucks[i];

                if (truck.model.state.Value == TruckModel.State.Move)
                {
//                    TimeSpan leftTime =
//                        TimeSpan.FromSeconds(truck.GetTransitTime() - truck.GetDeltaTime());

                    float leftTime = truck.GetTransitTime() - truck.GetDeltaTime();

                    if (leftTime > allArriveTime)
                    {
                        allArriveTime = leftTime;
                    }

                    if (UserDataManager.Instance.data.notiEachTruckArrive.Value
                        && leftTime > 0)
                    {
                        TimeSpan timeSpan = TimeSpan.FromSeconds(leftTime);
                        SendNotification(100 + i, Utilities.GetStringByData(20911),
                            string.Format(Utilities.GetStringByData(20921), truck.name),
                            DateTime.Now.Add(timeSpan),
                            ChannelId);
                    }

                    double fuelTime = leftTime + truck.GetMaxRefuelTime().TotalSeconds;

                    if (allFuelTime < fuelTime)
                    {
                        allFuelTime = fuelTime;
                    }

                    if (UserDataManager.Instance.data.notiEachRefuelingComplete.Value
                        && fuelTime > 0)
                    {
                        TimeSpan timeSpan = TimeSpan.FromSeconds(fuelTime);
                        SendNotification(200 + i, Utilities.GetStringByData(20912),
                            string.Format(Utilities.GetStringByData(20923), truck.name),
                            DateTime.Now.Add(timeSpan),
                            ChannelId);
                    }
                }
                else
                {
                    double fuelTime = truck.GetMaxRefuelTime().TotalSeconds;

                    if (allFuelTime < fuelTime)
                    {
                        allFuelTime = fuelTime;
                    }

                    if (UserDataManager.Instance.data.notiEachRefuelingComplete.Value
                        && fuelTime > 0)
                    {
                        TimeSpan timeSpan = TimeSpan.FromSeconds(fuelTime);
                        SendNotification(200 + i, Utilities.GetStringByData(20912),
                            string.Format(Utilities.GetStringByData(20923), truck.name),
                            DateTime.Now.Add(timeSpan),
                            ChannelId);
                    }
                }
            }

            if (UserDataManager.Instance.data.notiAllTruckArrive.Value && allArriveTime > 0)
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(allArriveTime);
                SendNotification(300, Utilities.GetStringByData(20911),
                    Utilities.GetStringByData(20922),
                    DateTime.Now.Add(timeSpan),
                    ChannelId);
            }

            if (UserDataManager.Instance.data.notiAllRefuelingComplete.Value && allFuelTime > 0)
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(allFuelTime);

                SendNotification(400, Utilities.GetStringByData(20912),
                    Utilities.GetStringByData(20924),
                    DateTime.Now.Add(timeSpan),
                    ChannelId);
            }

            DateTime allCraftTime = DateTime.MinValue;
            DateTime allUpgradeTime = DateTime.MinValue;

            for (int i = 0; i < Datas.partsProduction.Length; i++)
            {
                CityModel model =
                    UserDataManager.Instance.data.cityData.First(x => x.id.Value == Datas.partsProduction[i].city);

                if (model.state.Value == CityModel.State.Craft)
                {
                    if (allCraftTime < model.productTime.Value)
                    {
                        allCraftTime = model.productTime.Value;
                    }
                }
                else if (model.state.Value == CityModel.State.Upgrade)
                {
                    if (allUpgradeTime < model.productTime.Value)
                    {
                        allUpgradeTime = model.productTime.Value;
                    }
                }
            }

            if (UserDataManager.Instance.data.notiPartProduction.Value
                && allCraftTime > DateTime.MinValue)
            {
                SendNotification(500, Utilities.GetStringByData(20928),
                    Utilities.GetStringByData(20929),
                    allCraftTime,
                    ChannelId);
            }

            if (UserDataManager.Instance.data.notiProductionUpgrade.Value
                && allUpgradeTime > DateTime.MinValue)
            {
                SendNotification(600, Utilities.GetStringByData(20930),
                    Utilities.GetStringByData(20931),
                    allUpgradeTime,
                    ChannelId);
            }

            if (UserDataManager.Instance.data.notiEventDelivery.Value
                && DeliveryServiceManager.Instance.model != null
                && DeliveryServiceManager.Instance.model.isMove.Value)
            {
                SendNotification(700, Utilities.GetStringByData(20926),
                    Utilities.GetStringByData(20927),
                    DeliveryServiceManager.Instance.model.endTime.Value,
                    ChannelId);
            }
        }

        /// <summary>
        /// Queue a notification with the given parameters.
        /// </summary>
        /// <param name="title">The title for the notification.</param>
        /// <param name="body">The body text for the notification.</param>
        /// <param name="deliveryTime">The time to deliver the notification.</param>
        /// <param name="badgeNumber">The optional badge number to display on the application icon.</param>
        /// <param name="reschedule">
        /// Whether to reschedule the notification if foregrounding and the notification hasn't yet been shown.
        /// </param>
        /// <param name="channelId">Channel ID to use. If this is null/empty then it will use the default ID. For Android
        /// the channel must be registered in <see cref="GameNotificationsManager.Initialize"/>.</param>
        /// <param name="smallIcon">Notification small icon.</param>
        /// <param name="largeIcon">Notification large icon.</param>
        public void SendNotification(int id, string title, string body, DateTime deliveryTime, string channelId = null,
            int? badgeNumber = null,
            bool reschedule = false,
            string smallIcon = "icon_0", string largeIcon = "icon_1")
        {
            Debug.Log(
                $"SendNotification : {deliveryTime - DateTime.Now}, {deliveryTime}, {title}, {body}, {deliveryTime}, {channelId}");

            IGameNotification notification = GameNotificationsManager.Instance.CreateNotification();

            if (notification == null)
            {
                return;
            }

            notification.Id = id;
            notification.Title = title;
            notification.Body = body;
            notification.Group = !string.IsNullOrEmpty(channelId) ? channelId : ChannelId;
            notification.DeliveryTime = deliveryTime;
            notification.SmallIcon = smallIcon;
            notification.LargeIcon = largeIcon;

            if (badgeNumber != null)
            {
                notification.BadgeNumber = badgeNumber;
            }

            PendingNotification notificationToDisplay =
                GameNotificationsManager.Instance.ScheduleNotification(notification);
            notificationToDisplay.Reschedule = reschedule;
        }
    }
}