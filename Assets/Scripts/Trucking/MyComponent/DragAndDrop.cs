using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Trucking.MyComponent
{
    public class DragAndDrop : MonoBehaviour
    {
        public Camera camera;

        void Start()
        {
            // 부모 Canvas의 RectTransform을 기준으로 이용하는 
            var rectTransform = transform.root.gameObject.GetComponent<RectTransform>();

            // RectTransformUtility.ScreenPointToLocalPointInRectangle 일회용 변수 
            var tmpPosition = Vector2.zero;

//            var newPos = Vector2.zero;
//            var uiCamera = GameManager.Instance.camera3DUI;
//            var worldCamera = GameManager.Instance.camera3DUI;
//            var canvasRect = GameManager.Instance.canvasWorld.GetComponent<RectTransform>();
//
//            var screenPos = RectTransformUtility.WorldToScreenPoint(worldCamera, pos);
//            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, uiCamera, out newPos);
//            transform.localPosition = newPos;


            // ObservableEventTrigger을 개체에 추가 
            ObservableEventTrigger trigger = gameObject.AddComponent<ObservableEventTrigger>();

            // Drag 이벤트 등록 
            trigger.OnDragAsObservable()
                // 발생할 때마다 마우스 위치를 취득한다. 
                .Select(_ => Input.mousePosition)
                // 그러나 Input.mousePosition는 World 좌표이므로 그대로는 사용할 수 없다. 부모 Canvas의 UI 좌표로 변환한다. 
                .Select(mousePosition =>
                {
                    // ※ 참고 : 부모 Canvas의 Render Mode를 Screen Space Camera하고있는 경우는 이것으로 좋다, Screen Space Overlay의 경우 또 다른 설명된다. 
                    // http : // tsubakit1.hateblo.jp/entry/2016/03/01/020510를 참조하십시오.

                    Vector3 pos = mousePosition;
                    pos.z = 20;
                    return camera.ScreenToWorldPoint(mousePosition);

//                    RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePosition, camera,
//                        out tmpPosition);
//                    return tmpPosition;
                })
                // 김에 로그를 취해 본다 
                .Do(position =>
                    Debug.Log(
                        $"[Drag] Input.mousePosition.x : {Input.mousePosition.x} Input.mousePosition.y : {Input.mousePosition.y}, Canvas.LocalPosition.x : {position.x}, Canvas.LocalPosition. y : {position.y} "))
                // 자신의 위치를 마우스 UI 좌표에. 
                .Subscribe(position => transform.localPosition = position)
                // 자신이 파괴되면 이벤트도 종료. 
                .AddTo(this);

            // Drag 종료 이벤트 (즉 Drop) 등록 
            trigger.OnEndDragAsObservable()
                // 발생하면 마우스 위치를 취득한다. 
                .Select(_ => Input.mousePosition)
                .Select(mousePosition =>
                {
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePosition, camera,
                        out tmpPosition);
                    return tmpPosition;
                })
                // 드롭되면 로그를 추가 
                .Subscribe(position =>
                    Debug.Log(
                        $"[Drop] Input.mousePosition.x : {Input.mousePosition.x} Input.mousePosition.y : {Input.mousePosition.y}, Canvas.LocalPosition.x : {position.x}, Canvas.LocalPosition. y : {position.y} "))
                // 자신이 파괴되면 이벤트도 종료. 
                .AddTo(this);
        }
    }
}