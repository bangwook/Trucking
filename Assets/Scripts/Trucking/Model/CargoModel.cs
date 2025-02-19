using System;
using UniRx;

namespace Trucking.Model
{
    public class CargoModel
    {
        public ReactiveProperty<bool> isContract = new ReactiveProperty<bool>();
        public ReactiveProperty<int> weight = new ReactiveProperty<int>();
        public ReactiveProperty<int> grade = new ReactiveProperty<int>();
        public ReactiveProperty<int> id = new ReactiveProperty<int>(1);
        public ReactiveProperty<string> from = new ReactiveProperty<string>();
        public ReactiveProperty<string> to = new ReactiveProperty<string>();
        public ReactiveProperty<long> truckID = new ReactiveProperty<long>();
        public ReactiveCollection<RewardModel> rewardModels = new ReactiveCollection<RewardModel>();
//        public ReactiveProperty<RewardModel> rewardModel = new ReactiveProperty<RewardModel>();
//        public ReactiveProperty<RewardModel> rewardMaterialModel = new ReactiveProperty<RewardModel>();
//        public ReactiveProperty<RewardModel> rewardExpModel = new ReactiveProperty<RewardModel>();
        public ReactiveProperty<DateTime> refreshTime = new ReactiveProperty<DateTime>();
        public ReactiveProperty<float> distance = new ReactiveProperty<float>();

    }
}