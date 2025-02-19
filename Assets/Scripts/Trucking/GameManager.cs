using System;
using System.Collections.Generic;
using System.Linq;
using BitBenderGames;
using DatasTypes;
using DG.Tweening;
using EnhancedUI.EnhancedScroller;
using Facebook.Unity;
using LunarConsolePluginInternal;
using Trucking.Model;
using Trucking.MyComponent;
using Trucking.Common;
using Trucking.FSM;
using Trucking.Manager;
using Trucking.UI;
using Trucking.UI.Mission;
using Trucking.UI.Popup;
using Trucking.UI.ThreeDimensional;
using Trucking.UI.ToolTip;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using Utilities = Trucking.Common.Utilities;

namespace Trucking
{
    public sealed class GameManager : MonoSingleton<GameManager>
    {
        private const float inchToCm = 2.54f;
        private EventSystem eventSystem = null;
        private float dragThresholdCM = 0.5f;

        public Canvas canvas;
        public Canvas canvasWorld;

        public MobileTouchCamera camera;
        public Camera camera3DUI;
        public Camera cameraGray;
        public GrayscaleCamera grayscaleCamera;

        public Light light;

        [HideInInspector] public ReactiveProperty<City> selectedCity = new ReactiveProperty<City>();

        [HideInInspector] public List<City> cities = new List<City>();
        [HideInInspector] public ReactiveCollection<Road> roads = new ReactiveCollection<Road>();
        [HideInInspector] public ReactiveCollection<Truck> trucks = new ReactiveCollection<Truck>();


        [HideInInspector] public FiniteStateMachine<GameManager> fsm = new FiniteStateMachine<GameManager>();
        [HideInInspector] public WorldMapState worldMapState = new WorldMapState();
        [HideInInspector] public JobState jobState = new JobState();
        [HideInInspector] public HQState hqState = new HQState();
        [HideInInspector] public MapEditState mapEditState = new MapEditState();
        [HideInInspector] public AllocateTruckState allocateTruckState = new AllocateTruckState();
        [HideInInspector] public ChangeTruckState changeTruckState = new ChangeTruckState();
        [HideInInspector] public AddTruckState addTruckState = new AddTruckState();
        [HideInInspector] public SettingTruckState settingTruckState = new SettingTruckState();
        [HideInInspector] public ShopState shopState = new ShopState();
        [HideInInspector] public CraftState craftState = new CraftState();
        [HideInInspector] public AcheivementState acheivementState = new AcheivementState();
        [HideInInspector] public BoosterState boosterState = new BoosterState();
        [HideInInspector] public CityMenuState cityMenuState = new CityMenuState();

        public GameObject cityUIPrefab;
        public GameObject roadUIPrefab;
        public EnhancedScrollerCellView editCellViewPrefab;
        public EnhancedScrollerCellView cargoCellViewPrefab;

        public Transform trsCityUI;
        public Transform trsRoadUI;
        public Transform trsTrucks;
        public Transform trsUIObject3D;
        public Transform trs3DUIText;
        public Transform trsTouchObject;

        public Vector3 testVec3;


        public SpriteAtlas atlasCargo;
        public SpriteAtlas atlasUI;
        public SpriteAtlas atlasCity;
        public SpriteAtlas atlasMission;

        public Material matDirectionRoad;
        public Material matArrowRoad;
        public Material matWhiteRoad;
        public Material matClosedRoad_white;
        public Material matClosedRoad_Gray;

        private readonly ReactiveProperty<int> _routeCount = new ReactiveProperty<int>();
        public IReadOnlyReactiveProperty<int> RouteCount => _routeCount;

        private readonly ReactiveProperty<int> _roadCount = new ReactiveProperty<int>();
        public IReadOnlyReactiveProperty<int> RoadCount => _roadCount;

        private CompositeDisposable _compositeFB = new CompositeDisposable();

