using System;
using Trucking.Common;
using UniRx;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_Tutorial_Skip : Popup_Base<Popup_Tutorial_Skip>
    {
        public Button btnLeft;
        public Button btnRight;
        
        private Action OnCancle;
        private Action OnSkip;

        private void Start()
        {
            btnLeft.OnClickAsObservable()                                                                            
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    GameManager.Instance.fsm.PopState();
                    OnCancle?.Invoke();
                })
                .AddTo(this);
            
            btnRight.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    GameManager.Instance.fsm.PopState();
                    OnSkip?.Invoke();
                })
                .AddTo(this);            
        }
        
        public void Show(Action _OnCancle, Action _OnSkip)
        {
            base.Show();
            OnCancle = _OnCancle;
            OnSkip = _OnSkip;
        }
        
        public override void BackKey()
        {
            // nothing
        }
    }
}