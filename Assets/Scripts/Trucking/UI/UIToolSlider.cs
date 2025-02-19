using System;
using System.Collections.Generic;
using Trucking.Common;
using Trucking.Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI
{
    public class UIToolSlider : MonoBehaviour
    {
        public Image imgIcon;
        private List<RewardModel> rewardDatas;

        public void Show(Vector3 pos, bool isRed = false, Action _onClaim = null)
        {
            gameObject.SetActive(true);

            transform.localPosition = pos;
            imgIcon.color = isRed ? Utilities.GetColorByHtmlString("E70010") : Utilities.GetColorByHtmlString("01D81F");

            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(3.5f)).Subscribe(__ =>
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
    }
}