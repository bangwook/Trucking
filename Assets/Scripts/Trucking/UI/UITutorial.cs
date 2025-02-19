using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Newtonsoft.Json.Utilities;
using TMPro;
using Trucking.Common;
using Trucking.Manager;
using Trucking.Model;
using Trucking.UI.Popup;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI
{
    public class UITutorial : MonoSingleton<UITutorial>
    {
        public RectTransform trsCusor;
        public RectTransform trsDescUp;
        public TextMeshProUGUI txtDescUp;
        public RectTransform trsDescDown;
        public TextMeshProUGUI txtDescDown;
        public Button btnCusor;
        public Button btnSkipUp;
        public Button btnSkipDown;
        public Button btnContinue;
        public Button btnblackPanel;


        private TutorialData tutorialData;
        private TutorialData preTutorialData;
        private Transform trsTarget;
        private Transform trsCopyTarget;

        public void Init()
        {
            UserDataManager.Instance.data.hasTutorial.Subscribe(isTutorial =>
            {
                gameObject.SetActive(isTutorial);

                if (isTutorial)
                {
                    Truck truck = Truck.AddNewTruck(101);
                    Road road = GameManager.Instance.FindRoad(101, 102);
                    road.truck.Value = truck;
                    road = GameManager.Instance.FindRoad(102, 103);
                    road.truck.Value = truck;
                    truck.currentStation.Value = GameManager.Instance.FindStation(101);
                    truck.model.colorIndex.Value = UserDataManager.Instance.GetNextTruckColor();

                    truck = Truck.AddNewTruck(201);
                    road = GameManager.Instance.FindRoad(102, 106);
                    road.truck.Value = truck;
                    road = GameManager.Instance.FindRoad(106, 107);
                    road.truck.Value = truck;
                    truck.currentStation.Value = GameManager.Instance.FindStation(102);
                    truck.model.colorIndex.Value = UserDataManager.Instance.GetNextTruckColor();

                    truck = Truck.AddNewTruck(101);
                    road = GameManager.Instance.FindRoad(106, 104);
                    road.truck.Value = truck;
                    road = GameManager.Instance.FindRoad(104, 101);
                    road.truck.Value = truck;
                    truck.currentStation.Value = GameManager.Instance.FindStation(106);
                    truck.model.colorIndex.Value = UserDataManager.Instance.GetNextTruckColor();

                    truck = Truck.AddNewTruck(301);
                    road = GameManager.Instance.FindRoad(103, 105);
                    road.truck.Value = truck;
                    truck.currentStation.Value = GameManager.Instance.FindStation(103);
                    truck.model.colorIndex.Value = UserDataManager.Instance.GetNextTruckColor();

                    truck = Truck.AddNewTruck(101);
                    road = GameManager.Instance.FindRoad(105, 106);
                    road.truck.Value = truck;
                    truck.currentStation.Value = GameManager.Instance.FindStation(105);
                    truck.model.colorIndex.Value = UserDataManager.Instance.GetNextTruckColor();

                    truck = Truck.AddNewTruck(101);

                    City montpelier = GameManager.Instance.FindStation("MONTPELIER");
                    montpelier.cargos.Clear();
                    montpelier.AddCargo(Cargo.MakeWithValue(102, 106, 17, 1, 5, 50, 10));

                    for (int i = 0; i < UserDataManager.Instance.data.truckData.Count; i++)
                    {
                        UserDataManager.Instance.data
                            .truckPiecesUnlock[
                                Datas.truckData.ToArray().IndexOf(x =>
                                    x.id == UserDataManager.Instance.data.truckData[i].dataID)].Value = true;
                    }

                    Show();
                }
            }).AddTo(this);

            TutorialManager.Instance.tutorialIndex.Subscribe(index =>
            {
                if (UserDataManager.Instance.data.hasTutorial.Value)
                {
                    if (index <= TutorialManager.Instance.datas.Length - 1)
                    {
                        Show();
                    }
                }
            }).AddTo(this);

            btnblackPanel.OnClickAsObservable().Subscribe(_ =>
            {
                if (TutorialManager.Instance.tutorialIndex.Value
                    == TutorialManager.Instance.datas.Length - 1)
                {
                    btnContinue.onClick.Invoke();
                }
            }).AddTo(this);


            btnCusor.OnClickAsObservable().Subscribe(_ =>
            {
                TutorialData tutorialData = TutorialManager.Instance.GetData();
                tutorialData?.funcEvent.Invoke();
                trsCusor.gameObject.SetActive(false);

                if (trsCopyTarget != null && !tutorialData.keepCopyTarget)
                {
                    Utilities.RemoveObject(trsCopyTarget.gameObject);
                }

                UnirxExtension.DateTimer(DateTime.Now.AddSeconds(0.3f))
                    .Subscribe(__ => { TutorialManager.Instance.SetNext(); })
                    .AddTo(this);
            }).AddTo(this);

            btnSkipDown.OnClickAsObservable().Subscribe(_ => { Skip(); }).AddTo(this);
            btnSkipUp.OnClickAsObservable().Subscribe(_ => { Skip(); }).AddTo(this);
            btnContinue.OnClickAsObservable().Subscribe(_ =>
            {
                TutorialManager.Instance.SetNext();
                SetLast();
            }).AddTo(this);

            GameManager.Instance.camera.Cam.transform.LateUpdateAsObservable().Subscribe(_ =>
            {
                if (trsTarget != null)
                {
                    if (tutorialData.isWorldCanvas)
                    {
                        trsCusor.anchoredPosition = Utilities.worldToUISpace(GameManager.Instance.camera3DUI,
                            GameManager.Instance.canvas, trsTarget.position);
                    }
                    else
                    {
                        trsCusor.position = trsTarget.position;
                    }
                }
            }).AddTo(this);
        }

        void Skip()
        {
            AudioManager.Instance.PlaySound("sfx_button_main");

            Popup_Tutorial_Skip.Instance.Show(
                () =>
                {
                    switch (TutorialManager.Instance.tutorialIndex.Value)
                    {
                        case 1:
                            UICityMenu.Instance.Show(GameManager.Instance.FindStation("MONTPELIER"));
                            break;
                        case 17:
                            UICityMenu.Instance.Show(GameManager.Instance.FindStation("NEW YORK"));
                            break;
                    }
                },
                () =>
                {
                    FBAnalytics.FBAnalytics.LogTutorialSkipEvent(TutorialManager.Instance.tutorialIndex.Value);
                    UserDataManager.Instance.data.hasTutorial.Value = false;
                    SetLast();
                });
        }

        public void SetLast()
        {
            int[] arrCity = {101, 102, 103, 104, 105, 106, 107};

            int[][] arrCityCargos =
            {
                new[] {102, 102, 102, 104, 104, 104, 103, 103, 106, 106},
                new[] {106, 106, 106, 107, 107, 101, 101, 103, 103, 103},
                new[] {105, 105, 105, 105, 102, 102, 102, 101, 101, 101},
                new[] {102, 105, 107, 101, 101, 101, 106, 106, 106, 106},
                new[] {101, 102, 103, 103, 103, 106, 106, 106, 107, 104},
                new[] {105, 105, 107, 107, 102, 102, 104, 104, 101, 101},
                new[] {106, 106, 106, 106, 102, 102, 102, 105, 105, 104}
            };

            for (int i = 0; i < arrCity.Length; i++)
            {
                City city = GameManager.Instance.FindStation(arrCity[i]);
                city.cargos.Clear();

                for (int j = 0; j < arrCityCargos[i].Length; j++)
                {
                    city.AddCargo(Cargo.MakeWithData(city, GameManager.Instance.FindStation(arrCityCargos[i][j])));
                    Debug.Log($"Cargo {arrCity[i]} ==> {arrCityCargos[i][j]}");
                }
            }


            List<RewardModel> listReward = UIRewardManager.Instance.MakeRewardList(
                Datas.baseData[0].tutorial_end_cg[1],
                Datas.baseData[0].tutorial_end_cg[0],
                0);
            listReward.Add(UIRewardManager.Instance.MakeRewardBooster(Datas.buffData.speed, 30));

            Popup_Reward.Instance.Show(listReward, () =>
            {
                AudioManager.Instance.PlaySound("sfx_shop_booster_get");
                UserDataManager.Instance.SaveData();
            });
        }

        public void Show()
        {
            preTutorialData = tutorialData;
            tutorialData = TutorialManager.Instance.GetData();
            trsTarget = tutorialData.GetTarget();

            trsCusor.gameObject.SetActive(trsTarget != null);
            for (int i = 0; i < trsCusor.childCount; i++)
            {
                trsCusor.GetChild(i).gameObject
                    .SetActive(i == (int) tutorialData.cusorPos && tutorialData.cusorPos != eCusor.NONE);
            }

            if (LunarConsoleVariables.isTutorialSkip)
            {
                btnSkipDown.gameObject.SetActive(tutorialData.funcEvent != null
                                                 || tutorialData.funcFindTarget != null);
                btnSkipUp.gameObject.SetActive(tutorialData.funcEvent != null
                                               || tutorialData.funcFindTarget != null);
            }
            else
            {
                btnSkipDown.gameObject.SetActive(false);
                btnSkipUp.gameObject.SetActive(false);
            }

            btnContinue.gameObject.SetActive(TutorialManager.Instance.tutorialIndex.Value ==
                                             TutorialManager.Instance.datas.Length - 1);

            if (trsTarget != null)
            {
                if (tutorialData.cusorPos == eCusor.NONE && tutorialData.funcEvent == null)
                {
                    trsCopyTarget = Instantiate(trsTarget);
                    trsCopyTarget.SetParent(transform, false);
                    trsCopyTarget.SetAsFirstSibling();
                    trsCopyTarget.GetComponent<RectTransform>().sizeDelta = new Vector2(260, 96);
                    trsCopyTarget.position = trsTarget.position;
                    trsCopyTarget.DOScale(1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo);

                    UnirxExtension.DateTimer(DateTime.Now.AddSeconds(3))
                        .Subscribe(_ => TutorialManager.Instance.SetNext())
                        .AddTo(this);
                }
            }

            if (preTutorialData == null
                || preTutorialData.textId != tutorialData.textId
                || preTutorialData.messagePos != tutorialData.messagePos)
            {
                trsDescUp.parent.gameObject.SetActive(false);
                trsDescUp.gameObject.SetActive(false);
                trsDescDown.gameObject.SetActive(false);

                if (tutorialData.messagePos != eMessage.NONE)
                {
                    trsDescUp.parent.gameObject.SetActive(true);

                    switch (tutorialData.messagePos)
                    {
                        case eMessage.TOP:
                            trsDescUp.gameObject.SetActive(tutorialData.messagePos == eMessage.TOP);
                            txtDescUp.text = Utilities.GetStringByData(tutorialData.textId);
                            break;
                        case eMessage.BOTTOM:
                            trsDescDown.gameObject.SetActive(tutorialData.messagePos == eMessage.BOTTOM);
                            txtDescDown.text = Utilities.GetStringByData(tutorialData.textId);
                            break;
                    }
                }
            }
        }
    }
}