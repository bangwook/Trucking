using Trucking.Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI
{
    public class UICityMenuBlackPanel : MonoSingleton<UICityMenuBlackPanel>
    {
        public Transform trsHole;
        public Button btnBlack;
        public float scaleMin = 900;
        public float scaleMax = 700;

        private void Start()
        {
            btnBlack.OnClickAsObservable().Subscribe(_ => { GameManager.Instance.fsm.PopState(); }).AddTo(this);
        }

        private void LateUpdate()
        {
            if (UICityMenu.Instance.gameObject.activeSelf
                && UICityMenu.Instance.city.Value != null
                && Camera.main != null)
            {
                var newPos = Vector2.zero;
                var uiCamera = GameManager.Instance.camera3DUI;
                var worldCamera = GameManager.Instance.camera3DUI;
                var canvasRect = GameManager.Instance.canvasWorld.GetComponent<RectTransform>();

                var screenPos = RectTransformUtility.WorldToScreenPoint(worldCamera,
                    UICityMenu.Instance.city.Value.transform.position);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, uiCamera, out newPos);

                trsHole.localPosition = Utilities.worldToUISpace(GameManager.Instance.camera3DUI,
                    GameManager.Instance.canvas, UICityMenu.Instance.city.Value.transform.position);

                float size = Mathf.Lerp(scaleMin, scaleMax,
                    (GameManager.Instance.camera.CamZoom - GameManager.Instance.camera.CamZoomMin) /
                    (GameManager.Instance.camera.CamZoomMax - GameManager.Instance.camera.CamZoomMin));

                if (UICityMenu.Instance.city.Value.IsMega())
                {
                    size = Mathf.Lerp(scaleMin + 200, scaleMax + 200,
                        (GameManager.Instance.camera.CamZoom - GameManager.Instance.camera.CamZoomMin) /
                        (GameManager.Instance.camera.CamZoomMax - GameManager.Instance.camera.CamZoomMin));
                }

                trsHole.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
            }
        }
    }
}