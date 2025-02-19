using DG.Tweening;
using Dreamteck.Splines;
using Trucking.Common;
using Trucking.UI;
using UniRx;
using UnityEngine;

namespace Trucking.Model
{
    public class Road : MonoBehaviour
    {
        public City from;
        public City to;

        public UIRoad ui;

//	public RoadData roadData;
        public float distance;
        public SplineComputer splineComputer;
        public RoadModel model;
        public ReactiveProperty<Truck> truck = new ReactiveProperty<Truck>();
        public ReactiveProperty<bool> isFocus = new ReactiveProperty<bool>();

        public Transform trsStateColor;
        public Transform trsStateClosed;
        public Transform trsStateDirection;
        public Transform trsStateArrow;
        public Transform trsStateParticle;

        public bool isBuy;
        public float arrowLength = 0.3f;
        private MeshRenderer _directionMeshRenderer;
        private MeshRenderer _colorMeshRenderer;
        private MeshRenderer _arrowMeshRenderer;
        private MeshRenderer _whiteMeshRenderer;
        private MeshRenderer _closedMeshRenderer;

        private bool isEdit;
        private bool isMovingRoad;

        public static readonly Vector2 movingScroll = new Vector2(0, -0.5f);
        Vector2 movingOffset = new Vector2(0f, 0f);

        public static readonly Vector2 movingTruckScroll = new Vector2(0, -0.5f);
        Vector2 movingTruckOffset = new Vector2(0f, 0f);


        private Tweener tween;

        private void Init()
        {
            splineComputer = GetComponent<SplineComputer>();
            _directionMeshRenderer = trsStateDirection.GetComponent<MeshRenderer>();
            Material matDir = new Material(GameManager.Instance.matDirectionRoad);
            _directionMeshRenderer.sharedMaterial = matDir;

            _arrowMeshRenderer = trsStateArrow.GetComponent<MeshRenderer>();
            Material matArrow = new Material(GameManager.Instance.matArrowRoad);
            _arrowMeshRenderer.sharedMaterial = matArrow;

            _colorMeshRenderer = trsStateColor.GetComponent<MeshRenderer>();
            Material matColor = new Material(ColorManager.Instance.ColorList[0].roadMaterial);
            _colorMeshRenderer.sharedMaterial = matColor;

            _closedMeshRenderer = trsStateColor.GetComponent<MeshRenderer>();
            Material matClosed = new Material(ColorManager.Instance.ColorList[0].roadMaterial);
            _closedMeshRenderer.sharedMaterial = matClosed;


            SplineMesh splineMesh = GetComponent<SplineMesh>();
            trsStateColor.gameObject.SetActive(false);
            trsStateClosed.gameObject.SetActive(false);
            trsStateDirection.gameObject.SetActive(false);
            trsStateArrow.gameObject.SetActive(false);
            trsStateParticle.gameObject.SetActive(false);


            distance = splineMesh.CalculateLength(splineMesh.clipFrom, splineMesh.clipTo);

            splineMesh.GetChannel(0).count = (int) (distance / 10);
            trsStateColor.GetComponent<SplineMesh>().GetChannel(0).minScale = new Vector3(0.6f, 1, 1);
            trsStateColor.GetComponent<SplineMesh>().GetChannel(0).count = (int) (distance / 10);
            trsStateColor.GetComponent<SplineMesh>().GetChannel(0).clipFrom = 0;
            trsStateColor.GetComponent<SplineMesh>().GetChannel(0).clipTo = 1;

            trsStateClosed.GetComponent<SplineMesh>().GetChannel(0).count = (int) (distance / 10);

            trsStateDirection.GetComponent<SplineMesh>().GetChannel(0).count = (int) (distance / 10);
            trsStateDirection.GetComponent<SplineMesh>().GetChannel(0).minScale = new Vector2(1, 1);
//		trsStateDirection.GetComponent<SplineMesh>().GetChannel(0).clipFrom = splineMesh.clipFrom;
//		trsStateDirection.GetComponent<SplineMesh>().GetChannel(0).clipTo = splineMesh.clipTo;
            trsStateDirection.GetComponent<SplineMesh>().GetChannel(0).clipFrom = 0;
            trsStateDirection.GetComponent<SplineMesh>().GetChannel(0).clipTo = 1;

            trsStateArrow.GetComponent<SplineMesh>().GetChannel(0).count = (int) (distance / 30);
            trsStateArrow.GetComponent<SplineMesh>().GetChannel(0).clipFrom = 0;
            trsStateArrow.GetComponent<SplineMesh>().GetChannel(0).clipTo = 1;
            trsStateArrow.GetComponent<SplineMesh>().offset = new Vector3(0, 0.2f, 0);

            trsStateParticle.GetComponent<SplineMesh>().GetChannel(0).minOffset = new Vector2(-1, 0);
            trsStateParticle.GetComponent<SplineMesh>().GetChannel(1).minOffset = new Vector2(1, 0);
            trsStateClosed.GetComponent<MeshRenderer>().sharedMaterial
                = ColorManager.Instance.ColorList[0].roadMaterial;
            arrowLength = 0.3f;
        }

