using System;
using System.Collections.Generic;
using System.Linq;
using DatasTypes;
using Newtonsoft.Json.Utilities;
using Trucking.Common;
using Trucking.Graph;
using Trucking.Manager;
using Trucking.UI;
using Trucking.UI.CellView;
using Trucking.UI.Mission;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Trucking.Model
{
    public class City : Node
    {
        public CityModel model;
        public List<Road> roads = new List<Road>();
        public CityData data;
        public ReactiveCollection<Truck> trucks = new ReactiveCollection<Truck>();
        public ReactiveCollection<Cargo> cargos = new ReactiveCollection<Cargo>();
        public ReactiveProperty<bool> isFocus = new ReactiveProperty<bool>(false);
        public ReactiveCollection<CargoModel> completeCargoModels = new ReactiveCollection<CargoModel>();

        public UICity ui;
        public IReadOnlyReactiveProperty<int> CargoWeight;
        public IReadOnlyReactiveProperty<int> MaxCargo;
        public IReadOnlyReactiveProperty<int> CurrentCargoCount;

        private bool isEdit;
        private CompositeDisposable disposableRefreshCargo = new CompositeDisposable();
        private CompositeDisposable disposableProductTime = new CompositeDisposable();
        public int partIndex;
        public int productIndex;
        public int matIndex;
        public PartsProduction partData;

        public static Mesh GetCityMesh(City city)
        {
            string path;

            if (city.data.mega)
            {
                path =
                    $"Fbx/City/Big_city/{city.data.name.Replace(" ", "_").ToUpper()}_lv0{Datas.joblistExpansion[city.model.level.Value].city_img}";
            }
            else
            {
                string meshName = city.GetComponent<MeshFilter>().sharedMesh.name;
                string strType = meshName.Substring(meshName.Length - 1);

                path =
                    $"Fbx/City/Small_city/city_lv0{Datas.joblistExpansion[city.model.level.Value].city_img}_{strType}";
            }

            Mesh mesh = Resources.Load<Mesh>(path);

            return mesh;
        }

//        private void OnDestroy()
//        {
//            Debug.LogWarning("City OnDestroy : " + name);
//        }

        public void SetModel(CityModel _model)
        {
            model = _model;
            cargos.Clear();

            if (data.id == 103)
            {
                Debug.LogWarning($"City SetModel 101 : {GetHashCode()}");
            }

            foreach (var cg in model.cargos)
            {
                Cargo cargo = new Cargo(cg);
                AddCargo(cargo);
            }

            foreach (var time in model.refreshTime)
            {
                Cargo cargo = Cargo.GetEmptySlot();
                cargo.model.refreshTime.Value = time;
                AddCargo(cargo);
            }

            cargos.ObsSomeChanged(false).Subscribe(cgs =>
            {
                model.refreshTime.Clear();
                model.cargos.Clear();

                disposableRefreshCargo.Clear();

                foreach (var cg in cgs)
                {
                    if (cg.model.weight.Value == 0)
                    {
                        model.refreshTime.Add(cg.model.refreshTime.Value);
                    }
                    else if (cg.model.isContract.Value)
                    {
                        model.cargos.Add(cg.model);
                    }
                }

                for (int i = cgs.Count - 1; i >= 0; i--)
                {
                    if (cgs[i].model.weight.Value == 0)
                    {
                        Cargo cargo = cgs[i];
                        UnirxExtension.DateTimer(cargo.model.refreshTime.Value).Subscribe(_ =>
                        {
//                        Debug.Log("Refesh Cargo AD Slot");
                            RemoveCargo(cargo);
                            AddCargo(MakeCargo());
                        }).AddTo(disposableRefreshCargo);
                    }
                }
            }).AddTo(this);

            CargoWeight = cargos.ObserveEveryValueChanged(_ => _.Count)
                .Select(_ => { return GetCargoWeight(); }).ToReadOnlyReactiveProperty().AddTo(this);

            MaxCargo = model.level.Select(_ => GetMaxCargo()).ToReadOnlyReactiveProperty().AddTo(this);


            CurrentCargoCount = cargos.ObsSomeChanged()
                .Select(_ => { return cargos.Count(x => x.model.weight.Value > 0); }).ToReadOnlyReactiveProperty()
                .AddTo(this);

            trucks.ObsSomeChanged()
                .Subscribe(trs =>
                {
                    completeCargoModels.Clear();

                    for (int i = 0; i < trs.Count; i++)
                    {
                        for (int j = 0; j < trucks[i].model.completeCargoModels.Count; j++)
                        {
                            completeCargoModels.Add(trucks[i].model.completeCargoModels[j]);
                        }
                    }
                }).AddTo(this);


            model.productTime.Subscribe(time =>
            {
                disposableProductTime.Clear();

                if (model.state.Value == CityModel.State.Craft
                    || model.state.Value == CityModel.State.Upgrade)
                {
                    UnirxExtension.DateTimer(time).Subscribe(_ => CraftComplete())
                        .AddTo(disposableProductTime);
                }
            }).AddTo(this);

            if (IsMega())
            {
                partData = Datas.partsProduction.ToArray().FirstOrDefault(x => x.city == data.id);
                productIndex = Datas.partsProduction.ToArray().ToList().IndexOf(partData);
                partIndex = RewardModel.GetIndex(RewardData.eType.parts, partData.output);
                matIndex = RewardModel.GetIndex(RewardData.eType.material, partData.input);
            }
        }

        public bool IsOpenRoad()
        {
            foreach (var road in roads)
            {
                if (road.truck.Value != null)
                {
                    return true;
                }
            }

            return false;
        }

        public void SetTrucks(List<Truck> trucks)
        {
            this.trucks.Clear();
            this.trucks.AddRange(trucks);
        }

        public void AddTruck(Truck truck)
        {
            if (!trucks.Contains(truck))
            {
                trucks.Add(truck);
            }
        }


        public void RemoveTruck(Truck truck)
        {
            trucks.Remove(truck);
        }

        public void SetEditState(UICity.StateEnum state)
        {
            GetComponent<MeshRenderer>().enabled = state != UICity.StateEnum.Edit;

            ui.SetState(state);

            if (state == UICity.StateEnum.Edit)
            {
                ui.SetEditState(UICity.EditStateEnum.normal);
            }
        }


        public void SetDepartSelect(bool isSelect, bool changeLayer = true, Color color = default(Color))
        {
//        if (data.max_lv == MAX_LEVEL)
//        {
//            aniSelect.transform.localScale = new Vector3(1.8f, 0, 1.8f);
//        }
//        else
//        {
//            aniSelect.transform.localScale = new Vector3(1, 0, 1);
//        }

            if (isSelect)
            {
                if (changeLayer)
                {
                    Utilities.ChangeLayers(gameObject, "3D on Gray");
                }
            }
            else
            {
                Utilities.ChangeLayers(gameObject, "Default");
                ui.SetDepartUndo(false);
                ui.SetDepartFocus(false);
            }
        }

        public bool HasClosedRoad(Truck tr)
        {
            foreach (var road in roads)
            {
                if (road.truck.Value != tr)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasRoad(Truck tr = null)
        {
            foreach (var road in roads)
            {
                if (road.truck.Value == tr)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsOpen()
        {
            foreach (var road in roads)
            {
                if (road.model.isOpen.Value)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasTruck(Truck tr)
        {
            foreach (var truck in trucks)
            {
                if (truck == tr)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsMega()
        {
            return data.mega;
        }


        #region cargo

        public void AddCargo(Cargo cargo)
        {
            if (cargo != null)
            {
                cargos.Add(cargo);
            }
        }

        public void RemoveCargo(Cargo cargo)
        {
            cargos.Remove(cargo);
        }

        public void RefreshAllCargo()
        {
            cargos.Clear();

            int cargoCount = GetMaxCargo();

            for (int i = 0; i < cargoCount; i++)
            {
                AddCargo(MakeCargo());
            }
        }

        public void RefreshNotContractCargo()
        {
            List<Cargo> contractCargos = cargos.ToList();

            contractCargos.RemoveAll(x => !x.model.isContract.Value);
            cargos.Clear();
            cargos.AddRange(contractCargos);

            int cargoCount = GetMaxCargo();

            for (int i = cargos.Count; i < cargoCount; i++)
            {
                AddCargo(MakeCargo());
            }
        }


        public void RefreshEmptyCargo()
        {
            int maxCount = GetMaxCargo();
            int currentCargoCount = cargos.Count;

            for (int i = 0; i < maxCount - currentCargoCount; i++)
            {
                AddCargo(MakeCargo());
            }
        }


        public Cargo MakeCargo(Cargo cargo = null)
        {
            try
            {
                CargoData cargoData = null;
                List<Truck> addTrucks = new List<Truck>();

                foreach (var road in roads)
                {
                    if (road.truck.Value != null && !addTrucks.Contains(road.truck.Value))
                    {
                        addTrucks.Add(road.truck.Value);
                    }
                }

                List<City> linkedCities = new List<City>();

                foreach (var truck in addTrucks)
                {
//                    List<Road> linkedRoads = GameManager.Instance.roads.FindAll(x => x.truck.Value == truck);
                    List<Road> linkedRoads =
                        GameManager.Instance.roads.Where(x => x.truck.Value == truck).ToList();

                    foreach (var road in linkedRoads)
                    {
                        if (road.to != null && road.to != this && !linkedCities.Contains(road.to))
                        {
                            linkedCities.Add(road.to);
                        }

                        if (road.from != null && road.from != this && !linkedCities.Contains(road.from))
                        {
                            linkedCities.Add(road.from);
                        }
                    }
                }

//            Debug.Log("linkedCity =========================================");
//            foreach (var linkedCity in linkedCities)
//            {
//                Debug.Log(linkedCity.name);    
//            }
//            Debug.Log("=========================================");


                if (NewDailyMissionManager.Instance.model.hasMission.Value)
                {
//                    float not_connect_rate = NewDailyMissionManager.Instance.city.Value.IsOpen()
//                        ? 1f
//                        : Datas.conceptMissionSub[0].not_connect_rate_mag;

//                    foreach (var quest in NewDailyMissionManager.Instance.model.model.Value.listQuestModel)
                    {
                        QuestModel quest = NewDailyMissionManager.Instance.model.questModel.Value;

                        if (!quest.IsSuccess())
                        {
                            if (quest.qid.Value == QuestData.eType.to_city_cargo_id
                                && NewDailyMissionManager.Instance.city.Value != this
                                && Utilities.RandomRange(0, 100) < Datas.newDailyMission[0].cago_rate)
                            {
                                cargoData = Datas.cargoData.ToArray().FirstOrDefault(x => x.id == quest.fid.Value);

                                return Cargo.MakeWithData(this, NewDailyMissionManager.Instance.city.Value, cargoData,
                                    cargo,
                                    !linkedCities.Contains(NewDailyMissionManager.Instance.city.Value));
                            }

//                        if (quest.qid.Value == QuestData.eType.cargo_id
//                            && Utilities.RandomRange(0, 100) < Datas.conceptMissionSub[0].to_city_cago_rate)
//                        {
//                            cargoData = Datas.cargoData.ToArray().FirstOrDefault(x => x.id == quest.fid.Value);
//                        }
//
                            if (quest.qid.Value == QuestData.eType.from_city_cargo_id
                                && NewDailyMissionManager.Instance.city.Value == this
                                && Utilities.RandomRange(0, 100) < Datas.newDailyMission[1].cago_rate)
                            {
                                cargoData = Datas.cargoData.ToArray().FirstOrDefault(x => x.id == quest.fid.Value);
                            }
                        }
                    }
                }

                int rand = 0;
                int totalRate = Datas.cargoData[0].cargo_on_route_rate + Datas.cargoData[0].cargo_plus1_route_rate +
                                Datas.cargoData[0].cargo_remainder_route_rate;
                int cityRate = Utilities.RandomRange(0, totalRate);

                // linked city
                if (linkedCities.Count > 0 && cityRate < Datas.cargoData[0].cargo_on_route_rate)
                {
                    rand = UnityEngine.Random.Range(0, linkedCities.Count);
                    //Debug.Log("linked city @@@@@@@@@@@@@");
                    return Cargo.MakeWithData(this, linkedCities[rand], cargoData, cargo);
                }

                // next stop city
                if (cityRate >= Datas.cargoData[0].cargo_on_route_rate
                    && cityRate < Datas.cargoData[0].cargo_on_route_rate + Datas.cargoData[0].cargo_plus1_route_rate)
                {
                    List<City> findCities = new List<City>();
                    findCities.Add(this);
                    findCities.AddRange(linkedCities);

                    List<City> nextStopCities = new List<City>();

                    foreach (var city in findCities)
                    {
                        foreach (var road in city.roads)
                        {
                            if (road.truck.Value == null)
                            {
                                if (road.to != city && road.to != this && !nextStopCities.Contains(road.to))
                                {
                                    nextStopCities.Add(road.to);
                                }
                                else if (road.from != city && road.from != this && !nextStopCities.Contains(road.from))
                                {
                                    nextStopCities.Add(road.from);
                                }
                            }
                        }
                    }

//                    int area = UserDataManager.Instance.data.cloudOpen.Value * 100 + 100;
                    nextStopCities = nextStopCities.FindAll(x => x.HasCloudOpen());

                    if (nextStopCities.Count > 0)
                    {
                        rand = Random.Range(0, nextStopCities.Count);
                        //Debug.Log("nextStopCities @@@@@@@@@@@@@");
                        return Cargo.MakeWithData(this, nextStopCities[rand], cargoData, cargo, true);
                    }

//                Debug.LogAssertion("MakeCargo : nextStopCities.Count = 0,  " + name);
                }

                // opened random city
                List<City> opendCities = GameManager.Instance.cities.FindAll(x => x.IsOpenRoad() && x != this);
                rand = UnityEngine.Random.Range(0, opendCities.Count);

                if (opendCities.Count > 0)
                {
//                Debug.Log("opendCities @@@@@@@@@@@@@");
                    return Cargo.MakeWithData(this, opendCities[rand], cargoData, cargo,
                        !linkedCities.Contains(opendCities[rand]));
                }

                Debug.LogAssertion("MakeCargo : opendCities.Count = 0");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }


            return null;
        }


        public int GetCargoWeight()
        {
            int count = cargos.ToList().Sum(x => x.model.weight.Value);
            return count;
        }

        public int GetMaxCargo()
        {
            return Datas.joblistExpansion[model.level.Value].max_numb;
        }

//    public int GetRandomCargoLV(int lv)
//    {
//        int rand = Utilities.RandomRange(0, 100);
//        int[] arrCargoLv = Datas.citySpec.ToArray().FirstOrDefault(x => x.lv == lv).cargo_lv;
//        
//        for (int i = 0; i < arrCargoLv.Length; i++)
//        {
//            if (arrCargoLv[i] > 0 && rand <= arrCargoLv[i])
//            {
//                return i + 1;
//            }
//
//            if (i < arrCargoLv.Length - 1)
//            {
//                arrCargoLv[i + 1] += arrCargoLv[i];    
//            }
//        }
//
//        return arrCargoLv.ToArray().Where(x => x > 0).OrderBy(x => x).First() + 1;
//    }


//    public int GetCargoLV(int lv)
//    {
//        int[] arrCargoLv = Datas.citySpec.ToArray().FirstOrDefault(x => x.lv == lv).cargo_lv;
//        
//        for (int i = arrCargoLv.Length - 1; i >= 0; i--)
//        {
//            if (arrCargoLv[i] > 0)
//            {
//                return i + 1;
//            }
//        }
//
//        return 1;
//    }

//    public int GetCargoQttMag()
//    {
//        return Datas.citySpec[model.level.Value - 1].cargo_qtt_mag;
//    }


        public void Upgrade()
        {
            model.level.Value++;
            UserDataManager.Instance.data.cityUpgradeCount.Value++;
            RefreshNotContractCargo();
            UserDataManager.Instance.SaveData();
        }

        public List<CargoCellData> GetCargoCellList()
        {
            List<CargoCellData> _cargoCellDatas = new List<CargoCellData>();

            var sortCargoes = cargos.ToList()
                .OrderBy(x => x.model.weight.Value == 0)
                .ThenBy(x => x.model.distance.Value);

//        bool hasMoreJob = false;

            for (int i = 0; i < sortCargoes.Count(); i++)
            {
                CargoCellData cell = new CargoCellData();
                cell.cargo = sortCargoes.ElementAt(i);

                if (cell.cargo.model.weight.Value == 0)
                {
                    cell.type = CargoCellData.CargoCellType.SLOT;
                    cell.slotIndex = i;
                }
                else
                {
                    cell.type = CargoCellData.CargoCellType.CARGO;
                }

                _cargoCellDatas.Add(cell);
            }

//        foreach (var cargo in sortCargoes)
//        {
//            CargoCellData cell = new CargoCellData();
//            cell.cargo = cargo;
//
//            if (cargo.model.weight.Value == 0)
//            {
//                cell.type = CargoCellData.CargoCellType.SLOT;
//                cell.slotIndex = 1;
//            }
//            else
//            {
//                cell.type = CargoCellData.CargoCellType.CARGO;
//            }
//
//            _cargoCellDatas.Add(cell); 
//        }

            if (model.level.Value < Datas.joblistExpansion.Length - 1)
            {
                CargoCellData cell = new CargoCellData();
                cell.cargo = Cargo.GetEmptySlot();
                cell.type = CargoCellData.CargoCellType.MORE_JOB;
                _cargoCellDatas.Add(cell);

                _cargoCellDatas = _cargoCellDatas
                    .OrderBy(x => x.cargo.model.weight.Value == 0)
                    .ThenBy(x => x.cargo.model.distance.Value)
                    .ThenByDescending(x => x.type == CargoCellData.CargoCellType.MORE_JOB)
                    .ToList();
            }

            return _cargoCellDatas;
        }

        #endregion


        public void ResetRewardData(Truck tr = null)
        {
            if (tr == null)
            {
                completeCargoModels.Clear();

                foreach (var truck in trucks)
                {
                    truck.completeCargos.Clear();
                }
            }
            else
            {
                for (int i = 0; i < tr.model.completeCargoModels.Count; i++)
                {
                    completeCargoModels.Remove(trucks[i].model.completeCargoModels[i]);
                }
            }

            UserDataManager.Instance.SaveData();
        }

        public int GetCloudArea()
        {
            return data.id / 100;
        }

        public bool HasCloudOpen()
        {
            if (GetCloudArea() == 1)
            {
                return true;
            }

            return UserDataManager.Instance.data.cloudOpen[GetCloudArea() - 2].Value;
        }

        void CraftComplete()
        {
            if (model.state.Value == CityModel.State.Craft)
            {
                model.state.Value = CityModel.State.Craft_Collect;
            }
            else if (model.state.Value == CityModel.State.Upgrade)
            {
                model.state.Value = CityModel.State.Upgrade_Complete;
            }

            PartsProduction partData = Datas.partsProduction.ToArray().FirstOrDefault(x => x.city == data.id);
            int partIndex = RewardModel.GetIndex(RewardData.eType.parts, partData.output);
            UserDataManager.Instance.partsNoti[partIndex].Value = true;
            UserDataManager.Instance.SaveData();
        }

        public bool CanCraft()
        {
            return UserDataManager.Instance.data.cityMaterials[matIndex].Value >=
                   partData.material_count[model.productLevel.Value]
                   && UserDataManager.Instance.data.gold.Value >=
                   partData.pd_gold[model.productLevel.Value];
        }

        public bool CraftStart()
        {
            if (CanCraft())
            {
                UserDataManager.Instance.data.cityMaterials[matIndex].Value -=
                    partData.material_count[model.productLevel.Value];
                UserDataManager.Instance.data.gold.Value -= partData.pd_gold[model.productLevel.Value];

                model.state.Value = CityModel.State.Craft;
                model.productTime.Value =
                    DateTime.Now.AddSeconds(partData.pd_time[model.productLevel.Value]);

                if (LunarConsoleVariables.isCraft)
                {
                    model.productTime.Value = DateTime.Now.AddSeconds(10);
                }

                FBAnalytics.FBAnalytics.LogPartsCraftEvent(UserDataManager.Instance.data.lv.Value,
                    model.productLevel.Value + 1,
                    data.name);
                UserDataManager.Instance.SaveData();
                return true;
            }

            return false;
        }

        public void CraftCollect(bool hasEffect = false)
        {
            if (model.state.Value == CityModel.State.Craft_Collect)
            {
                AudioManager.Instance.PlaySound("sfx_resource_get");
                model.state.Value = CityModel.State.Wait;
                MissionManager.Instance.AddValue(QuestData.eType.produce_parts,
                    partData.parts_count[model.productLevel.Value]);
                UserDataManager.Instance.partsNoti[productIndex].Value = false;

                if (hasEffect)
                {
                    UIRewardManager.Instance.Show(
                        new RewardModel(RewardData.eType.parts,
                            partData.parts_count[model.productLevel.Value],
                            partIndex), transform.position);
                }
                else
                {
                    UserDataManager.Instance.data.truckParts[partIndex].Value +=
                        partData.parts_count[model.productLevel.Value];
                }

                UserDataManager.Instance.SaveData();
            }
        }

        public void UpgradeComplete()
        {
            if (model.state.Value == CityModel.State.Upgrade_Complete)
            {
                AudioManager.Instance.PlaySound("sfx_level");
                model.productLevel.Value++;
                model.state.Value = CityModel.State.Wait;
                MissionManager.Instance.AddValue(QuestData.eType.production_upgrade, 1);
                UserDataManager.Instance.partsNoti[productIndex].Value = false;
                UserDataManager.Instance.SaveData();
            }
        }
    }
}