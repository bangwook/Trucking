using System;
using DatasTypes;
using TMPro;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.Mission;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI
{
    public class UICity : MonoBehaviour
    {
        public enum StateEnum
        {
            None,
            Normal,
            Edit,
            Job
        }

        public enum EditStateEnum
        {
            normal,
            focus,
            plus,
            minus,
            select
        }

        public GameObject stateNormal;
        public GameObject stateEdit;

        public Button btnReward;
        public Button btnUndo;
        public Button btnFocus;
        public Button btnPlus;
        public Button btnMinus;
        public Button btnDot;

        public GameObject truckIcon;
        public GameObject startIcon;
        public Button btnTruckIcon;

//        public TextMeshProUGUI txtTruckNum;
        public TextMeshProUGUI txtName;

        public GameObject truckIcon_edit;
        public TextMeshProUGUI txtEditName;


        private City city;
        public GameObject dotSmall_white;
        public GameObject dotSmall_gray;

        public GameObject dotLarge_white;
        public GameObject dotLarge_plus;
        public GameObject dotLarge_minus;
        public GameObject cargoJobIcon;


        public TextMeshProUGUI txtPartsTime;
        public Image imgPart;
        public GameObject production_on;
        public GameObject production_off;
        public GameObject production_check;
        public GameObject production_upgrade;
        public GameObject production_alert;
        public Button btnProduction;

        public Button btnMission;
        public GameObject missionAlert;
        public Image imgMission;

        private ReactiveProperty<StateEnum> menuState = new ReactiveProperty<StateEnum>(StateEnum.None);

        private void Start()
        {
            btnUndo.gameObject.SetActive(false);
            btnFocus.gameObject.SetActive(false);
        }

        public void SetData(City _city)
        {
            city = _city;
            name = city.name;
            txtName.text = Utilities.GetStringByData(city.data.string_name);
            txtName.gameObject.SetActive(true);
            txtEditName.text = city.name;
            txtEditName.gameObject.SetActive(true);

            transform.localPosition = new Vector3(city.transform.localPosition.x,
                city.transform.localPosition.z,
                -20);

            btnTruckIcon.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    ((GameState) GameManager.Instance.fsm.GetCurrentState())
                        .OnClickStation(GameManager.Instance, city);
                }).AddTo(this);

            btnUndo.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (GameManager.Instance.fsm.GetCurrentState() == GameManager.Instance.jobState)
                    {
                        AudioManager.Instance.PlaySound("sfx_button_main");
                        JobView.Instance.truck.Value.UndoPath();
                    }
                }).AddTo(this);

            btnReward.OnClickAsObservable()
                .Subscribe(_ => { UIRewardManager.Instance.Show(city); }).AddTo(this);

            btnFocus.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (GameManager.Instance.fsm.GetCurrentState() == GameManager.Instance.jobState)
                    {
                        JobView.Instance.Button_City(city);
                    }
                    else
                    {
                        EditView.Instance.Button_Focus(city);
                    }
                }).AddTo(this);

            btnDot.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (GameManager.Instance.fsm.GetCurrentState() == GameManager.Instance.jobState)
                    {
                        JobView.Instance.Button_City(city);
                    }
                    else
                    {
                        EditView.Instance.Button_Focus(city);
                    }
                }).AddTo(this);


            btnPlus.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (EditView.Instance.selectCity != null)
                    {
                        Debug.Log(EditView.Instance.selectCity.name + " was clicked on " + gameObject.name);
                        EditView.Instance.Button_Plus(EditView.Instance.selectCity, city);
                    }
                }).AddTo(this);


            btnMinus.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (EditView.Instance.selectCity != null)
                    {
                        Debug.Log(EditView.Instance.selectCity.name + " was clicked on " + gameObject.name);
                        EditView.Instance.Button_Minus(EditView.Instance.selectCity, city);
                    }
                }).AddTo(this);

            btnProduction.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    ((GameState) GameManager.Instance.fsm.GetCurrentState())
                        .OnClickStation(GameManager.Instance, city);
                }).AddTo(this);

            btnMission.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    ((GameState) GameManager.Instance.fsm.GetCurrentState())
                        .OnClickStation(GameManager.Instance, city);
                    UICityMenu.Instance.btnMission.onClick.Invoke();
                }).AddTo(this);


            Observable.CombineLatest(city.completeCargoModels.ObsSomeChanged(), menuState,
                    (cargos, state) => cargos.Count > 0 && state == StateEnum.Normal)
                .Subscribe(value => { btnReward.gameObject.SetActive(value); })
                .AddTo(this);

            Observable.CombineLatest(city.trucks.ObsSomeChanged(),
                    menuState,
                    UICityMenu.Instance.gameObject.ObserveEveryValueChanged(x => x.activeSelf),
                    (trucks, state, uiCityMenu) => trucks.Count > 0 && state == StateEnum.Normal && !uiCityMenu)
                .Subscribe(value => { truckIcon.gameObject.SetActive(value); })
                .AddTo(this);

            Observable.CombineLatest(city.model.state,
                    menuState,
                    UICityMenu.Instance.city,
                    btnReward.gameObject.ObserveEveryValueChanged(x => x.activeSelf),
                    (cityState, menuState, cityMenu, rewardActive) =>
                        (cityState == CityModel.State.Wait
                         || cityState == CityModel.State.Craft
                         || cityState == CityModel.State.Upgrade
                         || cityState == CityModel.State.Craft_Collect
                         || cityState == CityModel.State.Upgrade_Complete)
                        && menuState == StateEnum.Normal
                        && city.IsMega()
                        && cityMenu == null
                        && !rewardActive)
                .Subscribe(value => { btnProduction.gameObject.SetActive(value); })
                .AddTo(this);

            city.model.state.Subscribe(state =>
            {
                production_on.SetActive(state == CityModel.State.Craft_Collect ||
                                        state == CityModel.State.Upgrade_Complete);
                production_off.SetActive(state != CityModel.State.Craft_Collect
                                         && state != CityModel.State.Upgrade_Complete);
                production_check.SetActive(state == CityModel.State.Craft_Collect);
                txtPartsTime.gameObject.SetActive(state == CityModel.State.Craft
                                                  || state == CityModel.State.Upgrade);
                production_upgrade.SetActive(state == CityModel.State.Upgrade
                                             || state == CityModel.State.Upgrade_Complete);
            }).AddTo(this);


            Observable.Merge(
                    UICityMenu.Instance.city.AsUnitObservable(),
                    menuState.AsUnitObservable(),
                    NewDailyMissionManager.Instance.city.AsUnitObservable(),
                    city.completeCargoModels.ObserveCountChanged().AsUnitObservable())
                .Subscribe(value =>
                {
                    btnMission.gameObject.SetActive(UICityMenu.Instance.city.Value == null
                                                    && menuState.Value == StateEnum.Normal
                                                    && NewDailyMissionManager.Instance.city.Value == city
                                                    && city.completeCargoModels.Count == 0);

                    if (NewDailyMissionManager.Instance.city.Value == city)
                    {
                        imgMission.sprite =
                            GameManager.Instance.GetCargoSprite(NewDailyMissionManager.Instance.GetCargoId());
                    }
                }).AddTo(this);

            Observable.CombineLatest(NewDailyMissionManager.Instance.hasReward,
                    NewDailyMissionManager.Instance.isNew,
                    (isSuccess, isNew) => isSuccess || isNew)
                .Subscribe(alert => missionAlert.SetActive(alert))
                .AddTo(this);

            if (city.IsMega())
            {
                imgPart.sprite = GameManager.Instance.GetRewardImage(RewardData.eType.parts,
                    RewardModel.GetIndex(RewardData.eType.parts, city.partData.output));

                Observable.CombineLatest(city.model.state,
                    UserDataManager.Instance.data.gold,
                    UserDataManager.Instance.data.cityMaterials[city.matIndex],
                    (state, gold, mat) => (state, gold, mat)).Subscribe(value =>
                {
                    production_alert.SetActive(city.CanCraft() && value.Item1 == CityModel.State.Wait);
                }).AddTo(this);

                Observable.EveryUpdate().Subscribe(x =>
                {
                    if (city.model.state.Value == CityModel.State.Craft
                        || city.model.state.Value == CityModel.State.Upgrade)
                    {
                        txtPartsTime.text = Utilities.GetTimeStringShort(city.model.productTime.Value - DateTime.Now);
                    }
                }).AddTo(this);
            }

            SetState(StateEnum.Normal);

            gameObject.SetActive(true);
            btnFocus.transform.parent.gameObject.SetActive(true);
        }

        public void SetState(StateEnum _state)
        {
            menuState.Value = _state;
            stateNormal.SetActive(_state == StateEnum.Normal || _state == StateEnum.Job);
            stateEdit.SetActive(_state == StateEnum.Edit);
            btnFocus.gameObject.SetActive(false);
            truckIcon.SetActive(_state == StateEnum.Normal && city.trucks.Count > 0);
            btnFocus.transform.parent.localPosition = new Vector3(0, 6, 0);

            SetStartIcon(false);
        }

        public void SetDepartUndo(bool _selected)
        {
            btnUndo.gameObject.SetActive(_selected);
        }

        public void SetDepartFocus(bool _selected)
        {
            btnFocus.gameObject.SetActive(_selected);
        }

        public void SetStartIcon(bool _start)
        {
            startIcon.SetActive(_start);
        }


        public void SetEditState(EditStateEnum _editState, Color color = default(Color), bool showTruck = false)
        {
            if (gameObject.activeSelf)
            {
                dotSmall_white.SetActive(_editState == EditStateEnum.normal && city.IsOpenRoad());
                dotSmall_white.transform.GetChild(0).gameObject.SetActive(_editState == EditStateEnum.normal);
                dotSmall_gray.SetActive(_editState == EditStateEnum.normal && !city.IsOpenRoad());
                dotLarge_white.SetActive(_editState == EditStateEnum.select || _editState == EditStateEnum.focus);
                dotLarge_plus.SetActive(_editState == EditStateEnum.plus);
                dotLarge_minus.SetActive(_editState == EditStateEnum.minus);
                btnFocus.gameObject.SetActive(_editState == EditStateEnum.focus);
                truckIcon_edit.SetActive(false);
                cargoJobIcon.SetActive(false);

                // select focus icon 20
                // defualt 6
                btnFocus.transform.parent.localPosition = new Vector3(0, showTruck ? 20 : 6, 0);

                if ((_editState == EditStateEnum.normal || _editState == EditStateEnum.select ||
                     _editState == EditStateEnum.focus) && color != default(Color))
                {
                    dotSmall_white.GetComponent<Image>().color = color;
                    dotLarge_white.GetComponent<Image>().color = color;
                }
                else
                {
                    dotSmall_white.GetComponent<Image>().color = Color.black;
                    dotLarge_white.GetComponent<Image>().color = Color.black;
                }
            }
        }

        public void ShowEditTruck(bool show)
        {
            truckIcon_edit.SetActive(show);
        }

        public void SetCargoIcon(bool show)
        {
            txtName.fontSize = show ? 15 : 10;
            txtName.color = show ? Utilities.GetColorByHtmlString("FFDB00") : Color.white;
            txtEditName.fontSize = show ? 17 : 14;
            txtEditName.color = show ? Utilities.GetColorByHtmlString("FFDB00") : Color.white;
            cargoJobIcon.SetActive(show);
        }
    }
}