using System;
using Trucking.UI.Popup;
using UniRx;
using UnityEngine.UI;

namespace Trucking.UI.Guide
{
    public class Popup_Guide_RouteReward : Popup_Base<Popup_Guide_RouteReward>
    {
        public Button btnClose;
        private Action OnClose;

        private void Start()
        {
            btnClose.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    GameManager.Instance.fsm.PopState();
                    OnClose?.Invoke();
                })
                .AddTo(this);
        }
        
        public void Show(Action _OnClose)
        {
            base.Show();
            OnClose = _OnClose;
        }
        
        public override void BackKey()
        {
            // nothing
        }
    }
}