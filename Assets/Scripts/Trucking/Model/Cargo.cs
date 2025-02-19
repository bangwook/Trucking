using System;
using System.Collections.Generic;
using System.Linq;
using DatasTypes;
using Trucking.Common;
using Trucking.Manager;
using UniRx;
using UnityEngine;

namespace Trucking.Model
{
    public class Cargo
    {
        public const int MAX_LEVEL = 5;
        public CargoModel model;

        public ReactiveProperty<City> from = new ReactiveProperty<City>();
        public ReactiveProperty<City> to = new ReactiveProperty<City>();

        public static List<CargoData> randomCargos;
        public static int totalRate;
        public static int rewardTotalRate;

        CompositeDisposable _disposable = new CompositeDisposable();

        public Cargo(CargoModel md)
        {
            _disposable.Clear();
            model = md;

            if (md.from.Value != null)
            {
                from.Value = GameManager.Instance.FindStation(md.from.Value);
            }

            if (md.to.Value != null)
            {
                to.Value = GameManager.Instance.FindStation(md.to.Value);
            }
        }

        public static Cargo MakeWithData(City from, City to, CargoData data = null, Cargo cargo = null,
            bool isNotOnRoute = false)
        {
            if (cargo == null)
            {
                cargo = new Cargo(new CargoModel());
            }

            if (data == null)
            {
                data = Datas.cargoData.Random();
            }

            if (from == to)
            {
                Debug.Assert(from == to, "from == to");
            }

            cargo.model.rewardModels.Clear();


            RewardModel rewardModel = null;
            CargoLevelData cargoLevelData = GetCargoLevelData();
            CargoRewardData cargoRewardData = GetRandomCargoRewardData();

            int _weight = Utilities.RandomRange(1, cargoLevelData.max_numb);
            int _grade = cargoLevelData.cargo_level;
            float _distance = WorldMap.Instance.GetDistance(from, to);
            int randScale =
                Utilities.RandomRange(-Datas.cargoData[0].cargo_randscale, Datas.cargoData[0].cargo_randscale);

            Debug.Assert(randScale >= -Datas.cargoData[0].cargo_randscale &&
                         randScale <= Datas.cargoData[0].cargo_randscale);

            if (cargoRewardData.gold > 0)
            {
                long count = 0;

                if (isNotOnRoute)
                {
                    count = (long) Math.Ceiling(_weight * cargoRewardData.gold *
                                                (1 + (cargoLevelData.gold_mag + Datas.cargoData[0].cargo_miles +
                                                      _weight + randScale) / 100) *
                                                (1 + Datas.cargoData[0].cargo_route_bonus / 100));
                }
                else
                {
                    count = (long) Math.Ceiling(_weight * cargoRewardData.gold *
                                                (1 + (cargoLevelData.gold_mag + Datas.cargoData[0].cargo_miles +
                                                      _weight + randScale) / 100));
                }

                if (count <= 0)
                {
                    Debug.Assert(count > 0);
                }

                rewardModel = new RewardModel(RewardData.eType.gold, count);
                cargo.model.rewardModels.Add(rewardModel);
            }
            else if (cargoRewardData.cash > 0)
            {
                rewardModel = new RewardModel(RewardData.eType.cash, cargoRewardData.cash);
                cargo.model.rewardModels.Add(rewardModel);

                if (rewardModel.count.Value <= 0)
                {
                    Debug.Assert(rewardModel.count.Value > 0);
                }
            }
            else if (cargoRewardData.crate > 0)
            {
                rewardModel = new RewardModel(RewardData.eType.crate, 1);
                cargo.model.rewardModels.Add(rewardModel);
            }

            RewardModel rewardMaterialModel = null;
            if (cargoRewardData.material > 0 && to.data.mega)
            {
                rewardMaterialModel = new RewardModel(RewardData.eType.material, cargoRewardData.material,
                    to.data.material_id);
                cargo.model.rewardModels.Add(rewardMaterialModel);
            }

            long expCount = 0;

            if (isNotOnRoute)
            {
                expCount = (long) Math.Ceiling(_weight * cargoRewardData.xp *
                                               (1 + (cargoLevelData.xp_mag + Datas.cargoData[0].cargo_miles + _weight +
                                                     randScale) / 100) *
                                               (1 + Datas.cargoData[0].cargo_route_bonus / 100));
            }
            else
            {
                expCount = (long) Math.Ceiling(_weight * cargoRewardData.xp *
                                               (1 + (cargoLevelData.xp_mag + Datas.cargoData[0].cargo_miles + _weight +
                                                     randScale) / 100));
            }

            if (expCount <= 0)
            {
                Debug.Assert(expCount > 0);
            }


            RewardModel rewardExpModel = new RewardModel(RewardData.eType.exp, expCount);
            cargo.model.rewardModels.Add(rewardExpModel);

            cargo.from.Value = from;
            cargo.to.Value = to;
            cargo.model.distance.Value = _distance;
            cargo.model.isContract.Value = false;
            cargo.model.from.Value = from.data.name;
            cargo.model.to.Value = to.data.name;
            cargo.model.grade.Value = _grade;
            cargo.model.id.Value = data.id;
            cargo.model.weight.Value = _weight;
//            cargo.model.rewardModel.Value = rewardModel;
//            cargo.model.rewardMaterialModel.Value = rewardMaterialModel;
//            cargo.model.rewardExpModel.Value = rewardExpModel;
            cargo.model.refreshTime.Value = DateTime.MinValue;


            return cargo;
        }

