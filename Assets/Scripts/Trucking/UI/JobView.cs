using System;
using System.Collections.Generic;
using System.Linq;
using DatasTypes;
using DG.Tweening;
using EnhancedUI.EnhancedScroller;
using Newtonsoft.Json.Utilities;
using TMPro;
using Trucking.Ad;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.CellView;
using Trucking.UI.Guide;
using Trucking.UI.Mission;
using Trucking.UI.Popup;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI
{
    public class JobView : MonoSingleton<JobView>, IEnhancedScrollerDelegate
    {
        public GameObject truckInfoLayer;
        public GameObject truckCargoLayer;
        public GameObject cargoLayer;

        public TextMeshProUGUI txtFuelTime;
        public TextMeshProUGUI txtPathFuelTime;
        public TextMeshProUGUI txtOwnStorageCount;
        public TextMeshProUGUI txtTruckCargoCount;
        public TextMeshProUGUI txtTruckTime;
        public TextMeshProUGUI txtRefreshJob;
        public TextMeshProUGUI txtRefreshJobCash;
        public TextMeshProUGUI txtRefreshJobPrepareAd;
        public TruckCellViewMini selectedTruckCellViewMini;
        public TruckCellViewMini[] truckCellViewMinis;

        public Slider sliderFuel;
        public Slider sliderPathFuel;

        public Button btnDepart;
        public Button btnJobTooltip;
        public Button btnRefreshJob;
        public Button btnPrepareAd;
        public Button btnCashAd;

        public EnhancedScroller scrollerPlatform;
        public List<CargoCellData> _cargoCellDatas = new List<CargoCellData>();
        public EnhancedScroller scrollerTruck;
        public List<CargoCellData> _cargoCellTruckDatas = new List<CargoCellData>();
        public ReactiveProperty<Truck> truck = new ReactiveProperty<Truck>();
        public ReactiveProperty<City> city = new ReactiveProperty<City>();

        private CompositeDisposable _disposableTruck = new CompositeDisposable();
        private CompositeDisposable _disposableCity = new CompositeDisposable();
        private int selectedCellIndex;
        private CompositeDisposable disposableRefreshJob = new CompositeDisposable();
        private ReactiveProperty<DateTime> refreshJobDate = new ReactiveProperty<DateTime>();
        private Tweener tweener;
        private Tweener tweenerTruckCargo;

        private void Start()
        {
            scrollerPlatform.Delegate = this;
            scrollerPlatform.cellViewVisibilityChanged = CellViewVisibilityChanged;

            scrollerTruck.Delegate = this;
            scrollerTruck.cellViewVisibilityChanged = CellViewVisibilityChanged_Truck;

            btnJobTooltip.OnClickAsObservable().Subscribe(_ =>
            {
                AudioManager.Instance.PlaySound("sfx_button_main");
                Popup_GuideMain.Instance.Show(3);
            }).AddTo(this);

            btnPrepareAd.OnClickAsObservable().Subscribe(_ =>
            {
                AudioManager.Instance.PlaySound("sfx_button_main");

                Popup_Common.Instance.Show(Utilities.GetStringByData(30068), Utilities.GetStringByData(30069), false)
                    //.SetResource(RewardData.eType.cash, 1)
                    .SetLeft(Utilities.GetStringByData(20066))
                    .SetRightReward(RewardData.eType.cash, 1, () =>
                    {
                        if (UserDataManager.Instance.UseCash(Datas.cargoData[0].refresh_job_cash))
                        {
                            MissionManager.Instance.AddValue(QuestData.eType.refresh_joblist, 1);
                            refreshJobDate.Value = DateTime.Now.AddMinutes(Datas.cargoData[0].refresh_job_min);
                            SetRefreshJobDate();
                            city.Value.RefreshNotContractCargo();
                            MakeCityCargoCell();
                            scrollerPlatform.JumpToDataIndex(0);
                            SetRefreshAnimation();
                        }
                    });
            }).AddTo(this);

            btnCashAd.OnClickAsObservable().Subscribe(_ =>
            {
                AudioManager.Instance.PlaySound("sfx_button_main");

                Popup_Common.Instance.Show(Utilities.GetStringByData(30068), Utilities.GetStringByData(30069), false)
                    .SetLeft(Utilities.GetStringByData(20066))
                    .SetRightReward(RewardData.eType.cash, 1, () =>
                    {
                        if (UserDataManager.Instance.UseCash(Datas.cargoData[0].refresh_job_cash))
                        {
                            MissionManager.Instance.AddValue(QuestData.eType.refresh_joblist, 1);
                            refreshJobDate.Value = DateTime.Now.AddMinutes(Datas.cargoData[0].refresh_job_min);
                            SetRefreshJobDate();
                            city.Value.RefreshNotContractCargo();
                            MakeCityCargoCell();
                            scrollerPlatform.JumpToDataIndex(0);
                            SetRefreshAnimation();
                            UserDataManager.Instance.SaveData();
                        }
                    });
            }).AddTo(this);


            btnRefreshJob.OnClickAsObservable().Subscribe(_ =>
            {
                AudioManager.Instance.PlaySound("sfx_button_main");
                Popup_Loading.Instance.Show();

                AdManager.Instance.ShowRewardLoad(AdUnit.Job_Refresh).Subscribe(result =>
                {
                    GameManager.Instance.fsm.PopState();
                    FBAnalytics.FBAnalytics.LogJobListRefAdEvent(UserDataManager.Instance.data.lv.Value,
                        city.Value.name,
                        (int) result);

                    if (result == AdResult.Success)
                    {
                        MissionManager.Instance.AddValue(QuestData.eType.refresh_joblist, 1);
                        refreshJobDate.Value = DateTime.Now.AddMinutes(Datas.cargoData[0].refresh_job_min);
                        city.Value.RefreshNotContractCargo();
                        MakeCityCargoCell();
                        scrollerPlatform.JumpToDataIndex(0);
                        SetRefreshAnimation();
                        SetRefreshJobDate();
                        UserDataManager.Instance.SaveData();
                    }
                    else if (result == AdResult.NoFill)
                    {
                        refreshJobDate.Value = DateTime.Now.AddMinutes(Datas.cargoData[0].refresh_job_min);
                        SetRefreshJobDate();

                        AudioManager.Instance.PlaySound("sfx_require");
                        UIToastMassage.Instance.Show(30055);
                    }
                    else
                    {
                        AudioManager.Instance.PlaySound("sfx_require");
                        UIToastMassage.Instance.Show(30056);
                    }
                }).AddTo(this);
            }).AddTo(this);

            foreach (var cellViewMini in truckCellViewMinis)
            {
                cellViewMini.btnTruck.OnClickAsObservable().Subscribe(_ =>
                {
                    Truck minisTruck = cellViewMini.GetTruck();

                    if (minisTruck != null && minisTruck != truck.Value)
                    {
                        AudioManager.Instance.PlaySound("sfx_button_main");

                        if (minisTruck.completeCargos.Count > 0)
                        {
                            UIRewardManager.Instance.Show(cellViewMini.GetTruck().currentStation.Value);
                        }

                        if (minisTruck != truck.Value)
                        {
                            truck.Value = minisTruck;
                            truckCargoLayer.GetComponent<RectTransform>().anchoredPosition = new Vector2(300,
                                truckCargoLayer.GetComponent<RectTransform>().anchoredPosition.y);
                            truckCargoLayer.GetComponent<RectTransform>().DOAnchorPosX(-284, 0.3f);
                        }
                    }
                    else
                    {
                        AudioManager.Instance.PlaySound("sfx_require");
                    }
                }).AddTo(this);
            }

            btnDepart.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (truck.Value.pathRoad.Count == 0)
                    {
                        AudioManager.Instance.PlaySound("sfx_require");
                        UIToastMassage.Instance.Show(20050);
                    }
                    else if (truck.Value.PathFuel.Value > truck.Value.model.fuel.Value)
                    {
                        AudioManager.Instance.PlaySound("sfx_require");
                        Popup_NoFuel.Instance.Show(truck.Value, btnDepart.onClick.Invoke);
                    }
                    else if (truck.Value.pathRoad.Count > 0)
                    {
                        UnirxExtension.DateTimer(DateTime.Now.AddSeconds(1.5f)).Subscribe(t =>
                        {
                            AudioManager.Instance.PlaySound("sfx_mode_job_depart");
                        }).AddTo(this);

                        truck.Value.MoveStart();
                        GameManager.Instance.fsm.PopState();
                        HQView.Instance.Show(truck.Value);
                    }
                })
                .AddTo(this);

            city.Subscribe(ct =>
            {
                _disposableCity.Clear();
                _cargoCellDatas.Clear();
                selectedCellIndex = -1;
                scrollerPlatform.ClearAll();
                WorldMap.Instance.SetCusor(ct?.transform);

                foreach (var _ct in GameManager.Instance.cities)
                {
                    _ct.ui.SetCargoIcon(false);
                }

                if (ct != null)
                {
                    foreach (var road in GameManager.Instance.roads)
                    {
                        road.SetTruckColor(road.truck.Value != null);
                    }

                    Observable.CombineLatest(ct.CurrentCargoCount, ct.MaxCargo)
                        .Select(counts => $"{counts[0]} / {counts[1]}")
                        .Subscribe(txt => { txtOwnStorageCount.text = txt.ToString(); })
                        .AddTo(_disposableCity);

                    ct.cargos.ObsSomeChanged().BatchFrame().Subscribe(_count => { MakeCityCargoCell(); })
                        .AddTo(_disposableCity);

                    ct.trucks.ObsSomeChanged().Subscribe(trucks => { ShowTruckList(); }).AddTo(_disposableCity);

                    scrollerPlatform.ReloadData();

                    cargoLayer.GetComponent<RectTransform>().anchoredPosition = new Vector2(300,
                        cargoLayer.GetComponent<RectTransform>().anchoredPosition.y);
                    cargoLayer.GetComponent<RectTransform>().DOAnchorPosX(-5, 0.3f);

                    truckInfoLayer.GetComponent<RectTransform>().anchoredPosition =
                        new Vector2(truckInfoLayer.GetComponent<RectTransform>().anchoredPosition.x, 500);
                    truckInfoLayer.GetComponent<RectTransform>().DOAnchorPosY(720, 0.3f);
                }
            }).AddTo(this);

            truck.Subscribe(tr =>
            {
                truckInfoLayer.SetActive(tr != null);
                truckCargoLayer.SetActive(tr != null);

                _disposableTruck.Clear();
                _cargoCellTruckDatas.Clear();
                scrollerTruck.ClearAll();


                if (tr != null)
                {
                    ShowTruckList();
                    scrollerPlatform.RefreshActiveCellViews();

                    foreach (var _ct in GameManager.Instance.cities)
                    {
                        _ct.ui.SetCargoIcon(false);
                    }

                    List<City> linkedCities = GameManager.Instance.FindLinkedStationWithTruck(tr);

                    foreach (var city in GameManager.Instance.cities)
                    {
                        city.SetDepartSelect(linkedCities.Contains(city), true, tr.color);
                        city.ui.SetEditState(UICity.EditStateEnum.normal);
                    }


                    foreach (var road in GameManager.Instance.roads)
                    {
                        road.SetTruckColor(road.truck.Value != null, true);

                        if (road.truck.Value == tr)
                        {
                            road.to.ui.SetEditState(UICity.EditStateEnum.select, tr.color);
                            road.from.ui.SetEditState(UICity.EditStateEnum.select, tr.color);
                        }
                    }

                    Observable.CombineLatest(tr.CargoWeight, tr.MaxWeight,
                            (w, c) => $"{w} / {c}")
                        .Subscribe(txt => { txtTruckCargoCount.text = txt.ToString(); })
                        .AddTo(_disposableTruck);

                    tr.cargos.ObsSomeChanged().BatchFrame().Subscribe(_count =>
                    {
                        scrollerTruck.ReloadData(scrollerTruck.NormalizedScrollPosition);

                        foreach (var ct in GameManager.Instance.cities)
                        {
                            ct.ui.SetCargoIcon(false);
                        }

                        foreach (var cargo in tr.cargos)
                        {
                            cargo.to.Value.ui.SetCargoIcon(true);
                        }
                    }).AddTo(_disposableTruck);

                    tr.pathStation.ObsSomeChanged(false).Subscribe(_ =>
                    {
                        foreach (var ct in GameManager.Instance.cities)
                        {
                            ct.ui.SetDepartUndo(false);
                            ct.ui.SetDepartFocus(false);
                            ct.ui.SetStartIcon(city.Value == ct && tr.pathStation.Count == 1);
                        }


                        if (tr.pathStation.Count > 0)
                        {
                            City lastCity = tr.pathStation.Last();

                            if (tr.pathStation.Count > 1)
                            {
                                lastCity.ui.SetDepartUndo(true);
                            }

                            List<City> focuscities = new List<City>();

                            foreach (var road in lastCity.roads)
                            {
                                if (road.truck.Value == tr)
                                {
                                    if (road.from != lastCity)
                                    {
                                        focuscities.Add(road.from);
                                    }
                                    else if (road.to != lastCity)
                                    {
                                        focuscities.Add(road.to);
                                    }
                                }
                            }

                            foreach (var focuscity in focuscities)
                            {
                                focuscity.ui.SetDepartFocus(true);
                            }
                        }
                    }).AddTo(_disposableTruck);

                    tr.pathRoad.ObsSomeChanged()
                        .Subscribe(count =>
                        {
                            foreach (var road in GameManager.Instance.roads)
                            {
                                road.SetEditState(true);
                                road.SetMovingAnimation(false);
                                road.SetColorRoadAni(road.truck.Value == tr);
                            }

                            foreach (var pathDirection in tr.pathRoad)
                            {
                                pathDirection.road.trsStateColor.gameObject.SetActive(false);
                                pathDirection.road.SetMovingAnimation(true, pathDirection.isReverse);
                            }

                            TimeSpan leftTime = TimeSpan.FromSeconds(tr.GetTransitTime());
                            txtTruckTime.text = Utilities.GetTimeString(leftTime);

                            if (UserDataManager.Instance.GetBooster(UserDataManager.BoosterType.SPEED) > 0)
                            {
                                txtTruckTime.text = Utilities.GetTimeString(leftTime) + "\n" +
                                                    Utilities.GetStringByData(20036);
                                txtTruckTime.color = Utilities.GetColorByHtmlString("FFC813");
                            }
                            else
                            {
                                txtTruckTime.text = Utilities.GetTimeString(leftTime);
                                txtTruckTime.color = Color.white;
                            }

                            scrollerPlatform.RefreshActiveCellViews();
                            scrollerTruck.RefreshActiveCellViews();

                            SetFuelSlider(tr);
                        }).AddTo(_disposableTruck);

                    tr.MaxFuel.Subscribe(value => { SetFuelSlider(tr); }).AddTo(_disposableTruck);

                    tr.model.fuel.Subscribe(value =>
                    {
                        sliderFuel.value = value;
                        SetFuelSlider(tr);
                    }).AddTo(_disposableTruck);

                    tr.ResetPath();
                    tr.AddPath(city.Value);

                    foreach (var cargo in truck.Value.cargos)
                    {
                        CargoCellData cell = new CargoCellData();
                        cell.cargo = cargo;
                        cell.type = CargoCellData.CargoCellType.CARGO;
                        _cargoCellTruckDatas.Add(cell);
                    }

                    scrollerTruck.ReloadData();
//                    ShowTruckList(false);
                }
            }).AddTo(this);
        }

        public void Show(City _city, Truck _truck = null)
        {
            GameManager.Instance.selectedCity.Value = _city;
            AudioManager.Instance.PlaySound("sfx_mode_job_loop", true);
            GameManager.Instance.fsm.PushState(GameManager.Instance.jobState);


            gameObject.SetActive(true);

            city.Value = _city;

            if (_truck != null)
            {
                truck.Value = _truck;
            }
            else if (_city.trucks.Count > 0)
            {
                truck.Value = _city.trucks[0];
            }
            else
            {
                truck.Value = null;
            }


            SetRefreshJobDate();
            ShowTween(true);
        }

        public void ShowTween(bool isShow, Action callback = null)
        {
            tweener?.Kill();
            tweenerTruckCargo?.Kill();

            if (isShow)
            {
                tweener = GetComponent<RectTransform>().DOAnchorPosX(-5, 0.3f);
                truckCargoLayer.GetComponent<RectTransform>().anchoredPosition = new Vector2(300,
                    truckCargoLayer.GetComponent<RectTransform>().anchoredPosition.y);
                tweenerTruckCargo = truckCargoLayer.GetComponent<RectTransform>().DOAnchorPosX(-284, 0.3f)
                    .OnComplete(() =>
                    {
                        tweener = null;
                        tweenerTruckCargo = null;
                    });
            }
            else
            {
                tweenerTruckCargo = truckCargoLayer.GetComponent<RectTransform>().DOAnchorPosX(300, 0.3f)
                    .OnComplete(() =>
                    {
                        tweener = null;
                        tweenerTruckCargo = null;
                        callback?.Invoke();
                    });

                truckInfoLayer.GetComponent<RectTransform>().DOAnchorPosY(500, 0.3f);
                cargoLayer.GetComponent<RectTransform>().DOAnchorPosX(300, 0.3f);
            }
        }

        public void Close()
        {
            AudioManager.Instance.Stop("sfx_mode_job_loop");
            disposableRefreshJob.Clear();

            foreach (var city in GameManager.Instance.cities)
            {
                city.SetDepartSelect(false);
            }

            foreach (var road in GameManager.Instance.roads)
            {
                road.SetTruckColor(false);
            }

            ShowTween(false, () =>
            {
                truck.Value = null;
                city.Value = null;

                _disposableTruck.Clear();
                _disposableCity.Clear();


                gameObject.SetActive(false);
            });
//            truckCargoLayer.GetComponent<RectTransform>().DOAnchorPosX(300, 0.3f);
//            cargoLayer.GetComponent<RectTransform>().DOAnchorPosX(300, 0.3f);
//            truckInfoLayer.GetComponent<RectTransform>().DOAnchorPosY(500, 0.3f).OnComplete(() =>
//            {
//                truck.Value = null;
//                city.Value = null;
//
//                _disposableTruck.Clear();
//                _disposableCity.Clear();
//
//
//                gameObject.SetActive(false);
//            });
        }

        void SetRefreshJobDate()
        {
            txtRefreshJobCash.text = Datas.cargoData[0].refresh_job_cash.ToString();
            txtRefreshJobPrepareAd.text = Datas.cargoData[0].refresh_job_cash.ToString();
            disposableRefreshJob.Clear();
            Observable.EveryUpdate().Subscribe(_ =>
            {
                btnRefreshJob.gameObject.SetActive(refreshJobDate.Value <= DateTime.Now &&
                                                   AdManager.Instance.IsLoadedReward.Value);
                btnPrepareAd.gameObject.SetActive(refreshJobDate.Value > DateTime.Now &&
                                                  AdManager.Instance.IsLoadedReward.Value);
                btnCashAd.gameObject.SetActive(!AdManager.Instance.IsLoadedReward.Value);

                if (refreshJobDate.Value > DateTime.Now)
                {
                    txtRefreshJob.text = Utilities.GetTimeStringShort(refreshJobDate.Value - DateTime.Now);
                }
            }).AddTo(disposableRefreshJob);
        }

        public void RefreshSelectedTruck()
        {
            if (truck.Value != null)
            {
                Truck tmpTruck = truck.Value;
                List<City> tmpPathStations = tmpTruck.pathStation.ToList();
                List<Truck.PathDirection> tmpPathDirections = tmpTruck.pathRoad.ToList();
                truck.Value = null;
                truck.Value = tmpTruck;
                truck.Value.pathStation.Clear();
                truck.Value.pathStation.AddRange(tmpPathStations);
                truck.Value.pathRoad.AddRange(tmpPathDirections);
            }
        }

        void ShowTruckList()
        {
            for (int i = 0; i < truckCellViewMinis.Length; i++)
            {
                if (i < city.Value.trucks.Count)
                {
                    truckCellViewMinis[i].gameObject.SetActive(true);
                    truckCellViewMinis[i].SetTruck(city.Value.trucks[i]);
                    truckCellViewMinis[i].SetSelect(city.Value.trucks[i] == truck.Value);
                }
                else
                {
                    truckCellViewMinis[i].SetTruck(null);
                }
            }
        }


        void SetFuelSlider(Truck tr)
        {
            sliderFuel.maxValue = tr.MaxFuel.Value;
            sliderPathFuel.maxValue = tr.MaxFuel.Value;

            float pathFuel = tr.PathFuel.Value;
            sliderPathFuel.gameObject.SetActive(pathFuel > 0);

            if (pathFuel > 0)
            {
                sliderPathFuel.value = tr.model.fuel.Value;
                sliderFuel.value = tr.model.fuel.Value - pathFuel;
            }
            else
            {
                sliderFuel.value = tr.model.fuel.Value;
            }

            if (UserDataManager.Instance.GetBooster(UserDataManager.BoosterType.GAS) > 0)
            {
                txtFuelTime.text = Utilities.GetStringByData(20036);
                txtFuelTime.color = Utilities.GetColorByHtmlString("FFC813");
            }
            else
            {
                txtFuelTime.text = $"{(int) (tr.model.fuel.Value)}/{tr.MaxFuel.Value}";
                txtFuelTime.color = Color.white;
            }

            txtPathFuelTime.text = tr.PathFuel.Value == 0 ? "---" : $"{(int) (tr.PathFuel.Value)}";
            txtPathFuelTime.color =
                tr.model.fuel.Value < tr.PathFuel.Value &&
                UserDataManager.Instance.GetBooster(UserDataManager.BoosterType.GAS) == 0
                    ? Color.red
                    : Color.white;
        }

        void SetRefreshAnimation()
        {
            Observable.NextFrame().Subscribe(_ =>
            {
                for (int i = scrollerPlatform.StartCellViewIndex; i < _cargoCellDatas.Count; i++)
                {
                    EnhancedScrollerCellView cellView = scrollerPlatform.GetCellViewAtDataIndex(i);

                    if (cellView != null)
                    {
                        cellView.transform.localScale = new Vector3(1, 0, 1);
                        cellView.transform.DOScaleY(1, 0.3f)
                            .SetDelay((i - scrollerPlatform.StartCellViewIndex) * 0.05f);
                    }
                }
            });
        }

        void MakeCityCargoCell()
        {
            float pos = Mathf.Clamp01(scrollerPlatform.NormalizedScrollPosition);
            _cargoCellDatas.Clear();
            _cargoCellDatas.AddRange(city.Value.GetCargoCellList());
            scrollerPlatform.ReloadData(pos);
        }

        public void Button_City(City city)
        {
            if (city.ui.btnUndo.gameObject.activeSelf)
            {
                AudioManager.Instance.PlaySound("sfx_button_main");
                truck.Value.UndoPath();
            }
            else
            {
                Button_Focus(city);
            }
        }

        public void Button_Focus(City city)
        {
            if (truck.Value != null && city != null)
            {
                City lastCity = truck.Value.pathStation.Last();

                if (lastCity != city)
                {
                    Road rd = GameManager.Instance.FindRoad(lastCity, city);

                    if (rd != null)
                    {
                        if (rd.truck.Value == truck.Value)
                        {
                            AudioManager.Instance.PlaySound("sfx_button_main");

                            city.isFocus.Value = true;
                            truck.Value.AddPath(city, rd);

                            float dis = truck.Value.GetPathDistance();
                            float maxDistance = truck.Value.MaxTravelDistance.Value;

                            if (dis > maxDistance)
                            {
                                truck.Value.UndoPath();
                                Popup_Guide_NoFuel.Instance.Show();
                            }
                            else
                            {
                                rd.isFocus.Value = true;
                            }
                        }
                        else
                        {
                            AudioManager.Instance.PlaySound("sfx_require");
                            UIToastMassage.Instance.Show(30027);
                        }
                    }
                    else if (GameManager.Instance.FindLinkedStationWithTruck(truck.Value).Contains(city))
                    {
                        AudioManager.Instance.PlaySound("sfx_require");
                        UIToastMassage.Instance.Show(30053);
                    }
                    else
                    {
                        AudioManager.Instance.PlaySound("sfx_require");
                        UIToastMassage.Instance.Show(30027);
                    }
                }
            }
        }

        #region cellEvent

        public void OnCellRefresh(CargoCellView cell)
        {
            if (cell == null)
            {
                return;
            }

            if (cell.data.type == CargoCellData.CargoCellType.CARGO)
            {
                cell.imgCargoBack.color = Color.white;

                if (truck.Value != null)
                {
                    List<City> linkedCities = GameManager.Instance.FindLinkedStationWithTruck(truck.Value)
                        .FindAll(x => x == cell.data.cargo.to.Value);

                    if (linkedCities.Count > 0)
                    {
                        cell.targetCity.color = Utilities.GetColorByHtmlString("#FF9200");
                    }
                    else
                    {
                        cell.targetCity.color = Utilities.GetColorByHtmlString("#7F7F7F");
                        cell.imgCargoBack.color = Utilities.GetColorByHtmlString("#E6E6E6");
                    }
                }
            }
        }

        public void OnCellClick(CargoCellView cell)
        {
            if (cell == null)
            {
                return;
            }


            if (cell.data.type == CargoCellData.CargoCellType.CARGO)
            {
                if (truck.Value != null && truck.Value.CanAddCargo(cell.data.cargo))
                {
                    AudioManager.Instance.PlaySound("sfx_mode_job_select");

                    truck.Value.cargos.Add(cell.data.cargo);
                    _cargoCellTruckDatas.Add(cell.data);


                    city.Value.RemoveCargo(cell.data.cargo);
                    city.Value.AddCargo(Cargo.GetEmptySlot());
                    MakeCityCargoCell();
                    UserDataManager.Instance.SaveData();
                }
                else
                {
                    AudioManager.Instance.PlaySound("sfx_require");

                    if (truck.Value != null)
                    {
                        Popup_Common.Instance.Show(Utilities.GetStringByData(31403),
                                Utilities.GetStringByData(20178))
                            .SetCenter(Utilities.GetStringByData(20074), Popup_Common.ButtonColor.Blue,
                                () => { Popup_GuideMain.Instance.Show(5); });
                    }

                    cell.transform.DOShakePosition(0.3f, 3);
                }
            }
            else if (cell.data.type == CargoCellData.CargoCellType.MORE_JOB)
            {
                AudioManager.Instance.PlaySound("sfx_button_main");
                Popup_CityUpgrade.Instance.Show(city.Value);
            }
        }

        public void OnCell_RemoveClick(CargoCellView cell)
        {
            if (cell == null)
            {
                return;
            }

            AudioManager.Instance.PlaySound("sfx_mode_job_select");

            if (cell.data.type == CargoCellData.CargoCellType.CARGO)
            {
                int msgID = cell.data.cargo.model.isContract.Value ? 30054 : 30028;

                Popup_Common.Instance.Show(Utilities.GetStringByData(20064),
                        Utilities.GetStringByData(msgID), false)
                    .SetLeft(Utilities.GetStringByData(20066))
                    .SetRight(Utilities.GetStringByData(20065), Popup_Common.ButtonColor.Red, () =>
                    {
                        MissionManager.Instance.AddValue(QuestData.eType.delete_cargo, 1);
                        AudioManager.Instance.PlaySound("sfx_mode_job_remove");
                        city.Value.RemoveCargo(cell.data.cargo);
                        city.Value.AddCargo(Cargo.GetEmptySlot());
                        MakeCityCargoCell();
                        UserDataManager.Instance.SaveData();
                    });
            }
        }


        public void OnTruckCellRefresh(CargoCellView cell)
        {
            if (cell == null || truck.Value == null)
            {
                return;
            }

            if (cell.data.type == CargoCellData.CargoCellType.CARGO)
            {
                cell.imgCargoBack.color = Color.white;

                if (truck.Value != null)
                {
                    List<City> linkedCities = GameManager.Instance.FindLinkedStationWithTruck(truck.Value)
                        .FindAll(x => x == cell.data.cargo.to.Value);

                    if (linkedCities.Count > 0)
                    {
                        cell.targetCity.color = Utilities.GetColorByHtmlString("#FF9200");
                    }
                    else
                    {
                        cell.targetCity.color = Utilities.GetColorByHtmlString("#7F7F7F");
                        cell.imgCargoBack.color = Utilities.GetColorByHtmlString("#E6E6E6");
                    }
                }
            }
        }


        public void OnTruckCellClick(CargoCellView cell)
        {
            if (cell == null || truck.Value == null)
            {
                return;
            }

            if (cell.data.type == CargoCellData.CargoCellType.CARGO)
            {
                if (city.Value.CurrentCargoCount.Value < city.Value.MaxCargo.Value)
                {
                    AudioManager.Instance.PlaySound("sfx_mode_job_select");

                    if (!UserDataManager.Instance.data.hasTutorial.Value)
                    {
                        Cargo lastSlot = city.Value.cargos.Where(x => x.model.weight.Value == 0)
                            .OrderBy(x => x.model.refreshTime.Value).LastOrDefault();

                        if (lastSlot != null)
                        {
                            city.Value.cargos.Remove(lastSlot);
                        }
                    }

                    cell.data.cargo.RefreshDistance(city.Value);
                    city.Value.AddCargo(cell.data.cargo);
                    MakeCityCargoCell();

                    truck.Value.RemoveCargo(cell.data.cargo);
                    _cargoCellTruckDatas.Remove(cell.data);
                    scrollerTruck.ReloadData(scrollerTruck.NormalizedScrollPosition);
                    UserDataManager.Instance.SaveData();
                }
                else
                {
                    AudioManager.Instance.PlaySound("sfx_require");

                    Popup_Common.Instance.Show(Utilities.GetStringByData(31404),
                            Utilities.GetStringByData(20179))
                        .SetCenter(Utilities.GetStringByData(20074), Popup_Common.ButtonColor.Blue,
                            () =>
                            {
                                Popup_GuideMain.Instance.Show(6);
                                cell.transform.DOShakePosition(0.3f, 3);
                            });
                }
            }
        }

        public void OnTruckCell_RemoveClick(CargoCellView cell)
        {
            if (cell == null || truck.Value == null)
            {
                return;
            }

            AudioManager.Instance.PlaySound("sfx_mode_job_select");

            int cellIndex = cell.dataIndex;
            int msgID = cell.data.cargo.model.isContract.Value ? 30054 : 30028;

            Popup_Common.Instance.Show(Utilities.GetStringByData(20064),
                    Utilities.GetStringByData(msgID), false)
                .SetLeft(Utilities.GetStringByData(20066))
                .SetRight(Utilities.GetStringByData(20065), Popup_Common.ButtonColor.Red, () =>
                {
                    AudioManager.Instance.PlaySound("sfx_mode_job_remove");
                    MissionManager.Instance.AddValue(QuestData.eType.delete_cargo, 1);
                    CargoCellData data = _cargoCellTruckDatas[cellIndex];
                    truck.Value.RemoveCargo(data.cargo);
                    _cargoCellTruckDatas.Remove(data);
                    scrollerTruck.ReloadData(scrollerTruck.NormalizedScrollPosition);
                    UserDataManager.Instance.SaveData();
                });
        }

        #endregion


        #region scroll

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            if (scroller.cellViewVisibilityChanged == CellViewVisibilityChanged)
            {
                return _cargoCellDatas.Count;
            }

            if (scroller.cellViewVisibilityChanged == CellViewVisibilityChanged_Truck)
            {
                return _cargoCellTruckDatas.Count;
            }

            return 0;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 96;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            CargoCellView cellView = scroller.GetCellView(GameManager.Instance.cargoCellViewPrefab) as CargoCellView;
            cellView.gameObject.name = "Cell " + dataIndex;

            return cellView;
        }

        private void CellViewVisibilityChanged(EnhancedScrollerCellView cellView)
        {
            // cast the cell view to our custom view
            CargoCellView view = cellView as CargoCellView;

            // if the cell is active, we set its data, 
            // otherwise we will clear the image back to 
            // its default state

            if (cellView.active)
            {
                view.SetData(_cargoCellDatas[cellView.dataIndex],
                    truck.Value,
                    OnCellClick,
                    OnCell_RemoveClick,
                    OnCellRefresh);

                OnCellRefresh(view);
            }
        }


        private void CellViewVisibilityChanged_Truck(EnhancedScrollerCellView cellView)
        {
            // cast the cell view to our custom view
            CargoCellView view = cellView as CargoCellView;

            // if the cell is active, we set its data, 
            // otherwise we will clear the image back to 
            // its default state

            if (cellView.active)
            {
                view.SetData(_cargoCellTruckDatas[cellView.dataIndex],
                    truck.Value,
                    OnTruckCellClick,
                    OnTruckCell_RemoveClick,
                    OnTruckCellRefresh);

                OnTruckCellRefresh(view);
            }
        }

        #endregion
    }
}