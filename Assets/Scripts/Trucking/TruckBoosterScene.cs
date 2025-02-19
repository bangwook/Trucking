using Trucking.Common;
using UnityEngine;

namespace Trucking
{
    public class TruckBoosterScene : MonoSingleton<TruckBoosterScene>
    {
        public GameObject baseObject;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Debug.Log("TruckBoosterScene OnDestroy");
        }

        private void OnDestory()
        {
            Debug.Log("TruckBoosterScene OnDestroy");
        }
    }
}