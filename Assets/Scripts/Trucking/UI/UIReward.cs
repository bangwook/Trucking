using System;
using System.Collections.Generic;
using DatasTypes;
using TMPro;
using Trucking.Common;
using Trucking.Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI
{
    public class UIReward : MonoBehaviour
    {
        public Transform trsGrid;
        public Image[] imgIcon;
        public TextMeshProUGUI[] txtCount;
        public TextMeshProUGUI[] txtBonus;
        public GameObject[] txtBoost;

        private List<RewardModel> rewardDatas;

        public void Show(List<RewardModel> _rewardDatas, Vector3 pos, bool isBoost = false, bool isBonus = false,
            Action _onClaim = null)
        {
            gameObject.SetActive(true);
            MakeIcon(_rewardDatas, isBoost, isBonus);

            var newPos = Vector2.zero;
            var uiCamera = GameManager.Instance.camera3DUI;
            var worldCamera = GameManager.Instance.camera3DUI;
            var canvasRect = GameManager.Instance.canvasWorld.GetComponent<RectTransform>();

            var screenPos = RectTransformUtility.WorldToScreenPoint(worldCamera, pos);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, uiCamera, out newPos);
            transform.localPosition = newPos;

            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(1.5f)).Subscribe(__ =>
            {
                gameObject.SetActive(false);
                _onClaim?.Invoke();
            }).AddTo(this);
        }

        private void LateUpdate()
        {
            if (Camera.main != null)
            {
                transform.rotation = Camera.main.transform.rotation;
            }
        }

        public void MakeIcon(List<RewardModel> _rewardDatas, bool isBoost = false, bool isBonus = false)
        {
            gameObject.SetActive(true);

            rewardDatas = _rewardDatas;

            for (int i = 0; i < trsGrid.childCount; i++)
            {
                trsGrid.GetChild(i).gameObject.SetActive(false);
            }

            Debug.Assert(_rewardDatas.Count <= trsGrid.childCount);

            int childIndex = 0;

            foreach (var rewardModel in rewardDatas)
            {
                GameObject child = trsGrid.GetChild(childIndex).gameObject;
                child.SetActive(true);

                txtBoost[childIndex].gameObject.SetActive(false);
                imgIcon[childIndex].sprite = GameManager.Instance.GetRewardImage(rewardModel);
                txtBonus[childIndex].gameObject.SetActive(isBonus);

                if (isBoost
                    && rewardModel.type.Value == RewardData.eType.gold
                    && UserDataManager.Instance.GetBooster(UserDataManager.BoosterType.GOLD) > 0)
                {
                    float boostRate = UserDataManager.Instance.GetBooster(UserDataManager.BoosterType.GOLD);

                    if (boostRate > 0)
                    {
                        txtBoost[childIndex].gameObject.SetActive(isBoost);
                        float result = rewardModel.count.Value * (1 + boostRate / 100);
                        txtCount[childIndex].text =
                            Utilities.GetNumberKKK((long) result) + "\n<color=#FFC813>+" + boostRate + "%";
                        rewardModel.count.Value = (long) result;
                    }
                }
                else if (isBoost
                         && rewardModel.type.Value == RewardData.eType.exp
                         && UserDataManager.Instance.GetBooster(UserDataManager.BoosterType.XP) > 0)
                {
                    float boostRate = UserDataManager.Instance.GetBooster(UserDataManager.BoosterType.XP);

                    if (boostRate > 0)
                    {
                        txtBoost[childIndex].gameObject.SetActive(isBoost);
                        float result = rewardModel.count.Value * (1 + boostRate / 100);
                        txtCount[childIndex].text =
                            Utilities.GetNumberKKK((long) result) + "\n<color=#FFC813>+" + boostRate + "%";
                        rewardModel.count.Value = (long) result;
                    }
                }
                else
                {
                    txtCount[childIndex].color = rewardModel.count.Value > 0 ? Color.white : Color.red;

                    if (rewardModel.count.Value > 0)
                    {
                        txtCount[childIndex].text = "+" + Utilities.GetNumberKKK(rewardModel.count.Value);
                    }
                    else
                    {
                        txtCount[childIndex].text = Utilities.GetNumberKKK(rewardModel.count.Value);
                    }
                }


                childIndex++;
            }
        }
    }
}