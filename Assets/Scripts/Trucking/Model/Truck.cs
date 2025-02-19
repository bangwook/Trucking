using UnityEngine;
using System;
using UniRx;
using System.Linq;
using System.Collections.Generic;
using DatasTypes;
using DG.Tweening;
using Dreamteck.Splines;
using Sirenix.Utilities;
using Trucking.Ad;
using Trucking.Common;
using Trucking.Manager;
using Trucking.UI.Mission;

namespace Trucking.Model
{
    public class Truck : MonoBehaviour
    {
        public const int MAX_LEVEL = 7;

        public TruckModel model;
        public SplinePositioner splinePositioner;


        public Color color;
        public ReactiveProperty<City> currentStation = new ReactiveProperty<City>();
        public ReactiveCollection<City> pathStation = new ReactiveCollection<City>();
        public ReactiveCollection<Cargo> cargos = new ReactiveCollection<Cargo>();
        public ReactiveCollection<Cargo> completeCargos = new ReactiveCollection<Cargo>();

        public ReactiveProperty<PathDirection> currentRoad = new ReactiveProperty<PathDirection>();
        public ReactiveCollection<PathDirection> pathRoad = new ReactiveCollection<PathDirection>();

        public TruckData data;
        public IReadOnlyReactiveProperty<int> CargoWeight;
        public IReadOnlyReactiveProperty<int> MaxFuel;
        public IReadOnlyReactiveProperty<int> MaxWeight;
        public IReadOnlyReactiveProperty<int> MaxTravelDistance;
        public IReadOnlyReactiveProperty<float> PathFuel;
        public ReactiveProperty<bool> isNew = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> isAdBooster = new ReactiveProperty<bool>();
        public GameObject goSmoke;

        private Tween pathTween;
        private float beforePathTime;

        public static GameObject GetTruckPrefab(TruckData _data, Transform parent = null)
        {
            GameObject truckObj = Instantiate(Resources.Load("Prefab/Truck_new/Truck_base"), parent) as GameObject;
            GameObject headObj = Instantiate(Resources.Load("Prefab/Truck_new/Head/" + _data.model_h)) as GameObject;

            headObj.transform.SetParent(truckObj.transform.GetChild(1), false);

            if (headObj.transform.childCount > 0)
            {
                headObj.transform.GetChild(0).gameObject.SetActive(false);
            }

            if (!_data.model_c.Equals("0"))
            {
                GameObject cargoObj =
                    Instantiate(Resources.Load("Prefab/Truck_new/Cargo/" + _data.model_c)) as GameObject;

                cargoObj.transform.SetParent(truckObj.transform.GetChild(0), false);

                if (cargoObj.transform.childCount > 0)
                {
                    cargoObj.transform.GetChild(0).gameObject.SetActive(false);
                }
            }

            return truckObj;
        }


        public static Truck MakeWithSavedData(TruckModel saveData, Transform parent = null)
        {
            TruckData truckData = Datas.truckData.ToArray().FirstOrDefault(x => x.id == saveData.dataID);
            GameObject go = GetTruckPrefab(truckData, parent);
            Truck truck = go.AddComponent<Truck>();
            truck.gameObject.SetActive(false);
            truck.name = Utilities.GetStringByData(truckData.name_id);
            truck.data = truckData;


            if (truck.transform.GetChild(0).childCount > 0)
            {
                truck.goSmoke = truck.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            }
            else
            {
                truck.goSmoke = truck.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
            }


            if (saveData.state.Value == TruckModel.State.Move)
            {
                truck.SetPath(saveData.pathStationName.ToList());
            }

            foreach (var cargoModel in saveData.cargoModels)
            {
                Cargo cargo = new Cargo(cargoModel);
                truck.cargos.Add(cargo);
            }

            foreach (var cargoModel in saveData.completeCargoModels)
            {
                Cargo cargo = new Cargo(cargoModel);
                truck.completeCargos.Add(cargo);
            }

            truck.SetModel(saveData);

            return truck;
        }

