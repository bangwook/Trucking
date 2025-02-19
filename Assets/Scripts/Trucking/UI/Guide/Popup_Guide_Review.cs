using System;
using Trucking.UI.Popup;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Guide
{
    public class Popup_Guide_Review : Popup_Base<Popup_Guide_Review>
    {
        public Button btnLeft;
        public Button btnRight;

        private Action onClickConfirm;
        
        private void Start()
        {
            btnLeft.OnClickAsObservable()                                                                            
                .Subscribe(_ =>
                {
                    GameManager.Instance.fsm.PopState();
                    onClickConfirm?.Invoke();
                })
                .AddTo(this);
            
            btnRight.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    GameManager.Instance.fsm.PopState();
                    string name = Application.identifier;
                    Application.OpenURL ("market://details?id=" + name);
                    onClickConfirm?.Invoke();
                })
                .AddTo(this);            
        }
        
        public void Show(Action _onClickConfirm = null)
        {
            base.Show();
            onClickConfirm = _onClickConfirm;            
        }
        
        public override void BackKey()
        {
            // nothing
        }
    }
}