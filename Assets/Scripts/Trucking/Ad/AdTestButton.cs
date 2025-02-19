using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.Ad
{
    [RequireComponent(typeof(Button))]
    public class AdTestButton : MonoBehaviour
    {
        [SerializeField] private AdUnit _unit;

        [SerializeField] private Text _text;

        private void Start()
        {
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => ShowReward()).AddTo(this);

            _text.text = _unit.ToString();
        }

        private void ShowReward()
        {
            AdManager.Instance.ShowReward(_unit, (result) => { Debug.Log($"ad result : {result}"); });
        }

//    private void OnValidate()
//    {
//        if (_text != null)
//        {
//            _text.text = _unit.ToString();
//        }
//    }
    }
}