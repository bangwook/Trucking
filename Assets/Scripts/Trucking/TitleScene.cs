using TMPro;
using Trucking.Common;
using Trucking.UI.Popup;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking
{
    public class TitleScene : MonoSingleton<TitleScene>
    {
        public Camera camera;
        public Canvas canvas;
        public Slider slider;
        public TextMeshProUGUI txtPercent;
        public TextMeshProUGUI txtVersion;

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private bool isFirst = true;

        private void Update()
        {
            if (isFirst)
            {
                isFirst = false;
                txtPercent.text = "0%";
                slider.maxValue = 1;
                slider.value = 0;

                if (Debug.isDebugBuild)
                {
                    txtVersion.text = "Dev - Ver " + Common.Trucking.GetVersionString();
                }
                else
                {
                    txtVersion.text = "Ver " + Common.Trucking.GetVersionString();
                }

                I2.Loc.LocalizationManager.CurrentLanguage = "English";

                if (Debug.isDebugBuild /*&& !Application.isEditor*/)
                {
                    Popup_DB_Update.Instance.Show(
                        CheckGDPR,
                        () =>
                        {
                            txtPercent.text = "Data Reloading...";
                            Datas.Reload(CheckGDPR);

                            Observable.EveryLateUpdate().Subscribe(_ =>
                            {
                                txtPercent.text = $"Data Reloading...{Datas.countLoaded}/{Datas.countLoadedMax}";
                            }).AddTo(_compositeDisposable);
                        });
                }
                else
                {
                    CheckGDPR();
                }
            }
        }

        void CheckGDPR()
        {
            if (PlayerPrefs.GetInt("GDPR") == 0)
            {
                Popup_GDPR.Instance.Show(GoGameScene);
            }
            else
            {
                GoGameScene();
            }
        }

        void GoGameScene()
        {
            _compositeDisposable.Clear();
            LoadingScene.LoadScene("GameScene");
        }
    }
}