using Trucking.Common;
using Trucking.UI;
using UniRx;
using UnityEngine;

namespace Trucking.Manager
{
    public class TouchObjectManager : MonoSingleton<TouchObjectManager>
    {
        public void Init()
        {
            UICityMenu.Instance.gameObject.ObserveEveryValueChanged(x => x.active).Subscribe(active =>
            {
                for (int i = 0; i < TouchObject_Plane.Instance.trsPlanes.childCount; i++)
                {
                    TouchObject_Plane.Instance.trsPlanes.GetChild(i).GetChild(0).gameObject.SetActive(!active);
                }

                TouchObject_BuoyGroup.Instance.gameObject.SetActive(!active);
            }).AddTo(this);
        }
    }
}