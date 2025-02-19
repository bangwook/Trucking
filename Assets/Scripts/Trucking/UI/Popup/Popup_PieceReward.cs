using System;
using System.Collections.Generic;
using DatasTypes;
using Newtonsoft.Json.Utilities;
using Trucking.Common;
using Trucking.Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_PieceReward : Popup_Base<Popup_PieceReward>
    {
//        public Button btnBlackPanel;
        public Button btnClaim;
        public Transform trsGrid;

        private Action onClaim;
        private bool animationDelay;

        private void Start()
        {
            disposableClose.Clear();
            disposableBlack.Clear();

            btnClaim.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_resource_get");
                    GameManager.Instance.fsm.PopState();
                    onClaim?.Invoke();
                }).AddTo(this);
        }

        public override void BackKey()
        {
            if (animationDelay)
            {
                return;
            }

            btnClaim.onClick.Invoke();
        }

        public override void Close()
        {
            Close(false);
        }

        public void Show(List<RewardModel> _rewardDatas, Action _onClaim = null)
        {
            animationDelay = true;
            onClaim = _onClaim;
            btnClaim.gameObject.SetActive(false);

            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(0.2f * (_rewardDatas.Count + 1)))
                .Subscribe(_ =>
                {
                    animationDelay = false;
                    btnClaim.gameObject.SetActive(true);
                })
                .AddTo(this);

            base.Show();

            for (int i = 0; i < trsGrid.childCount; i++)
            {
                trsGrid.GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < _rewardDatas.Count; i++)
            {
                int index = i;
                UnirxExtension.DateTimer(DateTime.Now.AddSeconds(0.2f * i)).Subscribe(value =>
                {
                    trsGrid.GetChild(index).gameObject.SetActive(true);
                    TruckData data =
                        Datas.truckData[
                            Datas.truckData.ToArray().IndexOf(x => x.id == _rewardDatas[index].index.Value)];
                    trsGrid.GetChild(index).GetComponent<Popup_PieceRewardCellView>().SetData(data);

                    UserDataManager.Instance.AddRewardType(_rewardDatas[index].type.Value,
                        _rewardDatas[index].count.Value,
                        Datas.truckData.ToArray().IndexOf(x => x.id == _rewardDatas[index].index.Value));

                    FBAnalytics.FBAnalytics.LogPieceCollectEvent(UserDataManager.Instance.data.lv.Value,
                        data.id);
                    UserDataManager.Instance.SaveData();
                }).AddTo(this);
            }
        }
    }
}