using System;
using DG.Tweening;
using EnhancedUI.EnhancedScroller;
using TMPro;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.Mission;
using Trucking.UI.Popup;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.CellView
{
    public class AchievementCellView : EnhancedScrollerCellView
    {
        public Button btnCollect;
        public Slider slider;
        public TextMeshProUGUI txtCount;
        public TextMeshProUGUI txtReward;
        public TextMeshProUGUI txtDescription;
        public TextMeshProUGUI txtTitle;
        public TextMeshProUGUI txtMaster;
        public TextMeshProUGUI txtCollect;
        
        public Transform trsIconImage;
        public Image imgReward;
        public Transform trsStep;

        public AchievementCellModel cell;

        private Action<AchievementCellView> OnClickCollect;
        
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();

        private void Start()
        {
            btnCollect.OnClickAsObservable().Subscribe(_ =>
            {
                AudioManager.Instance.PlaySound("sfx_button_main");
                
                Popup_Reward.Instance.Show(cell.GetRewardModel(), () =>
                {
                    long preCount = cell.quest.Value.count.Value;
                    long preMax = cell.quest.Value.max.Value;

                    if (cell.step.Value + 1 < cell.data.reward_type.Length)
                    {
                        cell.quest.Value = QuestModel.Make(cell.data, cell.step.Value + 1);
                    }

                    cell.quest.Value.count.Value = preCount - preMax;
                    cell.step.Value++;
                    RefreshData();
                    
                    transform.localScale = new Vector3(0, 1, 1);
                    transform.DOScaleX(1, 0.3f)
                        .SetDelay(0.05f);
                });
                
            }).AddTo(this);        
            
            _compositeDisposable.AddTo(this);
        }
        
        private void OnDisable()
        {
            _compositeDisposable.Clear();
        }

        public void SetData(AchievementCellModel _cell,
            Action<AchievementCellView> _onClickCollect
        )
        {
            cell = _cell;
            OnClickCollect = _onClickCollect;
            
            _compositeDisposable.Clear();
            
            txtTitle.text = Utilities.GetStringByData(cell.data.string_name);
            slider.maxValue = cell.quest.Value.max.Value;
            
            cell.quest.Value.count.Subscribe(count =>
            {
                slider.value = count;
                txtCount.text = $"{Utilities.GetNumberKKK(cell.quest.Value.count.Value)} / {Utilities.GetNumberKKK(cell.quest.Value.max.Value)}";

                btnCollect.gameObject.SetActive(cell.quest.Value.IsSuccess()
                                                && cell.step.Value < cell.data.reward_type.Length);
                slider.gameObject.SetActive(!cell.quest.Value.IsSuccess());
            }).AddTo(_compositeDisposable);

            cell.step.Subscribe(step =>
            {
                for (int i = 0; i < trsStep.childCount; i++)
                {
                    int value = step % 3;
                    trsStep.GetChild(i).GetComponent<Image>().enabled = i <= value || step == cell.data.reward_type.Length;
                    trsStep.GetChild(i).GetChild(0).gameObject.SetActive(i > value && step < cell.data.reward_type.Length);
                }
                
                for (int i = 0; i < trsIconImage.childCount; i++)
                {
                    trsIconImage.GetChild(i).gameObject.SetActive(i == Math.Min(step, cell.data.reward_type.Length - 1) / 3);             
                }

                if (cell.step.Value < cell.data.reward_type.Length)
                {
                    txtReward.text = Utilities.GetThousandCommaText(cell.data.reward_count[step]);
                    imgReward.sprite = GameManager.Instance.GetRewardImage(cell.data.reward_type[step].type, RewardModel.GetIndex(cell.data.reward_type[step].type, cell.data.reward_index[step]));
                    txtDescription.text = string.Format(Utilities.GetStringByData(cell.data.string_description), Utilities.GetNumberKKK(cell.data.count[step]));
                }
                
                txtReward.gameObject.SetActive(cell.step.Value < cell.data.reward_type.Length);
                imgReward.gameObject.SetActive(cell.step.Value < cell.data.reward_type.Length);
                txtDescription.gameObject.SetActive(cell.step.Value < cell.data.reward_type.Length);
                txtMaster.gameObject.SetActive(cell.step.Value == cell.data.reward_type.Length);
                btnCollect.gameObject.SetActive(cell.quest.Value.IsSuccess() 
                                                && cell.step.Value < cell.data.reward_type.Length);
                slider.gameObject.SetActive(!cell.quest.Value.IsSuccess() 
                                            && cell.step.Value < cell.data.reward_type.Length);
            }).AddTo(_compositeDisposable);
        }
        
        public void RefreshData()
        {
            SetData(cell,
                OnClickCollect
            );
            

        }

    }
}