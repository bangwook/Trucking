using Trucking.UI.Craft;
using UniRx;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_NeedTruck : Popup_Base<Popup_NeedTruck>
    {
        public Button btnLeft;
        public Button btnRight;

        private void Start()
        {
            btnLeft.OnClickAsObservable()
                .Subscribe(_ => { Popup_GuideMain.Instance.Show(10); })
                .AddTo(this);

            btnRight.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    GameManager.Instance.fsm.PopState();
                    CraftView.Instance.Show(CraftView.Type.Pieces);
                })
                .AddTo(this);
        }
    }
}