using System;
using Trucking.UI.Mission;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_DeliveryService_Cell : MonoBehaviour
    {
        public Slider slider;
        public GameObject check;
        public Image imgIcon;
        public Transform trsDay;

        private int currentIndex;

        public void SetIndex(int _index)
        {
            currentIndex = _index;

            check.SetActive(false);
            imgIcon.sprite =
                GameManager.Instance.GetRewardImage(DeliveryServiceManager.Instance.GetReward(currentIndex));

            if (slider != null)
            {
                slider.value = 0;
                slider.maxValue = 1;
            }

            DeliveryServiceManager.Instance.model.rewardIndex
                .Subscribe(idx =>
                {
                    check.gameObject.SetActive(currentIndex < idx);
                    trsDay.GetChild(0).gameObject.SetActive(idx < currentIndex);
                    trsDay.GetChild(1).gameObject.SetActive(idx > currentIndex);
                    trsDay.GetChild(2).gameObject.SetActive(idx == currentIndex);
                }).AddTo(this);

            DeliveryServiceManager.Instance.model.targetIndex.Subscribe(idx =>
            {
                if (slider != null)
                {
                    if (currentIndex > idx)
                    {
                        slider.value = 0;
                    }
                    else if (currentIndex < idx)
                    {
                        slider.value = slider.maxValue;
                    }
                    else
                    {
                        slider.value = 0;
                    }
                }
            }).AddTo(this);

            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (DeliveryServiceManager.Instance.model.targetIndex.Value == currentIndex
                    && DeliveryServiceManager.Instance.model.isMove.Value)
                {
                    float total = (float) (DeliveryServiceManager.Instance.model.endTime.Value -
                                           DeliveryServiceManager.Instance.model.startTime.Value).TotalSeconds;

                    slider.value =
                        (total - (float) (DeliveryServiceManager.Instance.model.endTime.Value - DateTime.Now)
                         .TotalSeconds) / total;
                }
            }).AddTo(this);
        }
    }
}