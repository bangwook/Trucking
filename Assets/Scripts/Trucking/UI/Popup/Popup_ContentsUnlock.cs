using System;
using DatasTypes;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_ContentsUnlock : Popup_Base<Popup_ContentsUnlock>
    {
        public Button btnOk;
        public Transform trsContents;

        private Action onClickConfirm;

        private void Start()
        {
            btnOk.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    GameManager.Instance.fsm.PopState();
                    onClickConfirm?.Invoke();
                })
                .AddTo(this);
        }

        public void Show(LevelData.eUnlock type, Action _onClickConfirm = null)
        {
            base.Show();

            onClickConfirm = _onClickConfirm;

            for (int i = 0; i < trsContents.childCount; i++)
            {
                trsContents.GetChild(i).gameObject.SetActive(false);
            }

            switch (type)
            {
                case LevelData.eUnlock.delivery:
                    trsContents.GetChild(0).gameObject.SetActive(true);
                    break;
                case LevelData.eUnlock.daily:
                    trsContents.GetChild(1).gameObject.SetActive(true);
                    break;
                case LevelData.eUnlock.level:
                    trsContents.GetChild(2).gameObject.SetActive(true);
                    break;
                case LevelData.eUnlock.operation:
                    trsContents.GetChild(3).gameObject.SetActive(true);
                    break;
                case LevelData.eUnlock.freeCash:
                    trsContents.GetChild(4).gameObject.SetActive(true);
                    break;
            }
        }
    }
}