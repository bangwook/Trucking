using System;
using Trucking.UI.Popup;
using UniRx;
using UnityEngine.UI;

namespace Trucking.UI.Guide
{
    public class Popup_Guide_SwapTruck : Popup_Base<Popup_Guide_SwapTruck>
    {
        public Button btnLeft;
        public Button btnRight;
        
        private Action OnChange;

        private void Start()
        {
            btnLeft.OnClickAsObservable()                                                                            
                .Subscribe(_ => GameManager.Instance.fsm.PopState())
                .AddTo(this);
            
            btnRight.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    GameManager.Instance.fsm.PopState();
                    OnChange?.Invoke();
                })
                .AddTo(this);            
        }
        
        public void Show(Action _OnChange)
        {
            base.Show();
            OnChange = _OnChange;
        }
        
        public override void BackKey()
        {
            // nothing
        }
    }
}