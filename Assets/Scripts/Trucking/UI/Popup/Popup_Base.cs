using System;
using DG.Tweening;
using Trucking.Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_Base<T> : MonoSingleton<T> where T : MonoSingleton<T>
    {
        protected Button btnBlackPanel;
        protected Button btnCloseX;
        protected Transform contents;
        protected Action OnClose;

        protected CompositeDisposable disposableClose = new CompositeDisposable();
        protected CompositeDisposable disposableBlack = new CompositeDisposable();

        private void Awake()
        {
            btnBlackPanel = transform.Find("black_panel")?.GetComponent<Button>();
            contents = transform.Find("contents")?.GetComponent<Transform>();
            btnCloseX = contents?.Find("x_Button")?.GetComponent<Button>();

            btnBlackPanel.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            btnBlackPanel.GetComponent<Image>().DOFade(0.7f, 0.5f);

            btnBlackPanel?.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (btnCloseX != null && btnCloseX.gameObject.activeSelf)
                    {
                        btnCloseX.onClick.Invoke();
                    }
                })
                .AddTo(disposableBlack);

            btnCloseX?.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    GameManager.Instance.fsm.PopState();
                    OnClose?.Invoke();
                })
                .AddTo(disposableClose);
        }

        public virtual void Show()
        {
            Show(null);
        }

        public void Show(Action _OnConfirm = null)
        {
            OnClose = _OnConfirm;
            gameObject.SetActive(true);
            GameManager.Instance.PushPopupState(this);

            if (contents != null)
            {
                contents.DOScale(0.9f, 0);
                contents.DOScale(1, 0.3f).SetEase(Ease.OutCubic);
            }
        }

        public void Close(bool isSound = true)
        {
            if (isSound)
            {
                AudioManager.Instance.PlaySound("sfx_button_cancle");
            }

            gameObject.SetActive(false);
        }

        public virtual void Close()
        {
            Close(true);
        }

        public virtual void BackKey()
        {
            btnBlackPanel?.onClick.Invoke();
        }
    }
}