using System;
using System.Linq;
using DatasTypes;
using TMPro;
using Trucking.Common;
using Trucking.Manager;
using Trucking.Model;
using Trucking.MyComponent;
using Trucking.UI.Craft;
using Trucking.UI.Mission;
using Trucking.UI.Popup;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI
{
    public class UICityMenu : MonoSingleton<UICityMenu>
    {
        public GameObject buttonGroup;
        public Button btnUpgrade;
        public Button btnJob;
        public Button btnJob_NoTruck;
        public Button btnMission;
        public Button btnParts;
        public Image imgMissionIcon;
        public TextMeshProUGUI txtMax;
        public GameObject upgradeAlert;
        public GameObject jobAlert;
        public GameObject missionAlert;
        public GameObject partsAlert;
        public Slider slider;
        public TextMeshProUGUI txtSlider;
        public Button btnBoost;
        public GameObject goProduction;

        public Button btnPart;
        public TextMeshProUGUI txtPart;
        public TextMeshProUGUI txtPartCount;
        public Image imgPart;
        public TextMeshProUGUI txtMaterial;
        public Button btnMaterial;
        public Button btnMaterial2;
        public Image imgMaterial;
        public Image imgMaterial2;
        public TextMeshProUGUI txtGold;
        public Image imgGold;
        public Image imgGold2;
        public ObservableEventTrigger triggerPart;
        public DragObject dragObject;
        public GameObject handAni;
        public GameObject boxNotiDelivery;
        public GameObject boxNotiPart;

        public ReactiveProperty<City> city = new ReactiveProperty<City>();
        private CompositeDisposable _disposableCity = new CompositeDisposable();

        public void Init()
        {
            dragObject.canvas = GameManager.Instance.canvas;

            btnUpgrade.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    Popup_CityUpgrade.Instance.Show(city.Value);
                }).AddTo(this);

            btnJob.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    FBAnalytics.FBAnalytics.LogTruckJobEvent("City Icon");
                    City ct = city.Value;
                    GameManager.Instance.fsm.PopState();

                    if (GameManager.Instance.fsm.GetCurrentState() == GameManager.Instance.hqState)
                    {
                        GameManager.Instance.fsm.PopState();

                        if (HQView.Instance.selectedTruck.Value != null)
                        {
                            Truck tr = HQView.Instance.selectedTruck.Value;

                            if (tr.model.state.Value == TruckModel.State.Wait && tr.currentStation.Value == ct)
                            {
                                JobView.Instance.Show(ct, tr);
                            }
                            else
                            {
                                JobView.Instance.Show(ct);
                            }
                        }
                        else
                        {
                            JobView.Instance.Show(ct);
                        }
                    }
                    else
                    {
                        JobView.Instance.Show(ct);
                    }
                }).AddTo(this);

            btnJob_NoTruck.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    btnJob.onClick.Invoke();
                }).AddTo(this);


            btnMission.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");

                    if (NewDailyMissionManager.Instance.isNew.Value)
                    {
                        Popup_NewDailyMissionGuide.Instance.Show();
                    }
                    else
                    {
                        Popup_NewDailyMission.Instance.Show();
                    }
                }).AddTo(this);

            btnParts.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    CraftView.Instance.Show(CraftView.Type.Parts, city.Value.productIndex);
                }).AddTo(this);

            btnBoost.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");

                    City _city = city.Value;
                    Popup_Common popup = null;
                    long boostCost = CraftView_Parts.GetBoostCost(_city);

                    if (_city.model.state.Value == CityModel.State.Craft)
                    {
                        popup = Popup_Common.Instance.Show(Utilities.GetStringByData(20148),
                            Utilities.GetStringByData(20149));
                    }
                    else if (_city.model.state.Value == CityModel.State.Upgrade)
                    {
                        popup = Popup_Common.Instance.Show(Utilities.GetStringByData(20150),
                            Utilities.GetStringByData(20151));
                    }

                    popup?.SetCenterReward(RewardData.eType.cash, boostCost, () =>
                    {
                        if (UserDataManager.Instance.UseCash(boostCost))
                        {
                            _city.model.productTime.Value = DateTime.Now;
                        }
                    });
                }).AddTo(this);

            btnPart.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    Popup_GuideMain.Instance.Show(13);
                }).AddTo(this);

            dragObject.GetComponent<Button>().OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    Popup_GuideMain.Instance.Show(13);
                }).AddTo(this);


            city.Subscribe(ct =>
            {
                _disposableCity.Clear();

                if (ct != null)
                {
                    transform.localPosition = new Vector3(ct.transform.localPosition.x,
                        ct.transform.localPosition.z,
                        -20);

                    imgMaterial2.gameObject.SetActive(false);
                    imgGold2.gameObject.SetActive(false);
                    dragObject.gameObject.SetActive(true);
                    btnUpgrade.gameObject.SetActive(ct.IsOpen());
                    btnJob.gameObject.SetActive(ct.IsOpen());
                    goProduction.SetActive(ct.IsMega());

                    UserDataManager.Instance.data.lv.Subscribe(lv =>
                    {
                        boxNotiPart.SetActive(lv <= 5 && ct.IsMega());
                        boxNotiDelivery.SetActive(lv <= 5 && !ct.IsMega());
                    }).AddTo(_disposableCity);

                    if (ct.IsMega())
                    {
                        imgPart.sprite = GameManager.Instance.GetRewardImage(RewardData.eType.parts, ct.partIndex);
                        imgMaterial.sprite =
                            GameManager.Instance.GetRewardImage(RewardData.eType.material, ct.matIndex);
                        imgMaterial2.sprite =
                            GameManager.Instance.GetRewardImage(RewardData.eType.material, ct.matIndex);

                        Observable.CombineLatest(ct.model.productLevel,
                            UserDataManager.Instance.data.cityMaterials[ct.matIndex],
                            UserDataManager.Instance.data.gold,
                            (pd_lv, mat, gold) => (pd_lv, mat, gold)).Subscribe(value =>
                        {
                            txtPart.text =
                                Utilities.GetTimeStringShort(TimeSpan.FromSeconds(ct.partData.pd_time[value.Item1]));
                            txtPartCount.text = ct.partData.parts_count[value.Item1].ToString();
                            txtMaterial.text =
                                $"{value.Item2}/{ct.partData.material_count[value.Item1]}";
                            txtGold.text = ct.partData.pd_gold[value.Item1].ToString();

                            if (value.Item2 <
                                ct.partData.material_count[value.Item1])
                            {
                                txtMaterial.color = Color.red;
                            }
                            else
                            {
                                txtMaterial.color = Color.white;
                            }

                            if (value.Item3 <
                                ct.partData.pd_gold[value.Item1])
                            {
                                txtGold.color = Color.red;
                            }
                            else
                            {
                                txtGold.color = Color.white;
                            }
                        }).AddTo(_disposableCity);

                        ct.model.productLevel.Subscribe(pd_lv =>
                        {
                            txtPart.text =
                                Utilities.GetTimeStringShort(TimeSpan.FromSeconds(ct.partData.pd_time[pd_lv]));
                            txtPartCount.text = ct.partData.parts_count[pd_lv].ToString();
                            txtMaterial.text =
                                $"{UserDataManager.Instance.data.cityMaterials[ct.matIndex]}/{ct.partData.material_count[pd_lv]}";
                            txtGold.text = ct.partData.pd_gold[pd_lv].ToString();
                        }).AddTo(_disposableCity);
                    }

                    ct.model.state.Subscribe(state =>
                    {
                        goProduction.SetActive(ct.IsMega() && (state == CityModel.State.Wait
                                                               || state == CityModel.State.Craft
                                                               || state == CityModel.State.Upgrade));
                        btnBoost.gameObject.SetActive(state == CityModel.State.Craft
                                                      || state == CityModel.State.Upgrade);
                        slider.gameObject.SetActive(state == CityModel.State.Craft
                                                    || state == CityModel.State.Upgrade);
                        handAni.SetActive(ct.IsMega() && state == CityModel.State.Wait);
                        txtMaterial.transform.parent.gameObject.SetActive(ct.IsMega() && state == CityModel.State.Wait);
                        txtGold.transform.parent.gameObject.SetActive(ct.IsMega() && state == CityModel.State.Wait);
                        dragObject.gameObject.SetActive(state == CityModel.State.Wait);

                        if (state == CityModel.State.Craft)
                        {
                            slider.maxValue = ct.partData.pd_time[ct.model.productLevel.Value] * 1000;
                        }
                        else if (state == CityModel.State.Upgrade)
                        {
                            slider.maxValue = ct.partData.up_time[ct.model.productLevel.Value + 1] * 1000;
                        }

                        if (LunarConsoleVariables.isCraft)
                        {
                            slider.maxValue = (float) TimeSpan.FromSeconds(10).TotalMilliseconds;
                        }
                    }).AddTo(_disposableCity);

                    Observable.EveryUpdate().Subscribe(x =>
                    {
                        if (ct.model.state.Value == CityModel.State.Craft
                            || ct.model.state.Value == CityModel.State.Upgrade)
                        {
                            slider.value = slider.maxValue -
                                           (float) (ct.model.productTime.Value - DateTime.Now).TotalMilliseconds;
                            txtSlider.text = Utilities.GetTimeStringShort(ct.model.productTime.Value - DateTime.Now);
                        }
                    }).AddTo(_disposableCity);

                    city.Value.trucks.ObsSomeChanged().Select(list => list.Count).Subscribe(count =>
                    {
                        btnJob.gameObject.SetActive(count > 0);
                        btnJob_NoTruck.gameObject.SetActive(count == 0);
                    }).AddTo(_disposableCity);

                    NewDailyMissionManager.Instance.city.Subscribe(missionCity =>
                    {
                        btnMission.gameObject.SetActive(ct == missionCity);
                    }).AddTo(_disposableCity);

                    btnParts.gameObject.SetActive(ct.data.mega);

                    Observable.CombineLatest(NewDailyMissionManager.Instance.hasReward,
                            NewDailyMissionManager.Instance.isNew,
                            (isSuccess, isNew) => isSuccess || isNew)
                        .Subscribe(alert => { missionAlert.SetActive(alert); }).AddTo(this);

                    Observable.CombineLatest(UserDataManager.Instance.data.gold,
                            ct.model.level,
                            (gold, cityLv) => cityLv < Datas.joblistExpansion.Length - 2
                                              && Datas.joblistExpansion[cityLv + 1].gold <=
                                              UserDataManager.Instance.data.gold.Value)
                        .Subscribe(alert => { upgradeAlert.SetActive(alert); })
                        .AddTo(_disposableCity);

                    ct.model.level.Subscribe(lv => { txtMax.gameObject.SetActive(false); }).AddTo(_disposableCity);
                }

                triggerPart.OnDropAsObservable().Subscribe(_ =>
                {
                    if (ct.CraftStart())
                    {
                        AudioManager.Instance.PlaySound("sfx_parts_start");
                    }
                    else
                    {
                        AudioManager.Instance.PlaySound("sfx_require");
                    }
                }).AddTo(_disposableCity);
            }).AddTo(this);

            gameObject.SetActive(false);
        }

        public void Show(City _city, bool isPop = false)
        {
            if (_city != null && _city.IsOpen())
            {
                city.Value = _city;
                GameManager.Instance.selectedCity.Value = _city;

                if (!isPop)
                {
                    GameManager.Instance.fsm.PushState(GameManager.Instance.cityMenuState);
                }

                gameObject.SetActive(true);
                UICityMenuBlackPanel.Instance.gameObject.SetActive(true);

                int count = 0;

                foreach (var animator in GetComponentsInChildren<Animator>().Where(x => x.gameObject.activeSelf))
                {
                    animator.gameObject.SetActive(false);

                    UnirxExtension.DateTimer(DateTime.Now.AddSeconds(0.05f * count)).Subscribe(_ =>
                    {
                        animator.gameObject.SetActive(true);
                        animator.enabled = true;
                    }).AddTo(this);

                    count++;
                }

                if (NewDailyMissionManager.Instance.city.Value == _city)
                {
                    imgMissionIcon.sprite =
                        GameManager.Instance.GetCargoSprite(NewDailyMissionManager.Instance.GetCargoId());
                }
            }
        }

        public void Close()
        {
            city.Value = null;
            gameObject.SetActive(false);
            UICityMenuBlackPanel.Instance.gameObject.SetActive(false);
        }

        private void LateUpdate()
        {
            if (city != null)
            {
                if (Camera.main != null)
                {
                    transform.rotation = Camera.main.transform.rotation;
                }
            }
        }
    }
}