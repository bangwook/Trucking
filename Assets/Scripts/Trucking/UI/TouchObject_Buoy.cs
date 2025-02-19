using System;
using Trucking.Common;
using UniRx;
using UnityEngine;

namespace Trucking.UI
{
    public class TouchObject_Buoy : MonoBehaviour
    {
        public ParticleSystem particle;

        public void Show()
        {
            AudioManager.Instance.PlaySound("sfx_buoy");
            gameObject.SetActive(true);
            GetComponentInChildren<Animator>().Play("buoy_create", -1, 0);
            particle.gameObject.SetActive(false);
        }

        public void Touch()
        {
            AudioManager.Instance.PlaySound("sfx_button_main");

            transform.GetChild(0).gameObject.SetActive(false);

            particle.gameObject.SetActive(true);
            particle.Stop();
            particle.Clear();
            particle.Play();

            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(1)).Subscribe(_ =>
            {
                transform.GetChild(0).gameObject.SetActive(true);
                particle.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }).AddTo(this);

            transform.GetComponentInParent<TouchObject_BuoyGroup>().Touch(this);
        }
    }
}