        protected override void OnDestroy()
        {
            base.OnDestroy();

            NotificationsManager.Instance = null;
            UIGuideQuest.Instance = null;
            UIMain.Instance = null;
            UICityMenu.Instance = null;
            UITutorial.Instance = null;
            UIToastMassage.Instance = null;
            TouchObjectManager.Instance = null;
            TouchObject_Plane.Instance = null;
            TouchObject_BuoyGroup.Instance = null;
            UICloudManager.Instance = null;
            WorldMap.Instance = null;
            UIBlockTouch.Instance = null;
            TruckBoosterScene.Instance = null;
            Popup_ToolTip_Boost.Instance = null;
            Popup_ToolTip_Level.Instance = null;
        }

        private void Start()
        {
            Debug.Log("Start  =======");

            //LoadBoostScene();
            canvas.gameObject.SetActive(false);

            if (SceneManager.sceneCount >= 2)
            {
                LoadingScene.Instance.SetExtraPercent(() =>
                {
                    canvas.gameObject.SetActive(true);

                    SceneManager.UnloadSceneAsync("Loading").ObserveEveryValueChanged(x => x.isDone).Subscribe(done =>
                    {
                        if (done)
                        {
                            LoadingScene.Instance.gameObject.SetActive(false);
                            canvas.gameObject.SetActive(true);
                        }
                    });
                });
            }
            else
            {
                canvas.gameObject.SetActive(true);
            }

            Debug.Log("Start End =======");
        }

        private void Awake()
        {
            camera?.GetComponent<TouchInputController>().AddMaskLayer("3D on UI");
            camera?.GetComponent<TouchInputController>().AddMaskLayer("UI");

            Init();

            canvas.gameObject.SetActive(false);
            Debug.Log("Awake End =======");
        }


        // Use this for initialization
        public void Init()
        {
            Debug.Log("Init  =======");

            SetQualitySetting();
            SetDragThreshold();

            // 1.
            InitCity();
            InitRoad();

            // 2.
            bool hasSaveData = UserDataManager.Instance.Load();
            AudioManager.Instance.SoundMuted(!UserDataManager.Instance.data.settingSound.Value);
            AudioManager.Instance.MusicMuted(!UserDataManager.Instance.data.settingMusic.Value);

            if (UserDataManager.Instance.data.hasTutorial.Value)
            {
                UserDataManager.Instance.Init();
            }

            if (!hasSaveData || UserDataManager.Instance.data.hasTutorial.Value)
            {
                UserDataManager.Instance.SaveData();
            }
            else
            {
                InitTrucks();
            }

            LoadCities();
            LoadRoads();
            LoadData();

            InitCitiesCargo(!hasSaveData);

            UIGuideQuest.Instance.Init();
            UIMain.Instance.Init();
            UICityMenu.Instance.gameObject.SetActive(true);


            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyUp(KeyCode.Escape))
                .Subscribe(_ =>
                {
                    if (!UserDataManager.Instance.data.hasTutorial.Value)
                    {
                        ((GameState) fsm.GetCurrentState()).OnBackButton(this);
                    }
                })
                .AddTo(this);


            Observable.EveryApplicationFocus()
                .Subscribe(SetApplicationFocus).AddTo(this);

            DOTween.Init();
            I2.Loc.LocalizationManager.CurrentLanguage = "English";
            UICityMenu.Instance.Init();
            TouchObjectManager.Instance.Init();

