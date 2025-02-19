using System;
using DG.Tweening;
using TMPro;
using Trucking.Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.ToolTip
{
    public class Popup_ToolTip_Level : MonoSingleton<Popup_ToolTip_Level>
    {
        public TextMeshProUGUI txtDes;
        public Slider slider;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void Show(Vector3 targetPos)
        {
            Popup_ToolTip_Boost.Instance.Close();
            Close();
            transform.position = new Vector3(targetPos.x, transform.position.y, 0);

            AudioManager.Instance.PlaySound("sfx_guide");
            GetComponent<CanvasGroup>().alpha = 1;

            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(2f)).Subscribe(_ =>
            {
                GetComponent<CanvasGroup>().DOFade(0, 0.3f);
                UnirxExtension.DateTimer(DateTime.Now.AddSeconds(0.3f)).Subscribe(__ => { Close(); }).AddTo(_compositeDisposable);
            }).AddTo(_compositeDisposable);

            Observable.EveryUpdate().Subscribe(_ =>
            {
                long current = UserDataManager.Instance.data.exp.Value;
                long max = UserDataManager.Instance.GetNextExp();

                slider.value = current;
                slider.maxValue = max;
                gameObject.SetActive(true);

                txtDes.text = string.Format(Utilities.GetStringByData(31108), max - current);
            }).AddTo(_compositeDisposable);
        }

        public void Close()
        {
            _compositeDisposable.Clear();
            gameObject.SetActive(false);
        }
    }
}