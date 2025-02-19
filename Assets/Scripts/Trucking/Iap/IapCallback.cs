/*
 * Created on 2019년 3월 19일 화요일
 * Creator jhlee5@cookapps.com
 * Copyright (c) 2019 CookAppsPlay
 */

using System;
using System.Collections.Generic;
using System.Linq;
using DatasTypes;
using Trucking.Common;
using Trucking.Manager;
using Trucking.Model;
using Trucking.UI;
using Trucking.UI.Popup;

namespace Trucking.Iap
{
    public static class IapCallback
    {
        public static void OnIap(StoreData goods, Action success)
        {
            var market = goods.ToMarketList();
            var iapData = Datas.iAPData.ToArray().FirstOrDefault(value => value.iap_id == market);

            if (iapData != null)
            {
                OnIapData(iapData, success);
            }
        }

        /// <summary>
        /// coin, cash 등 IAPData사용하는 재원
        /// </summary>
        /// <param name="iapData"></param>
        /// <param name="success"></param>
        private static void OnIapData(IAPData iapData, Action success)
        {
            if (iapData.item_type.type == RewardData.eType.cash)
            {
                Popup_Reward.Instance.Show(0, iapData.count, 0, () =>
                {
                    success.Invoke();
                    AudioManager.Instance.PlaySound("sfx_shop_cash_get");
                    UserDataManager.Instance.data.hasPurchase.Value = true;
                });
            }
            else if (iapData.item_type.type == RewardData.eType.crate)
            {
                RewardModel rewardModel = new RewardModel(iapData.item_type.type,
                    iapData.count, iapData.reward_id);

                Popup_Reward.Instance.Show(rewardModel, () =>
                {
                    success.Invoke();
                    AudioManager.Instance.PlaySound("sfx_resource_get");
                    UserDataManager.Instance.data.hasPurchase.Value = true;
                });
            }
        }

        /// <summary>
        /// eventPackData
        /// </summary>
        /// <param name="eventTruckData"></param>
        /// <param name="success"></param>
//        private static void OnEventPack(EventPackData eventTruckData, Action success)
//        {
//            List<RewardModel> listRewardModels = UIRewardManager.Instance.MakeRewardList(eventTruckData.gold,
//                eventTruckData.cash,
//                0);
//
//            RewardModel truckReward = new RewardModel(RewardData.eType.truck_id, eventTruckData.truck_id, 0);
//            listRewardModels.Add(truckReward);
//
//            Popup_Reward.Instance.Show(listRewardModels,
//                () =>
//                {
//                    int dataIndex = Array.IndexOf(Datas.eventPackData.ToArray(), eventTruckData);
//                    UserDataManager.Instance.data.eventTruckShopData.Value.count[dataIndex]++;
//                    EventTruckManager.Instance.model.hasEvent.Value = false;
//                    success.Invoke();
//
//                    FBAnalytics.FBAnalytics.LogEventPackEvent(UserDataManager.Instance.data.lv.Value,
//                        UserDataManager.Instance.data.hasPurchase.Value,
//                        eventTruckData.iap_id.id);
//
//                    UserDataManager.Instance.data.hasPurchase.Value = true;
//                });
//        }
    }
}