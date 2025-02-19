//using System;
//using System.Linq;
//using Coffee.UIExtensions;
//using DatasTypes;
//using TMPro;
//using Trucking.Common;
//using Trucking.Iap;
//using Trucking.Manager;
//using Trucking.Model;
//using Trucking.UI.ThreeDimensional;
//using UniRx;
//using UnityEngine;
//using UnityEngine.UI;
//
//
//namespace Trucking.UI.Popup
//{
//    public class Popup_EventTruck : Popup_Base<Popup_EventTruck>
//    {
//        
////        public Button btnClose;
////        public Button btnBlackPanel;
//        public Transform trsStar;
//        public TextMeshProUGUI txtTime;
//        public TextMeshProUGUI txtName;
//        public TextMeshProUGUI txtSpeed1;
//        public TextMeshProUGUI txtFuel1;
//        public TextMeshProUGUI txtCargo1;
//        public TextMeshProUGUI txtCash;
//        public TextMeshProUGUI txtGold;
//        public Text txtBuy;
//        public Button btnConfirm;
//        public UIObject3D uiObject3D;
//
//        private ReactiveProperty<EventPackData> eventTruckData = new ReactiveProperty<EventPackData>();
//        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
//
//
//        private void Start()
//        {
//            _compositeDisposable.AddTo(this);
//            
////            btnClose.OnClickAsObservable()
////                .Subscribe(_ => { GameManager.Instance.fsm.PopState(); })
////                .AddTo(this);
////
////            btnBlackPanel.OnClickAsObservable()
////                .Subscribe(_ => { GameManager.Instance.fsm.PopState(); })
////                .AddTo(this);
//            
//            btnConfirm.OnClickAsObservable()
//                .Subscribe(_ =>
//                {
//                    // inapp result
//                    GameManager.Instance.fsm.PopState();
//                    IapManager.Instance.Purchase(eventTruckData.Value.iap_id.id);                    
//                })
//                .AddTo(this);
//            
//
//            eventTruckData.Subscribe(tr =>
//            {               
//                _compositeDisposable.Clear();
//                
//                if (tr != null)
//                {
//                    TruckData truckData = Datas.truckData.ToArray().FirstOrDefault(x => x.id == eventTruckData.Value.truck_id);
//
//                    txtName.text = Utilities.GetStringByData(truckData.name_id);
//                    txtSpeed1.text = $"{truckData.speed[0]}({truckData.speed[truckData.max_lv - 1]})";
//                    txtFuel1.text = $"{truckData.fuel[0]}({truckData.fuel[truckData.max_lv - 1]})";
//                    txtCargo1.text = $"{truckData.cargo[0]}({truckData.cargo[truckData.max_lv - 1]})";
//
//                    ContentLoader.LoadTruckUIObject3DAsync(truckData.model_h, truckData.model_c)
//                        .TakeUntilDisable(this)
//                        .Subscribe(tran =>
//                        {
//                            tran.SetParent(GameManager.Instance.trsUIObject3D);
//                            uiObject3D.ObjectPrefab = tran;
//                            uiObject3D.ImageColor = Color.white;
//                            uiObject3D.TargetOffset = new Vector2(-0.3f, -2);
//                            uiObject3D.TargetRotation = new Vector3(3, 140, 0);
//                            uiObject3D.CameraFOV = 30;
//                            uiObject3D.CameraDistance = -18;
//                        }).AddTo(this);
//
//                    for (int i = 0; i < Truck.MAX_LEVEL; i++)
//                    {
//                        trsStar.GetChild(i).gameObject.SetActive(i < truckData.max_lv);
//                        trsStar.GetChild(i).GetChild(0).gameObject.SetActive(i >= 1);
//                        trsStar.GetChild(i).GetChild(1).gameObject.SetActive(i < 1);
//                    }
//
//                    txtGold.text = Utilities.GetNumberKKK(tr.gold);
//                    txtCash.text = Utilities.GetNumberKKK(tr.cash);
//
//                    EventTruckManager.Instance.model.hasEvent.Subscribe(has =>
//                        {
//                            btnConfirm.GetComponent<UIEffect>().enabled = !has;
//                        }).AddTo(_compositeDisposable);
//
//                    Observable.Interval(TimeSpan.FromSeconds(1)).StartWith(0).Subscribe(_ =>
//                    {
//                        txtTime.text =
//                            Utilities.GetTimeString(EventTruckManager.Instance.model.endTime.Value - DateTime.Now);
//                    }).AddTo(_compositeDisposable);
//
//                    IapManager.Instance.ObsLocalizedPrice(tr.iap_id.id)
//                        .SubscribeToText(txtBuy).AddTo(_compositeDisposable);
//                }
//                else
//                {
//                    GameManager.Instance.fsm.PopState();
//                }
//                
//                
//            }).AddTo(this);
//        }
//                
//        public override void Show()
//        {
//            if (EventTruckManager.Instance.model.hasEvent.Value)
//            {
//                base.Show();
//                eventTruckData.Value = Datas.eventPackData[EventTruckManager.Instance.model.eventIndex.Value];
//                UIMain.Instance.eventTruckAlert.SetActive(false);
//            }
//
//        }
//
//    }
//    
//
//}