        public static Truck MakeTruck(int id, int colorIndex = 0)
        {
            TruckModel truckModel = new TruckModel(id);
            Truck truck = MakeWithSavedData(truckModel, GameManager.Instance.trsTrucks);

            truck.model.upgradeLv.Value = 1;
            truck.model.colorIndex.Value = colorIndex;
            truck.model.fuel.Value = truck.MaxFuel.Value;

            return truck;
        }


        public static Truck AddNewTruck(int id, bool isBuy = false)
        {
            Truck truck = MakeTruck(id);
            truck.isNew.Value = isBuy;
            UserDataManager.Instance.data.truckData.Add(truck.model);
            GameManager.Instance.trucks.Add(truck);
            return truck;
        }

        public static Truck AddTutorialTruck(int index)
        {
            return AddNewTruck(Datas.truckData[index].id);
        }

        public void SetColor(int colorIndex)
        {
            GameObject headObj = transform.GetChild(1).GetChild(0).gameObject;
            Material[] headMaterials = headObj.GetComponent<MeshRenderer>().materials;
            for (int i = 0; i < headMaterials.Length; i++)
            {
                if (headMaterials[i].name.Contains("M_head"))
                {
                    headMaterials[i] = ColorManager.Instance.ColorList[colorIndex].truckMaterial;
                }
            }

            headObj.GetComponent<MeshRenderer>().materials = headMaterials;

            if (transform.GetChild(0).childCount > 0)
            {
                GameObject cargoObj = transform.GetChild(0).GetChild(0).gameObject;
                Material[] cargoMaterials = cargoObj.GetComponent<MeshRenderer>().materials;
                for (int i = 0; i < cargoMaterials.Length; i++)
                {
                    if (cargoMaterials[i].name.Contains("M_head"))
                    {
                        cargoMaterials[i] = ColorManager.Instance.ColorList[colorIndex].truckMaterial;
                    }
                }

                cargoObj.GetComponent<MeshRenderer>().materials = cargoMaterials;
            }
        }


        //
        private void Start()
        {
        }

