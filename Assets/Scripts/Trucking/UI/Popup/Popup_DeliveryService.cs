using System;
using TMPro;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.Mission;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_DeliveryService : Popup_Base<Popup_DeliveryService>
    {
        public Button btnClaim;
        public TextMeshProUGUI txtOnDeliverty;
        public TextMeshProUGUI txtTargetCityName;
        public TextMeshProUGUI txtTime;
        public TextMeshProUGUI txtTargetReward;
        public Transform trsBubble;
        public Image imgReward;
        public RectTransform truck;
        public Transform trsCells;

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private Popup_DeliveryService_Cell[] cells = new Popup_DeliveryService_Cell[5];

        private void Start()
        {
            for (int i = 0; i < trsCells.childCount; i++)
            {
                cells[i] = trsCells.GetChild(i).GetComponent<Popup_DeliveryService_Cell>();
                cells[i].SetIndex(i);
            }

            btnClaim.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");

                    FBAnalytics.FBAnalytics.LogNPDSEvent(UserDataManager.Instance.data.lv.Value,
                        DeliveryServiceManager.Instance.model.targetIndex.Value + 1);

                    Popup_Reward.Instance.Show(
                        DeliveryServiceManager.Instance.GetReward(DeliveryServiceManager.Instance.model.targetIndex
                            .Value), () =>
                        {
                            DeliveryServiceManager.Instance.SetNext();


//                        if (DeliveryServiceManager.Instance.model.rewardIndex.Value < 0
//                            || DeliveryServiceManager.Instance.model.rewardIndex.Value >= 4)
//                        {
//                            GameManager.Instance.fsm.PopState();
//                        }
                        });
                })
                .AddTo(this);

            DeliveryServiceManager.Instance.model.targetIndex.Subscribe(idx =>
            {
                truck.position = cells[idx].slider.handleRect.position;
                txtTime.text = Utilities.GetTimeString(DeliveryServiceManager.Instance.GetTime());

                RewardModel reweaModel =
                    DeliveryServiceManager.Instance.GetReward(DeliveryServiceManager.Instance.model.targetIndex.Value);

                txtTargetReward.text = Utilities.GetNumberKKK(reweaModel.count.Value);
                txtTargetCityName.text =
                    DeliveryServiceManager.Instance.GetCityName(idx);
                trsBubble.localPosition = cells[idx].imgIcon.transform.parent.localPosition + new Vector3(0, 70, 0);
                imgReward.sprite = GameManager.Instance.GetRewardImage(reweaModel);
            }).AddTo(_compositeDisposable);

            DeliveryServiceManager.Instance.model.rewardIndex
                .Subscribe(idx => { trsBubble.gameObject.SetActive(idx < 5); }).AddTo(_compositeDisposable);

            DeliveryServiceManager.Instance.model.isMove
                .Subscribe(move =>
                {
                    txtOnDeliverty.gameObject.SetActive(move);

                    if (!move)
                    {
                        txtTime.text = Utilities.GetTimeString(DeliveryServiceManager.Instance.GetTime());
                    }
                }).AddTo(_compositeDisposable);

            DeliveryServiceManager.Instance.model.hasReward
                .Subscribe(hasReward => { btnClaim.gameObject.SetActive(hasReward); }).AddTo(_compositeDisposable);

            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (DeliveryServiceManager.Instance.model.isMove.Value)
                {
                    truck.position = cells[DeliveryServiceManager.Instance.model.targetIndex.Value].slider.handleRect
                        .position;
                    txtTime.text =
                        Utilities.GetTimeString(DeliveryServiceManager.Instance.model.endTime.Value - DateTime.Now);
                }
                else if (!DeliveryServiceManager.Instance.model.hasEvent.Value)
                {
                    txtTime.text =
                        Utilities.GetTimeString(DeliveryServiceManager.Instance.model.endTime.Value - DateTime.Now);
                }
            }).AddTo(_compositeDisposable);
        }
    }
}