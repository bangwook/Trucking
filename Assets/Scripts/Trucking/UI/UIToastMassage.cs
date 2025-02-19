using System;
using DG.Tweening;
using TMPro;
using Trucking.Common;
using UniRx;
using UnityEngine;

namespace Trucking.UI
{
    public class UIToastMassage : MonoSingleton<UIToastMassage>
    {
        public TextMeshProUGUI txtDes;

        private CompositeDisposable disposable = new CompositeDisposable();
        private Tweener tweener;

        public void Show(string str, int sec = 2)
        {
            if (UserDataManager.Instance.data.hasTutorial.Value)
            {
                return;
            }

            disposable.Clear();

            txtDes.text = str;
            tweener?.Kill();
            GetComponent<CanvasGroup>().alpha = 0;
            tweener = GetComponent<CanvasGroup>().DOFade(1, 0.5f);
            gameObject.SetActive(true);
            Observable.NextFrame().Subscribe(_ => { }).AddTo(this);

            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(sec)).Subscribe(t => { Hide(); }).AddTo(disposable);
        }

        public void Show(int stringID, bool noFade = false)
        {
            Show(Utilities.GetStringByData(stringID), 4);
        }

        public void Hide()
        {
            if (gameObject.activeSelf)
            {
                tweener = GetComponent<CanvasGroup>().DOFade(0, 0.5f)
                    .OnComplete(() => { gameObject.SetActive(false); });
            }
        }

        public void Close()
        {
            tweener?.Kill();
            gameObject.SetActive(false);
        }
    }
}