using UniRx;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_City_Guide : Popup_Base<Popup_City_Guide>
    {
        public Button btnOk;

        private void Start()
        {
            btnOk.OnClickAsObservable()
                .Subscribe(_ =>
                {                    
                    GameManager.Instance.fsm.PopState();
                })
                .AddTo(this);
        }

        public override void BackKey()
        {
            // nothing
        }

    }
}