using System.Collections.Generic;
using System.Linq;
using DatasTypes;
using Trucking.Common;
using Trucking.Model;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Trucking.UI
{
    public class UIRewardManager : MonoSingleton<UIRewardManager>
    {
        public UIReward uiRewardPrefab;

        private List<UIReward> listUIReward = new List<UIReward>();
        private City city;

        public void Show(RewardModel rewardModel, Vector3 pos)
        {
            List<RewardModel> listRewardModels = new List<RewardModel>();
            listRewardModels.Add(rewardModel);
            Show(listRewardModels, pos);
        }

        public void Show(City _city, Truck _tr = null)
        {
            city = _city;
            List<RewardModel> listRewardModels = MakeRewardList(city);

//            if (Utilities.RandomRange(0, 100) < Datas.baseData[0].complete_popup_rate
//                && !UserDataManager.Instance.data.hasTutorial.Value)
//            {
//                Popup_DoubleChance.Instance.Show(listRewardModels,
//                    success => { ShowCityReward(listRewardModels, success); });
//            }
//            else if (LunarConsoleVariables.isDouble)
//            {
//                Popup_DoubleChance.Instance.Show(listRewardModels,
//                    success => { ShowCityReward(listRewardModels, success); });
//            }
//            else
            {
                ShowCityReward(listRewardModels);
            }
        }

        public void Show(City _city, long gold, long cash, int exp)
        {
            city = _city;
            List<RewardModel> listRewardModels = MakeRewardList(gold, cash, exp);

            Show(listRewardModels, city.transform.position + new Vector3(0, 0, 40));
        }

        public void Show(Vector3 pos, long gold, long cash, int exp)
        {
            List<RewardModel> listRewardModels = MakeRewardList(gold, cash, exp);

            Show(listRewardModels, pos);
        }

        public void Show(List<RewardModel> _rewardDatas, Vector3 pos, bool isBoost = false, bool isBonus = false)
        {
            UIReward ui = Find();
            ui.Show(_rewardDatas, pos, isBoost, isBonus);
            AudioManager.Instance.PlaySound("sfx_resource_get");

            foreach (var rewardModel in _rewardDatas)
            {
                UserDataManager.Instance.AddRewardType(rewardModel.type.Value,
                    rewardModel.count.Value, rewardModel.index.Value);

                if (isBonus)
                {
                    UserDataManager.Instance.AddRewardType(rewardModel.type.Value,
                        rewardModel.count.Value, rewardModel.index.Value);
                }
            }
        }

        void ShowCityReward(List<RewardModel> listRewardModels, bool isBonus = false)
        {
            Show(listRewardModels, city.transform.position + new Vector3(0, 0, 40), true, isBonus);

            FBAnalytics.FBAnalytics.LogDeliveryRewardEvent(UserDataManager.Instance.data.lv.Value,
                city.name,
                city.completeCargoModels.Count,
                city.completeCargoModels.Sum(x =>
                    x.rewardModels.Where(y => y.type.Value == RewardData.eType.gold).Sum(y => y.count.Value)),
                city.completeCargoModels.Sum(x =>
                    x.rewardModels.Where(y => y.type.Value == RewardData.eType.cash).Sum(y => y.count.Value)),
                city.completeCargoModels.Sum(x =>
                    x.rewardModels.Where(y => y.type.Value == RewardData.eType.exp).Sum(y => y.count.Value)),
                city.completeCargoModels.Sum(x =>
                    x.rewardModels.Where(y => y.type.Value == RewardData.eType.material && y.index.Value == 0)
                        .Sum(y => y.count.Value)),
                city.completeCargoModels.Sum(x =>
                    x.rewardModels.Where(y => y.type.Value == RewardData.eType.material && y.index.Value == 1)
                        .Sum(y => y.count.Value)),
                city.completeCargoModels.Sum(x =>
                    x.rewardModels.Where(y => y.type.Value == RewardData.eType.material && y.index.Value == 2)
                        .Sum(y => y.count.Value)),
                city.completeCargoModels.Sum(x =>
                    x.rewardModels.Where(y => y.type.Value == RewardData.eType.material && y.index.Value == 3)
                        .Sum(y => y.count.Value))
            );

            city.ResetRewardData();
        }


        UIReward Find()
        {
            foreach (var uiReward in listUIReward)
            {
                if (!uiReward.gameObject.activeSelf)
                {
                    return uiReward;
                }
            }

            UIReward ui = Object.Instantiate(uiRewardPrefab, transform);
            listUIReward.Add(ui);
            return ui;
        }

        public List<RewardModel> MakeRewardList(RewardData.eType type, long value, int index)
        {
            List<RewardModel> listRewardModels = new List<RewardModel>();

            RewardModel reward = new RewardModel(type, value, index);
            listRewardModels.Add(reward);

            return listRewardModels;
        }

        public List<RewardModel> MakeRewardList(City city)
        {
            List<RewardModel> listRewardModels = new List<RewardModel>();

            foreach (var cargoModel in city.completeCargoModels)
            {
                listRewardModels.AddRange(cargoModel.rewardModels);
            }

            List<RewardModel> mergeData = new List<RewardModel>();

            long gold = listRewardModels.Where(x => x.type.Value == RewardData.eType.gold).Sum(x => x.count.Value);
            if (gold > 0)
            {
                RewardModel reward = new RewardModel(RewardData.eType.gold, gold);
                mergeData.Add(reward);
            }

            long cash = listRewardModels.Where(x => x.type.Value == RewardData.eType.cash).Sum(x => x.count.Value);
            if (cash > 0)
            {
                RewardModel reward = new RewardModel(RewardData.eType.cash, cash);
                mergeData.Add(reward);
            }

            long crate = listRewardModels.Where(x => x.type.Value == RewardData.eType.crate).Sum(x => x.count.Value);
            if (crate > 0)
            {
                RewardModel reward = new RewardModel(RewardData.eType.crate, crate);
                mergeData.Add(reward);
            }

            for (int i = 0; i < 4; i++)
            {
                long material = listRewardModels
                    .Where(x => x.type.Value == RewardData.eType.material && x.index.Value == i)
                    .Sum(x => x.count.Value);
                if (material > 0)
                {
                    RewardModel reward = new RewardModel(RewardData.eType.material, material, i);
                    mergeData.Add(reward);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                long part = listRewardModels.Where(x => x.type.Value == RewardData.eType.parts && x.index.Value == i)
                    .Sum(x => x.count.Value);
                if (part > 0)
                {
                    RewardModel reward = new RewardModel(RewardData.eType.parts, part, i);
                    mergeData.Add(reward);
                }
            }

            long exp = listRewardModels.Where(x => x.type.Value == RewardData.eType.exp).Sum(x => x.count.Value);
            if (exp > 0)
            {
                RewardModel reward = new RewardModel(RewardData.eType.exp, exp);
                mergeData.Add(reward);
            }

            return mergeData;
        }

        public List<RewardModel> MakeRewardList(long gold, long cash, int exp)
        {
            List<RewardModel> listRewardModels = new List<RewardModel>();

            if (gold != 0)
            {
                RewardModel rewardModel = new RewardModel(RewardData.eType.gold, gold, 0);
                listRewardModels.Add(rewardModel);
            }

            if (cash != 0)
            {
                RewardModel rewardModel = new RewardModel(RewardData.eType.cash, cash, 0);
                listRewardModels.Add(rewardModel);
            }

            if (exp != 0)
            {
                RewardModel rewardModel = new RewardModel(RewardData.eType.exp, exp, 0);
                listRewardModels.Add(rewardModel);
            }

            return listRewardModels;
        }

        public RewardModel MakeRewardBooster(BuffData data, int time)
        {
            int boosterIndex = RewardModel.GetIndex(RewardData.eType.booster, data.id);

            return new RewardModel(RewardData.eType.booster, time, boosterIndex);
        }

        public List<RewardModel> MakeRandomBox(int index)
        {
            AudioManager.Instance.PlaySound("sfx_box_open");

            RandomBox randomBox = Datas.randomBox[index];

            int slot = Random.Range(randomBox.slot_min, randomBox.slot_max + 1);
            int rateSum = randomBox.reward_type_rate.Sum();

            Debug.Assert(slot <= randomBox.reward_type.Length);

            List<RewardModel> listRewardModels = new List<RewardModel>();

            while (listRewardModels.Count < slot)
            {
                int rate = 0;

                for (int i = 0; i < randomBox.reward_type.Length; i++)
                {
                    int typeIndex = RewardModel.GetIndex(randomBox.reward_type[i].type, randomBox.type_index[i]);
                    RewardModel findModel = listRewardModels.Find(x => x.type.Value == randomBox.reward_type[i].type
                                                                       && x.index.Value == typeIndex);

                    if (findModel == null)
                    {
                        rate += randomBox.reward_type_rate[i];

                        int random = Random.Range(0, rateSum);

                        if (random < rate)
                        {
                            rateSum -= randomBox.reward_type_rate[i];

                            long rewardCount = Random.Range(randomBox.reward_count_min[i],
                                randomBox.reward_count_max[i] + 1);

                            RewardModel rewardModel = new RewardModel(randomBox.reward_type[i].type, rewardCount,
                                randomBox.type_index[i]);
                            Debug.Log(
                                $"MakeRandomBox  slot : {slot},   type : {rewardModel.type},    count : {rewardModel.index.Value},    index : {rewardModel.index.Value}");
                            listRewardModels.Add(rewardModel);
                            break;
                        }
                    }
                }
            }

            return listRewardModels;
        }

        public List<RewardModel> MakeCrateBox(Crate crate)
        {
            AudioManager.Instance.PlaySound("sfx_box_open");

            int slot = Random.Range(crate.slot_min, crate.slot_max + 1);
            int rateSum = crate.reward_type_rate.Sum();

            Debug.Assert(slot <= crate.piece_type.Length);

            List<RewardModel> listRewardModels = new List<RewardModel>();

            while (listRewardModels.Count < slot)
            {
                int rate = 0;

                for (int i = 0; i < crate.piece_type.Length; i++)
                {
                    rate += crate.reward_type_rate[i];

                    int random = Random.Range(0, rateSum);

                    if (random < rate)
                    {
                        rateSum -= crate.reward_type_rate[i];
                        int pieceIndex = crate.piece_type[i];
                        long rewardCount = Random.Range(crate.reward_count_min, crate.reward_count_max + 1);
                        Debug.Log("MakeCrateBox pieceIndex : " + pieceIndex);
//                        randomTypesDictionary.Add(pieceIndex, rewardCount);

                        RewardModel rewardModel = new RewardModel(RewardData.eType.truck_pc, rewardCount, pieceIndex);
                        listRewardModels.Add(rewardModel);
                        break;
                    }
                }
            }


            return listRewardModels;
        }
    }
}