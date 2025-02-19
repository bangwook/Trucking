//using System.Linq;
//using DatasTypes;
//using TMPro;
//using Trucking.Common;
//using Trucking.UI.ThreeDimensional;
//using UniRx;
//using UnityEngine;
//using UnityEngine.UI;
//
//namespace Trucking.UI.Popup
//{
//    public class Popup_LuckyBoxRowCellView : MonoBehaviour
//    {
//        public Image imgIcon;
//        public TextMeshProUGUI txtReward;
//        public Button btnTruck;
//        public Image imgInfo;
//        public UIObject3D uiObject3D;
//
//        private LuckyBoxRowData data;
//        private TruckData truckData;
//        
//        private void Start()
//        {
//            btnTruck.OnClickAsObservable()
//                .Subscribe(_ =>
//                {
//                    AudioManager.Instance.PlaySound("sfx_button_main");
//                    Popup_TruckInformation.Instance.ShowInfomation(truckData);
//                }).AddTo(this);
//        }
//
//        public void SetData(LuckyBoxRowData _data)
//        {
//            data = _data;
//            truckData = null;
//            
//            gameObject.SetActive(data != null);
//            
//            if (data != null)
//            {
//                imgInfo.gameObject.SetActive(data.type == RewardData.eType.truck_id);
//                imgIcon.gameObject.SetActive(data.type != RewardData.eType.truck_id);
//                btnTruck.enabled = data.type == RewardData.eType.truck_id;
//                uiObject3D.gameObject.SetActive(data.type == RewardData.eType.truck_id);
//                txtReward.color = Color.white;
//                txtReward.gameObject.SetActive(data.type != RewardData.eType.truck_id);
//                
//                if (data.type == RewardData.eType.truck_id)
//                {
//                    truckData = Datas.truckData.ToArray().FirstOrDefault(x => x.id == data.count);
//                    
////                    gameObject.SetActive(truckData == null);
//                    
//                    if (truckData != null)
//                    {
//                        ContentLoader.LoadTruckUIObject3DAsync(truckData.model_h, truckData.model_c)
//                            .TakeUntilDisable(this)
//                            .Subscribe(tran =>
//                            {
//                                tran.SetParent(GameManager.Instance.trsUIObject3D);
//                                uiObject3D.ObjectPrefab = tran;
//                                uiObject3D.ImageColor = Color.white;
//                                uiObject3D.TargetOffset = new Vector2(-0.3f, -2);
//                                uiObject3D.TargetRotation = new Vector3(3, 140, 0);
//                                uiObject3D.CameraFOV = 30;
//                                uiObject3D.CameraDistance = -18;
//                            }).AddTo(this);
//
//                        
//                        
//                        
////                        GameManager.Instance.RemakeUIObject3D(UiObject3D, Truck.GetTruckPrefab(truckData).transform);
////                        UiObject3D.TargetOffset = new Vector2(truckData.offset_x, -2f);
//
////                        int count = GameManager.Instance.trucks.Count(x => x.data.id == truckData.id);
////                        int max = truckData.limit_numb;
////
////                        txtReward.text = $"{count}/{max}";
////
////                        if (count >= max)
////                        {
////                            txtReward.color = Color.red;
////                        }
//                    }
//                }
//                else
//                {
//                    imgIcon.sprite = GameManager.Instance.GetRewardImage(data.type);
//                    txtReward.text = Utilities.GetNumberKKK(data.count);
//                }
//            }
//        }
//    }
//}