            UITutorial.Instance.Init();
            UICloudManager.Instance.Init();
            TouchObject_Plane.Instance.gameObject.SetActive(true);
            UIToastMassage.Instance.gameObject.SetActive(false);
            Popup_Option.Instance.SetMaxFps(UserDataManager.Instance.data.maxFps.Value);

            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (FB.IsInitialized)
                {
                    NotificationsManager.Instance.Init();
                    NotificationsManager.Instance.CheckGetLastNotification();
                    _compositeFB.Clear();
                }
            }).AddTo(_compositeFB);


            trucks.ObsSomeChanged().SelectMany(trs => trs.Select(tr => tr.model.hasRoute).CombineLatest())
                .Select(hasRoutes => hasRoutes.Count(x => x)).Subscribe(count => { _routeCount.Value = count; })
                .AddTo(this);

            roads.ObsSomeChanged().SelectMany(rds => rds.Select(rd => rd.truck).CombineLatest())
                .Select(rds => rds.Count(x => x != null)).Subscribe(count => { _roadCount.Value = count; })
                .AddTo(this);

            Observable.CombineLatest(RouteCount,
                GuideQuestManager.Instance.model.index,
                (count, questIndex) => count).Subscribe(count =>
            {
                MissionManager.Instance.SetValue(QuestData.eType.route, count);
            }).AddTo(this);

            Observable.CombineLatest(RoadCount,
                GuideQuestManager.Instance.model.index,
                (count, questIndex) => count).Subscribe(count =>
            {
                MissionManager.Instance.SetValue(QuestData.eType.road, count);
            }).AddTo(this);

            Observable.CombineLatest(UserDataManager.Instance.data.truckUpgradeCount,
                GuideQuestManager.Instance.model.index,
                (count, questIndex) => count).Subscribe(count =>
            {
                MissionManager.Instance.SetValue(QuestData.eType.truck_upgrade, count);
            }).AddTo(this);

            Observable.CombineLatest(UserDataManager.Instance.data.cityUpgradeCount,
                GuideQuestManager.Instance.model.index,
                (count, questIndex) => count).Subscribe(count =>
            {
                MissionManager.Instance.SetValue(QuestData.eType.city_upgrade, count);
            }).AddTo(this);


            WorldMap.Instance.SetEditView(false);
            UIBlockTouch.Instance.Close();

            if (!UserDataManager.Instance.data.hasTutorial.Value)
            {
                Truck truck = trucks
                    .OrderByDescending(x => x.model.hasRoute.Value)
                    .ThenByDescending(x => x.model.state.Value == TruckModel.State.Wait)
                    .ThenByDescending(x => x.model.state.Value == TruckModel.State.Move)
                    .ThenBy(x => x.GetLeftTime())
                    .ThenBy(x => x.GetMaxRefuelTime())
                    .ToList().FirstOrDefault();

                if (truck != null)
                {
                    WorldMap.Instance.SetCamera(truck.transform.position, true);
                }
            }


            fsm.Configure(this, worldMapState);
