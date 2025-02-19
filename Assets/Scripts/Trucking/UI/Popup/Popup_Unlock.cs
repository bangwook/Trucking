using System;
using TMPro;
using Trucking.Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Trucking.UI.Popup
{
    public class Popup_Unlock : Popup_Base<Popup_Unlock>
    {
        public Button btnClaim;
        public TextMeshProUGUI txtDesc;
        public GameObject imgMap;
        public GameObject imgRoute;

        private Action onClickClose;

        private void Start()
        {
            btnClaim.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    GameManager.Instance.fsm.PopState();
                    onClickClose?.Invoke();
                })
                .AddTo(this);
        }

        public void ShowMap(Action _onClickClose = null)
        {
            Show();

            onClickClose = _onClickClose;

            imgMap.SetActive(true);
            imgRoute.SetActive(false);

            txtDesc.text = Utilities.GetStringByData(30043);
        }

        public void ShowRoute(Action _onClickClose = null)
        {
            Show();

            onClickClose = _onClickClose;

            imgMap.SetActive(false);
            imgRoute.SetActive(true);

            txtDesc.text = Utilities.GetStringByData(30042);
        }

        public override void BackKey()
        {
            //
        }
    }
}