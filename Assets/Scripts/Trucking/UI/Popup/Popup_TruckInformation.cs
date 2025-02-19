using System;
using DatasTypes;
using TMPro;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.Craft;
using Trucking.UI.Mission;
using Trucking.UI.ThreeDimensional;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Trucking.UI.Popup
{
    public class Popup_TruckInformation : Popup_Base<Popup_TruckInformation>
    {
        public Button btnTooltip;
        public Button btnClaim;
        public Button btnSell;
        public Button btnUpgradeGold;

        public Transform trsStar;
        public TextMeshProUGUI txtName;

        public TextMeshProUGUI txtSpeed1;
        public TextMeshProUGUI txtSpeed2;
        public TextMeshProUGUI txtSpeed3;

        public TextMeshProUGUI txtFuel1;
        public TextMeshProUGUI txtFuel2;
        public TextMeshProUGUI txtFuel3;

        public TextMeshProUGUI txtCargo1;
        public TextMeshProUGUI txtCargo2;
        public TextMeshProUGUI txtCargo3;

//        public TextMeshProUGUI txtUpgradeCash;
        public TextMeshProUGUI txtUpgradeGold;

        public TextMeshProUGUI txtPatrs1;
        public TextMeshProUGUI txtPatrs2;
        public TextMeshProUGUI txtPatrs3;

        public Image imgPatrs1;
        public Image imgPatrs2;
        public Image imgPatrs3;
        public Image imgBtnUpgradeGoldGray;


        public Slider sliderSpeed;
        public Slider sliderSpeed2;
        public Slider sliderFuel;
        public Slider sliderFuel2;
        public Slider sliderCargo;
        public Slider sliderCargo2;

        public GameObject upgradeGroup;

        public UIObject3D uiObject3D;

        public TextMeshProUGUI txtMax;

        public ParticleSystem particle;

        public GameObject goSell;
        public TextMeshProUGUI txtSellGold;

        public GameObject goUpgrade;
        public GameObject goClaim;

        protected ReactiveProperty<Truck> truck = new ReactiveProperty<Truck>();
        protected CompositeDisposable _compositeDisposable = new CompositeDisposable();
        protected Action OnAction;
        protected Truck instatTruck;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Debug.Log("Popup_TruckInformation OnDestroy");
        }


        private void Start()
        {
            disposableBlack.Clear();

            btnBlackPanel.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (!goClaim.activeSelf)
                    {
                        AudioManager.Instance.PlaySound("sfx_button_main");
                        GameManager.Instance.fsm.PopState();
                    }
                })
                .AddTo(this);

            btnUpgradeGold.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (UserDataManager.Instance.data.truckParts[0].Value >=
                        truck.Value.data.parts1[truck.Value.model.upgradeLv.Value]
                        && UserDataManager.Instance.data.truckParts[1].Value >=
                        truck.Value.data.parts2[truck.Value.model.upgradeLv.Value]
                        && UserDataManager.Instance.data.truckParts[2].Value >=
                        truck.Value.data.parts3[truck.Value.model.upgradeLv.Value]
                        && UserDataManager.Instance.data.truckParts[3].Value >=
                        truck.Value.data.parts4[truck.Value.model.upgradeLv.Value]
                        && UserDataManager.Instance.UseResource(
                            truck.Value.data.gold[truck.Value.model.upgradeLv.Value], 0))
                    {
                        AudioManager.Instance.PlaySound("sfx_upgrade");
                        UserDataManager.Instance.UsePart(0, truck.Value.data.parts1[truck.Value.model.upgradeLv.Value]);
                        UserDataManager.Instance.UsePart(1, truck.Value.data.parts2[truck.Value.model.upgradeLv.Value]);
                        UserDataManager.Instance.UsePart(2, truck.Value.data.parts3[truck.Value.model.upgradeLv.Value]);
                        UserDataManager.Instance.UsePart(3, truck.Value.data.parts4[truck.Value.model.upgradeLv.Value]);

                        truck.Value.Upgrade();
                        PlayParticle();
                        FBAnalytics.FBAnalytics.LogUpgradeTruckEvent(UserDataManager.Instance.data.lv.Value,
                            truck.Value.data.id, truck.Value.model.upgradeLv.Value,
                            "coin");
                        OnAction?.Invoke();
                    }
                    else
                    {
                        AudioManager.Instance.PlaySound("sfx_require");
//                        btnUpgradeGold.transform.DOShakePosition(0.3f, 3);
                    }
                })
                .AddTo(this);