        public void SetModel(TruckModel _model)
        {
            splinePositioner = gameObject.AddComponent<SplinePositioner>();
            model = _model;


            model.colorIndex.Subscribe(index =>
            {
                color = ColorManager.Instance.ColorList[index].color;
                SetColor(index);
            }).AddTo(this);

            model.state.Pairwise()
                .Subscribe(value =>
                {
                    if (value.Previous == TruckModel.State.Move
                        && value.Current == TruckModel.State.Wait)
                    {
                        AudioManager.Instance.PlaySound("sfx_dirve_complete");
                        isAdBooster.Value = false;
                        MissionManager.Instance.AddValue(QuestData.eType.mileage, (int) GetPathDistance());
                        MissionManager.Instance.AddValue(QuestData.eType.cargo, GetCompleteCargoWeight());
                        MissionManager.Instance.AddValue(QuestData.eType.arrive, 1);

                        foreach (var cargo in completeCargos)
                        {
                            MissionManager.Instance.AddValue(QuestData.eType.cargo_to_city_id,
                                cargo.to.Value,
                                cargo.model.weight.Value);
                            MissionManager.Instance.AddValue(QuestData.eType.cargo_from_city_id,
                                cargo.from.Value,
                                cargo.model.weight.Value);

                            MissionManager.Instance.AddValue(QuestData.eType.to_city_cargo,
                                cargo.to.Value,
                                cargo.model.weight.Value);
                            MissionManager.Instance.AddValue(QuestData.eType.from_city_cargo,
                                cargo.from.Value,
                                cargo.model.weight.Value);

                            MissionManager.Instance.AddValue(QuestData.eType.to_city_cargo_id,
                                cargo.to.Value,
                                cargo,
                                cargo.model.weight.Value);
                            MissionManager.Instance.AddValue(QuestData.eType.from_city_cargo_id,
                                cargo.from.Value,
                                cargo,
                                cargo.model.weight.Value);

                            MissionManager.Instance.AddValue(QuestData.eType.cargo_id,
                                cargo,
                                cargo.model.weight.Value);

//                            MissionManager.Instance.AddValue(QuestData.eType.to_city_reward_id,
//                                cargo.to.Value,
//                                cargo.model.rewardModel.Value.type.Value,
//                                cargo.model.rewardModel.Value.count.Value);
//
//                            MissionManager.Instance.AddValue(QuestData.eType.from_city_reward_id,
//                                cargo.to.Value,
//                                cargo.model.rewardModel.Value.type.Value,
//                                cargo.model.rewardModel.Value.count.Value);
                        }

                        currentStation.Value.AddTruck(this);
                        FBAnalytics.FBAnalytics.LogArriveTruckEvent(UserDataManager.Instance.data.lv.Value,
                            currentStation.Value.name,
                            completeCargos.Count,
                            completeCargos.Sum(x =>
                                x.model.rewardModels.Where(y => y.type.Value == RewardData.eType.gold)
                                    .Sum(y => y.count.Value)),
                            completeCargos.Sum(x =>
                                x.model.rewardModels.Where(y => y.type.Value == RewardData.eType.cash)
                                    .Sum(y => y.count.Value)),
                            completeCargos.Sum(x =>
                                x.model.rewardModels.Where(y => y.type.Value == RewardData.eType.exp)
                                    .Sum(y => y.count.Value)),
                            data.id);
                    }

                    if (value.Previous == TruckModel.State.Wait
                        && value.Current == TruckModel.State.Move)
                    {
                        currentStation.Value.RemoveTruck(this);

                        FBAnalytics.FBAnalytics.LogSendTruckEvent(UserDataManager.Instance.data.lv.Value,
                            pathStation.First().name,
                            pathStation.Last().name,
                            cargos.Count,
                            completeCargos.Sum(x =>
                                x.model.rewardModels.Where(y => y.type.Value == RewardData.eType.gold)
                                    .Sum(y => y.count.Value)),
                            completeCargos.Sum(x =>
                                x.model.rewardModels.Where(y => y.type.Value == RewardData.eType.cash)
                                    .Sum(y => y.count.Value)),
                            completeCargos.Sum(x =>
                                x.model.rewardModels.Where(y => y.type.Value == RewardData.eType.exp)
                                    .Sum(y => y.count.Value)),
                            data.id);
                    }
                }).AddTo(this);

            model.state.Subscribe(state =>
            {
                goSmoke.SetActive(state == TruckModel.State.Move);
                gameObject.SetActive(state == TruckModel.State.Move);
            }).AddTo(this);

            currentStation.Pairwise()
                .Subscribe(value =>
                {
                    if (value.Current)
                    {
                        model.hasRoute.Value = true;
                        model.currentStation = value.Current.name;
                        transform.localPosition = value.Current.transform.localPosition;

                        if (model.state.Value == TruckModel.State.Wait)
                        {
                            value.Current.AddTruck(this);
                        }
                    }
                    else
                    {
                        model.hasRoute.Value = false;
                        model.currentStation = null;
                        cargos.Clear();
                        completeCargos.Clear();
                    }

                    if (value.Previous)
                    {
                        value.Previous.RemoveTruck(this);
                    }
                }).AddTo(this);

            CargoWeight = cargos.ObsSomeChanged().Select(_ => GetCargoWeight()).ToReadOnlyReactiveProperty()
                .AddTo(this);

            MaxFuel = model.upgradeLv.Select(value => data.fuel[value - 1]).ToReadOnlyReactiveProperty().AddTo(this);

            MaxWeight = model.upgradeLv.Select(value => data.cargo[value - 1]).ToReadOnlyReactiveProperty().AddTo(this);

            MaxTravelDistance = model.upgradeLv
                .Select(value => (int) (data.fuel[value - 1] * Datas.baseData[0].fuel_efficiency))
                .ToReadOnlyReactiveProperty().AddTo(this);

            PathFuel = pathRoad.ObsSomeChanged().Select(count =>
            {
                float dis = pathRoad.Sum(x => x.road.distance);
                return dis / Datas.baseData[0].fuel_efficiency;
            }).ToReadOnlyReactiveProperty().AddTo(this);

            // fuel add
            Observable.Interval(TimeSpan.FromSeconds(0.1f))
                .Where(_ => model.state.Value == TruckModel.State.Wait)
                .Subscribe(_ => { AddFuel(0.1f); }).AddTo(this);

            pathStation.ObsSomeChanged()
                .Subscribe(path =>
                {
                    model.pathStationName.Clear();

                    for (int i = 0; i < path.Count; i++)
                    {
                        model.pathStationName.Add(pathStation[i].name);
                    }
                }).AddTo(this);

            cargos.ObsSomeChanged()
                .Subscribe(cgs =>
                {
                    model.cargoModels.Clear();

                    for (int i = 0; i < cgs.Count; i++)
                    {
                        model.cargoModels.Add(cargos[i].model);
                    }
                }).AddTo(this);

            completeCargos.ObsSomeChanged()
                .Subscribe(cgs =>
                {
                    model.completeCargoModels.Clear();

                    for (int i = 0; i < cgs.Count; i++)
                    {
                        model.completeCargoModels.Add(completeCargos[i].model);
                    }
                }).AddTo(this);

            Observable.EveryLateUpdate().Subscribe(_ =>
            {
                if (model != null
                    && model.state.Value == TruckModel.State.Move)
                {
                    PathDirection pathDirection = pathRoad.ElementAt(model.pathIndex);
                    Road road = pathDirection.road;
                    bool isReverse = pathDirection.isReverse;
                    float deltaTime = GetDeltaTime();

                    float followDuration = road.distance / GetSpeed() * Datas.baseData[0].mile;
                    double percent = (deltaTime - beforePathTime) / followDuration;

                    splinePositioner.SetPercent(isReverse ? 1 - percent : percent);

                    road.SetTruckPosition(percent, isReverse);

                    if (percent >= 1)
                    {
                        Debug.Log("Truck LateUpdate");
                        beforePathTime += pathRoad[model.pathIndex].road.distance / GetSpeed() * Datas.baseData[0].mile;
                        model.pathIndex++;
                        currentStation.Value = pathStation[model.pathIndex];
                        DepartCargoToCity(pathStation[model.pathIndex]);
                        SetNextPathInMoving();
                        UserDataManager.Instance.SaveData();
                    }
                }
            }).AddTo(this);

            currentStation.Value = GameManager.Instance.FindStation(model.currentStation);
            SetPositionFromTime();

            if (model.state.Value == TruckModel.State.Wait)
            {
                double restTime = (DateTime.Now - UserDataManager.Instance.data.savedTime.Value).TotalSeconds;
                AddFuel((float) restTime);
            }
        }

