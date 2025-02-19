using UniRx;

namespace Trucking.Model
{
    public class RoadModel
    {
        public ReactiveProperty<long> truckBirthID = new ReactiveProperty<long>();
        public ReactiveProperty<bool> isOpen = new ReactiveProperty<bool>();
    }
}