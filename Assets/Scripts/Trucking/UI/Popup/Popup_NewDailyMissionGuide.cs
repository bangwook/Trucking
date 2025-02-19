using System;
using TMPro;
using Trucking.Common;
using Trucking.UI.Mission;
using UniRx;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_NewDailyMissionGuide : Popup_Base<Popup_NewDailyMissionGuide>
    {
        public Button btnClose;
        public TextMeshProUGUI txtName;

        private bool isCity;
        private CompositeDisposable _disposableTimer = new CompositeDisposable();

        private void Start()
        {
            btnClose.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    NewDailyMissionManager.Instance.isNew.Value = false;
                    GameManager.Instance.fsm.PopState();
                    Popup_NewDailyMission.Instance.Show();
                })
                .AddTo(this);
        }

        public override void Show()
        {
            base.Show();

            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (NewDailyMissionManager.Instance.model.nextTime.Value > DateTime.Now
                    && !NewDailyMissionManager.Instance.model.questModel.Value.IsSuccess())
                {
                    txtName.text =
                        Utilities.GetTimeStringShort(
                            NewDailyMissionManager.Instance.model.nextTime.Value - DateTime.Now);
                }
                else
                {
                    txtName.text = NewDailyMissionManager.Instance.city.Value.name;
                }
            }).AddTo(_disposableTimer);
        }

        public override void BackKey()
        {
            // nothing
        }

        public override void Close()
        {
            base.Close();
            _disposableTimer.Clear();
        }
    }
}