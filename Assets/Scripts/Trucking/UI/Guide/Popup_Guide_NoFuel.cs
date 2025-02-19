using System;
using Trucking.Common;
using Trucking.UI.Popup;
using UniRx;
using UnityEngine.UI;

namespace Trucking.UI.Guide
{
    public class Popup_Guide_NoFuel : Popup_Base<Popup_Guide_NoFuel>
    {
        public Button btnClose;

        private void Start()
        {
            btnClose.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    GameManager.Instance.fsm.PopState();
                    OnClose?.Invoke();
                })
                .AddTo(this);
        }

        public void Show(Action _OnConfirm = null)
        {
            base.Show();
            OnClose = _OnConfirm;
        }

        public override void BackKey()
        {
            // nothing
        }
    }
}