using System;
using System.Collections;
using System.Collections.Generic;
using BitBenderGames;
using DG.Tweening;
using Trucking.Common;
using Trucking.Graph;
using Trucking.Model;
using Trucking.UI;
using UniRx;
using UnityEngine;

namespace Trucking
{
    public class WorldMap : MonoSingleton<WorldMap>
    {
        public Transform trsStations;
        public Transform trsRoads;
        public Transform trsCloud;
        public Transform trsFoliage;
        public GameObject ground_gray_under;
        public GameObject plan_editmode;
        public GameObject sea;
        public Material roadParticleMaterial;
        public Material cloudMat;
        public Transform citySelectAni_big;

        WorldGraph graph;
        private float camRotateX;
        private Quaternion oriRotation = Quaternion.identity;
        private CompositeDisposable _disposableCusor = new CompositeDisposable();
        private Transform cusorTarget;

        private void OnDestroy()
        {
            Debug.LogWarning("WorldMap OnDestroy");
        }

        public void Init(List<City> cities)
        {
            oriRotation = Quaternion.Euler(GameManager.Instance.camera.transform.rotation.eulerAngles);

            graph = gameObject.GetComponent<WorldGraph>();

            if (graph == null)
            {
                graph = gameObject.AddComponent<WorldGraph>();
            }

            graph.nodes.Clear();
            graph.nodes.AddRange(cities);
            trsCloud.gameObject.SetActive(true);

            trsFoliage.GetChild(0).gameObject.SetActive(true);
            foreach (var city in cities)
            {
                city.gameObject.SetActive(city.GetCloudArea() == 1);
                city.ui.gameObject.SetActive(city.GetCloudArea() == 1);
            }

            for (int i = 0; i < UserDataManager.Instance.data.cloudOpen.Count; i++)
            {
                int index = i;

                UserDataManager.Instance.data.cloudOpen[index].Pairwise()
                    .StartWith(new Pair<bool>(UserDataManager.Instance.data.cloudOpen[index].Value,
                        UserDataManager.Instance.data.cloudOpen[index].Value)).Subscribe(open =>
                    {
                        if (open.Current && !open.Previous)
                        {
                            SetCloudAnimation(index);
                            StartCoroutine(StartCityOpenAnimation(index + 2));
                        }
                        else
                        {
                            trsCloud.GetChild(index).gameObject.SetActive(!open.Current);
                            trsFoliage.GetChild(index + 1).gameObject.SetActive(open.Current);

                            foreach (var city in cities)
                            {
                                if (city.GetCloudArea() == index + 2)
                                {
                                    city.gameObject.SetActive(open.Current);
                                    city.ui.gameObject.SetActive(open.Current);
                                }
                            }
                        }
                    }).AddTo(this);
            }
        }

        IEnumerator StartCityOpenAnimation(int index)
        {
            yield return new WaitForSeconds(0.6f);

            List<City> cloudCities = GameManager.Instance.cities.FindAll(ct => ct.GetCloudArea() == index);

            for (int i = 0; i < cloudCities.Count; i++)
            {
                cloudCities[i].gameObject.SetActive(true);
                cloudCities[i].GetComponent<Animator>().Play("Unlock_cloud_city");
                cloudCities[i].ui.gameObject.SetActive(true);
                AudioManager.Instance.PlaySound("sfx_city_appear");

                yield return new WaitForSeconds(0.2f);
            }
        }

        public float GetDistance(City from, City to)
        {
            try
            {
                return graph.GetShortestPath(from, to).length;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return 0;
            }
        }

        public void SetCloudAnimation(int index)
        {
            UIBlockTouch.Instance.ShowClear();
            trsCloud.GetChild(index).gameObject.SetActive(true);
            trsFoliage.GetChild(index + 1).gameObject.SetActive(true);
            MeshRenderer meshRenderer = trsCloud.GetChild(index).GetChild(0).GetComponent<MeshRenderer>();
            Material mat = new Material(cloudMat);
            meshRenderer.sharedMaterial = mat;
            meshRenderer.sharedMaterial.DOColor(Color.white, 0);
            meshRenderer.sharedMaterial.DOColor(new Color(1, 1, 1, 0), 2).OnComplete(() =>
            {
                trsCloud.GetChild(index).gameObject.SetActive(false);
                UIBlockTouch.Instance.Close();
            });

            SetCamera(UICloudManager.Instance.uiClouds[index].transform.position);
        }

        public void SetEditView(bool isEdit)
        {
            ground_gray_under.SetActive(!isEdit);
            plan_editmode.SetActive(isEdit);
            sea.SetActive(!isEdit);


            GameManager.Instance.camera.EnableZoomTilt = !isEdit;
            GameManager.Instance.camera.Cam.orthographic = isEdit;
            GameManager.Instance.camera3DUI.orthographic = isEdit;
            GameManager.Instance.cameraGray.orthographic = isEdit;
            GameManager.Instance.camera3DUI.orthographicSize
                = GameManager.Instance.camera.Cam.orthographicSize;
            GameManager.Instance.cameraGray.orthographicSize
                = GameManager.Instance.camera.Cam.orthographicSize;

            Vector3 pos = Vector3.zero;
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(ray, out var hitPoint))
            {
                pos = hitPoint.point;
//			Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 10);
            }

