//using System;
//using System.Linq;
//using Coffee.UIExtensions;
//using DatasTypes;
//using EnhancedUI.EnhancedScroller;
//using TMPro;
//using Trucking.Common;
//using Trucking.Model;
//using Trucking.UI.ThreeDimensional;
//using UniRx;
//using UnityEngine;
//using UnityEngine.UI;
//
//
//namespace Trucking.UI.Shop
//{
//    public class ShopCellView_Truck : EnhancedScrollerCellView
//    {
//        public Button btnBuy;
//        public Button btnSell;
//        public Transform trsOnRoute;
//        
//        public Transform trsStar;
//
//        public TextMeshProUGUI txtName;
//        public TextMeshProUGUI truckSpeed;
//        public TextMeshProUGUI truckFuel;
//        public TextMeshProUGUI truckCargo;
//        public TextMeshProUGUI txtGold;
//        public TextMeshProUGUI txtVoucher;
//        public TextMeshProUGUI txtCash;
//        public TextMeshProUGUI txtLock;
//        public TextMeshProUGUI txtLevel;
//        public TextMeshProUGUI txtSellCost;
//        
//        public GameObject goLock;
////        public Button btnUpgrade;
////        public GameObject txtMax;
////        public GameObject imgUpgrade;
//        public UIObject3D truckUI3D;
////        public GameObject upgradeAlert;
//        public GameObject newAlert;
//        
//
//        [HideInInspector] public TruckData data;
////        [HideInInspector] public data data;
//
//        private Action<ShopCellView_Truck> OnClickBuy;
//        private Action<ShopCellView_Truck> OnClickSell;
//        private CompositeDisposable _disposable = new CompositeDisposable();
//
//        private void Start()
//        {
//            btnBuy.OnClickAsObservable()
//                .Subscribe(_ =>
//                {
//                    OnClickBuy?.Invoke(this);
//                })
//                .AddTo(this);
//            
//            btnSell.OnClickAsObservable()
//                .Subscribe(_ =>
//                {
//                    OnClickSell?.Invoke(this);
//                })
//                .AddTo(this);
//
//            _disposable.AddTo(this);
//        }
//        
//        private void OnDisable()
//        {
//            _disposable.Clear();
//        }
//
//
//        public void SetData(TruckData _data,
//            Action<ShopCellView_Truck> _onClickBuy,
//            Action<ShopCellView_Truck> _onClickSell
//        )
//        {
//            _disposable.Clear();
//            data = _data;
//            Truck truck = GameManager.Instance.trucks.FirstOrDefault(x => x.data.id == data.id);
//            data = Datas.truckData.ToArray().FirstOrDefault(x => x.id == data.id);
//            OnClickBuy = _onClickBuy;
//            OnClickSell = _onClickSell;
//
//            txtLevel.text = Utilities.GetStringByData(20008) + ".";
//            txtName.text = Utilities.GetStringByData(data.name_id);
//            txtLock.text = data.require_lv.ToString();
//            
//            ContentLoader.LoadTruckUIObject3DAsync(data.model_h, data.model_c)
//                .TakeUntilDisable(this)
//                .Subscribe(tran =>
//                {
//                    tran.SetParent(GameManager.Instance.trsUIObject3D);
//                    truckUI3D.ObjectPrefab = tran;
//                    truckUI3D.ImageColor = Color.white;
//                    truckUI3D.TargetOffset = new Vector2(-0.3f, -2);
//                    truckUI3D.TargetRotation = new Vector3(3, 140, 0);
//                    truckUI3D.CameraFOV = 30;
//                    truckUI3D.CameraDistance = -16;
//                }).AddTo(_disposable);
//
//            btnBuy.gameObject.SetActive(truck == null);
//            btnSell.gameObject.SetActive(truck != null && !truck.model.hasRoute.Value);
//            trsOnRoute.parent.gameObject.SetActive(true);
//            trsOnRoute.gameObject.SetActive(truck != null && truck.model.hasRoute.Value);
//            
//            newAlert.SetActive(UserDataManager.Instance.data.shopLv.Value == data.require_lv 
//                               && UserDataManager.Instance.data.shopAlert.Value);
//            
//            float boostRate = UserDataManager.Instance.GetBooster(UserDataManager.BoosterType.SPEED);
//            
//            UserDataManager.Instance.data.lv.Subscribe(lv =>
//            {
//                btnBuy.GetComponent<UIEffect>().enabled = lv < data.require_lv;
//                goLock.SetActive(lv < data.require_lv);
//            }).AddTo(_disposable);
//
//            if (truck == null)
//            {
//                for (int i = 0; i < Truck.MAX_LEVEL; i++)
//                {
//                    trsStar.GetChild(i).gameObject.SetActive(i < data.max_lv);
//                    trsStar.GetChild(i).GetChild(0).gameObject.SetActive(i >= data.start_lv);
//                    trsStar.GetChild(i).GetChild(1).gameObject.SetActive(i < data.start_lv);
//                }
//    
//                int speed = (int)(data.speed[data.start_lv - 1] * (1 + boostRate / 100));
//                int speedMax = (int)(data.speed[data.max_lv - 1] * (1 + boostRate / 100));
//
//                truckSpeed.text = $"<color=#197CB4>{speed} </color>({speedMax})";
//    
//                int fuel = data.fuel[data.start_lv - 1];
//                int fuelMax = data.fuel[data.max_lv - 1];
//                
//                truckFuel.text = $"<color=#197CB4>{fuel} </color>({fuelMax})";
//                
//                int cargo = data.cargo[data.start_lv - 1];
//                int cargoMax = data.cargo[data.max_lv - 1];
//    
//                truckCargo.text = $"<color=#197CB4>{cargo} </color>({cargoMax})";
//
//    
//                txtGold.transform.parent.gameObject.SetActive(data.gold[data.start_lv - 1] > 0);
//                txtGold.text = Utilities.GetThousandCommaText(data.gold[data.start_lv - 1]);
//                
//                UserDataManager.Instance.data.gold.Subscribe(gold =>
//                {
//                    txtGold.color = Color.white;
//                    if (gold < data.gold[data.start_lv - 1])
//                    {
//                        txtGold.color = Color.red;
//                    }                    
//                }).AddTo(_disposable);
//    
//                txtVoucher.transform.parent.gameObject.SetActive(data.voucher[data.start_lv - 1] > 0);
//                txtVoucher.text = Utilities.GetThousandCommaText(data.voucher[data.start_lv - 1]);
//                
//                UserDataManager.Instance.data.voucher.Subscribe(voucher =>
//                {
//                    txtVoucher.color = Color.white;
//                    if (voucher < data.voucher[data.start_lv - 1])
//                    {
//                        txtVoucher.color = Color.red;
//                    }
//                }).AddTo(_disposable);
//
//                
//                txtCash.transform.parent.gameObject.SetActive(data.cash[data.start_lv - 1] > 0);
//                txtCash.text = Utilities.GetThousandCommaText(data.cash[data.start_lv - 1]);
//                
//                UserDataManager.Instance.data.cash.Subscribe(cash =>
//                {
//                    txtCash.color = Color.white;
//                    if (cash < data.cash[data.start_lv - 1])
//                    {
//                        txtCash.color = Color.red;
//                    }
//                }).AddTo(_disposable);
//
//            }
//            else
//            {
////                txtMax.SetActive(truck.model.upgradeLv.Value >= truck.data.max_lv);
////                imgUpgrade.SetActive(truck.model.upgradeLv.Value < truck.data.max_lv);
//        
//                for (int i = 0; i < Truck.MAX_LEVEL; i++)
//                {
//                    trsStar.GetChild(i).gameObject.SetActive(i < truck.data.max_lv);
//                    trsStar.GetChild(i).GetChild(0).gameObject.SetActive(i >= truck.model.upgradeLv.Value);
//                    trsStar.GetChild(i).GetChild(1).gameObject.SetActive(i < truck.model.upgradeLv.Value);
//                }
//                
//                int speed = truck.GetSpeed();
//                int speedMax = (int)(data.speed[data.max_lv - 1] * (1 + boostRate / 100));
//                //<color=#197CB4>25 </color>(45)
//                truckSpeed.text = $"<color=#197CB4>{speed} </color>({speedMax})";
//    
//                int fuel = truck.GetFuel(truck.model.upgradeLv.Value);
//                int fuelMax = data.fuel[data.max_lv - 1];
//                
//                truckFuel.text = $"<color=#197CB4>{fuel} </color>({fuelMax})";
//                
//                int cargo = truck.GetCargo(truck.model.upgradeLv.Value);
//                int cargoMax = data.cargo[data.max_lv - 1];
//    
//                truckCargo.text = $"<color=#197CB4>{cargo} </color>({cargoMax})";
//
//                if (!truck.model.hasRoute.Value)
//                {
//                    txtSellCost.text = Utilities.GetThousandCommaText(truck.data.resell_gold[truck.model.upgradeLv.Value - 1]);
//                }
//
//            }            
//        }
//
//        public void RefreshData()
//        {
//            SetData(data,
//                OnClickBuy,
//                OnClickSell
//            );
//        }
//    }
//}