        public void AddFuel(double restTime)
        {
            model.fuel.Value = Mathf.Clamp(
                (float) (model.fuel.Value + restTime * MaxFuel.Value / 100 / Datas.baseData[0].charge_time),
                0,
                MaxFuel.Value);
        }


        #region Cargo

        public bool AddCargo(Cargo cg)
        {
            if (CanAddCargo(cg))
            {
                cargos.Add(cg);
                return true;
            }

            return false;
        }

        public bool CanAddCargo(Cargo cg)
        {
            if (GetCargoWeight() + cg.model.weight.Value <= MaxWeight.Value)
            {
                return true;
            }

            return false;
        }

        public bool RemoveCargo(Cargo cg)
        {
            return cargos.Remove(cg);
        }

        public void RemoveAllCargo()
        {
            cargos.Clear();
        }

        public int GetCargoWeight(City city = null)
        {
            if (city == null)
            {
                return cargos.Sum(x => x.model.weight.Value);
            }

            return cargos.ToList().FindAll(x => x.from.Value != city).Sum(x => x.model.weight.Value);
        }

        public int GetCompleteCargoWeight()
        {
            return completeCargos.Sum(x => x.model.weight.Value);
        }


        public void DepartCargoToCity(City city)
        {
            for (int i = cargos.Count - 1; i >= 0; i--)
            {
                if (cargos[i].to.Value == city)
                {
                    //city.AddRewardData(cargos[i].model.rewardModel.Value);
                    completeCargos.Add(cargos[i]);
                    cargos.RemoveAt(i);
                }
            }
        }

