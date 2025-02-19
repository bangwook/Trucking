using System;
using DatasTypes;
using DG.Tweening;
using TMPro;
using Trucking.Common;
using Trucking.Model;
using UniRx;
using UnityEngine;

namespace Trucking.UI.ToolTip
{
    public class Popup_ToolTip_Boost : MonoSingleton<Popup_ToolTip_Boost>
    {
        public TextMeshProUGUI txtDes;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void Show(BuffData data, RectTransform targetPos)
        {
            Popup_ToolTip_Level.Instance.Close();
            Close();
            AudioManager.Instance.PlaySound("sfx_guide");
            gameObject.SetActive(true);

            int index = RewardModel.GetIndex(RewardData.eType.booster, data.id);
            txtDes.text = string.Format(Utilities.GetStringByData(data.string_description),
                Utilities.GetTimeString(UserDataManager.Instance.data.boosterShopData[index] -
                                        DateTime.Now));
            transform.position = Utilities.CopyVector3FromRectTransform(targetPos, 40);

            GetComponent<CanvasGroup>().alpha = 1;
            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(2f)).Subscribe(_ =>
            {
                GetComponent<CanvasGroup>().DOFade(0, 0.3f);
                UnirxExtension.DateTimer(DateTime.Now.AddSeconds(0.3f)).Subscribe(__ => { Close(); }).AddTo(_compositeDisposable);
            }).AddTo(_compositeDisposable);
        }

        public void Close()
        {
            _compositeDisposable.Clear();
            gameObject.SetActive(false);
        }
    }
}