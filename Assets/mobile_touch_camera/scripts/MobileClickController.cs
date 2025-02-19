// /************************************************************
// *                                                           *
// *   Mobile Touch Camera                                     *
// *                                                           *
// *   Created 2015 by BitBender Games                         *
// *                                                           *
// *   bitbendergames@gmail.com                                *
// *                                                           *
// ************************************************************/

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace BitBenderGames {

  [RequireComponent(typeof(MobileTouchCamera))]
  public class MobileClickController : MonoBehaviour {

    public enum SelectionAction {
      Select,
      Deselect,
    }

    #region inspector
    [SerializeField]
    [Tooltip("When set to true, the position of dragged items snaps to discrete units.")]
    private bool snapToGrid = true;
    [SerializeField]
    [Tooltip("Size of the snap units when snapToGrid is enabled.")]
    private float snapUnitSize = 1;
    [SerializeField]
    [Tooltip("When snapping is enabled, this value defines a position offset that is added to the center of the object when dragging. When a top-down camera is used, these 2 values are applied to the X/Z position.")]
    private Vector2 snapOffset = Vector2.zero;
    [SerializeField]
    [Tooltip("When set to Straight, picked items will be snapped to a perfectly horizontal and vertical grid in world space. Diagonal snaps the items on a 45 degree grid.")]
    private SnapAngle snapAngle = SnapAngle.Straight_0_Degrees;
    [Header("Advanced")]
    [SerializeField]
    [Tooltip("When this flag is enabled, more than one item can be selected and moved at the same time.")]
    private bool isMultiSelectionEnabled = false;
    [SerializeField]
    [Tooltip("When setting this variable to true, pickables can only be moved by long tapping on them first.")]
    private bool requireLongTapForMove = false;
    [Header("Event Callbacks")]
    [SerializeField]
    [Tooltip("Here you can set up callbacks to be invoked when a pickable transform is selected.")]
    private UnityEventWithTransform OnPickableTransformSelected;
    [SerializeField]
    [Tooltip("Here you can set up callbacks to be invoked when a pickable transform is selected through a long tap.")]
    private UnityEventWithPickableSelected OnPickableTransformSelectedExtended;
    [SerializeField]
    [Tooltip("Here you can set up callbacks to be invoked when a pickable transform is deselected.")]
    private UnityEventWithTransform OnPickableTransformDeselected;
    [SerializeField]
    [Tooltip("Here you can set up callbacks to be invoked when the moving of a pickable transform is started.")]
    private UnityEventWithTransform OnPickableTransformMoveStarted;
    [SerializeField]
    [Tooltip("Here you can set up callbacks to be invoked when a pickable transform is moved to a new position.")]
    private UnityEventWithTransform OnPickableTransformMoved;
    [SerializeField]
    [Tooltip("Here you can set up callbacks to be invoked when the moving of a pickable transform is ended. The event requires 2 parameters. The first is the start position of the drag. The second is the dragged transform. The start position can be used to reset the transform in case the drag has ended on an invalid position.")]
    private UnityEventWithPositionAndTransform OnPickableTransformMoveEnded;
    #endregion

    #region expert mode tweakables
    [Header("Expert Mode")]
    [SerializeField]
    private bool expertModeEnabled;
    [SerializeField]
    [Tooltip("When setting this to false, pickables will not become deselected when the user clicks somewhere on the screen, except when he clicks on another pickable.")]
    private bool deselectPreviousColliderOnClick = true;
    [SerializeField]
    [Tooltip("When setting this to false, the OnPickableTransformSelect event will only be sent once when clicking on the same pickable repeatedly.")]
    private bool repeatEventSelectedOnClick = true;
    [SerializeField]
    [Tooltip("Previous versions of this asset may have fired the OnPickableTransformMoveStarted too early, when it hasn't actually been moved.")]
    private bool useLegacyTransformMovedEventOrder = false;
    #endregion

    private TouchInputController touchInputController;

    private MobileTouchCamera mobileTouchCam;

    private Component SelectedCollider {
      get {
        if(SelectedColliders.Count == 0) {
          return null;
        }
        return SelectedColliders[SelectedColliders.Count - 1];
      }
    }

    public List<Component> SelectedColliders { get; private set; }

    private bool isSelectedViaLongTap = false;

    public MobileTouchPickable CurrentlyClickable { get; private set; }

    private Transform CurrentlyDraggedTransform {
      get {
        if (CurrentlyClickable != null) {
          return (CurrentlyClickable.PickableTransform);
        } else {
          return null;
        }
      }
    }

    private Vector3 draggedTransformOffset = Vector3.zero;

    private Vector3 draggedTransformHeightOffset = Vector3.zero;

    private Vector3 draggedItemCustomOffset = Vector3.zero;

    public bool SnapToGrid {
      get { return snapToGrid; }
      set { snapToGrid = value; }
    }


    public const float snapAngleDiagonal = 45 * Mathf.Deg2Rad;

    private bool isManualSelectionRequest;

    public bool IsMultiSelectionEnabled {
      get {
        return isMultiSelectionEnabled;
      }
      set {
        isMultiSelectionEnabled = value;
        if(value == false) {
          DeselectAll();
        }
      }
    }

    private Dictionary<Component, Vector3> selectionPositionOffsets = new Dictionary<Component, Vector3>();

    public void Awake() {
      SelectedColliders = new List<Component>();
      mobileTouchCam = FindObjectOfType<MobileTouchCamera>();
      if (mobileTouchCam == null) {
        Debug.LogError("No MobileTouchCamera found in scene. This script will not work without this.");
      }
      touchInputController = mobileTouchCam.GetComponent<TouchInputController>();
      if (touchInputController == null) {
        Debug.LogError("No TouchInputController found in scene. Make sure this component exists and is attached to the MobileTouchCamera gameObject.");
      }
    }

    public void Start() {
      touchInputController.OnInputClick += InputControllerOnInputClick;
      touchInputController.OnFingerDown += InputControllerOnFingerDown;
      touchInputController.OnFingerUp += InputControllerOnFingerUp;
    }

    public void OnDestroy() {
      touchInputController.OnInputClick -= InputControllerOnInputClick;
      touchInputController.OnFingerDown -= InputControllerOnFingerDown;
      touchInputController.OnFingerUp -= InputControllerOnFingerUp;
    }

    public void LateUpdate() {
      if(isManualSelectionRequest == true && TouchWrapper.TouchCount == 0) {
        isManualSelectionRequest = false;
      }
    }

    #region public interface
    /// <summary>
    /// Method that allows to set the currently selected collider for the picking controller by code.
    /// Useful for example for auto-selecting newly spawned items or for selecting items via a menu button.
    /// Use this method when you want to select just one item.
    /// </summary>
    public void SelectCollider(Component collider) {
      if (IsMultiSelectionEnabled == true) {
        Select(collider, false, false);
      } else {
        SelectColliderInternal(collider, false, false);
        isManualSelectionRequest = true;
      }
    }

    /// <summary>
    /// Method to deselect the last selected collider.
    /// </summary>
    public void DeselectSelectedCollider() {
      Deselect(SelectedCollider);
    }

    /// <summary>
    /// In case multi-selection is enabled, this method allows to deselect
    /// all colliders at once.
    /// </summary>
    public void DeselectAllSelectedColliders() {
      var collidersToRemove = new List<Component>(SelectedColliders);
      foreach (var colliderToRemove in collidersToRemove) {
        Deselect(colliderToRemove);
      }
    }

    /// <summary>
    /// Method to deselect the given collider.
    /// In case the collider hasn't been selected before, the method returns false.
    /// </summary>
    private bool Deselect(Component colliderComponent) {
      bool wasRemoved = SelectedColliders.Remove(colliderComponent);
      if (wasRemoved == true) {
        OnSelectedColliderChanged(SelectionAction.Deselect, colliderComponent.GetComponent<MobileTouchPickable>());
      }
      return wasRemoved;
    }

    /// <summary>
    /// Method to deselect all currently selected colliders.
    /// </summary>
    /// <returns></returns>
    public int DeselectAll() {
      SelectedColliders.RemoveAll(item => item == null);
      int colliderCount = SelectedColliders.Count;
      foreach (Component colliderComponent in SelectedColliders) {
        OnSelectedColliderChanged(SelectionAction.Deselect, colliderComponent.GetComponent<MobileTouchPickable>());
      }
      SelectedColliders.Clear();
      return colliderCount;
    }

    public Component GetClosestColliderAtScreenPoint(Vector3 screenPoint, out Vector3 intersectionPoint) {

      Component hitCollider = null;
      float hitDistance = float.MaxValue;
      Ray camRay = mobileTouchCam.Cam.ScreenPointToRay(screenPoint);
      RaycastHit hitInfo;
      intersectionPoint = Vector3.zero;
      if (Physics.Raycast(camRay, out hitInfo) == true) {
        hitDistance = hitInfo.distance;
        hitCollider = hitInfo.collider;
        intersectionPoint = hitInfo.point;
      }
      RaycastHit2D hitInfo2D = Physics2D.Raycast(camRay.origin, camRay.direction);
      if (hitInfo2D == true) {
        if (hitInfo2D.distance < hitDistance) {
          hitCollider = hitInfo2D.collider;
          intersectionPoint = hitInfo2D.point;
        }
      }
      return (hitCollider);
    }

    #endregion

    private void SelectColliderInternal( Component colliderComponent, bool isDoubleClick, bool isLongTap ) {

      if(deselectPreviousColliderOnClick == false) {
        if(colliderComponent == null || colliderComponent.GetComponent<MobileTouchPickable>() == null) {
          return; //Skip selection change in case the user requested to deselect only in case another pickable is clicked.
        }
      }

      if(isManualSelectionRequest == true) {
        return; //Skip selection when the user has already requested a manual selection with the same click.
      }

      Component previouslySelectedCollider = SelectedCollider;
      bool skipSelect = false;

      if (isMultiSelectionEnabled == false) {
        if (previouslySelectedCollider != null && previouslySelectedCollider != colliderComponent) {
          Deselect(previouslySelectedCollider);
        }
      } else {
        skipSelect = Deselect(colliderComponent);
      }

      if (skipSelect == false) {
        if (colliderComponent != null) {
          if (colliderComponent != previouslySelectedCollider || repeatEventSelectedOnClick == true) {
            Select(colliderComponent, isDoubleClick, isLongTap);
            isSelectedViaLongTap = isLongTap;
          }
        }
      }
    }

    private void InputControllerOnInputClick(Vector3 clickPosition, bool isDoubleClick, bool isLongTap) {
      Vector3 intersectionPoint;
      var newCollider = GetClosestColliderAtScreenPoint(clickPosition, out intersectionPoint);
      SelectColliderInternal(newCollider, isDoubleClick, isLongTap);
    }

//    private void RequestDragPickable(Vector3 fingerDownPos) {
//      Vector3 intersectionPoint = Vector3.zero;
//      Component pickedCollider = GetClosestColliderAtScreenPoint(fingerDownPos, out intersectionPoint);
//      if(pickedCollider != null && SelectedColliders.Contains(pickedCollider)) {
//        RequestDragPickable(pickedCollider, fingerDownPos, intersectionPoint);
//      }
//    }

//    private void RequestDragPickable(Component colliderComponent, Vector2 fingerDownPos, Vector3 intersectionPoint) {
//
//      if (requireLongTapForMove == true && isSelectedViaLongTap == false) {
//        return;
//      }
//
//      CurrentlyClickable = null;
//      bool isDragStartedOnSelection = colliderComponent != null && SelectedColliders.Contains(colliderComponent);
//      if (isDragStartedOnSelection == true) {
//        mobileTouchPickable mobileTouchPickable = colliderComponent.GetComponent<mobileTouchPickable>();
//        if (mobileTouchPickable != null) {
//          mobileTouchCam.OnDragSceneObject(); //Lock camera movement.
//          CurrentlyClickable = mobileTouchPickable;
//          selectionPositionOffsets.Clear();
////          foreach (Component selectionComponent in SelectedColliders) {
////                         selectionPositionOffsets.Add(selectionComponent, currentDragStartPos - selectionComponent.transform.position);
////                       }
//
//          draggedTransformOffset = Vector3.zero;
//          draggedTransformHeightOffset = Vector3.zero;
//          draggedItemCustomOffset = Vector3.zero;
//
//          //Find offset of item transform relative to ground.
//          Vector3 groundPosCenter = Vector3.zero;
//          Ray groundScanRayCenter = new Ray(CurrentlyDraggedTransform.position, -mobileTouchCam.RefPlane.normal);
//          bool rayHitSuccess = mobileTouchCam.RaycastGround(groundScanRayCenter, out groundPosCenter);
//          if(rayHitSuccess == true) {
//            draggedTransformHeightOffset = CurrentlyDraggedTransform.position - groundPosCenter;
//          } else {
//            groundPosCenter = CurrentlyDraggedTransform.position;
//          }
//
//          draggedTransformOffset = groundPosCenter - intersectionPoint;
//        }
//      }
//    }

    private void InputControllerOnFingerDown(Vector3 fingerDownPos) {
//      if(requireLongTapForMove == false || isSelectedViaLongTap == true) {
//        RequestDragPickable(fingerDownPos);
//      }
    }

    private void InputControllerOnFingerUp() {
      
    }

      
    private void OnSelectedColliderChanged(SelectionAction selectionAction, MobileTouchPickable mobileTouchPickable) {
      if (mobileTouchPickable != null) {
        if (selectionAction == SelectionAction.Select) {
          InvokeTransformActionSafe(OnPickableTransformSelected, mobileTouchPickable.PickableTransform);
        } else if (selectionAction == SelectionAction.Deselect) {
          InvokeTransformActionSafe(OnPickableTransformDeselected, mobileTouchPickable.PickableTransform);
        }
      }
    }

    private void OnSelectedColliderChangedExtended(SelectionAction selectionAction, MobileTouchPickable mobileTouchPickable, bool isDoubleClick, bool isLongTap) {
      if (mobileTouchPickable != null) {
        if (selectionAction == SelectionAction.Select) {
          PickableSelectedData pickableSelectedData = new PickableSelectedData() {
            SelectedTransform = mobileTouchPickable.PickableTransform,
            IsDoubleClick = isDoubleClick,
            IsLongTap = isLongTap };
          InvokeGenericActionSafe(OnPickableTransformSelectedExtended, pickableSelectedData);
        }
      }
    }

    private void InvokeTransformActionSafe(UnityEventWithTransform eventAction, Transform selectionTransform) {
      if (eventAction != null) {
        eventAction.Invoke(selectionTransform);
      }
    }

    private void InvokeGenericActionSafe<T1, T2>(T1 eventAction, T2 eventArgs) where T1 : UnityEvent<T2> {
      if (eventAction != null) {
        eventAction.Invoke(eventArgs);
      }
    }

    private void Select(Component colliderComponent, bool isDoubleClick, bool isLongTap) {
      MobileTouchPickable mobileTouchPickable = colliderComponent.GetComponent<MobileTouchPickable>();
      if (mobileTouchPickable != null) {
        if (SelectedColliders.Contains(colliderComponent) == false) {
          SelectedColliders.Add(colliderComponent);
        }
      }
      OnSelectedColliderChanged(SelectionAction.Select, mobileTouchPickable);
      OnSelectedColliderChangedExtended(SelectionAction.Select, mobileTouchPickable, isDoubleClick, isLongTap);
    }
  }
}
