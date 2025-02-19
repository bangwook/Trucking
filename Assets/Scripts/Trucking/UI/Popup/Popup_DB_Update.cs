using System;
using Trucking.Common;
using UniRx;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_DB_Update : MonoSingleton<Popup_DB_Update>
    {
        public Button btnLeft;
        public Button btnRight;
        
        private Action OnCancel;
        private Action OnUpdate;

        private void Start()
        {
            btnLeft.OnClickAsObservable()                                                                            
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    OnCancel?.Invoke();
                    gameObject.SetActive(false);
                })
                .AddTo(this);
            
            btnRight.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    OnUpdate?.Invoke();
                    gameObject.SetActive(false);
                })
                .AddTo(this);            
        }
        
        public void Show(Action _OnCancel, Action _OnUpdate)
        {
            OnCancel = _OnCancel;
            OnUpdate = _OnUpdate;
            
            gameObject.SetActive(true);
        }        
    }
}