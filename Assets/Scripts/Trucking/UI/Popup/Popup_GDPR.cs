using System;
using Trucking.Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_GDPR : MonoSingleton<Popup_GDPR>
    {
        public Button btnCenter;
        public Button btnTextLink;

        private Action OnCenter;

        private void Start()
        {
            btnCenter.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    PlayerPrefs.SetInt("GDPR", 1);
                    OnCenter?.Invoke();
                    gameObject.SetActive(false);
                })
                .AddTo(this);

            btnTextLink.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    Application.OpenURL("http://policy.cookapps.com/pp.html");
                })
                .AddTo(this);
        }

        public void Show(Action _OnCenter)
        {
            OnCenter = _OnCenter;

            gameObject.SetActive(true);
        }
    }
}