        #endregion


        #region Path

        public void ResetPath()
        {
            model.state.Value = TruckModel.State.Wait;
            beforePathTime = 0;
            pathRoad.Clear();
            pathStation.Clear();
            splinePositioner.ResetTriggers();
            currentRoad.Value = null;
        }

        public void AddPath(City city, Road road = null)
        {
//        Debug.Log("AddPath : " + city.name + "     " + road);

            pathStation.Add(city);

            if (road != null)
            {
//            Debug.Log("IsReverse : " + road.IsReverse(city));
                pathRoad.Add(new PathDirection(road, road.IsReverse(city)));
            }
        }

        public void UndoPath()
        {
            if (pathRoad.Count > 0)
            {
                pathStation.RemoveAt(pathStation.Count - 1);
                pathRoad.RemoveAt(pathRoad.Count - 1);
            }
        }

        public List<City> GetPath()
        {
            return pathStation.ToList();
        }

        public void SetPath(List<string> pathName)
        {
            for (int i = 0; i < pathName.Count; i++)
            {
                City city = GameManager.Instance.FindStation(pathName[i]);

                if (i == 0)
                {
                    AddPath(city);
                }
                else
                {
                    Road road = GameManager.Instance.FindRoad(pathName[i - 1], pathName[i]);
                    AddPath(city, road);
                }
            }
        }

        public void MoveStart()
        {
            if (pathRoad.Count > 0)
            {
                foreach (var cargo in cargos)
                {
                    cargo.model.truckID.Value = model.birthID;
                    cargo.model.isContract.Value = true;
                }

                model.pathIndex = 0;
                model.startTime = DateTime.Now;
                model.state.Value = TruckModel.State.Move;
                currentStation.Value.trucks.Remove(this);

                float dis = pathRoad.Sum(x => x.road.distance);

                if (UserDataManager.Instance.GetBooster(UserDataManager.BoosterType.GAS) == 0)
                {
                    model.fuel.Value -= dis / Datas.baseData[0].fuel_efficiency;
                }

                SetNextPathInMoving();

                int random = UnityEngine.Random.RandomRange(0, 100);

                //test
                if (LunarConsoleVariables.isADBooster)
                {
                    random = 5;
                }

                isAdBooster.Value = !UserDataManager.Instance.data.hasTutorial.Value
                                    && AdManager.Instance.IsLoadedReward.Value
                                    && random <= Datas.baseData[0].delivery_boost_ad;
                MissionManager.Instance.AddValue(QuestData.eType.delivery_start, 1);
                UserDataManager.Instance.SaveData();
                gameObject.SetActive(true);
            }
        }

        public void SetPositionFromTime()
        {
            if (model.state.Value == TruckModel.State.Move)
            {
                gameObject.SetActive(true);

                float totalDis = GetPathDistance();
                float deltaTime = GetDeltaTime();
                float moveDis = deltaTime * GetSpeed() / Datas.baseData[0].mile;

                float moved = 0;

                moveDis = Mathf.Clamp(moveDis, 0, totalDis);
                beforePathTime = 0;

                for (int i = 0; i < pathRoad.Count; i++)
                {
                    float roadDis = pathRoad.ElementAt(i).road.distance;
                    moved += roadDis;

                    if (moved >= moveDis)
                    {
                        float partDis = moveDis - (moved - roadDis);
                        model.pathIndex = i;
                        SetNextPathInMoving(partDis / roadDis);
                        break;
                    }

                    DepartCargoToCity(pathStation[i + 1]);
                    currentStation.Value = pathStation[model.pathIndex];
                    beforePathTime += roadDis / GetSpeed() * Datas.baseData[0].mile;
                }
            }
        }

