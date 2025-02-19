using System;
using System.Collections.Generic;
using System.Linq;
using DatasTypes;
using TMPro;
using Trucking.Common;
using Trucking.Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_Reward : Popup_Base<Popup_Reward>
    {
        public Button btnClaim;
        public GameObject itemCell;
        public Transform trsGrid;

        private List<RewardModel> rewardDatas;
        private Action onClaim;
        private int truckRewardIndex;
        private List<RewardModel> truckRewardModels;

        private bool animationDelay;

        private void Start()
        {
            disposableBlack.Clear();

            btnBlackPanel.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (animationDelay)
                    {
                        return;
                    }

                    btnClaim.onClick.Invoke();
                })
                .AddTo(this);

            btnClaim.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (animationDelay)
                    {
                        return;
                    }

                    AudioManager.Instance.PlaySound("sfx_resource_get");
                    Utilities.RemoveAllChildren(trsGrid);

                    Debug.Log("Reward Click ==============================================================");


                    GameManager.Instance.fsm.PopState();

                    if (truckRewardModels?.Count > 0)
                    {
                        ShowTruckRewardList();
                        return;
                    }

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

        public void Show(RewardModel rewardData, Action _onClaim = null)
        {
            Debug.Log("Reward Show ==============================================================");

            List<RewardModel> listRewardModels = new List<RewardModel>();
            listRewardModels.Add(rewardData);
            Show(listRewardModels, _onClaim);
        }

        public void Show(long gold, long cash, int exp, Action _onClaim = null)
        {
            List<RewardModel> listRewardModels = UIRewardManager.Instance.MakeRewardList(gold, cash, exp);

            Show(listRewardModels, _onClaim);
        }


        public void Show(List<RewardModel> _rewardDatas, Action _onClaim = null)
        {
            animationDelay = true;
            truckRewardIndex = 0;
            onClaim = _onClaim;
            rewardDatas = _rewardDatas;

            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(0.2f))
                .Subscribe(_ => { animationDelay = false; })
                .AddTo(this);

            truckRewardModels = rewardDatas.FindAll(x => x.type.Value == RewardData.eType.truck_id);

            if (truckRewardModels.Count > 0 && rewardDatas.Count == 1)
            {
                ShowTruckRewardList();
                return;
            }

            base.Show();

            foreach (var rewardModel in rewardDatas)
            {
                if (rewardModel.type.Value == RewardData.eType.truck_id
                    || rewardModel.IsRandomBox())
                {
                    continue;
                }

                GameObject copyCell = Instantiate(itemCell);
                copyCell.transform.SetParent(trsGrid, false);
                copyCell.SetActive(true);

                copyCell.GetComponentInChildren<Image>().sprite = GameManager.Instance.GetRewardImage(rewardModel);

                if (rewardModel.IsBooster())
                {
                    copyCell.GetComponentInChildren<TextMeshProUGUI>().text = string.Format(
                        Utilities.GetStringByData(Datas.buffData[rewardModel.index.Value].string_collect),
                        rewardModel.count.Value);
                }
                else
                {
                    copyCell.GetComponentInChildren<TextMeshProUGUI>().text =
                        Utilities.GetNumberKKK(rewardModel.count.Value);
                }

                UserDataManager.Instance.AddRewardType(rewardModel.type.Value,
                    rewardModel.count.Value, rewardModel.index.Value);
            }

            itemCell.SetActive(false);
            UserDataManager.Instance.SaveData();
        }

        void ShowTruckRewardList()
        {
            ShowTruckReward(truckRewardModels[truckRewardIndex], () =>
            {
                truckRewardIndex++;
                if (truckRewardIndex < truckRewardModels.Count)
                {
                    ShowTruckRewardList();
                }
                else
                {
                    onClaim?.Invoke();
                }
            });
        }

        void ShowTruckReward(RewardModel _truckRewardModel, Action _onClaim = null)
        {
            TruckData truckData = Datas.truckData.ToArray().FirstOrDefault(x => x.id == _truckRewardModel.count.Value);

            if (truckData != null)
            {
                Truck newTruck = Truck.AddNewTruck(truckData.id, true);
                UserDataManager.Instance.SaveData();
                Popup_TruckInformation.Instance.ShowClaim(newTruck, () => { _onClaim?.Invoke(); });
            }
            else
            {
                _onClaim?.Invoke();
                Debug.Assert(false, "Error! Truck ID");
            }
        }

        public void Show(int booster_id, Action _onClaim = null)
        {
            int boosterIndex = booster_id / 100 - 1;
            RewardModel reward = new RewardModel(RewardData.eType.booster, booster_id, boosterIndex);
            Show(reward, _onClaim);
        }
    }
}