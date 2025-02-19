namespace Trucking.MyComponent
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;

    [RequireComponent(typeof(Image))]
    public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Canvas canvas;

        private GameObject draggingObject;
        private IDropHandler _dropHandlerImplementation;

        public void OnBeginDrag(PointerEventData pointerEventData)
        {
            CreateDragObject();
            draggingObject.transform.position = pointerEventData.position;
        }

        public void OnDrag(PointerEventData pointerEventData)
        {
            draggingObject.transform.position = pointerEventData.position;
        }

        public void OnEndDrag(PointerEventData pointerEventData)
        {
//            gameObject.GetComponent<Image>().color = Vector4.one;
            Destroy(draggingObject);
        }

        // ドラッグオブジェクト作成
        public void CreateDragObject()
        {
            draggingObject = Instantiate(gameObject);
            draggingObject.transform.SetParent(canvas.transform);
            draggingObject.transform.SetAsLastSibling();
            draggingObject.transform.localScale = Vector3.one;

            // レイキャストがブロックされないように
            CanvasGroup canvasGroup = draggingObject.AddComponent<CanvasGroup>();
            canvasGroup.blocksRaycasts = false;
        }
    }
}