using Trucking.Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_GuideMain : Popup_Base<Popup_GuideMain>
    {
        public Button btnLeft;
        public Button btnRight;
        public Transform trsContents;
        public Transform trsTitles;
        public Transform trsPages;

        private ReactiveProperty<int> index = new ReactiveProperty<int>();

        private void Start()
        {
            btnLeft.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    index.Value--;
                }).AddTo(this);

            btnRight.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    index.Value++;
                }).AddTo(this);

            index.Subscribe(idx =>
            {
                for (int i = 0; i < trsContents.childCount; i++)
                {
                    trsContents.GetChild(i).gameObject.SetActive(i == idx - 1);
                    trsTitles.GetChild(i).gameObject.SetActive(i == idx - 1);
                    trsPages.GetChild(i).GetChild(0).gameObject.SetActive(i != idx - 1);
                    trsPages.GetChild(i).GetChild(1).gameObject.SetActive(i == idx - 1);
                }

                btnLeft.gameObject.SetActive(idx > 1);
                btnRight.gameObject.SetActive(idx < trsContents.childCount);
            }).AddTo(this);
        }

        public void Show(int idx = 1)
        {
            base.Show();
            index.Value = idx;
        }
    }
}