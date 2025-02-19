using System.Collections.Generic;
using System.Linq;
using DatasTypes;
using UnityEngine.Purchasing;

namespace Trucking.Iap
{
    public class StoreData
    {
        public readonly ProductType type;
        public readonly string id;
        public readonly string google;
        public readonly string apple;
        public readonly string amazon;
        public readonly string mac;
        public readonly string facebook;

        public StoreData(ProductType type, string id, string google, string apple, string amazon, string mac,
            string facebook)
        {
            this.type = type;
            this.id = id;
            this.google = google;
            this.apple = apple;
            this.amazon = amazon;
            this.mac = mac;
            this.facebook = facebook;
        }

        public MarketList ToMarketList() => Datas.marketList.ToArray().FirstOrDefault(value => value.id == id);

        public static readonly List<StoreData> StoreDatas = Datas.marketList.ToArray().Select(item =>
            new StoreData(ProductType.Consumable,
                item.id, item.google_ID, item.apple_ID, item.amazon_ID, item.mAC_ID, item.facebook_ID)).ToList();

        public static StoreData GetById(string id)
        {
            return StoreDatas.FirstOrDefault(value => value.id.Equals(id));
        }
    }
}