//            btnUpgradeCash.OnClickAsObservable()
//                .Subscribe(_ =>
//                {
//                    if(UserDataManager.Instance.data.truckParts[0].Value >= truck.Value.data.parts1[truck.Value.model.upgradeLv.Value]
//                       && UserDataManager.Instance.data.truckParts[1].Value >= truck.Value.data.parts2[truck.Value.model.upgradeLv.Value]
//                       && UserDataManager.Instance.data.truckParts[2].Value >= truck.Value.data.parts3[truck.Value.model.upgradeLv.Value]
//                       && UserDataManager.Instance.data.truckParts[3].Value >= truck.Value.data.parts4[truck.Value.model.upgradeLv.Value]
//                       && UserDataManager.Instance.UseCash(truck.Value.data.cash[truck.Value.model.upgradeLv.Value]))
//                    {
//                        AudioManager.Instance.PlaySound("sfx_upgrade");
//                        UserDataManager.Instance.UsePart(0, truck.Value.data.parts1[truck.Value.model.upgradeLv.Value]);
//                        UserDataManager.Instance.UsePart(1, truck.Value.data.parts2[truck.Value.model.upgradeLv.Value]);
//                        UserDataManager.Instance.UsePart(2, truck.Value.data.parts3[truck.Value.model.upgradeLv.Value]);
//                        UserDataManager.Instance.UsePart(3, truck.Value.data.parts4[truck.Value.model.upgradeLv.Value]);
//                        
//                        truck.Value.Upgrade();
//                        PlayParticle();
//                        FBAnalytics.FBAnalytics.LogUpgradeTruckEvent(UserDataManager.Instance.data.lv.Value, 
//                            truck.Value.data.id, truck.Value.model.upgradeLv.Value,
//                            "cash");
//                        OnAction?.Invoke();
//                    }
//                    else
//                    {
//                        AudioManager.Instance.PlaySound("sfx_require");
//                        btnUpgradeCash.transform.DOShakePosition(0.3f, 3);
//                    }
//                })
//                .AddTo(this);

            btnClaim.OnClickAsObservable()
                .Subscribe(_ =>
                {
//                    AudioManager.Instance.PlaySound("sfx_button_main");
//                    GameManager.Instance.fsm.PopState();
//                    OnAction?.Invoke();

                    AudioManager.Instance.PlaySound("sfx_button_main");
                    Truck tr = truck.Value;
                    MissionManager.Instance.AddValue(QuestData.eType.truck, 1);
                    GameManager.Instance.fsm.PopState();

                    if (GameManager.Instance.fsm.GetCurrentState() == GameManager.Instance.shopState
                        && GameManager.Instance.fsm.GetPreviousState() == GameManager.Instance.addTruckState)
                    {
                        GameManager.Instance.fsm.PopState();
                        EditView.Instance.editViewAddTruck.SelectTruck(tr);
                    }
                    else
                    {
                        OnAction?.Invoke();
                    }
                })
                .AddTo(this);

            btnSell.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    GameManager.Instance.fsm.PopState();
                    OnAction?.Invoke();
                })
                .AddTo(this);

            btnTooltip.OnClickAsObservable().Subscribe(x =>
            {
                AudioManager.Instance.PlaySound("sfx_button_main");
                Popup_GuideMain.Instance.Show(12);
            }).AddTo(this);

            truck.Subscribe(tr =>
            {
                _compositeDisposable.Clear();

                if (tr != null)
                {
                    txtName.text = Utilities.GetStringByData(tr.data.name_id);

                    int masSpeed = tr.GetSpeed(tr.data.max_lv);
                    sliderSpeed.maxValue = masSpeed;
                    sliderSpeed2.maxValue = masSpeed;

                    int maxFuel = tr.GetFuel(tr.data.max_lv);
                    sliderFuel.maxValue = maxFuel;
                    sliderFuel2.maxValue = maxFuel;

                    int maxCargo = tr.GetCargo(tr.data.max_lv);
                    sliderCargo.maxValue = maxCargo;
                    sliderCargo2.maxValue = maxCargo;

                    if (goSell.activeSelf)
                    {
                        txtSellGold.text =
                            Utilities.GetThousandCommaText(tr.data.resell_gold[tr.model.upgradeLv.Value - 1]);
                    }
                    else if (goUpgrade.activeSelf)
                    {
                        btnUpgradeGold.gameObject.SetActive(tr.model.upgradeLv.Value < tr.data.max_lv);
                    }

//                    uiObject3D.ImageColor = new Color(1, 1, 1, 0);

                    tr.model.upgradeLv.Subscribe(lv =>
                    {
                        ContentLoader.LoadTruckUIObject3DAsync(tr.data.model_h, tr.data.model_c)
                            .TakeUntilDisable(this)
                            .Subscribe(tran =>
                            {
                                tran.SetParent(GameManager.Instance.trsUIObject3D);
                                uiObject3D.ObjectPrefab = tran;
                                uiObject3D.ImageColor = Color.white;
//                                uiObject3D.imageComponent.DOColor(Color.white, 0.2f);
                                uiObject3D.TargetOffset = new Vector2(tr.data.offset_x2, tr.data.offset_y2);
                                uiObject3D.TargetRotation = new Vector3(3, 140, 0);
                                uiObject3D.CameraFOV = 30;
                                uiObject3D.CameraDistance = -18;
                            }).AddTo(this);

                        for (int i = 0; i < Truck.MAX_LEVEL; i++)
                        {
                            trsStar.GetChild(i).gameObject.SetActive(i < tr.data.max_lv);

                            if (goUpgrade.activeSelf)
                            {
                                trsStar.GetChild(i).GetComponent<Animator>().Play("star_up_idle", -1, 0);
                            }
                            else
                            {
                                trsStar.GetChild(i).GetComponent<Animator>().Play("star_on_idle", -1, 0);
                            }

                            trsStar.GetChild(i).GetChild(0).gameObject.SetActive(i >= lv);
                            trsStar.GetChild(i).GetChild(1).gameObject.SetActive(i < lv);
                            trsStar.GetChild(i).GetChild(2).gameObject.SetActive(i == lv && goUpgrade.activeSelf);
                        }

                        txtSpeed1.text = tr.GetSpeed(lv).ToString();
                        txtFuel1.text = tr.GetFuel(lv).ToString();
                        txtCargo1.text = tr.GetCargo(lv).ToString();

                        if (!goUpgrade.activeSelf)
                        {
                            sliderSpeed2.value = tr.GetSpeed(lv);
                            sliderFuel2.value = tr.GetFuel(lv);
                            sliderCargo2.value = tr.GetCargo(lv);
                        }

                        sliderSpeed.value = tr.GetSpeed(lv);
                        sliderFuel.value = tr.GetFuel(lv);
                        sliderCargo.value = tr.GetCargo(lv);

                        txtSpeed2.gameObject.SetActive(goUpgrade.activeSelf && lv < tr.data.max_lv);
                        txtFuel2.gameObject.SetActive(goUpgrade.activeSelf && lv < tr.data.max_lv);
                        txtCargo2.gameObject.SetActive(goUpgrade.activeSelf && lv < tr.data.max_lv);

                        btnUpgradeGold.gameObject.SetActive(goUpgrade.activeSelf && lv < tr.data.max_lv);

//                        btnUpgradeCash.gameObject.SetActive(goUpgrade.activeSelf && lv < tr.data.max_lv);
                        upgradeGroup.SetActive(goUpgrade.activeSelf && lv < tr.data.max_lv);
                        txtMax.gameObject.SetActive(goUpgrade.activeSelf && lv >= tr.data.max_lv);

                        txtSpeed3.text = $"({tr.GetSpeed(tr.data.max_lv)})";
                        txtFuel3.text = $"({tr.GetFuel(tr.data.max_lv)})";
                        txtCargo3.text = $"({tr.GetCargo(tr.data.max_lv)})";


                        if (lv < tr.data.max_lv && goUpgrade.activeSelf)
                        {
                            sliderSpeed2.value = tr.GetSpeed(lv + 1);
                            sliderFuel2.value = tr.GetFuel(lv + 1);
                            sliderCargo2.value = tr.GetCargo(lv + 1);

                            txtSpeed2.text = "+" + (tr.GetSpeed(lv + 1) - tr.GetSpeed(lv));
                            txtSpeed2.gameObject.SetActive(tr.GetSpeed(lv + 1) > tr.GetSpeed(lv));
                            txtFuel2.text = "+" + (tr.GetFuel(lv + 1) - tr.GetFuel(lv));
                            txtFuel2.gameObject.SetActive(tr.GetFuel(lv + 1) > tr.GetFuel(lv));
                            txtCargo2.text = "+" + (tr.GetCargo(lv + 1) - tr.GetCargo(lv));
                            txtCargo2.gameObject.SetActive(tr.GetCargo(lv + 1) > tr.GetCargo(lv));


//                            txtUpgradeCash.transform.parent.gameObject.SetActive(tr.data.cash[lv] > 0);
//                            txtUpgradeCash.text = Utilities.GetThousandCommaText(tr.data.cash[lv]);
//
//                            UserDataManager.Instance.data.cash.Subscribe(cash =>
//                            {
//                                txtUpgradeCash.color = Color.white;
//                                if (cash < tr.data.cash[lv])
//                                {
//                                    txtUpgradeCash.color = Color.red;
//                                }
//
//                            }).AddTo(_compositeDisposable);


                            txtUpgradeGold.transform.parent.parent.gameObject.SetActive(tr.data.gold[lv] > 0);
                            txtUpgradeGold.text = Utilities.GetThousandCommaText(tr.data.gold[lv]);

                            UserDataManager.Instance.data.gold.Subscribe(gold =>
                            {
                                txtUpgradeGold.color = Color.white;
                                if (gold < tr.data.gold[lv])
                                {
                                    txtUpgradeGold.color = Color.red;
                                }
                            }).AddTo(_compositeDisposable);

                            int[] arrParts =
                                {tr.data.parts1[lv], tr.data.parts2[lv], tr.data.parts3[lv], tr.data.parts4[lv]};
                            TextMeshProUGUI[] arrTextParts = {txtPatrs1, txtPatrs2, txtPatrs3};
                            Image[] arrImgParts = {imgPatrs1, imgPatrs2, imgPatrs3};
                            int partIndex = 0;

                            for (int i = 0; i < arrTextParts.Length; i++)
                            {
                                arrTextParts[i].transform.parent.gameObject.SetActive(false);
                            }

                            for (int i = 0; i < arrParts.Length; i++)
                            {
                                if (arrParts[i] > 0)
                                {
                                    arrTextParts[partIndex].transform.parent.gameObject.SetActive(true);
                                    arrImgParts[partIndex].sprite =
                                        GameManager.Instance.GetRewardImage(RewardData.eType.parts, i);
                                    arrImgParts[partIndex].GetComponent<Button>().OnClickAsObservable().Subscribe(_ =>
                                    {
                                        Popup_Common.Instance.Show(Utilities.GetStringByData(20128),
                                                Utilities.GetStringByData(20189))
                                            .SetCenter(Utilities.GetStringByData(20078), Popup_Common.ButtonColor.Blue,
                                                () =>
                                                {
                                                    GameManager.Instance.fsm.PopState();
                                                    CraftView.Instance.Show(CraftView.Type.Parts, partIndex);
                                                });
                                    }).AddTo(_compositeDisposable);

                                    int tmpPartIndex = partIndex;
                                    int tmpIndex = i;
                                    UserDataManager.Instance.data.truckParts[i].Subscribe(part =>
                                    {
                                        if (part < arrParts[tmpIndex])
                                        {
                                            arrTextParts[tmpPartIndex].text =
                                                $"<color=#FF0000>{UserDataManager.Instance.data.truckParts[tmpIndex].Value}</color> / {Utilities.GetThousandCommaText(arrParts[tmpIndex])}";
                                        }
                                        else
                                        {
                                            arrTextParts[tmpPartIndex].text =
                                                $"{UserDataManager.Instance.data.truckParts[tmpIndex].Value} / {Utilities.GetThousandCommaText(arrParts[tmpIndex])}";
                                        }
                                    }).AddTo(_compositeDisposable);

                                    partIndex++;
                                }
                            }
                        }

                        Observable.CombineLatest(UserDataManager.Instance.data.gold,
                                UserDataManager.Instance.data.truckParts[0],
                                UserDataManager.Instance.data.truckParts[1],
                                UserDataManager.Instance.data.truckParts[2],
                                UserDataManager.Instance.data.truckParts[3],
                                (gold, parts1, parts2, parts3, parts4) =>
                                {
                                    return gold < truck.Value.data.gold[lv]
                                           || parts1 < truck.Value.data.parts1[lv]
                                           || parts2 < truck.Value.data.parts2[lv]
                                           || parts3 < truck.Value.data.parts3[lv]
                                           || parts4 < truck.Value.data.parts4[lv];
                                }).Subscribe(value => { imgBtnUpgradeGoldGray.gameObject.SetActive(value); })
                            .AddTo(_compositeDisposable);
                    }).AddTo(_compositeDisposable);
                }
            }).AddTo(this);
        }

        public void ShowUpgrade(Truck _truck, Action _onAction = null)
        {
            Show();
            btnCloseX.gameObject.SetActive(true);
            particle.gameObject.SetActive(false);
            goUpgrade.SetActive(true);
            goSell.SetActive(false);
            goClaim.SetActive(false);

            truck.Value = _truck;
            OnAction = _onAction;
            GetComponent<Animator>().Play("popup_truck_upgrade_idle", -1, 0);
        }

        public void ShowInfomation(TruckData data)
        {
            Show();
            btnCloseX.gameObject.SetActive(true);
            particle.gameObject.SetActive(false);
            goUpgrade.SetActive(false);
            goSell.SetActive(false);
            goClaim.SetActive(false);

            instatTruck = Truck.MakeTruck(data.id);
            truck.Value = instatTruck;
            GetComponent<Animator>().Play("popup_truck_upgrade_idle", -1, 0);
        }

        public void ShowClaim(Truck _truck, Action _onAction = null)
        {
            Show();
            btnCloseX.gameObject.SetActive(false);
            goUpgrade.SetActive(false);
            goSell.SetActive(false);
            goClaim.SetActive(true);

            truck.Value = _truck;
            OnAction = _onAction;
            PlayParticle();
        }

        public void ShowSell(Truck _truck, Action _onAction = null)
        {
            Show();
            btnCloseX.gameObject.SetActive(true);
            particle.gameObject.SetActive(false);
            goUpgrade.SetActive(false);
            goSell.SetActive(true);
            goClaim.SetActive(false);

            truck.Value = _truck;
            OnAction = _onAction;
            GetComponent<Animator>().Play("popup_truck_upgrade_idle", -1, 0);
        }

        public override void Close()
        {
            base.Close();

            truck.Value = null;

            if (instatTruck != null)
            {
                Observable.NextFrame().Subscribe(unit =>
                {
                    Utilities.RemoveObject(instatTruck.gameObject);
                    instatTruck = null;
                }).AddTo(this);
            }
        }

        void PlayParticle()
        {
            AudioManager.Instance.PlaySound("sfx_upgrade");
            particle.gameObject.SetActive(true);
            particle.Stop();
            particle.Clear();
            particle.Play();
            GetComponent<Animator>().Play("popup_truck_upgrade", -1, 0);

            if (goUpgrade.activeSelf)
            {
                trsStar.GetChild(truck.Value.model.upgradeLv.Value - 1).GetComponent<Animator>()
                    .Play("star_on_start", -1, 0);
            }
        }

        public override void BackKey()
        {
            btnBlackPanel.onClick.Invoke();
        }
    }
}