        public static Cargo MakeWithValue(int fromId, int toId, int cargoID, int grade, int weight, int gold, int exp)
        {
            City from = GameManager.Instance.FindStation(fromId);
            City to = GameManager.Instance.FindStation(toId);

            if (from == to)
            {
                Debug.Assert(from == to, "from == to");
            }

            RewardModel rewardModel = new RewardModel(RewardData.eType.gold, gold * weight);
            RewardModel rewardExpModel = new RewardModel(RewardData.eType.exp, exp * weight);

            Cargo cargo = new Cargo(new CargoModel());
            cargo.from.Value = from;
            cargo.to.Value = to;
            cargo.model.distance.Value = WorldMap.Instance.GetDistance(from, to);
            cargo.model.isContract.Value = false;
            cargo.model.from.Value = from.data.name;
            cargo.model.to.Value = to.data.name;
            cargo.model.grade.Value = grade;
            cargo.model.id.Value = cargoID;
            cargo.model.weight.Value = weight;
            cargo.model.rewardModels.Add(rewardModel);
            cargo.model.rewardModels.Add(rewardExpModel);
//            cargo.model.rewardModel.Value = rewardModel;
//            cargo.model.rewardExpModel.Value = rewardExpModel;
            cargo.model.refreshTime.Value = DateTime.MinValue;

            return cargo;
        }

        public static CargoLevelData GetCargoLevelData()
        {
            int index = 0;

            for (int i = Datas.cargoLevelData.Length - 1; i >= 0; i--)
            {
                if (UserDataManager.Instance.data.lv.Value >= Datas.cargoLevelData[i].user_lv)
                {
                    index = i;
                    break;
                }
            }

            int rand = Utilities.RandomRange(0, 100);
            int value = 0;

            for (int i = 0; i < Datas.cargoLevelData[index].cargo_level_rate.Length; i++)
            {
                value += Datas.cargoLevelData[index].cargo_level_rate[i];

                if (rand <= value)
                {
                    return Datas.cargoLevelData[i];
                }
            }

            return Datas.cargoLevelData[0];
        }

        public static CargoRewardData GetRandomCargoRewardData()
        {
            if (rewardTotalRate == 0)
            {
                rewardTotalRate = Datas.cargoRewardData.ToArray().Sum(x => x.rate);
            }

            // test
//            return Datas.cargoRewardData[7];
//            return Datas.cargoRewardData[1];

            int rand = Utilities.RandomRange(0, rewardTotalRate);
            int value = 0;

            for (int i = 0; i < Datas.cargoRewardData.Length; i++)
            {
                value += Datas.cargoRewardData[i].rate;

                if (rand <= value)
                {
                    return Datas.cargoRewardData[i];
                }
            }

            return Datas.cargoRewardData[0];
        }

        public static Cargo GetEmptySlot()
        {
            Cargo cargo = new Cargo(new CargoModel());

            if (LunarConsoleVariables.isCargoSlotTime)
            {
                cargo.model.refreshTime.Value = DateTime.Now + TimeSpan.FromSeconds(20);
            }
            else
            {
                cargo.model.refreshTime.Value = DateTime.Now + TimeSpan.FromMinutes(Datas.cargoData[0].cargo_refresh);
            }

            return cargo;
        }

        public void RefreshDistance(City fromCity)
        {
            model.distance.Value = WorldMap.Instance.GetDistance(fromCity, to.Value);
        }
    }
}