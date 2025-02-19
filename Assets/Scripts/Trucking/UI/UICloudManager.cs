using Trucking.Common;
using UnityEngine;

namespace Trucking.UI
{
    public class UICloudManager : MonoSingleton<UICloudManager>
    {
        public UICloud[] uiClouds = new UICloud[7];

        public void Init()
        {
            Vector3[] arrPos =
            {
                new Vector3(200, 110),
                new Vector3(340, -290),
                new Vector3(-10, -230),
                new Vector3(-220, 50),
                new Vector3(-280, -350),
                new Vector3(-700, -40),
                new Vector3(-750, 250)
            };

            for (int i = 0; i < uiClouds.Length; i++)
            {
                uiClouds[i].Init(i);
                uiClouds[i].transform.localPosition = arrPos[i];
            }
        }
    }
}