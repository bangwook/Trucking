using System;
using DatasTypes;
using TMPro;
using Trucking.Ad;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.Mission;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_NoFuel : Popup_Base<Popup_NoFuel>
    {
        public Button btnLeft;
        public Button btnRight;
        public TextMeshProUGUI txtLeft;
        public GameObject cashBtnLeft;

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private Truck truck;
        private Action OnFuel;

        private void Start()
        {
            btnLeft.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    int costGold = truck.GetReFuelGold();

                    if (UserDataManager.Instance.UseGold(costGold))
                    {
                        GameManager.Instance.fsm.PopState();
                        AddFuel(truck.MaxFuel.Value - truck.model.fuel.Value);
                        OnFuel?.Invoke();
                    }
                }).AddTo(this);

            btnRight.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");

                    if (AdManager.Instance.IsLoadedReward.Value)
                    {
                        Popup_Loading.Instance.Show();
                        AdManager.Instance.ShowRewardLoad(AdUnit.Free_Fuel)
                            .Subscribe(
                                result =>
                                {
                                    GameManager.Instance.fsm.PopState();
                                    GameManager.Instance.fsm.PopState();

                                    if (result == AdResult.Success)
                                    {
                                        AddFuel(truck.PathFuel.Value);
                                        OnFuel?.Invoke();
                                    }
                                    else if (result == AdResult.NoFill)
                                    {
                                        AudioManager.Instance.PlaySound("sfx_require");
                                        UIToastMassage.Instance.Show(30055);
                                    }
                                    else
                                    {
                                        AudioManager.Instance.PlaySound("sfx_require");
                                        UIToastMassage.Instance.Show(30056);
                                    }
                                }).AddTo(this);
                    }
                    else
                    {
                        AudioManager.Instance.PlaySound("sfx_require");
                        UIToastMassage.Instance.Show(30056);
                    }

//                    int costCash = truck.GetReFuelCash();
//
//                    if (UserDataManager.Instance.UseCash(costCash))
//                    {
//                        GameManager.Instance.fsm.PopState(false);
//                        MissionManager.Instance.AddValue(QuestData.eType.truck_cash_refuel, 
//                            (int)(truck.MaxFuel.Value - truck.model.fuel.Value));
//                        truck.model.fuel.Value = truck.MaxFuel.Value;
//                        OnFuel?.Invoke();
//                    }
                }).AddTo(this);
        }

        public void Show(Truck _truck, Action _OnFuel)
        {
            base.Show();
            truck = _truck;
            OnFuel = _OnFuel;
            _compositeDisposable.Clear();

            int costGold = truck.GetReFuelGold();
//            int costCash = truck.GetReFuelCash();

            btnLeft.GetComponent<Image>().sprite = GameManager.Instance.atlasUI.GetSprite("button_edit_y");
            txtLeft.transform.localPosition = new Vector3(22, 0, 0);
            txtLeft.text = Utilities.GetThousandCommaText(costGold);
            UserDataManager.Instance.data.gold.Subscribe(gold =>
            {
                txtLeft.color = gold < costGold ? Color.red : Color.white;
                cashBtnLeft.SetActive(gold < costGold);
            }).AddTo(_compositeDisposable);

//            btnRight.GetComponent<Image>().sprite = GameManager.Instance.atlasUI.GetSprite("button_edit_g");
//            txtRight.transform.localPosition = new Vector3(22, 0, 0);
//            txtRight.text = Utilities.GetThousandCommaText(costCash);
//            UserDataManager.Instance.data.cash.Subscribe(cash =>
//            {
//                txtRight.color = cash < costCash ? Color.red : Color.white;
//            }).AddTo(_compositeDisposable);
        }

        public override void BackKey()
        {
            btnCloseX.onClick.Invoke();
        }

        void AddFuel(float fuel)
        {
            MissionManager.Instance.AddValue(QuestData.eType.truck_refuel,
                (int) (fuel));
            truck.model.fuel.Value += fuel;
        }
    }
}