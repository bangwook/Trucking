using System;
using DatasTypes;
using Newtonsoft.Json.Utilities;
using UniRx;

namespace Trucking.Model
{
    public class RewardModel
    {
        public ReactiveProperty<RewardData.eType> type = new ReactiveProperty<RewardData.eType>();
        public ReactiveProperty<int> index = new ReactiveProperty<int>(0);
        public ReactiveProperty<long> count = new ReactiveProperty<long>(0);

        public RewardModel()
        {
        }

        public RewardModel(RewardData.eType _type, long _count, string _id)
        {
            type.Value = _type;
            count.Value = _count;
            FindIndex(_id);
        }

        public RewardModel(RewardData.eType _type, long _count, int _index = 0)
        {
            type.Value = _type;
            count.Value = _count;
            index.Value = _index;
        }

        public bool IsRandomBox()
        {
            switch (type.Value)
            {
                case RewardData.eType.random_box:
                    return true;
            }

            return false;
        }

        public bool IsBooster()
        {
            switch (type.Value)
            {
                case RewardData.eType.booster:
                    return true;
            }

            return false;
        }

        public void FindIndex(string _id)
        {
            index.Value = GetIndex(type.Value, _id);
        }

        public static int GetIndex(RewardData.eType _type, string _id)
        {
            switch (_type)
            {
                case RewardData.eType.random_box:
                    return Datas.randomBox.ToArray().IndexOf(x => x.id == _id);
                case RewardData.eType.material:
                case RewardData.eType.parts:
                case RewardData.eType.crate:
                    if (!string.IsNullOrEmpty(_id))
                    {
                        return Int32.Parse(_id[_id.Length - 1].ToString()) - 1;
                    }

                    break;
                case RewardData.eType.booster:
                    return Datas.buffData.ToArray().IndexOf(x => x.id == _id);
            }

            return 0;
        }
    }
}