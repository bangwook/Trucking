using System;
using System.Linq;
using Coffee.UIExtensions;
using DatasTypes;
using EnhancedUI.EnhancedScroller;
using TMPro;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.ThreeDimensional;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Trucking.UI.CellView
{
    public class EditCellView : EnhancedScrollerCellView
    {
        public GameObject hq;
        public GameObject goMaxSpec;
        public GameObject goAdBooster;

        public Button btnTruck;
        public Button btnAddRoute;
        public Button btnAddTruck;
        public Button btnNewTruck;

        public Button btnBooster;
        public Button btnChange;
        public Button btnPosition;
        public Button btnUpgrade;
        public Button btnConfirm;
        public Button btnSell;
        public Button btnSetting;
        public Button btnJob;
        public Button btnComplete;

        public TextMeshProUGUI txtAddRoute;
        public TextMeshProUGUI txtAddRouteLv;
        public TextMeshProUGUI txtNewTruck;
        public TextMeshProUGUI txtMaxUpgrade;
        public GameObject boxNotiUpgrade;

        #region truck

        public RectTransform trsTruck;
        public Transform trsStar;
        public Image truckColor;
        public Image truckActive;
        public TextMeshProUGUI truckName;
        public TextMeshProUGUI truckSpeed;
        public TextMeshProUGUI truckFuel;
        public TextMeshProUGUI truckCargo;
        public TextMeshProUGUI truckSpeedMax;
        public TextMeshProUGUI truckFuelMax;
        public TextMeshProUGUI truckCargoMax;
        public UIObject3D truckUI3D;

        public TextMeshProUGUI cityName;
        public TextMeshProUGUI cityTime;
        public TextMeshProUGUI cityTimeBoost;
        public TextMeshProUGUI fuelTime;
        public Slider sliderPath;
        public Slider sliderFuel;
        public RawImage rawImage;
        public GameObject truckAlert;
        public GameObject upgradeAlert;

        #endregion

        #region addroute

        public GameObject addRouteAlert;
        public TextMeshProUGUI userLv;
        public TextMeshProUGUI unlockGold;

        #endregion

        [HideInInspector] public EditCellData data;


        private Action<EditCellView> OnClick;
        private Action<EditCellView> OnClickChange;
        private Action<EditCellView> OnClickPosition;
        private Action<EditCellView> OnClickUpgrade;
        private Action<EditCellView> OnClickConfirm;
        private Action<EditCellView> OnClickBoost;
        private Action<EditCellView> OnClickSell;
        private Action<EditCellView> OnClickSetting;
        private Action<EditCellView> OnClickJob;
        private Action<EditCellView> OnClickComplete;

        private Action<EditCellView> OnRefresh;
        private EditCellData prev_data;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private CompositeDisposable disposableButton = new CompositeDisposable();

        Vector2 sliderOffset = new Vector2(0f, 0f);

        private void Start()
        {
            btnTruck.OnClickAsObservable()
                .Subscribe(_ => { OnClick?.Invoke(this); })
                .AddTo(this);

            btnAddRoute.OnClickAsObservable()
                .Subscribe(_ => { OnClick?.Invoke(this); })
                .AddTo(this);

            btnAddTruck.OnClickAsObservable()
                .Subscribe(_ => { OnClick?.Invoke(this); })
                .AddTo(this);

            btnNewTruck.OnClickAsObservable()
                .Subscribe(_ => { OnClick?.Invoke(this); })
                .AddTo(this);


            btnBooster.OnClickAsObservable()
                .Subscribe(_ => { OnClickBoost?.Invoke(this); })
                .AddTo(this);

            btnChange.OnClickAsObservable()
                .Subscribe(_ => { OnClickChange?.Invoke(this); })
                .AddTo(this);

            btnPosition.OnClickAsObservable()
                .Subscribe(_ => { OnClickPosition?.Invoke(this); })
                .AddTo(this);

            btnUpgrade.OnClickAsObservable()
                .Subscribe(_ => { OnClickUpgrade?.Invoke(this); })
                .AddTo(this);

            btnConfirm.OnClickAsObservable()
                .Subscribe(_ => { OnClickConfirm?.Invoke(this); })
                .AddTo(this);

            btnSell.OnClickAsObservable()
                .Subscribe(_ => { OnClickSell?.Invoke(this); })
                .AddTo(this);

            btnSetting.OnClickAsObservable()
                .Subscribe(_ => { OnClickSetting?.Invoke(this); })
                .AddTo(this);

            btnJob.OnClickAsObservable()
                .Subscribe(_ => { OnClickJob?.Invoke(this); })
                .AddTo(this);

            btnComplete.OnClickAsObservable()
                .Subscribe(_ => { OnClickComplete?.Invoke(this); })
                .AddTo(this);


            _compositeDisposable.AddTo(this);
            disposableButton.AddTo(this);
        }

        private void OnDisable()
        {
            _compositeDisposable.Clear();
            disposableButton.Clear();
        }

        public void SetData(EditCellData _data,
            Action<EditCellView> _onRefresh = null,
            Action<EditCellView> _onClick = null,
            Action<EditCellView> _onClickConfirm = null,
            Action<EditCellView> _onClickUpgrade = null,
            Action<EditCellView> _onClickChange = null,
            Action<EditCellView> _onClickPosition = null,
            Action<EditCellView> _onClickBoost = null,
            Action<EditCellView> _onClickSell = null,
            Action<EditCellView> _onClickSetting = null,
            Action<EditCellView> _onClickJob = null,
            Action<EditCellView> _onClickComplete = null
        )
        {
            gameObject.SetActive(true);
            _compositeDisposable.Clear();
            disposableButton.Clear();

            prev_data = data;
            data = _data;
            OnClick = _onClick;
            OnClickChange = _onClickChange;
            OnClickPosition = _onClickPosition;
            OnClickUpgrade = _onClickUpgrade;
            OnClickConfirm = _onClickConfirm;
            OnClickBoost = _onClickBoost;
            OnClickSell = _onClickSell;
            OnClickSetting = _onClickSetting;
            OnClickJob = _onClickJob;
            OnClickComplete = _onClickComplete;
            OnRefresh = _onRefresh;

            HideAllButton();
            hq.SetActive(data.targetCity != null);
            goMaxSpec.SetActive(true);
            sliderFuel.gameObject.SetActive(false);

            if (data.targetCity == null)
            {
                trsTruck.anchoredPosition = new Vector2(55, 2.5f);
            }
            else
            {
                trsTruck.anchoredPosition = new Vector2(55, 18f);
            }

            btnBooster.transform.parent.gameObject.SetActive(true);
            btnTruck.gameObject.SetActive(data.type == EditCellData.EditCellType.TRUCK);
            btnAddRoute.gameObject.SetActive(data.type == EditCellData.EditCellType.ADD_ROUTE);
            btnAddTruck.gameObject.SetActive(data.type == EditCellData.EditCellType.ADD_TRUCK);
            btnNewTruck.gameObject.SetActive(data.type == EditCellData.EditCellType.NEW_TRUCK);

            if (data.type == EditCellData.EditCellType.TRUCK)
            {
                Observable.CombineLatest(data.truck.model.hasRoute,
                    data.truck.model.colorIndex,
                    (hasRoute, colorIndex) => hasRoute).Subscribe(hasRoute =>
                {
                    try
                    {
                        if (!hasRoute)
                        {
                            truckActive.color = Utilities.GetColorByHtmlString("E7E7E7");
                            truckColor.color = Utilities.GetColorByHtmlString("828282");
                        }
                        else
                        {
                            truckActive.color = Color.white;
                            truckColor.color = ColorManager.Instance.ColorList[data.truck.model.colorIndex.Value].color;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }).AddTo(_compositeDisposable);

                goMaxSpec.ObserveEveryValueChanged(x => x.activeSelf).Subscribe(active =>
                    {
                        cityName.gameObject.SetActive(!active);
                    })
                    .AddTo(_compositeDisposable);

                data.truck.isNew.Subscribe(isNew => { truckAlert.SetActive(isNew); }).AddTo(_compositeDisposable);

                data.truck.model.completeCargoModels.ObsSomeChanged().Subscribe(cargos =>
                {
                    btnJob.GetComponent<UIEffect>().enabled = cargos.Count > 0;
                }).AddTo(_compositeDisposable);

                Observable.CombineLatest(UserDataManager.Instance.data.gold,
                        UserDataManager.Instance.data.truckParts[0],
                        UserDataManager.Instance.data.truckParts[1],
                        UserDataManager.Instance.data.truckParts[2],
                        UserDataManager.Instance.data.truckParts[3],
                        data.truck.model.upgradeLv,
                        (gold, parts1, parts2, parts3, parts4, lv) => lv < data.truck.data.max_lv
                                                                      && data.truck.data.gold[lv] <=
                                                                      UserDataManager.Instance.data.gold.Value
                                                                      && parts1 >= data.truck.data.parts1[lv]
                                                                      && parts2 >= data.truck.data.parts2[lv]
                                                                      && parts3 >= data.truck.data.parts3[lv]
                                                                      && parts4 >= data.truck.data.parts4[lv])
                    .Subscribe(alert => { upgradeAlert.SetActive(alert); })
                    .AddTo(_compositeDisposable);

                btnUpgrade.gameObject.ObserveEveryValueChanged(x => x.activeSelf).Subscribe(active =>
                {
                    boxNotiUpgrade.SetActive(active && UserDataManager.Instance.data.lv.Value <= 5);
                }).AddTo(_compositeDisposable);

                data.truck.model.upgradeLv.Subscribe(lv =>
                {
                    if (data.truck.data.max_lv == 7)
                    {
                        trsStar.GetComponent<GridLayoutGroup>().spacing = new Vector2(-6, 0);
                    }
                    else if (data.truck.data.max_lv == 6)
                    {
                        trsStar.GetComponent<GridLayoutGroup>().spacing = new Vector2(-4, 0);
                    }
                    else
                    {
                        trsStar.GetComponent<GridLayoutGroup>().spacing = new Vector2(-2, 0);
                    }

                    for (int i = 0; i < Truck.MAX_LEVEL; i++)
                    {
                        trsStar.GetChild(Truck.MAX_LEVEL - 1 - i).gameObject.SetActive(i < data.truck.data.max_lv);
                        trsStar.GetChild(Truck.MAX_LEVEL - 1 - i).GetChild(0).gameObject.SetActive(i >= lv);
                    }

                    truckSpeed.text = data.truck.GetSpeed().ToString();
                    truckFuel.text = data.truck.MaxFuel.Value.ToString();
                    truckCargo.text = data.truck.MaxWeight.Value.ToString();


                    int masSpeed = data.truck.GetSpeed(data.truck.data.max_lv);
                    int maxFuel = data.truck.GetFuel(data.truck.data.max_lv);
                    int maxCargo = data.truck.GetCargo(data.truck.data.max_lv);

                    truckSpeedMax.text = masSpeed.ToString();
                    truckFuelMax.text = maxFuel.ToString();
                    truckCargoMax.text = maxCargo.ToString();

                    txtMaxUpgrade.gameObject.SetActive(false);

                    ContentLoader.LoadTruckUIObject3DAsync(data.truck.data.model_h, data.truck.data.model_c)
                        .TakeUntilDisable(this)
                        .Subscribe(tran =>
                        {
                            tran.SetParent(GameManager.Instance.trsUIObject3D);
                            truckUI3D.ObjectPrefab = tran;
                            truckUI3D.ImageColor = Color.white;
                            truckUI3D.TargetOffset = new Vector2(data.truck.data.offset_x, data.truck.data.offset_y);
                            truckUI3D.TargetRotation = new Vector3(6, 155, 0);
                            truckUI3D.CameraFOV = 25;
                            truckUI3D.CameraDistance = data.truck.data.cam_dis; //, -26, -28.5;
                        }).AddTo(_compositeDisposable);
                }).AddTo(_compositeDisposable);

                truckName.text = data.truck.GetName();

                if (data.targetTruck != null)
                {
                    int speedGap = data.truck.GetSpeed() - data.targetTruck.GetSpeed();

                    if (speedGap < 0)
                    {
                        truckSpeed.color = Utilities.GetColorByHtmlString("FF1B1B");
                    }
                    else if (speedGap > 0)
                    {
                        truckSpeed.color = Utilities.GetColorByHtmlString("34DB00");
                    }
                    else
                    {
                        truckSpeed.color = Color.white;
                    }

                    int fuelGap = data.truck.MaxFuel.Value - data.targetTruck.MaxFuel.Value;

                    if (fuelGap < 0)
                    {
                        truckFuel.color = Utilities.GetColorByHtmlString("FF1B1B");
                    }
                    else if (fuelGap > 0)
                    {
                        truckFuel.color = Utilities.GetColorByHtmlString("34DB00");
                    }
                    else
                    {
                        truckFuel.color = Color.white;
                    }

                    int weightGap = data.truck.MaxWeight.Value - data.targetTruck.MaxWeight.Value;

                    if (weightGap < 0)
                    {
                        truckCargo.color = Utilities.GetColorByHtmlString("FF1B1B");
                    }
                    else if (weightGap > 0)
                    {
                        truckCargo.color = Utilities.GetColorByHtmlString("34DB00");
                    }
                    else
                    {
                        truckCargo.color = Color.white;
                    }
                }

                if (data.targetCity != null)
                {
                    cityName.text = data.targetCity.name;
                    sliderPath.maxValue = data.truck.GetTransitTime();

                    Observable.EveryUpdate().StartWith(0)
                        .Subscribe(_ =>
                        {
                            TimeSpan leftTime =
                                TimeSpan.FromSeconds(data.truck.GetTransitTime() - data.truck.GetDeltaTime());
                            cityTime.text = Utilities.GetTimeString(leftTime);

                            if (UserDataManager.Instance.GetBooster(UserDataManager.BoosterType.SPEED) > 0)
                            {
                                cityTime.color = Utilities.GetColorByHtmlString("FFC813");
                                cityTimeBoost.gameObject.SetActive(true);
                            }
                            else
                            {
                                cityTime.color = Color.white;
                                cityTimeBoost.gameObject.SetActive(false);
                            }

                            sliderPath.value = data.truck.GetDeltaTime();
                            sliderOffset += new Vector2(-1, 0) * Time.deltaTime;

                            rawImage.uvRect =
                                new Rect(sliderOffset.x
                                    , 0
                                    , sliderPath.fillRect.rect.width
                                      / rawImage.texture.width
                                    , 1);
                        }).AddTo(_compositeDisposable);

                    data.truck.model.state.Pairwise().Subscribe(value => { _compositeDisposable.Clear(); })
                        .AddTo(_compositeDisposable);
                }
                else
                {
                    sliderFuel.maxValue = data.truck.MaxFuel.Value;

                    Observable.EveryUpdate().StartWith(0)
                        .Subscribe(_ =>
                        {
                            if (UserDataManager.Instance.GetBooster(UserDataManager.BoosterType.GAS) > 0)
                            {
                                sliderFuel.value = data.truck.MaxFuel.Value;
                                fuelTime.text = Utilities.GetStringByData(20036);
                                fuelTime.color = Utilities.GetColorByHtmlString("FFC813");
                            }
                            else
                            {
                                sliderFuel.value = data.truck.model.fuel.Value;
                                fuelTime.text = Utilities.GetTimeStringShort(data.truck.GetMaxRefuelTime());
                                fuelTime.color = Color.white;

                                if (data.truck.model.fuel.Value >= data.truck.MaxFuel.Value)
                                {
                                    fuelTime.text = Utilities.GetStringByData(20051);
                                }
                            }
                        }).AddTo(_compositeDisposable);
                }
            }
            else if (data.type == EditCellData.EditCellType.ADD_ROUTE)
            {
                Observable.CombineLatest(UserDataManager.Instance.data.gold,
                    GameManager.Instance.roads.Select(roads => roads.model.isOpen).CombineLatest(),
                    GameManager.Instance.trucks.Select(truck => truck.model.hasRoute).CombineLatest(),
                    (gold, open, hasRoute) => (gold, open, hasRoute)).Subscribe(value =>
                {
                    int roadCount = value.Item2.Count(x => x);
                    long roadCost = Datas.roadCostData.ToArray().FirstOrDefault(x => x.sequence == roadCount).road_cost;
                    bool hasWaitTruck = value.Item3.Count(x => !x) > 0;

                    LevelData levelData = GameManager.Instance.GetRouteCountLevelData();
                    addRouteAlert.SetActive(false);

                    if (levelData != null)
                    {
                        unlockGold.text = Utilities.GetThousandCommaText(levelData.route_price + roadCost);
                        unlockGold.color = Utilities.GetColorByHtmlString("FCB100");
                        if (value.Item1 < levelData.route_price + roadCost)
                        {
                            unlockGold.color = Color.red;
                        }

                        addRouteAlert.SetActive(value.Item1 >= levelData.route_price + roadCost
                                                && hasWaitTruck);
                        btnAddRoute.GetComponent<UIEffect>().enabled =
                            value.Item1 < levelData.route_price + roadCost;
                    }
                }).AddTo(this);
            }
        }

        public void HideAllButton()
        {
            btnBooster.gameObject.SetActive(false);
            btnChange.gameObject.SetActive(false);
            btnPosition.gameObject.SetActive(false);
            btnUpgrade.gameObject.SetActive(false);
            btnConfirm.gameObject.SetActive(false);
            btnSell.gameObject.SetActive(false);
            btnSetting.gameObject.SetActive(false);
            btnJob.gameObject.SetActive(false);
            btnComplete.gameObject.SetActive(false);
            goAdBooster.SetActive(false);

            disposableButton.Clear();
        }

        public override void RefreshCellView()
        {
            // update the UI text with the cell data
            //HideAllButton();


//            OnRefresh(this);
            RefreshData();
        }

        public void RefreshData()
        {
            SetData(data,
                OnRefresh,
                OnClick,
                OnClickConfirm,
                OnClickUpgrade,
                OnClickChange,
                OnClickPosition,
                OnClickBoost,
                OnClickSell,
                OnClickSetting,
                OnClickJob,
                OnClickComplete
            );

            OnRefresh(this);
        }

        public void SetButtonAni_Chnage_Setting()
        {
            if (!btnChange.gameObject.activeSelf)
            {
                disposableButton.Clear();
                btnChange.gameObject.SetActive(true);

                UnirxExtension.DateTimer(DateTime.Now.AddSeconds(0.1f)).Subscribe(_ =>
                    {
                        btnSetting.gameObject.SetActive(true);
                    })
                    .AddTo(disposableButton);
            }
        }
    }

    public class EditCellData
    {
        public Truck truck;
        public Truck targetTruck;
        public City targetCity;
        public EditCellType type;

        public enum EditCellType
        {
            TRUCK = 0,
            ADD_ROUTE,
            ADD_TRUCK,
            NEW_TRUCK
        }
    }
}