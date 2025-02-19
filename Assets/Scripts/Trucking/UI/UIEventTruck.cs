//using System;
//using TMPro;
//using UniRx;
//using UnityEngine;
//using UnityEngine.UI;
//
//namespace UI
//{
//    public class UIEventTruck : MonoSingleton<UIEventTruck>
//    {
//        public Button btnBubble;
//        public TextMeshProUGUI txtTime;
//        
//        private void Start()
//        {
//            EventTruckManager.Instance.model.hasEvent.Subscribe(has =>
//            {
//                gameObject.SetActive(has);
//            }).AddTo(this);
//            
//            btnBubble.OnClickAsObservable()
//                .Subscribe(_ =>
//                {
//                    Popup_EventTruck.Instance.Show();
//                })
//                .AddTo(this);
//
//
//            Observable.Interval(TimeSpan.FromSeconds(1)).StartWith(0).Subscribe(_ =>
//            {
//                txtTime.text = Utilities.GetTimeString(EventTruckManager.Instance.model.endTime.Value - DateTime.Now);
//            }).AddTo(this);
//            
////            transform.localPosition = new Vector3(WorldMap.Instance.trsTruckShop.localPosition.x, 
////                WorldMap.Instance.trsTruckShop.localPosition.z, 
////                -20);
//        }
//                
//        private void LateUpdate()
//        {
//            transform.rotation = Camera.main.transform.rotation;          
//        }
//    }
//}

namespace Trucking.UI
{
}