        public bool HasArrival()
        {
            if (model != null
                && model.state.Value == TruckModel.State.Move)
            {
                PathDirection pathDirection = pathRoad.ElementAt(model.pathIndex);
                Road road = pathDirection.road;
                float deltaTime = GetDeltaTime();

                float followDuration = road.distance / GetSpeed() * Datas.baseData[0].mile;
                double percent = (deltaTime - beforePathTime) / followDuration;

                return percent >= 1;
            }

            return false;
        }

//        private void LateUpdate()
//        {
//            if (model != null
//                && model.state.Value == TruckModel.State.Move)
//            {
//                PathDirection pathDirection = pathRoad.ElementAt(model.pathIndex);
//                Road road = pathDirection.road;
//                bool isReverse = pathDirection.isReverse;
//                float deltaTime = GetDeltaTime();
//
//                float followDuration = road.distance / GetSpeed() * Datas.baseData[0].mile;
//                double percent = (deltaTime - beforePathTime) / followDuration;
//
//                splinePositioner.SetPercent(isReverse ? 1 - percent : percent);
//
//                road.SetTruckPosition(percent, isReverse);
//
//                if (percent >= 1)
//                {
//                    Debug.Log("Truck LateUpdate");
//                    beforePathTime += pathRoad[model.pathIndex].road.distance / GetSpeed() * Datas.baseData[0].mile;
//                    model.pathIndex++;
//                    currentStation.Value = pathStation[model.pathIndex];
//                    DepartCargoToCity(pathStation[model.pathIndex]);
//                    SetNextPathInMoving();
//                    UserDataManager.Instance.SaveData();
//                }
//            }
//        }

        public void SetNextPathInMoving(double percent = 0f)
        {
            if (model.pathIndex < pathRoad.Count)
            {
                Road road = pathRoad.ElementAt(model.pathIndex).road;
                bool isReverse = pathRoad.ElementAt(model.pathIndex).isReverse;
                currentRoad.Value = pathRoad.ElementAt(model.pathIndex);

                road.SetMovingTruckAnimation(true, isReverse);
                road.SetTruckPosition(percent, isReverse);

                SplineMesh splineMesh = road.GetComponent<SplineMesh>();
                splinePositioner.computer = road.splineComputer;
                splinePositioner.mode = SplinePositioner.Mode.Percent;
                splinePositioner.direction = isReverse ? Spline.Direction.Backward : Spline.Direction.Forward;
                splinePositioner.SetClipRange(splineMesh.clipFrom, splineMesh.clipTo);
                splinePositioner.SetPercent(isReverse ? 1 - percent : percent);
            }
            else
            {
                if (model.startTime != DateTime.MinValue)
                {
                    TimeSpan completeFuelTime = DateTime.Now - model.startTime.AddSeconds(GetTransitTime());

                    if (completeFuelTime.TotalSeconds > 1)
                    {
                        AddFuel(completeFuelTime.TotalSeconds);
                    }
                }

//            model.repair.Value -= dis / Datas.baseData[0].durability;
                currentRoad.Value?.road.SetMovingTruckAnimation(false);
                ResetPath();
                gameObject.SetActive(false);
            }
        }

        public void SetBoost(RewardData.eType type)
        {
            if (model.state.Value == TruckModel.State.Move)
            {
                FBAnalytics.FBAnalytics.LogTruckBoostEvent(UserDataManager.Instance.data.lv.Value,
                    GameManager.Instance.trucks.Count,
                    type == RewardData.eType.cash ? "Cash" : "Ad");
                MissionManager.Instance.AddValue(QuestData.eType.truck_boost, 1);
                model.startTime = DateTime.MinValue;
                SetPositionFromTime();
            }
        }

