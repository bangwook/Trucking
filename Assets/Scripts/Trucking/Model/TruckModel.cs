using System;
using UniRx;

namespace Trucking.Model
{
    public class TruckModel
    {
        public enum State
        {
            Wait,
            Move
        };


        public int dataID;
        public int pathIndex;
        public string currentStation;
        public ReactiveProperty<bool> hasRoute = new ReactiveProperty<bool>();

        public long birthID = DateTime.Now.ToBinary();
        public DateTime startTime;
        public ReactiveProperty<float> fuel = new ReactiveProperty<float>(0);
        public ReactiveProperty<int> upgradeLv = new ReactiveProperty<int>(1);
        public ReactiveProperty<int> colorIndex = new ReactiveProperty<int>(0);
        public ReactiveProperty<State> state = new ReactiveProperty<State>(State.Wait);
        public ReactiveCollection<string> pathStationName = new ReactiveCollection<string>();
        public ReactiveCollection<CargoModel> cargoModels = new ReactiveCollection<CargoModel>();
        public ReactiveCollection<CargoModel> completeCargoModels = new ReactiveCollection<CargoModel>();


        public TruckModel(int _dataID)
        {
            dataID = _dataID;
        }
    }
}