//             UIBlockTouch.Instance.SetAppStart();
            Debug.Log("Init End =======");
        }


        public void InitCity()
        {
            UserDataManager.Instance.data.cityData.Clear();

            foreach (Transform child in WorldMap.Instance.trsStations)
            {
                City city = child.GetComponent<City>();
                city.gameObject.SetActive(true);
                cities.Add(city);
                city.roads.Clear();
                city.connections.Clear();

                List<CityData> cityData = Datas.cityData.ToArray().ToList();
                city.data = cityData.Find(x => x.name == city.name);
                city.name = city.data.name;

                Debug.Assert(city.data != null);

                if (city.data == null)
                {
                    Debug.Log("city.data null : " + city.name);
                }

                UserDataManager.Instance.data.cityData.Add(new CityModel(city.data.id));
            }

            Debug.LogWarning(
                $"InitCity id : {GetInstanceID()}, WorldMap id : {WorldMap.Instance.GetHashCode()}, citie[0] id : {cities[0].GetHashCode()}");
        }

        public void InitRoad()
        {
            UserDataManager.Instance.data.roadData.Clear();

            foreach (Transform child in WorldMap.Instance.trsRoads)
            {
                Road road = child.GetComponent<Road>();
                roads.Add(road);

                string[] stringSeparators = new string[] {" -> "};
                string[] cityName = road.name.Split(stringSeparators, StringSplitOptions.None);

                road.from = cities.Find(x => x.name == cityName[0]);
                road.to = cities.Find(x => x.name == cityName[1]);

                Debug.Assert(road.from != null && road.to != null);

                if (road.from == null || road.to == null)
                {
                    Debug.Log("road Fail");
                    Debug.Log("from : " + cityName[0] + "      to : " + cityName[1]);
                }

                road.from.roads.Add(road);
                road.from.connections.Add(road.to);

                road.to.roads.Add(road);
                road.to.connections.Add(road.from);

                UserDataManager.Instance.data.roadData.Add(new RoadModel());
            }
        }

        public void LoadData()
        {
            GuideQuestManager.Instance.SetModel(UserDataManager.Instance.data.guideQuestData.Value);
            LevelMissionManager.Instance.SetModel(UserDataManager.Instance.data.levelMissionData.Value);
            NewDailyMissionManager.Instance.SetModel(UserDataManager.Instance.data.newDailyMissionData.Value);
            DeliveryServiceManager.Instance.SetModel(UserDataManager.Instance.data.deliveryData.Value);
            OperationManager.Instance.SetModel(UserDataManager.Instance.data.operationData.Value);
            FreeCashManager.Instance.SetModel(UserDataManager.Instance.data.freeCashData.Value);
            AchievementManager.Instance.SetModel(UserDataManager.Instance.data.achievementData.Value);
//            EventTruckManager.Instance.SetModel(UserDataManager.Instance.data.eventTruckShopData.Value);
            TouchObjectManager_Plane.Instance.SetModel(UserDataManager.Instance.data.planeObjectData.Value);
        }

        public void LoadCities()
        {
            Utilities.RemoveAllChildren(trsCityUI);

            for (int i = 0; i < cities.Count; i++)
            {
                cities[i].SetModel(UserDataManager.Instance.data.cityData[i]);
                cities[i].ui = Instantiate(cityUIPrefab, trsCityUI).GetComponent<UICity>();
                cities[i].ui.SetData(cities[i]);
                cities[i].gameObject.SetActive(true);
            }

            cityUIPrefab.gameObject.SetActive(false);
            WorldMap.Instance.Init(cities);

            Debug.LogWarning(
                $"LoadCities id : {GetInstanceID()}, WorldMap id : {WorldMap.Instance.GetHashCode()}, cities id : {cities.GetHashCode()}, cities[0] : {cities[0].GetHashCode()}");
        }

        public void LoadRoads()
        {
            Utilities.RemoveAllChildren(trsRoadUI);

            for (int i = 0; i < roads.Count; i++)
            {
                roads[i].ui = Instantiate(roadUIPrefab, trsRoadUI).GetComponent<UIRoad>();
                roads[i].SetModel(UserDataManager.Instance.data.roadData[i]);
                //            roads[i].roadData = Datas.roadData.ToArray().ToList().Find(x => x.name.Equals(roads[i].name));
                roads[i].ui.SetData(roads[i]);
                roads[i].SetEditState(false);

                Truck truck = trucks.ToList().Find(x => x.model.birthID == roads[i].model.truckBirthID.Value);

                if (truck != null)
                {
                    roads[i].truck.Value = truck;
                }
            }

            //
            //
            //        Road w_to_n = FindRoad("WASHINGTON D.C.", "NEW YORK");
            //        Transform trsParticle = w_to_n.transform.GetChild(4);
            //        Transform trsArrow = w_to_n.transform.GetChild(3);
            //        SplineMesh splineMesh = w_to_n.GetComponent<SplineMesh>();
            //
            for (int i = 0; i < roads.Count; i++)
            {
                //            SplineMesh splineMesh = roads[i].trsStateColor.GetComponent<SplineMesh>();
                //            SplineMesh.Channel channel = splineMesh.GetChannel(0); 
                //            
                //            float lastZ = channel.minScale.z;
                //            Vector3 scale = channel.minScale;
                //            scale = new Vector3(0.6f, 1, 1);
                //            scale += Vector3.forward * lastZ;
                //            channel.minScale = scale;


                //            channel.minScale = new Vector2(0.6f, 1);
                //            float lastZ = channel.minScale.z;
                //            Vector3 scale = channel.minScale;
                //            scale += Vector3.forward * lastZ;
                //            channel.minScale = scale;

                //            splineMesh.GetChannel(0).minScale = new Vector2(0.6f, 1);

                //            roads[i].trsStateArrow = roads[i].transform.GetChild(3);
                //            roads[i].trsStateParticle = roads[i].transform.GetChild(4);
                //            
                //            roads[i].trsStateArrow.GetComponent<SplineMesh>().GetChannel(0).count = 1;
                //            
                //            roads[i].trsStateColor.name = "road_color";
                //            roads[i].trsStateClosed.name = "road_closed";
                //            roads[i].trsStateDirection.name = "road_direction";
                //            roads[i].trsStateArrow.name = "road_arrow";
                //            roads[i].trsStateParticle.name = "road_particle";
            }
        }

        void InitTrucks()
        {
            for (int i = 0; i < UserDataManager.Instance.data.truckData.Count; i++)
            {
                Truck truck = Truck.MakeWithSavedData(UserDataManager.Instance.data.truckData[i]
                    , trsTrucks);
                trucks.Add(truck);
            }
        }

        void InitCitiesCargo(bool isFirst)
        {
            if (isFirst)
            {
                for (int i = 0; i < cities.Count; i++)
                {
                    if (cities[i].IsOpen())
                    {
                        cities[i].RefreshAllCargo();
                    }
                }

                UserDataManager.Instance.SaveData();
            }
            else
            {
                for (int i = 0; i < cities.Count; i++)
                {
                    if (cities[i].IsOpen())
                    {
                        cities[i].RefreshEmptyCargo();
                    }
                }
            }

            for (int i = 0; i < cities.Count; i++)
            {
                if (!cities[i].IsOpen() && cities[i].data.mega)
                {
                    cities[i].model.state.Value = CityModel.State.Lock;
                }
            }
        }


        public City FindStation(string name)
        {
            if (name == null)
            {
                return null;
            }

            return cities.Find(x => x.data.name.Equals(name.ToUpper()));
        }

        public City FindStation(int id)
        {
            return cities.Find(x => x.data.id == id);
        }

        public Road FindRoad(City from, City to)
        {
            return roads.ToList().Find(x => (x.from == from && x.to == to) || (x.from == to && x.to == from));
        }

        public Road FindRoad(int fromId, int toId)
        {
            City stFrom = FindStation(fromId);
            City stTo = FindStation(toId);

            return FindRoad(stFrom, stTo);
        }

        public Road FindRoad(string from, string to)
        {
            City stFrom = cities.Find(x => x.name.Equals(from.ToUpper()));
            City stTo = cities.Find(x => x.name.Equals(to.ToUpper()));

            return FindRoad(stFrom, stTo);
        }


        public List<City> FindLinkedStationWithTruck(Truck truck)
        {
            List<Road> fillteredRoads = roads.ToList().FindAll(x => x.truck.Value == truck);
            List<City> linkedStations = new List<City>();

            foreach (var road in fillteredRoads)
            {
                if (!linkedStations.Contains(road.to))
                {
                    linkedStations.Add(road.to);
                }

                if (!linkedStations.Contains(road.from))
                {
                    linkedStations.Add(road.from);
                }
            }

            return linkedStations;
        }

        public void PushPopupState<T>(Popup_Base<T> popup) where T : MonoSingleton<T>
        {
            fsm.PushState(new PopupState<T>(popup));
        }

        void SetQualitySetting()
        {
            QualitySettings.antiAliasing = 0; // 0 : 안함 / 2 : x2 / 4 : x4 인것에 주의.

            QualitySettings.masterTextureLimit = 0; // 텍스쳐 정상사이즈
            //        QualitySettings.masterTextureLimit = 1; // 반사이즈

            QualitySettings.vSyncCount = 0; // 수직동기화
            Application.targetFrameRate = 50; // 프레임

            AudioListener.volume = 1f; // 볼륨 0~1f
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            Screen.fullScreen = true;
        }

        private void SetDragThreshold()
        {
            eventSystem = FindObjectOfType<EventSystem>();

            if (eventSystem != null)
            {
                eventSystem.pixelDragThreshold = (int) (dragThresholdCM * Screen.dpi / inchToCm);
            }
        }

        private void OnInputClick(Vector3 clickScreenPosition, bool isDoubleClick, bool isLongTap)
        {
            Debug.Log("OnInputClick(clickScreenPosition: " + clickScreenPosition + ", isDoubleClick: " + isDoubleClick +
                      ", isLongTap: " + isLongTap + ")");
        }

        private void LateUpdate()
        {
            camera3DUI.orthographicSize
                = camera.Cam.orthographicSize;

            cameraGray.orthographicSize
                = camera.Cam.orthographicSize;
        }

        private int clickCount = 0;
        private DateTime clickTime;

        public void ClickStation(string cityName)
        {
            ((GameState) fsm.GetCurrentState()).OnClickStation(this, FindStation(cityName));
        }

        public void OnPickItem(RaycastHit hitInfo)
        {
            if (fsm.GetCurrentState() == cityMenuState)
            {
                ((GameState) fsm.GetCurrentState()).OnClickStation(this, null);
                return;
            }

            Truck truck = hitInfo.transform.GetComponentInParent<Truck>();
            City city = hitInfo.transform.GetComponent<City>();
            Road road = hitInfo.transform.GetComponent<Road>();
            bool isTouchObjcet = false;

            if (hitInfo.transform.name.Contains("touch_object"))
            {
                isTouchObjcet = true;
                TouchObject_Plane.Instance.Touch(hitInfo.transform.localPosition);
            }
            else if (hitInfo.transform.name.Contains("buoy"))
            {
                isTouchObjcet = true;
                hitInfo.transform.GetComponentInParent<TouchObject_Buoy>().Touch();
            }
            else if (truck != null && truck.model.state.Value == TruckModel.State.Wait)
            {
                city = truck.currentStation.Value;
                ((GameState) fsm.GetCurrentState()).OnClickStation(this, city);
            }
            else if (city != null)
            {
                ((GameState) fsm.GetCurrentState()).OnClickStation(this, city);
            }
            else if (road != null)
            {
                ((GameState) fsm.GetCurrentState()).OnClickRoad(this, road);
            }

            if (city == null && road == null && truck == null && !isTouchObjcet)
            {
                if (UICityMenu.Instance.gameObject.activeSelf)
                {
                    AudioManager.Instance.PlaySound("sfx_button_cancle");
                }

                ((GameState) fsm.GetCurrentState()).OnClickStation(this, city);
            }
        }

        public void OnPickItemDoubleClick(RaycastHit hitInfo)
        {
        }

        public void RemakeUIObject3D(UIObject3D obj, Transform trs)
        {
            Animator animator = trs.GetComponent<Animator>();

            if (animator != null)
            {
                animator.enabled = false;
            }

            Utilities.ChangeLayers(trs.gameObject, "UIObject3D");
            trs.SetParent(trsUIObject3D);
            obj.EnableCameraLight = false;
            obj.LightIntensity = 0.0f;
            obj.ImageColor = Color.white;
            obj.ObjectPrefab = trs;
        }

        public void ClearUIObject3D()
        {
            //         Utilities.RemoveAllChildren(trsUIObject3D);
        }

        public Sprite GetRewardImage(RewardModel rewardModel)
        {
            return GetRewardImage(rewardModel.type.Value, rewardModel.index.Value);
        }

        public Sprite GetRewardImage(RewardData.eType type, int index = 0)
        {
            string temp;

            switch (type)
            {
                case RewardData.eType.gold:
                    return atlasUI.GetSprite("goods_gold");
                case RewardData.eType.cash:
                    return atlasUI.GetSprite("goods_cash");
                case RewardData.eType.exp:
                    return atlasUI.GetSprite("main_icon_xp_02");
                case RewardData.eType.random_box:
                    return atlasUI.GetSprite("random_box_0" + Datas.randomBox[index].model);
                case RewardData.eType.material:
                    switch (index)
                    {
                        case 0:
                            return atlasUI.GetSprite("goods_metal");
                        case 1:
                            return atlasUI.GetSprite("goods_steel");
                        case 2:
                            return atlasUI.GetSprite("goods_oiltong");
                        case 3:
                            return atlasUI.GetSprite("goods_spring");
                    }

                    break;
                case RewardData.eType.parts:
                    switch (index)
                    {
                        case 0:
                            return atlasUI.GetSprite("goods_teeth");
                        case 1:
                            return atlasUI.GetSprite("goods_frame");
                        case 2:
                            return atlasUI.GetSprite("goods_tire");
                        case 3:
                            return atlasUI.GetSprite("goods_suspension");
                    }

                    break;
                case RewardData.eType.truck_pc:
                    return atlasUI.GetSprite("100009");
                case RewardData.eType.crate:
                    return atlasUI.GetSprite("goods_gacha_" + (index + 1));
                case RewardData.eType.booster:
                    switch (index)
                    {
                        case 0:
                            return atlasUI.GetSprite("shop_boost_xp");
                        case 1:
                            return atlasUI.GetSprite("shop_boost_gold");
                        case 2:
                            return atlasUI.GetSprite("shop_boost_speed");
                        case 3:
                            return atlasUI.GetSprite("shop_boost_oil");
                    }

                    break;
                case RewardData.eType.truck_id:
                    return atlasUI.GetSprite("main_icon_r_04");
            }

            return null;
        }

        public Sprite GetCargoSprite(int id)
        {
            return atlasCargo.GetSprite("cago_item_" + id.ToString("D3"));
        }

        public Sprite GetQuestIcon(QuestModel questModel)
        {
            QuestData questData = QuestManager.GetQuest(questModel.qid.Value);

            switch (questData.quest_icon)
            {
                // quest_icon: 1(일반 아이콘), 2(운송 아이콘), 3(factor의 리워드 아이콘), 4(factor의 화물 아이콘)
                case 1:
                    return atlasCargo.GetSprite("cago_item_general");
                case 2:
                    return atlasCargo.GetSprite("cago_item_transport");
                case 3:
                    return GetRewardImage((RewardData.eType) questModel.fid.Value);
                case 4:
                    return GetCargoSprite(questModel.fid.Value);
            }

            return null;
        }

        void SetApplicationFocus(bool focus)
        {
            if (!focus)
            {
                if (UserDataManager.Instance.data.truckData.Count > 0)
                {
                    UserDataManager.Instance.SaveData();
                }
            }
            else if (!Application.isEditor)
            {
                double restTime = (DateTime.Now - UserDataManager.Instance.data.savedTime.Value).TotalSeconds;

                foreach (var truck in trucks)
                {
                    if (truck.model.state.Value == TruckModel.State.Wait)
                    {
                        truck.AddFuel((float) restTime);
                    }
                }
            }
        }


