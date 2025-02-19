using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Trucking.MyComponent
{
    public class ChangeParentAndFollow : MonoBehaviour
    {
        private void Awake()
        {
            transform.parent?.OnEnableAsObservable()
                .Subscribe(_ => gameObject.SetActive(true)).AddTo(this);

            transform.parent?.OnDisableAsObservable()
                .Subscribe(_ => gameObject.SetActive(false)).AddTo(this);

            Observable.NextFrame().Subscribe(_ => { transform.SetParent(GameManager.Instance.trs3DUIText); })
                .AddTo(this);
        }

        private void LateUpdate()
        {
            if (Camera.main != null)
            {
                transform.rotation = Camera.main.transform.rotation;
            }
        }
    }
}