        public void SetModel(RoadModel _model)
        {
            Init();

            model = _model;

            model.isOpen.Subscribe(open => { SetEditState(isEdit); })
                .AddTo(this);

            truck.Subscribe(_truck =>
                {
                    if (_truck != null)
                    {
                        model.truckBirthID.Value = _truck.model.birthID;
                        model.isOpen.Value = true;

                        if (to.model.state.Value == CityModel.State.Lock)
                        {
                            to.model.state.Value = CityModel.State.Wait;
                        }
                        else if (from.model.state.Value == CityModel.State.Lock)
                        {
                            from.model.state.Value = CityModel.State.Wait;
                        }
                    }

                    SetEditState(isEdit);
                })
                .AddTo(this);
        }

        public void SetEditState(bool _isEdit)
        {
            isEdit = _isEdit;
            GetComponent<MeshRenderer>().enabled = !_isEdit;
            trsStateDirection.gameObject.SetActive(false);
            trsStateParticle.gameObject.SetActive(false);
            trsStateArrow.gameObject.SetActive(false);
            ui.Clear();

            if (truck.Value != null)
            {
                _colorMeshRenderer.sharedMaterial.color =
                    ColorManager.Instance.ColorList[truck.Value.model.colorIndex.Value].roadMaterial.color;
            }
            else
            {
                _colorMeshRenderer.sharedMaterial.color =
                    ColorManager.Instance.ColorList[0].roadMaterial.color;
            }

            if (_isEdit)
            {
                trsStateColor.GetComponent<SplineMesh>().GetChannel(0).minScale = new Vector3(0.6f, 1, 1);
                trsStateColor.gameObject.SetActive(false);
                trsStateClosed.gameObject.SetActive(false);

                if (truck.Value != null || model.isOpen.Value)
                {
                    trsStateColor.gameObject.SetActive(true);
                    Utilities.ChangeLayers(trsStateColor.gameObject, "3D on Gray");
                }
                else
                {
                    trsStateClosed.gameObject.SetActive(true);
                }
            }
            else
            {
                trsStateColor.GetComponent<SplineMesh>().GetChannel(0).minScale = new Vector3(0.6f, 1, 1);
                trsStateColor.gameObject.SetActive(false);
                trsStateClosed.gameObject.SetActive(!model.isOpen.Value);
                GetComponent<MeshRenderer>().enabled = model.isOpen.Value;

                if (!model.isOpen.Value)
                {
                    Utilities.ChangeLayers(trsStateClosed.gameObject, "Default");
                }
            }
        }

        public void SetColorRoadAni(bool isSelect)
        {
            tween?.Kill();

            if (truck.Value != null)
            {
                _colorMeshRenderer.sharedMaterial.color =
                    ColorManager.Instance.ColorList[truck.Value.model.colorIndex.Value].roadMaterial.color;
            }

            trsStateColor.gameObject.SetActive(truck.Value != null || model.isOpen.Value);

            if (isSelect)
            {
                trsStateColor.GetComponent<SplineMesh>().GetChannel(0).minScale = new Vector3(0.6f, 1, 1);
                tween = _colorMeshRenderer.sharedMaterial.DOColor(Color.white, 0.5f)
                    .SetLoops(-1, LoopType.Yoyo);
            }
        }