            if (isEdit)
            {
                oriRotation = GameManager.Instance.camera.Transform.rotation;
                GameManager.Instance.camera.GetComponent<Light>()
                    .DOColor(Utilities.GetColorByHtmlString("#808080"), 0.5f);
                GameManager.Instance.camera.Transform.RotateAround(pos, Vector3.left,
                    oriRotation.eulerAngles.x - 90);
//			Debug.DrawRay(Camera.main.transform.position, Vector3.down * 1000, Color.blue, 10);
            }
            else if (oriRotation.x > 0)
            {
                GameManager.Instance.camera.Transform.RotateAround(pos, Vector3.left,
                    90 - oriRotation.eulerAngles.x);
//			Debug.DrawRay(Camera.main.transform.position, Vector3.down * 1000, Color.blue, 10);
                GameManager.Instance.camera.ResetZoomTilt();
            }


            foreach (var road in GameManager.Instance.roads)
            {
                road.SetEditState(isEdit);

                if (isEdit)
                {
                    road.SetColorRoadAni(false);
                }
            }

            foreach (var city in GameManager.Instance.cities)
            {
                city.SetEditState(isEdit ? UICity.StateEnum.Edit : UICity.StateEnum.Normal);
            }

            foreach (var truck in GameManager.Instance.trucks)
            {
                if (isEdit)
                {
                    Utilities.ChangeLayers(truck.gameObject, "3D on Gray");
                }
                else
                {
                    Utilities.ChangeLayers(truck.gameObject, "Default");
                }
            }

            SetGray(isEdit);
        }

        public void SetJobView(bool isJob)
        {
            SetEditView(isJob);

            if (isJob)
            {
                SetCamera(GameManager.Instance.selectedCity.Value.transform.position + new Vector3(220, 0, 0),
                    Quaternion.Euler(90, 0, 0));
            }
            else
            {
                SetCamera(GameManager.Instance.selectedCity.Value.transform.position, oriRotation);
                GameManager.Instance.camera.ResetZoomTilt();
            }
        }

        public void SetWorldMapView()
        {
            int count = 0;

            foreach (var road in GameManager.Instance.roads)
            {
                if (road.isBuy)
                {
                    road.isBuy = false;
                    road.ui.SetNewRoad();
                    road.trsStateParticle.gameObject.SetActive(true);
                }
            }

            roadParticleMaterial.SetColor("_TintColor", new Color(1, 1, 1, 0.0f));
            roadParticleMaterial?.DOColor(new Color(1, 1, 1, 0.4f), "_TintColor", .8f)
                .SetLoops(2, LoopType.Yoyo)
                .OnComplete(() =>
                {
                    foreach (var road in GameManager.Instance.roads)
                    {
                        road.trsStateParticle.gameObject.SetActive(false);
                    }
                });

            foreach (var truck in GameManager.Instance.trucks)
            {
                truck.currentRoad.Value?.road.SetMovingTruckAnimation(
                    truck.model.state.Value == TruckModel.State.Move,
                    truck.currentRoad.Value.isReverse);
            }
        }

        public void SetCamera(Vector3 targetPosition, bool nonMove = false)
        {
//		Debug.Log("SetCamera : " + targetPosition);
            if (nonMove)
            {
                GameManager.Instance.camera.GetComponent<FocusCameraOnItem>().TransitionDuration = 0f;
                SetCamera(targetPosition, GameManager.Instance.camera.transform.rotation);
                GameManager.Instance.camera.GetComponent<FocusCameraOnItem>().TransitionDuration = 0.5f;
            }
            else
            {
                SetCamera(targetPosition, GameManager.Instance.camera.transform.rotation);
            }
        }

        public void SetCamera(Vector3 targetPosition, Quaternion targetRotation)
        {
//		Debug.Log($"SetCamera : {targetPosition}, {targetRotation.eulerAngles}, {oriCamZoom}");
            GameManager.Instance.camera.GetComponent<FocusCameraOnItem>()
                .FocusCameraOnTarget(targetPosition, targetRotation);
        }

        public void SetGray(bool isGray)
        {
            DOTween.To(
                () => GameManager.Instance.grayscaleCamera.intensity, // 무엇을 대상으로할지 
                num => GameManager.Instance.grayscaleCamera.intensity = num, // 값 업데이트 
                isGray ? 1 : 0, // 최종 값 
                0.5f // 애니메이션 시간 
            );
        }

        public void SetCusor(Transform target)
        {
            _disposableCusor.Clear();
            citySelectAni_big.gameObject.SetActive(target != null);
            cusorTarget = target;

            if (cusorTarget != null)
            {
                Observable.EveryUpdate().Subscribe(_ =>
                {
                    citySelectAni_big.transform.position = Utilities.CopyVector3(cusorTarget.position, 0, 4);
                }).AddTo(_disposableCusor);
            }
        }
    }
}