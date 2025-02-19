using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.Iap
{
    /// <summary>
    /// 애플 인앱 복구 버튼
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class IapRestoreAppleButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => OnClick()).AddTo(this);
        }

        private void OnClick()
        {
            IapManager.Instance.RestoreApple((result) =>
            {
                // TODO result noti
            });
        }
    }
}