        public void SetFocusCloseRoad(bool isSelect)
        {
            trsStateClosed.GetComponent<SplineMesh>().GetChannel(0).minScale = Vector3.one * 0.8f;

            if (isSelect)
            {
                trsStateClosed.GetComponent<MeshRenderer>().sharedMaterial = GameManager.Instance.matClosedRoad_white;
                trsStateClosed.GetComponent<SplineMesh>().GetChannel(0).minScale = Vector3.one * 1.2f;
            }
            else
            {
                trsStateClosed.GetComponent<MeshRenderer>().sharedMaterial =
                    ColorManager.Instance.ColorList[0].roadMaterial;
            }
        }


        public void SetTruckColor(bool isSelect, bool changeLayer = true)
        {
            SetMovingAnimation(false);
            SetMovingTruckAnimation(false);

            trsStateColor.gameObject.SetActive(isSelect);

            if (isSelect)
            {
                _colorMeshRenderer.sharedMaterial.color =
                    ColorManager.Instance.ColorList[truck.Value.model.colorIndex.Value].roadMaterial.color;


//			trsStateColor.GetComponent<MeshRenderer>().sharedMaterial
//				= ColorManager.Instance.ColorList[truck.Value.model.colorIndex.Value].roadMaterial;

                if (changeLayer)
                {
                    Utilities.ChangeLayers(gameObject, "3D on Gray");
                }
                else
                {
                    Utilities.ChangeLayers(gameObject, "Default");
                }
            }
            else
            {
                Utilities.ChangeLayers(gameObject, "Default");
            }
        }

        public void SetMovingAnimation(bool isMove, bool _isReverse = false)
        {
            isMovingRoad = isMove;
//		trsStateColor.gameObject.SetActive(!isMove);
            trsStateDirection.gameObject.SetActive(isMove);

//		if (isMove)
//		{
////			Debug.Log($"Road Direction : {name} = {(_isReverse ? -1 : 1)}");
//			trsStateColor.gameObject.SetActive(false);
//		}

            _directionMeshRenderer.sharedMaterial.mainTextureScale = new Vector2(1, _isReverse ? 1 : -1);
        }

        public void SetMovingTruckAnimation(bool isMove, bool _isReverse = false)
        {
            trsStateArrow.gameObject.SetActive(isMove);

            if (isMove && _arrowMeshRenderer)
            {
                _arrowMeshRenderer.sharedMaterial.mainTextureScale = new Vector2(1, _isReverse ? 1 : -1);
                _arrowMeshRenderer.sharedMaterial.color = Color.white;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isMovingRoad)
            {
                movingOffset += movingScroll * Time.deltaTime;
                _directionMeshRenderer.sharedMaterial.mainTextureOffset = movingOffset;
            }
        }

        public void SetTruckPosition(double percent, bool isReverse)
        {
            if (_arrowMeshRenderer)
            {
                double unClipPercent = percent;
                double disPercent = isReverse ? 1 - percent : percent;

                splineComputer.GetComponent<SplineMesh>().UnclipPercent(ref unClipPercent);
                trsStateArrow.GetComponent<SplineMesh>().clipFrom = isReverse ? disPercent - arrowLength : disPercent;
                trsStateArrow.GetComponent<SplineMesh>().clipTo = isReverse ? disPercent : disPercent + arrowLength;
//                trsStateArrow.GetComponent<SplineMesh>().GetChannel(0).count = 4;


                movingTruckOffset += movingTruckScroll * Time.deltaTime;
                _arrowMeshRenderer.sharedMaterial.mainTextureOffset = movingTruckOffset;

                if (percent >= 1)
                {
                    trsStateArrow.gameObject.SetActive(false);
                }
            }
        }

        public bool IsReverse(City _to)
        {
            if (to != _to)
            {
                return true;
            }

            return false;
        }

        public override bool Equals(object Obj)
        {
            Road other = (Road) Obj;
            return (from == other.from && to == other.to)
                   || (from == other.to && to == other.from);
        }

        public bool Equals(City from, City to)
        {
            return (from == from && to == to)
                   || (from == to && to == from);
        }

        public override string ToString()
        {
            return name;
        }
    }
}