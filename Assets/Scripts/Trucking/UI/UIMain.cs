using System;
using System.Collections.Generic;
using System.Linq;
using DatasTypes;
using DG.Tweening;
using Newtonsoft.Json.Utilities;
using TMPro;
using Trucking.Ad;
using Trucking.Common;
using Trucking.Manager;
using Trucking.Model;
using Trucking.UI.Craft;
using Trucking.UI.Mission;
using Trucking.UI.Popup;
using Trucking.UI.Shop;
using Trucking.UI.ToolTip;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI
{
    public class UIMain : MonoSingleton<UIMain>
    {
        public TextMeshProUGUI userLevel;
        public TextMeshProUGUI exp;
        public TextMeshProUGUI txtDeliveryEvent;
        public TextMeshProUGUI txtNewDailyTime;
        public TextMeshProUGUI[] txtBoost;
        public TextMeshProUGUI txtBackTitle;
        public TextMeshProUGUI txtEventShopTime;
        public TextMeshProUGUI txtLuckyBoxTime;
        public TextMeshProUGUI txtFreeBoosterTime;
        public TextMeshProUGUI txtHQAlert;
        public TextMeshProUGUI txtHQTime;
        public TextMeshProUGUI txtRouteCount;
        public TextMeshProUGUI txtCrateTime;


        public GameObject achievementAlert;
        public GameObject levelMissionAlert;
        public GameObject shopAlert;

        public GameObject newDailyMissionAlert;
        public GameObject deliveryAlert;
        public GameObject luckyBoxAlert;
        public GameObject eventTruckAlert;
        public GameObject routesAlert;
        public GameObject craftAlert;
        public GameObject freeBoosterAlert;

        public Button buttonLevel;
        public Button buttonGold;
        public Button buttonCash;
        public Button buttonAchievement;
        public Button buttonOption;
        public Button buttonGuide;
        public Button buttonFacebook;

        public Button buttonHQ;
        public Button buttonEdit;

        public Button buttonFreeCash;
        public Button buttonFreeBooster;
        public Button buttonDeliveryEvent;
        public Button buttonNewDailyMission;
        public Button buttonLevelMission;

        public Button buttonTruckShop;
        public Button buttonCraft;
        public Button buttonEventShop;
        public Button buttonLuckyBox;

        public Button buttonBoostXp;
        public Button buttonBoostGold;
        public Button buttonBoostSpeed;
        public Button buttonBoostFuel;


        public Button buttonBack;


        public GameObject resources;
        public GameObject level;
        public GameObject rightCenter;
        public GameObject boost;
        public Image imgNewDailyMissionIcon;
        public ReactiveProperty<Truck> firstArrivaingTruck = new ReactiveProperty<Truck>();
        public ReactiveProperty<City> firstProductCity = new ReactiveProperty<City>();

        private Tweener btnBacktweener;
        private Tweener resourceTweener;


        private Tweener expTweener;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Debug.Log("UIMain OnDestroy");
        }

        public void Init()
        {
            routesAlert.SetActive(false);

            UserDataManager.Instance.data.lv
                .Subscribe(value => { userLevel.text = value.ToString("N0"); }).AddTo(this);

            UserDataManager.Instance.data.lv
                .Pairwise().Subscribe(value =>
                {
                    if (value.Previous != 0)
                    {
                        Debug.Log("Level Up ==============================================================");

                        UserDataManager.Instance.data.levelUpReward.Value = true;

                        if (GameManager.Instance.fsm.GetCurrentState().GetType() == typeof(WorldMapState))
                        {
                            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(0.3f)).Subscribe(_ =>
                            {
                                if (GameManager.Instance.fsm.GetCurrentState().GetType() == typeof(WorldMapState))
                                {
                                    Popup_LevelUp.Instance.Show();
                                }
                            });
                        }
                    }
                }).AddTo(this);


            UserDataManager.Instance.data.exp.ObserveEveryValueChanged(exp => exp.Value).Pairwise().Subscribe(pair =>
            {
                expTweener?.Kill(this);
                var gap = pair.Current - pair.Previous;
                exp.gameObject.SetActive(true);

                if (gap < 0)
                {
                    gap = Datas.levelData[UserDataManager.Instance.data.lv.Value - 1].request_xp - pair.Previous +
                          pair.Current;
                }

                Debug.Assert(gap >= 0);

                expTweener = DOTween.To(() => 0, x => exp.text = "+" + x.ToString("N0"), gap, 0.5f)
                    .OnComplete(() =>
                    {
                        exp.text = "+ " + gap.ToString("N0");
                        exp.gameObject.SetActive(false);
                    }).SetTarget(this);
            }).AddTo(this);


            buttonGold.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    ShopView.Instance.Show(ShopView.Type.Coin);
                }).AddTo(this);

            buttonCash.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    ShopView.Instance.Show(ShopView.Type.Cash);
                }).AddTo(this);

            buttonAchievement.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    AchievementView.Instance.Show();
                }).AddTo(this);

            buttonOption.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    Popup_Option.Instance.Show();
                }).AddTo(this);

            buttonGuide.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    Popup_GuideMain.Instance.Show();
                }).AddTo(this);

            buttonFacebook.OnClickAsObservable()
                .Subscribe(_ => { AudioManager.Instance.PlaySound("sfx_button_main"); }).AddTo(this);


            buttonHQ.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    HQView.Instance.Show();
                }).AddTo(this);

            buttonEdit.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    EditView.Instance.Show();
                }).AddTo(this);


            buttonFreeCash.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    FBAnalytics.FBAnalytics.LogFreeCashButtonEvent(UserDataManager.Instance.data.lv.Value);
                    ShopView.Instance.Show(ShopView.Type.Cash);
                }).AddTo(this);

            buttonFreeBooster.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    Popup_OperationManager.Instance.Show();
                }).AddTo(this);

            buttonDeliveryEvent.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    FBAnalytics.FBAnalytics.LogNPDSButtonEvent(UserDataManager.Instance.data.lv.Value);
                    Popup_DeliveryService.Instance.Show();
                }).AddTo(this);

            NewDailyMissionManager.Instance.model.hasMission.Subscribe(hasMission =>
            {
                buttonNewDailyMission.gameObject.SetActive(hasMission);
            }).AddTo(this);

            NewDailyMissionManager.Instance.hasReward.Subscribe(hasReward =>
            {
                txtNewDailyTime.gameObject.SetActive(!hasReward);
            }).AddTo(this);

            buttonNewDailyMission.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    City city = NewDailyMissionManager.Instance.city.Value;

                    if (city != null)
                    {
                        WorldMap.Instance.SetCamera(city.transform.position);

                        if (NewDailyMissionManager.Instance.isNew.Value)
                        {
                            Popup_NewDailyMissionGuide.Instance.Show();
                        }
                        else
                        {
                            Popup_NewDailyMission.Instance.Show();
                        }
                    }
                }).AddTo(this);


            //DeliveryServiceManager.Instance.model.

            buttonLevelMission.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    Popup_LevelMission.Instance.Show();
                }).AddTo(this);

            buttonTruckShop.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    ShopView.Instance.Show(ShopView.Type.Crate);
                }).AddTo(this);

            buttonCraft.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    CraftView.Instance.Show(CraftView.Type.Crates);
                }).AddTo(this);

            buttonBack.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    ((GameState) GameManager.Instance.fsm.GetCurrentState()).OnBackButton(GameManager.Instance);
                }).AddTo(this);

            UserDataManager.Instance.data.levelMissionData.Value.hasMission.Subscribe(hasMission =>
            {
                buttonLevelMission.gameObject.SetActive(hasMission);
            }).AddTo(this);

            UserDataManager.Instance.data.levelMissionData.Value.rewardIndex.Subscribe(index =>
            {
                buttonLevelMission.transform.GetChild(1).GetComponent<Image>().sprite
                    = GameManager.Instance.GetRewardImage(RewardData.eType.random_box, index);
            }).AddTo(this);

            UserDataManager.Instance.data.levelMissionData.Value.isSuccess
                .Subscribe(success => { levelMissionAlert.SetActive(success); }).AddTo(this);


            Observable.CombineLatest(DeliveryServiceManager.Instance.model.hasReward,
                    DeliveryServiceManager.Instance.model.isMove, (hasReward, isMove) =>
                    {
                        if (!isMove || hasReward)
                        {
                            return true;
                        }

                        return false;
                    })
                .Subscribe(alert => { deliveryAlert.SetActive(alert); })
                .AddTo(this);

            DeliveryServiceManager.Instance.model.hasEvent.Subscribe(hasEvent =>
            {
                buttonDeliveryEvent.gameObject.SetActive(hasEvent);
            }).AddTo(this);

            NewDailyMissionManager.Instance.model.questModel.Subscribe(quest =>
            {
                if (quest != null)
                {
                    imgNewDailyMissionIcon.sprite =
                        GameManager.Instance.GetCargoSprite(NewDailyMissionManager.Instance.GetCargoId());
                }
            }).AddTo(this);

            NewDailyMissionManager.Instance.model.hasMission.Subscribe(hasMission =>
            {
                buttonNewDailyMission.gameObject.SetActive(hasMission);
            }).AddTo(this);

            Observable.CombineLatest(NewDailyMissionManager.Instance.hasReward,
                    NewDailyMissionManager.Instance.isNew,
                    (isSuccess, isNew) => isSuccess || isNew)
                .Subscribe(alert => { newDailyMissionAlert.SetActive(alert); }).AddTo(this);

            GuideQuestManager.Instance.model.hasMission.Subscribe(mission => { UIGuideQuest.Instance.Show(); })
                .AddTo(this);

            AchievementManager.Instance.hasReward.Subscribe(has => { achievementAlert.SetActive(has); }).AddTo(this);

            OperationManager.Instance.model.startEvent.Subscribe(hasEvent =>
            {
                buttonFreeBooster.gameObject.SetActive(hasEvent);
            }).AddTo(this);


            GameManager.Instance.trucks.ObsSomeChanged()
                .SelectMany(list => list.Select(tr => tr.model.state).CombineLatest())
                .Select(list => list.Count(state => state == TruckModel.State.Wait))
                .Subscribe(_ =>
                {
                    int count = GameManager.Instance.trucks.Count(x => x.model.state.Value == TruckModel.State.Wait
                                                                       && x.model.hasRoute.Value);

                    txtHQAlert.transform.parent.gameObject.SetActive(count > 0);
                    txtHQAlert.text = count.ToString();
                }).AddTo(this);

            GameManager.Instance.trucks.ObsSomeChanged()
                .SelectMany(list => list.Select(tr => tr.model.state).CombineLatest())
                .Select(list => list.Count(state => state == TruckModel.State.Move))
                .Subscribe(moveCount =>
                {
                    List<Truck> trs = GameManager.Instance.trucks.ToList()
                        .OrderByDescending(x => x.model.state.Value == TruckModel.State.Move)
                        .ThenBy(x => x.GetLeftTime())
                        .ToList();

                    if (trs.Count > 0 && trs[0].model.state.Value == TruckModel.State.Move)
                    {
                        firstArrivaingTruck.Value = trs[0];
                    }
                    else
                    {
                        firstArrivaingTruck.Value = null;
                    }
                }).AddTo(this);

            ReactiveCollection<City> productCities = new ReactiveCollection<City>();
            productCities.AddRange(GameManager.Instance.cities.FindAll(ct => ct.IsMega()));

            productCities.ObsSomeChanged()
                .SelectMany(list => list.Select(ct => ct.model.state).CombineLatest())
                .Subscribe(_ =>
                {
                    List<City> cities = productCities.OrderByDescending(x =>
                            x.model.state.Value == CityModel.State.Craft ||
                            x.model.state.Value == CityModel.State.Upgrade)
                        .ThenBy(x => x.model.productTime.Value).ToList();

                    if (cities.Count > 0
                        && (cities[0].model.state.Value == CityModel.State.Craft ||
                            cities[0].model.state.Value == CityModel.State.Upgrade))
                    {
                        firstProductCity.Value = cities[0];
                    }
                    else
                    {
                        firstProductCity.Value = null;
                    }
                }).AddTo(this);


            Observable.CombineLatest(FreeCashManager.Instance.model.hasStart,
                    AdManager.Instance.IsLoadedReward,
                    (start, load) => start && load)
                .Subscribe(value => { buttonFreeCash.gameObject.SetActive(value); }).AddTo(this);

            Observable.CombineLatest(OperationManager.Instance.model.endTime,
                    OperationManager.Instance.model.rewardIndex,
                    (endTime, index) => (endTime, index))
                .Subscribe(value => { freeBoosterAlert.SetActive(value.Item1 < DateTime.Now && value.Item2 == 0); })
                .AddTo(this);

            Observable.CombineLatest(UserDataManager.Instance.crateNoti.Merge(),
                    UserDataManager.Instance.pieceNoti.Merge(),
                    UserDataManager.Instance.partsNoti.Merge(),
                    (crate, piece, parts) => crate || piece || parts)
                .Subscribe(alert => { craftAlert.SetActive(alert); }).AddTo(this);

            firstArrivaingTruck.Subscribe(tr =>
            {
                if (tr != null)
                {
                }
            }).AddTo(this);

            Observable.EveryLateUpdate().StartWith(0).Subscribe(_ =>
            {
                for (int i = 0; i < UserDataManager.Instance.data.boosterShopData.Count; i++)
                {
                    TimeSpan timeSpan = UserDataManager.Instance.data.boosterShopData[i] - DateTime.Now;

                    txtBoost[i].transform.parent.gameObject.SetActive(timeSpan.TotalMilliseconds > 0);

                    if (timeSpan.TotalMilliseconds > 0)
                    {
                        txtBoost[i].text = Utilities.GetTimeString(timeSpan);
                    }
                }

                if (NewDailyMissionManager.Instance.model.hasMission.Value
                    && !NewDailyMissionManager.Instance.hasReward.Value)
                {
//                    txtNewDailyTime.gameObject.SetActive(NewDailyMissionManager.Instance.model.nextTime.Value >
//                                                         DateTime.Now);
                    txtNewDailyTime.text =
                        Utilities.GetTimeStringShort(
                            NewDailyMissionManager.Instance.model.nextTime.Value - DateTime.Now);
                }


                txtDeliveryEvent.gameObject.SetActive(DeliveryServiceManager.Instance.model.isMove.Value);
                if (DeliveryServiceManager.Instance.model.isMove.Value)
                {
                    txtDeliveryEvent.text = Utilities.GetTimeStringShort(
                        DeliveryServiceManager.Instance.model.endTime.Value
                        - DateTime.Now);
                }

                txtFreeBoosterTime.gameObject.SetActive(OperationManager.Instance.model.endTime.Value > DateTime.Now);
                txtFreeBoosterTime.text =
                    Utilities.GetTimeStringShort(OperationManager.Instance.model.endTime.Value - DateTime.Now);

                txtHQTime.gameObject.SetActive(firstArrivaingTruck.Value != null);
                if (firstArrivaingTruck.Value != null)
                {
                    TimeSpan leftTime =
                        TimeSpan.FromSeconds(firstArrivaingTruck.Value.GetTransitTime() -
                                             firstArrivaingTruck.Value.GetDeltaTime());
                    txtHQTime.text = Utilities.GetTimeString(leftTime);
                }

                txtCrateTime.gameObject.SetActive(firstProductCity.Value != null);
                if (firstProductCity.Value != null)
                {
                    txtCrateTime.text =
                        Utilities.GetTimeStringShort(firstProductCity.Value.model.productTime.Value - DateTime.Now);
                }
            }).AddTo(this);


            #region  tooltip

            buttonLevel.OnClickAsObservable().Subscribe(_ =>
            {
                Popup_ToolTip_Level.Instance.Show(buttonLevel.transform.position);
            }).AddTo(this);

            buttonBoostXp.OnClickAsObservable().Subscribe(x =>
            {
                Popup_ToolTip_Boost.Instance.Show(Datas.buffData.xp, buttonBoostXp.GetComponent<RectTransform>());
            }).AddTo(this);

            buttonBoostGold.OnClickAsObservable().Subscribe(x =>
            {
                Popup_ToolTip_Boost.Instance.Show(Datas.buffData.gold,
                    buttonBoostGold.GetComponent<RectTransform>());
            }).AddTo(this);

            buttonBoostSpeed.OnClickAsObservable().Subscribe(x =>
            {
                Popup_ToolTip_Boost.Instance.Show(Datas.buffData.speed,
                    buttonBoostSpeed.GetComponent<RectTransform>());
            }).AddTo(this);

            buttonBoostFuel.OnClickAsObservable().Subscribe(x =>
            {
                Popup_ToolTip_Boost.Instance.Show(Datas.buffData.gas,
                    buttonBoostFuel.GetComponent<RectTransform>());
            }).AddTo(this);

            #endregion

            gameObject.SetActive(true);
            shopAlert.SetActive(false);
            SetMain();
        }

        public void SetMain()
        {
            buttonHQ.gameObject.SetActive(true);
            buttonEdit.gameObject.SetActive(true);
            rightCenter.SetActive(true);
            boost.SetActive(true);
            buttonTruckShop.gameObject.SetActive(true);
            buttonCraft.gameObject.SetActive(true);
//            buttonEventShop.gameObject.SetActive(EventTruckManager.Instance.model.hasEvent.Value);
//            buttonLuckyBox.gameObject.SetActive(LuckyBoxManager.Instance.hasEvent.Value &&
//                                                !Trucking.Common.Trucking.CloseLuckyBox);
            buttonAchievement.gameObject.SetActive(true);
            buttonOption.gameObject.SetActive(true);
            buttonGuide.gameObject.SetActive(true);
            buttonFacebook.gameObject.SetActive(true);

            level.SetActive(true);
            txtRouteCount.gameObject.SetActive(false);
            UIGuideQuest.Instance.Show();
            ResetBackButtonTween();
            ResetResourceTween();
            resourceTweener = resources.GetComponent<RectTransform>().DOAnchorPosX(0, 0.3f);
            btnBacktweener = buttonBack.GetComponent<RectTransform>().DOAnchorPosX(-350, 0.3f)
                .OnComplete(() => { buttonBack.gameObject.SetActive(false); });
        }

        public void SetOnlyResource()
        {
            ResetBackButtonTween();
            ResetResourceTween();

            resourceTweener = resources.GetComponent<RectTransform>().DOAnchorPosX(720, 0.3f);

            buttonHQ.gameObject.SetActive(false);
            buttonEdit.gameObject.SetActive(false);
            rightCenter.SetActive(false);
            boost.SetActive(false);
            buttonTruckShop.gameObject.SetActive(false);
            buttonCraft.gameObject.SetActive(false);
            buttonEventShop.gameObject.SetActive(false);
            buttonLuckyBox.gameObject.SetActive(false);
            buttonAchievement.gameObject.SetActive(false);
            buttonOption.gameObject.SetActive(false);
            buttonGuide.gameObject.SetActive(false);
            buttonFacebook.gameObject.SetActive(false);
            level.SetActive(true);
            UIGuideQuest.Instance.Close();
        }

        public void SetBackTitle(string title)
        {
            SetOnlyResource();
            ResetBackButtonTween();

            txtBackTitle.text = title.ToUpper();
            txtRouteCount.gameObject.SetActive(false);

            if (!buttonBack.gameObject.activeSelf)
            {
                buttonBack.GetComponent<RectTransform>().anchoredPosition = new Vector2(-350,
                    buttonBack.GetComponent<RectTransform>().anchoredPosition.y);
                buttonBack.GetComponent<RectTransform>().DOAnchorPosX(7, 0.3f);
            }
            else
            {
                buttonBack.GetComponent<RectTransform>().anchoredPosition = new Vector2(7,
                    buttonBack.GetComponent<RectTransform>().anchoredPosition.y);
            }

            buttonBack.gameObject.SetActive(true);
        }

        public void ShowRouteCount()
        {
            txtRouteCount.gameObject.SetActive(true);
        }

        void ResetBackButtonTween()
        {
            btnBacktweener?.Kill();
        }

        void ResetResourceTween()
        {
            resourceTweener?.Kill();
        }
    }
}