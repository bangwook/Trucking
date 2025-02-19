using DG.Tweening;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.CellView;
using Trucking.UI.Popup;
using UnityEngine;

namespace Trucking.UI
{
    public class EditVeiw_Allocate : MonoBehaviour
    {
        public EditCellView targetTruckView;

        private int selectedCellIndex;
        private City selectedCity;

        public void Show(EditCellData targetData)
        {
            targetTruckView.SetData(targetData);
            targetTruckView.HideAllButton();
            GameManager.Instance.fsm.PushState(GameManager.Instance.allocateTruckState);

            GetComponent<RectTransform>().anchoredPosition =
                new Vector2(-600, GetComponent<RectTransform>().anchoredPosition.y);
            gameObject.SetActive(true);
            GetComponent<RectTransform>().DOAnchorPosX(0, 0.3f);
        }

        public void Close()
        {
            GetComponent<RectTransform>().DOAnchorPosX(-600, 0.3f).OnComplete(() => { gameObject.SetActive(false); });
        }

        public void BackKey()
        {
            Popup_Common.Instance.Show(Utilities.GetStringByData(20064)
                    , Utilities.GetStringByData(30012)
                    , false)
                .SetLeft(Utilities.GetStringByData(20066))
                .SetRight(Utilities.GetStringByData(20065), Popup_Common.ButtonColor.Blue, () =>
                {
                    GameManager.Instance.fsm.PopState();
                    EditView.Instance.MakeList();
                    EditView.Instance.SelectTruckLane();
                });
        }

        public void ShowDraggableCities()
        {
            foreach (var city in GameManager.Instance.cities)
            {
                city.ui.SetEditState(UICity.EditStateEnum.normal);
            }

            foreach (var city in GameManager.Instance.cities)
            {
                if (city.IsOpen() && city.HasRoad())
                {
                    city.ui.SetEditState(UICity.EditStateEnum.focus, EditView.Instance.selectedTruck.Value.color);
                }
            }

            foreach (var road in GameManager.Instance.roads)
            {
                road.ui.Clear();
            }
        }
    }
}