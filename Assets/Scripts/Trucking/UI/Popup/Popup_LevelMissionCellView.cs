using System;
using UnityEngine;
using TMPro;
using Trucking.Common;
using Trucking.UI.Mission;
using UnityEngine.UI;
using UniRx;

namespace Trucking.UI.Popup
{
    public class Popup_LevelMissionCellView : MonoBehaviour
    {
        public TextMeshProUGUI txtTitle;
        public TextMeshProUGUI txtPercent;
        public TextMeshProUGUI txtCount;

        public Slider slider;

        public GameObject stateSuccess;
        public Image imgPercentBox;

        public bool isSuccess;

        private MissionBaseModel model;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void SetModel(MissionBaseModel _model)
        {
            _compositeDisposable.Clear();
            model = _model;
            
            model.mid.Subscribe(mid =>
            {
                txtTitle.text = model.listQuestModel[0].GetDescription();
                long count = Math.Min(model.listQuestModel[0].count.Value, model.listQuestModel[0].max.Value);
                txtPercent.text = (int)(count / model.listQuestModel[0].max.Value * 100) + "%";
                txtCount.text = Utilities.GetNumberKKK(count) 
                                + " / "
                                + Utilities.GetNumberKKK(model.listQuestModel[0].max.Value);
                slider.maxValue = model.listQuestModel[0].max.Value;
                slider.value = count;
                
            }).AddTo(_compositeDisposable);

            foreach (var baseModel in model.listQuestModel)
            {
                baseModel.count.Subscribe(_ =>
                {
                    long count = Math.Min(model.listQuestModel[0].count.Value, model.listQuestModel[0].max.Value);

                    txtPercent.text =
                        $"{(count * 100.0f / model.listQuestModel[0].max.Value):F0}" + "%";
                    txtCount.text = Utilities.GetNumberKKK(count)
                                    + " / " 
                                    + Utilities.GetNumberKKK(model.listQuestModel[0].max.Value);
                    slider.maxValue = model.listQuestModel[0].max.Value;
                    slider.value = count;

                    bool success = model.IsSuccess();
                    
                    stateSuccess.SetActive(success);
                    txtPercent.gameObject.SetActive(!success);
                    
                    if (success)
                    {
                        txtTitle.color = Utilities.GetColorByHtmlString("#008219");
                        GetComponent<Image>().color = Utilities.GetColorByHtmlString("#A4F2B2");
                        imgPercentBox.color = Utilities.GetColorByHtmlString("#09DD30");
                    }
                    else
                    {
                        txtTitle.color = Utilities.GetColorByHtmlString("#0086D1");
                        GetComponent<Image>().color = Utilities.GetColorByHtmlString("#C1E8FC");
                        imgPercentBox.color = Utilities.GetColorByHtmlString("#58C2FE");
                    }
                    
                }).AddTo(_compositeDisposable);
            }
        }
    }
}