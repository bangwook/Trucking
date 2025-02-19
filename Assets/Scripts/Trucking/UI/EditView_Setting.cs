using System.Collections.Generic;
using System.Linq;
using Coffee.UIExtensions;
using DatasTypes;
using DG.Tweening;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.CellView;
using Trucking.UI.Popup;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI
{
    public class EditView_Setting : MonoBehaviour
    {
        public EditCellView targetTruckView;
        public Transform trsColorButtons;
        public Button btnPullout;

        private Truck targetTruck;

        private void Start()
        {
            btnPullout.OnClickAsObservable().Subscribe(_ =>
            {
                if (btnPullout.GetComponent<UIEffect>().enabled)
                {
                    AudioManager.Instance.PlaySound("sfx_require");
                    btnPullout.transform.DOShakePosition(0.3f, 3);
                }
                else
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    LevelData levelData =
                        GameManager.Instance.GetRouteCountLevelData(GameManager.Instance.RouteCount.Value);
                    int roadCount = GameManager.Instance.roads.Count(x => x.truck.Value == targetTruck);

                    Popup_Common.Instance.Show(Utilities.GetStringByData(30013),
                            Utilities.GetStringByData(30014), false)
                        .SetResource(RewardData.eType.gold,
                            Datas.levelData[0].route_delete_refund * roadCount + levelData.route_price)
                        .SetLeft(Utilities.GetStringByData(20066))
                        .SetRight(Utilities.GetStringByData(20152), Popup_Common.ButtonColor.Red, () =>
                        {
                            List<Road> roads = GameManager.Instance.roads.ToList()
                                .FindAll(x => x.truck.Value == targetTruck);

                            foreach (var road in roads)
                            {
                                road.truck.Value = null;
                                road.model.truckBirthID.Value = 0;
                            }

                            UIRewardManager.Instance.Show(targetTruck.currentStation.Value,
                                Datas.levelData[0].route_delete_refund * roadCount + levelData.route_price, 0, 0);
                            targetTruck.model.hasRoute.Value = false;
                            targetTruck.currentStation.Value = null;

                            UserDataManager.Instance.SaveData();
                            GameManager.Instance.fsm.PopState();
                            EditView.Instance.MakeList();
                            EditView.Instance.SelectTruckLane();
                        });
                }
            }).AddTo(this);

            for (int i = 0; i < trsColorButtons.childCount; i++)
            {
                trsColorButtons.GetChild(i).GetComponent<Button>()
                    .OnClickButtonAsObservable()
                    .Subscribe(button =>
                    {
                        AudioManager.Instance.PlaySound("sfx_button_main");
                        SetColor(button.transform.GetSiblingIndex());

                        foreach (var road in GameManager.Instance.roads.Where(x => x.truck.Value == targetTruck))
                        {
                            road.SetTruckColor(true);
                        }

                        EditView.Instance.SelectTruckLane(targetTruck);
                        UserDataManager.Instance.SaveData();
                    }).AddTo(this);
            }
        }

        public void Show(EditCellData targetData)
        {
            GameManager.Instance.fsm.PushState(GameManager.Instance.settingTruckState);

            gameObject.SetActive(true);
            GetComponent<RectTransform>().anchoredPosition =
                new Vector2(-600, GetComponent<RectTransform>().anchoredPosition.y);
            GetComponent<RectTransform>().DOAnchorPosX(0, 0.3f);

            targetTruck = targetData.truck;
            targetTruckView.SetData(targetData);
            targetTruckView.HideAllButton();
            targetTruckView.goMaxSpec.SetActive(true);
            SetColor(targetTruck.model.colorIndex.Value - 1);

            bool lastRoute = GameManager.Instance.trucks.Count(x => x.model.hasRoute.Value) <= 1;
            btnPullout.GetComponent<UIEffect>().enabled = lastRoute;

            UIToastMassage.Instance.Hide();
        }

        public void Close()
        {
            EditView.Instance.SelectTruckLane(targetTruck);

            GetComponent<RectTransform>().DOAnchorPosX(-600, 0.3f).OnComplete(() =>
            {
                gameObject.SetActive(false);
//                GameManager.Instance.fsm.PopState();
            });
        }

        void SetColor(int index)
        {
            for (int j = 0; j < trsColorButtons.childCount; j++)
            {
                if (j == index)
                {
                    trsColorButtons.GetChild(j).GetComponent<Image>().color = Utilities.GetColorByHtmlString("4D4D4D");
                }
                else
                {
                    trsColorButtons.GetChild(j).GetComponent<Image>().color = Color.white;
                }
            }

            targetTruck.model.colorIndex.Value = index + 1;
        }
    }
}