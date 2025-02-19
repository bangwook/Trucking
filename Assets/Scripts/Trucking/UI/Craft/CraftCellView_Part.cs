using System;
using DatasTypes;
using EnhancedUI.EnhancedScroller;
using TMPro;
using Trucking.Common;
using Trucking.Manager;
using Trucking.Model;
using Trucking.UI.Popup;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Craft
{
    public class CraftCellView_Part : EnhancedScrollerCellView
    {
        public GameObject goLock;
        public GameObject goLevel;
        public GameObject goLevelUp;
        public GameObject goLevelUpButton;
        public GameObject goParts;
        public GameObject goBoost;
        public GameObject goCraft;
        public GameObject goCollect;
        public GameObject goComplete;

        public Button btnCraft;
        public GameObject grayCraft;
        public Button btnUpgrade;
        public Button btnBoost;
        public GameObject grayBoost;
        public Button btnCollect;
        public Button btnComplete;

        public Image imgCity;
        public TextMeshProUGUI txtName;
        public Image imgPart;
        public TextMeshProUGUI txtPart;
        public TextMeshProUGUI txtTime;
        public Image imgMaterial;
        public TextMeshProUGUI txtMaterial;
        public TextMeshProUGUI txtGoldCost;

        public TextMeshProUGUI txtProgressTime;
        public TextMeshProUGUI txtLevelUp_L;
        public TextMeshProUGUI txtLevelUp_R;
        public TextMeshProUGUI txtLevel;
        public TextMeshProUGUI txtBoostCost;

        public Slider slider;

        public Image imgCityLock;
        public Image imgPartLock;
        public TextMeshProUGUI txtPartLock;
        public TextMeshProUGUI txtNameLock;
        public TextMeshProUGUI txtDesLock;
        public TextMeshProUGUI txtUpgradeLv;
        public GameObject noti;

        [HideInInspector] public PartsProduction data;
        [HideInInspector] public City city;

        private Action<CraftCellView_Part> OnClickCraft;
        private Action<CraftCellView_Part> OnClickUpgrade;
        private Action<CraftCellView_Part> OnClickBoost;
        private CompositeDisposable _disposable = new CompositeDisposable();
        private int partIndex;
        private int matIndex;
        private long boostCost;

        private void Start()
        {
            btnCraft.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (city.CraftStart())
                    {
                        AudioManager.Instance.PlaySound("sfx_button_main");
                    }
                    else
                    {
                        AudioManager.Instance.PlaySound("sfx_require");
                    }

                    OnClickCraft?.Invoke(this);
                })
                .AddTo(this);

            btnUpgrade.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    Popup_PartsUpgrade.Instance.Show(city);
                    OnClickUpgrade?.Invoke(this);
                })
                .AddTo(this);

            btnBoost.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (UserDataManager.Instance.data.cash.Value >= boostCost)
                    {
                        AudioManager.Instance.PlaySound("sfx_button_main");

                        Popup_Common popup = null;

                        if (city.model.state.Value == CityModel.State.Craft)
                        {
                            popup = Popup_Common.Instance.Show(Utilities.GetStringByData(20148),
                                Utilities.GetStringByData(20149));
                        }
                        else if (city.model.state.Value == CityModel.State.Upgrade)
                        {
                            popup = Popup_Common.Instance.Show(Utilities.GetStringByData(20150),
                                Utilities.GetStringByData(20151));
                        }

                        popup?.SetCenterReward(RewardData.eType.cash, boostCost, () =>
                        {
                            if (UserDataManager.Instance.UseCash(boostCost))
                            {
                                AudioManager.Instance.PlaySound("sfx_parts_boost");
                                city.model.productTime.Value = DateTime.Now;
                                OnClickBoost?.Invoke(this);
                            }
                            else
                            {
                                AudioManager.Instance.PlaySound("sfx_require");
                            }
                        });
                    }
                    else
                    {
                        AudioManager.Instance.PlaySound("sfx_require");
                    }
                })
                .AddTo(this);

            btnCollect.OnClickAsObservable()
                .Subscribe(_ => { city.CraftCollect(); })
                .AddTo(this);

            btnComplete.OnClickAsObservable()
                .Subscribe(_ => { city.UpgradeComplete(); })
                .AddTo(this);


            _disposable.AddTo(this);
        }

        private void OnDisable()
        {
            UserDataManager.Instance.partsNoti[dataIndex].Value = false;
            _disposable.Clear();
        }


        public void SetData(PartsProduction _data,
            Action<CraftCellView_Part> _onClickCraft,
            Action<CraftCellView_Part> _onClickUpgrade,
            Action<CraftCellView_Part> _onClickBoost
        )
        {
            _disposable.Clear();
            data = _data;
            OnClickCraft = _onClickCraft;
            OnClickUpgrade = _onClickUpgrade;
            OnClickBoost = _onClickBoost;

            city = GameManager.Instance.cities.Find(x => x.data.id == data.city);
            partIndex = RewardModel.GetIndex(RewardData.eType.parts, data.output);
            matIndex = RewardModel.GetIndex(RewardData.eType.material, data.input);

            txtName.text = city.name;
            imgCity.sprite = GameManager.Instance.atlasCity.GetSprite("city_" + city.data.id);
            imgPart.sprite = GameManager.Instance.GetRewardImage(RewardData.eType.parts, partIndex);
            imgMaterial.sprite = GameManager.Instance.GetRewardImage(RewardData.eType.material, matIndex);

            city.model.productLevel.Subscribe(lv =>
            {
                txtLevel.text = Utilities.GetStringByData(20008) + "." + (lv + 1);
                txtPart.text = "x" + data.parts_count[lv];
                txtTime.text = Utilities.GetTimeStringShort(TimeSpan.FromSeconds(data.pd_time[lv]));
                txtGoldCost.text = data.pd_gold[lv].ToString();
                txtUpgradeLv.text = (lv + 2).ToString();
            }).AddTo(_disposable);

            Observable.CombineLatest(city.model.productLevel,
                UserDataManager.Instance.data.gold,
                (lv, gold) => gold >= data.pd_gold[lv]).Subscribe(value =>
            {
                txtGoldCost.color = value ? Utilities.GetColorByHtmlString("#464646") : Color.red;
            }).AddTo(_disposable);

            Observable.CombineLatest(city.model.productLevel,
                UserDataManager.Instance.data.cityMaterials[matIndex],
                (lv, material) => new[] {lv, material}).Subscribe(value =>
            {
                if (value[1] < data.material_count[value[0]])
                {
                    txtMaterial.text = $"<color=#FF0000>{value[1]}</color> / {data.material_count[value[0]]}";
                }
                else
                {
                    txtMaterial.text = $"{value[1]} / {data.material_count[value[0]]}";
                }
            }).AddTo(_disposable);

            Observable.CombineLatest(city.model.productLevel,
                    UserDataManager.Instance.data.gold,
                    UserDataManager.Instance.data.cityMaterials[matIndex],
                    (lv, gold, material) => gold < data.pd_gold[lv] || material < data.material_count[lv])
                .Subscribe(value => { grayCraft.SetActive(value); }).AddTo(_disposable);

            UserDataManager.Instance.partsNoti[dataIndex].Subscribe(_noti => { noti.SetActive(_noti); })
                .AddTo(_disposable);


            //

            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (city.model.state.Value == CityModel.State.Craft ||
                    city.model.state.Value == CityModel.State.Upgrade)
                {
                    slider.value = slider.maxValue -
                                   (float) (city.model.productTime.Value - DateTime.Now).TotalMilliseconds;
                    txtProgressTime.text = Utilities.GetTimeStringShort(city.model.productTime.Value - DateTime.Now);

                    boostCost = CraftView_Parts.GetBoostCost(city);
                    txtBoostCost.text = boostCost.ToString();

                    if (UserDataManager.Instance.data.cash.Value >= boostCost)
                    {
                        txtBoostCost.color = Utilities.GetColorByHtmlString("#464646");
                    }
                    else
                    {
                        txtBoostCost.color = Color.red;
                    }

                    grayBoost.SetActive(boostCost > UserDataManager.Instance.data.cash.Value);
                }
            }).AddTo(_disposable);


            // lock
            imgCityLock.sprite = imgCity.sprite;
            imgPartLock.sprite = imgPart.sprite;
            txtPartLock.text = "x" + data.parts_count[city.model.productLevel.Value];
            txtNameLock.text = city.name;
            txtDesLock.text = string.Format(Utilities.GetStringByData(20136), city.name);

            Observable.CombineLatest(city.model.productLevel,
                city.model.state,
                (lv, state) =>
                {
                    if (state == CityModel.State.Wait && lv < data.parts_count.Length - 1)
                    {
                        return true;
                    }

                    return false;
                }).Subscribe(value => { btnUpgrade.transform.parent.gameObject.SetActive(value); }).AddTo(_disposable);

            // gameobject
            city.model.state.Subscribe(state =>
            {
                goLevel.SetActive(state == CityModel.State.Wait || state == CityModel.State.Craft ||
                                  state == CityModel.State.Craft_Collect);
                goLevelUp.SetActive(state == CityModel.State.Upgrade);
                goBoost.SetActive(state == CityModel.State.Craft || state == CityModel.State.Upgrade);
                goParts.SetActive(state == CityModel.State.Wait || state == CityModel.State.Craft ||
                                  state == CityModel.State.Craft_Collect);
                goLock.SetActive(state == CityModel.State.Lock);
                goCraft.SetActive(state == CityModel.State.Wait);

                goLevelUpButton.SetActive(state == CityModel.State.Wait);
                goCollect.SetActive(state == CityModel.State.Craft_Collect);
                goComplete.SetActive(state == CityModel.State.Upgrade_Complete);

                if (state == CityModel.State.Craft)
                {
                    slider.maxValue = data.pd_time[city.model.productLevel.Value] * 1000;
                }
                else if (state == CityModel.State.Upgrade)
                {
                    slider.maxValue = data.up_time[city.model.productLevel.Value + 1] * 1000;
                    txtLevelUp_L.text = (city.model.productLevel.Value + 1).ToString();
                    txtLevelUp_R.text = (city.model.productLevel.Value + 2).ToString();
                }

                if (LunarConsoleVariables.isCraft)
                {
                    slider.maxValue = (float) TimeSpan.FromSeconds(10).TotalMilliseconds;
                }
            }).AddTo(_disposable);
        }

        public void RefreshData()
        {
            SetData(data,
                OnClickCraft,
                OnClickUpgrade,
                OnClickBoost
            );
        }
    }
}