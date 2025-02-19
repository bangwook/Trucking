using System;
using UniRx;

namespace Trucking.Model
{
    
    public class CityModel
    {
        public enum State
        {
            Wait,
            Craft,
            Upgrade,
            Lock,
            Craft_Collect,
            Upgrade_Complete
        };

        public CityModel(int _id)
        {
            id.Value = _id;
        }

        public ReactiveProperty<int> id = new ReactiveProperty<int>();
        public ReactiveProperty<int> level = new ReactiveProperty<int>();
        public ReactiveProperty<int> productLevel = new ReactiveProperty<int>();
        public ReactiveProperty<State> state = new ReactiveProperty<State>();
        public ReactiveProperty<DateTime> productTime = new ReactiveProperty<DateTime>();
        public ReactiveCollection<CargoModel> cargos = new ReactiveCollection<CargoModel>();
        public ReactiveCollection<DateTime> refreshTime = new ReactiveCollection<DateTime>();
        
    }
}