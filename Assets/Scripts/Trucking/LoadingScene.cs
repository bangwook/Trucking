using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using Trucking.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Trucking
{
    public class LoadingScene : MonoSingleton<LoadingScene>
    {
        public static string nextScene;

        public Slider slider;
        public TextMeshProUGUI txtPercent;
        public TextMeshProUGUI txtVersion;

        private void Start()
        {
            GC.Collect();
            StartCoroutine(LoadScene());
        }

        string nextSceneName;

        public static void LoadScene(string sceneName)
        {
            nextScene = sceneName;
            SceneManager.LoadScene("Loading");
        }

        public void SetExtraPercent(Action action)
        {
            DOTween.To(
                () => slider.value, // 무엇을 대상으로할지 
                num => slider.value = num, // 값 업데이트 
                1.01f,
                2f // 애니메이션 시간 
            ).OnComplete(() => { action?.Invoke(); }).OnUpdate(() =>
            {
                txtPercent.text = (int) (slider.value * 100) + "%";
            });
        }

        IEnumerator LoadScene()
        {
            txtPercent.text = "0%";
            slider.maxValue = 1;
            slider.value = 0;
            txtVersion.text = "Ver " + Common.Trucking.GetVersionString();

            yield return null;

            AsyncOperation op = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
            op.allowSceneActivation = false;

            float timer = 0.0f;
            bool tween = false;

            while (!op.isDone)
            {
                yield return null;

                timer += Time.deltaTime;

                if (op.progress >= 0.9f)
                {
                    if (!tween)
                    {
                        tween = true;
                        DOTween.To(
                            () => slider.value, // 무엇을 대상으로할지 
                            num => slider.value = num, // 값 업데이트 
                            0.9f,
                            2f // 애니메이션 시간 
                        ).OnComplete(() =>
                        {
                            op.allowSceneActivation = true;
                            GC.Collect();
                        });
                    }

//                    op.allowSceneActivation = true;
                    txtPercent.text = (int) (slider.value * 100) + "%";
                }
                else
                {
                    slider.value = Mathf.Lerp(slider.value, op.progress, timer);
                    if (slider.value >= op.progress)
                    {
                        timer = 0f;
                    }

                    txtPercent.text = (int) (slider.value * 100) + "%";
                }
            }
        }
    }
}