//        void LoadBoostScene()
//        {
//            TruckBoosterScene.Instance.baseObject.SetActive(true);
////            string boostSceneName = "TruckBoosterScene";
////            SceneManager.LoadSceneAsync(boostSceneName, LoadSceneMode.Additive).AsObservable().Subscribe(_ =>
////            {
////                TruckBoosterScene.Instance.baseObject.SetActive(false);
////            });
//        }

        public void SetBoostScene(Truck tr, Action end)
        {
            string boostSceneName = "TruckBoosterScene";
            fsm.PushState(boosterState);
            camera.gameObject.SetActive(false);

//            Scene boostScene = SceneManager.GetSceneByName(boostSceneName);
//            SceneManager.SetActiveScene(boostScene);
            TruckBoosterScene.Instance.baseObject.SetActive(true);
            GameObject obj = Truck.GetTruckPrefab(tr.data, TruckBoosterScene.Instance.baseObject.transform).gameObject;
            Utilities.ChangeLayers(obj, "booster");
            obj.gameObject.SetActive(true);
            obj.GetComponent<Animator>().Play("Truck_Booster");
            AudioManager.Instance.PlaySound("sfx_truck_horn");

            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(1.3f)).ObserveOnMainThread().Subscribe(___ =>
            {
                Utilities.RemoveObject(obj);
                TruckBoosterScene.Instance.baseObject.SetActive(false);
                camera.gameObject.SetActive(true);
//                Scene gameScene = SceneManager.GetSceneByName("GameScene");
//                SceneManager.SetActiveScene(gameScene);
                fsm.PopState();
                end?.Invoke();
            });
        }


        public LevelData GetRouteCountLevelData(int lv = 0)
        {
            if (lv == 0)
            {
                return Datas.levelData.ToArray().FirstOrDefault(x => x.level == RouteCount.Value + 1);
            }

            return Datas.levelData.ToArray().FirstOrDefault(x => x.level == lv);
        }
    }
}