using System;
using System.Collections.Generic;
using DatasTypes;
using Newtonsoft.Json.Utilities;
using Trucking.Common;
using Trucking.Model;
using UniRx;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_RandomBoxOpenEffect : Popup_Base<Popup_RandomBoxOpenEffect>
    {
        public Image imgRandomBox;

        public void Show(int index, Action _onClaim = null)
        {
            base.Show();
            
            imgRandomBox.sprite = GameManager.Instance.GetRewardImage(RewardData.eType.random_box, index);
            imgRandomBox.SetNativeSize();
            
            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(2.0f)).Subscribe(_ =>
            {
                GameManager.Instance.fsm.PopState();
                
                Popup_Reward.Instance.Show(UIRewardManager.Instance.MakeRandomBox(index),
                    () =>
                    {
                        _onClaim?.Invoke();
                    });
            }).AddTo(this);
        }
        
        public void ShowCrateBox(Crate _data, Action _onClaim = null)
        {
            base.Show();
            
            int dataIndex = Datas.crate.ToArray().IndexOf(x => x == _data);
            imgRandomBox.sprite = GameManager.Instance.atlasUI.GetSprite("random_gacha_0" + (dataIndex + 1));
            imgRandomBox.SetNativeSize();
            
            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(2.0f)).Subscribe(_ =>
            {
                GameManager.Instance.fsm.PopState();
                List<RewardModel> rewardModels = UIRewardManager.Instance.MakeCrateBox(_data);
                Popup_PieceReward.Instance.Show(rewardModels);
            }).AddTo(this);
        }
        
        public void Show(List<RewardModel> rewardModels, Action _onClaim = null)
        {
            base.Show();
            imgRandomBox.sprite = GameManager.Instance.atlasUI.GetSprite("main_icon_lucky_box_2");
            imgRandomBox.SetNativeSize();
            
            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(2.0f)).Subscribe(_ =>
            {
                GameManager.Instance.fsm.PopState();
                
                Popup_Reward.Instance.Show(rewardModels,
                    () =>
                    {
                        _onClaim?.Invoke();
                    });
            }).AddTo(this);
        }

    }
}