        public float GetMovedDistance()
        {
            if (pathTween != null
                && model.state.Value == TruckModel.State.Move)
            {
                return pathTween.ElapsedPercentage() * GetPathDistance();
            }

            return 0;
        }

        public Vector3[] GetPathPositions()
        {
            if (pathStation.Count < 1)
            {
                return null;
            }

            Vector3[] vector3s = new Vector3[pathStation.Count];

            for (int i = 0; i < pathStation.Count; i++)
            {
                vector3s[i] = pathStation[i].transform.position;
            }

            return vector3s;
        }

        public float GetPathDistance()
        {
            float dis = 0;

            foreach (var rd in pathRoad)
            {
                dis += rd.road.distance;
            }

            return dis;
        }

        #endregion


        #region Data

        public string GetName()
        {
            return Utilities.GetStringByData(data.name_id);
        }

        public int GetSpeed(int lv)
        {
            return data.speed[lv - 1];
        }

        public int GetFuel(int lv)
        {
            return data.fuel[lv - 1];
        }

        public int GetCargo(int lv)
        {
            return data.cargo[lv - 1];
        }

        public float GetTransitTime()
        {
            if (pathStation.Count < 1)
            {
                return 0;
            }

            return GetPathDistance() / GetSpeed() * Datas.baseData[0].mile;
        }

        public float GetDeltaTime()
        {
            if (pathStation.Count < 1)
            {
                return 0;
            }

            return (float) (DateTime.Now - model.startTime).TotalMilliseconds / 1000;
        }

        public float GetLeftTime()
        {
            return GetTransitTime() - GetDeltaTime();
        }

        public int GetReFuelGold()
        {
            return (int) ((MaxFuel.Value - model.fuel.Value) * Datas.baseData[0].charge_gold);
        }

        public int GetReFuelCash()
        {
//        return (int)Math.Ceiling((MaxFuel.Value - model.fuel.Value) * Datas.baseData[0].charge_cash);
            return (int) Math.Ceiling((MaxFuel.Value - model.fuel.Value) / MaxFuel.Value * 100 *
                                      Datas.baseData[0].charge_cash);
        }

        public int GetSpeed()
        {
            if (LunarConsoleVariables.isTruckSpeedHack)
            {
                return (int) (data.speed[model.upgradeLv.Value - 1] * 10);
            }

            float boostRate = UserDataManager.Instance.GetBooster(UserDataManager.BoosterType.SPEED);
            return (int) (data.speed[model.upgradeLv.Value - 1] * (1 + boostRate / 100));
        }

        public TimeSpan GetMaxRefuelTime()
        {
            // 1L == 60초
            float addFuelSec = (MaxFuel.Value - model.fuel.Value) / MaxFuel.Value * 100 * Datas.baseData[0].charge_time;
//        float addFuelSec = (MaxFuel.Value - model.fuel.Value) * Datas.baseData[0].charge_time;

            return TimeSpan.FromSeconds(addFuelSec);
        }

        public TimeSpan GetPathRefuelTime()
        {
            // 1L == 60초
            float addFuelSec = (MaxFuel.Value - (model.fuel.Value - PathFuel.Value)) / MaxFuel.Value * 100 *
                               Datas.baseData[0].charge_time;
//        float addFuelSec = (MaxFuel.Value - (model.fuel.Value - PathFuel.Value)) * Datas.baseData[0].charge_time;

            return TimeSpan.FromSeconds(addFuelSec);
        }


        public void Upgrade()
        {
            model.upgradeLv.Value++;
            UserDataManager.Instance.data.truckUpgradeCount.Value++;
            model.fuel.Value = MaxFuel.Value;
            UserDataManager.Instance.SaveData();
        }

        #endregion


        public class PathDirection
        {
            public Road road;
            public bool isReverse;

            public PathDirection(Road _road, bool _isReverse)
            {
                road = _road;
                isReverse = _isReverse;
            }
        }
    }
}