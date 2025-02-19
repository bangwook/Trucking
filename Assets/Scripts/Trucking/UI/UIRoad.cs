using System;
using DG.Tweening;
using Dreamteck.Splines;
using TMPro;
using Trucking.Common;
using Trucking.Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI
{
    public class UIRoad : MonoBehaviour
    {
        public TextMeshProUGUI txtRoadGold;
        public TextMeshProUGUI txtNewRoad;
        public GameObject stopIcon;
        public Button btnPlus;
        public Button btnMinus;
        public GameObject toolSlider;
        public Image imgToolSlider;

        private Road road;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        
        public void SetData(Road _road)
        {
            road = _road;
            name = _road.name;
            
            txtRoadGold.transform.localPosition = Vector3.zero;
            
            Clear();

            road.truck.Subscribe(tr =>
            {
                stopIcon.SetActive(tr == null && _road.model.isOpen.Value);
            }).AddTo(this);

            GetComponent<SplinePositioner>().computer = road.splineComputer;
            GetComponent<SplinePositioner>().SetPercent(0.5f);

            btnPlus.OnClickAsObservable()
                .Subscribe(_ =>
                {
//                    EditView.Instance.Button_Plus(road.from, road.to);
                    
                    if (EditView.Instance.selectCity == road.from)
                    {
                        EditView.Instance.Button_Plus(EditView.Instance.selectCity, road.to);
                    }
                    else if (EditView.Instance.selectCity == road.to)
                    {
                        EditView.Instance.Button_Plus(EditView.Instance.selectCity, road.from);
                    }
                    else
                    {
                        EditView.Instance.Button_Plus(road.from, road.to);
                    }
                }).AddTo(this);

            
            btnMinus.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    EditView.Instance.Button_Minus(road.from, road.to);
                }).AddTo(this);
        }

        public void SetPlus()
        {
            btnPlus.gameObject.SetActive(true);
            btnMinus.gameObject.SetActive(false);
        }
        
        public void SetMinus()
        {
            btnPlus.gameObject.SetActive(false);
            btnMinus.gameObject.SetActive(true);    
        }


        public void SetGold(int gold)
        {
            _compositeDisposable.Clear();
            txtRoadGold.gameObject.SetActive(false);
            
            


//            txtRoadGold.gameObject.SetActive(gold > 0);
//            
//            if (gold > 0)
//            {
//                txtRoadGold.text = Utilities.GetNumberKKK(gold);    
//            }

//            UserDataManager.Instance.data.gold.Subscribe(userGold =>
//            {
//                txtRoadGold.color = userGold >= gold ? Color.white : Color.red;
//            }).AddTo(_compositeDisposable);
        }

        public void SetNewRoad()
        {
            txtNewRoad.gameObject.SetActive(true);      
            txtNewRoad.transform.localPosition = Vector3.zero;            
            txtNewRoad.transform.DOMoveZ(txtNewRoad.transform.position.z + 20, 1.4f)
                .OnComplete(() =>
                {
                    txtNewRoad.gameObject.SetActive(false);
                });
        }

        public void SetStopIcon()
        {
            if (road != null)
            {
                stopIcon.SetActive(road.truck.Value == null 
                                   && road.model.isOpen.Value);    
            }
        }
        
        public void ShowToolSlider(bool isRed = false, Action _onClaim = null)
        {
            _compositeDisposable.Clear();
            toolSlider.SetActive(true);
            btnPlus.gameObject.SetActive(false);
            btnMinus.gameObject.SetActive(false);
            stopIcon.gameObject.SetActive(false);
            
            imgToolSlider.color = isRed ? Utilities.GetColorByHtmlString("E70010") : Utilities.GetColorByHtmlString("01D81F");

            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(3f)).Subscribe(__ =>
            {
                toolSlider.SetActive(false);
                _onClaim?.Invoke();
            }).AddTo(_compositeDisposable);   
        }

        public void Clear()
        {
            _compositeDisposable.Clear();
            txtNewRoad.gameObject.SetActive(false);      
            txtRoadGold.gameObject.SetActive(false);
            btnPlus.gameObject.SetActive(false);
            btnMinus.gameObject.SetActive(false);
            toolSlider.gameObject.SetActive(false);

            SetStopIcon();
        }

        private void LateUpdate()
        {
            if (road != null)
            {
                if (Camera.main != null)
                {
                    transform.rotation = Camera.main.transform.rotation;    
                }
            }
        }
    }
}