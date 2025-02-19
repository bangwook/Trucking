using System;
using UniRx;
using System.Collections.Generic;
using System.Linq;
using DatasTypes;
using Newtonsoft.Json;
using Trucking.Common;
using Trucking.Model;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Trucking.UI.Mission
{
    public class QuestModel
    {
        public ReactiveProperty<long> count = new ReactiveProperty<long>();
        public ReactiveProperty<long> max = new ReactiveProperty<long>();
        public ReactiveProperty<int> fid = new ReactiveProperty<int>();
        public ReactiveProperty<QuestData.eType> qid = new ReactiveProperty<QuestData.eType>();

        [JsonIgnore] public IObservable<QuestModel> ObsChanged => count.DistinctUntilChanged().Select(_ => this);

        public static List<QuestModel> Make(LevelMissionData data)
        {
            List<QuestModel> questModels = new List<QuestModel>();

            for (int j = 0; j < data.qid.Length; j++)
            {
                QuestModel questModel = new QuestModel();
                questModel.qid.Value = data.qid[j].type;
                float countMag = MissionManager.Instance.GetLevelMagCount(questModel.qid.Value);
                questModel.max.Value = (long) Math.Ceiling(data.count[j] * countMag);

                if (data.fid.Length > 0)
                {
                    questModel.fid.Value = data.fid[j];

                    if (data.fid[j] == -1)
                    {
                        FactorTypeData.eType fType = QuestManager.GetFactorType(data.qid[j].type);
                        SetRandomFid(questModel, fType);
                    }
                }

                questModels.Add(questModel);
            }

            return questModels;
        }

//        public static List<QuestModel> Make(ConceptMissionData data, float lvMagCount, City city = null)
//        {
//            List<QuestModel> questModels = new List<QuestModel>();
//            LevelMag lvMag = UserDataManager.Instance.GetLevelMag();
//
//            for (int j = 0; j < data.qid.Length; j++)
//            {
//                QuestModel questModel = new QuestModel();
//                questModel.qid.Value = data.qid[j].type;
//                //questModel.max.Value = (long)(Math.Ceiling(data.count[j] * lvMagCount));
//                float countMag = MissionManager.Instance.GetLevelMagCount(questModel.qid.Value);
//                questModel.max.Value = (long)Math.Ceiling(data.count[j] * countMag * lvMagCount);
//                
//                if (data.fid.Length > 0)
//                {
//                    questModel.fid.Value = data.fid[j];
//
//                    if (data.fid[j] == -1)
//                    {
//                        FactorTypeData.eType fType = QuestManager.GetFactorType(data.qid[j].type);
//                        
//                        do
//                        {
//                            SetRandomFid(questModel, fType);
//                        } while (questModels.Find(x => x.fid.Value == questModel.fid.Value) != null);
//                        
//                        if (fType == FactorTypeData.eType.none
//                            && city != null
//                            && (questModel.qid.Value == QuestData.eType.to_city_cargo
//                                || questModel.qid.Value == QuestData.eType.from_city_cargo))
//                        {
//                            questModel.fid.Value = city.data.id;
//                        }
//                    }
//                }
//                
//                questModels.Add(questModel);
//            }
//
//            return questModels;
//        }

//        public static QuestModel Make(DailyMissionData data)
//        {
//            QuestModel questModel = new QuestModel();
//            questModel.qid.Value = data.qid.type;
//
//            long totalCount = 0;
//            
//            for (int i = 0; i < data.count.Length; i++)
//            {
//                totalCount += (long)(data.count[i] * MissionManager.Instance.GetLevelMagCount(questModel.qid.Value));
//            }
//            
//            questModel.max.Value = totalCount;
//
//            return questModel;
//        }

        public static QuestModel Make(NewDailyMission data)
        {
            QuestModel questModel = new QuestModel();
            questModel.qid.Value = data.qid.type;
            questModel.max.Value = (long) (data.count * MissionManager.Instance.GetLevelMagCount(questModel.qid.Value));

            if (data.fid == -1)
            {
                FactorTypeData.eType fType = QuestManager.GetFactorType(data.qid.type);
                SetRandomFid(questModel, fType);
            }

            return questModel;
        }


        public static QuestModel Make(AchievementData data, int step)
        {
            QuestModel questModel = new QuestModel
            {
                qid = {Value = data.qid[0].type},
                max = {Value = data.count[step]},
                fid = {Value = data.fid[0]}
            };

            return questModel;
        }

        public static QuestModel Make(GuideQuestData data, long count = 0)
        {
            QuestModel questModel = new QuestModel();
            questModel.qid.Value = data.qid.type;
            questModel.max.Value = data.count;
            questModel.count.Value = count;

            return questModel;
        }

        public static void SetRandomFid(QuestModel questModel, FactorTypeData.eType fType)
        {
            switch (fType)
            {
                case FactorTypeData.eType.reward_id:
                    break;
                case FactorTypeData.eType.cargo_id:
                    questModel.fid.Value = Random.Range(1, Datas.cargoData.ToArray().Last().id + 1);
                    break;
                case FactorTypeData.eType.city_id:
                    List<City> openedCities = GameManager.Instance.cities.FindAll(x => x.IsOpen());

                    Debug.Assert(openedCities.Count > 0);

                    if (openedCities.Count > 0)
                    {
                        City randomCity = openedCities[Random.Range(0, openedCities.Count)];
                        questModel.fid.Value = randomCity.data.id;
                    }

                    break;
                case FactorTypeData.eType.truck_id:
                    questModel.fid.Value = Random.Range(0, Datas.truckData.Length);
                    break;
            }
        }


        public static string GetFactorText(FactorTypeData.eType type, int fid)
        {
            switch (type)
            {
                case FactorTypeData.eType.reward_id:
                    return Datas.rewardData.ToArray().FirstOrDefault(x => x._value == fid)?.id;
                case FactorTypeData.eType.cargo_id:
                    return $"{Datas.cargoData.ToArray().FirstOrDefault(x => x.id == fid)?.name}";
                case FactorTypeData.eType.city_id:
                    return $"{Datas.cityData.ToArray().FirstOrDefault(x => x.id == fid)?.name}";
                case FactorTypeData.eType.truck_id:
                    return
                        $"{Utilities.GetStringByData(Datas.truckData.ToArray().FirstOrDefault(x => x.id == fid).name_id)}";
            }

            return "???";
        }

        public string GetDescription(City city = null)
        {
            QuestData questData = QuestManager.GetQuest(qid.Value);
            string str = Utilities.GetStringByData(questData.description);
            string strFactor = GetFactorText(questData.factor_type.type, fid.Value);

            switch (questData.type)
            {
                case QuestData.eType.to_city_reward_id:
                case QuestData.eType.from_city_reward_id:
                    return string.Format(str, city.data.name, strFactor, Utilities.GetNumberKKK(max.Value));
                case QuestData.eType.to_city_cargo:
                case QuestData.eType.from_city_cargo:
                    return string.Format(str, city.data.name, Utilities.GetNumberKKK(max.Value));
                case QuestData.eType.to_city_cargo_id:
                case QuestData.eType.from_city_cargo_id:
                    return string.Format(str, city.data.name, Utilities.GetNumberKKK(max.Value));
            }

            switch (questData.factor_type.type)
            {
                case FactorTypeData.eType.none:
                case FactorTypeData.eType.cargo_id:
                    return string.Format(str, Utilities.GetNumberKKK(max.Value));
                case FactorTypeData.eType.reward_id:
                case FactorTypeData.eType.truck_id:
                case FactorTypeData.eType.city_id:
                    return string.Format(str, strFactor, Utilities.GetNumberKKK(max.Value));
            }

            return null;
        }


        public void AddValue(QuestData.eType eType, long value)
        {
            if (qid.Value == eType
                && fid.Value == (int) FactorTypeData.eType.none)
            {
                count.Value += value;
            }
        }

        public void SetValue(QuestData.eType eType, long value)
        {
            if (qid.Value == eType
                && fid.Value == (int) FactorTypeData.eType.none)
            {
                count.Value = value;
            }
        }

        public void AddValue(QuestData.eType eType, RewardData.eType type, long value)
        {
            if (qid.Value == eType
                && fid.Value == (int) type)
            {
                count.Value += value;
//                count.Value = LongClamp(count.Value + value, 0, max.Value);
            }
        }

        public void AddValue(QuestData.eType eType, int cargoId, long value)
        {
            if (qid.Value == eType
                && fid.Value == cargoId)
            {
                count.Value += value;
//                count.Value = LongClamp(count.Value + value, 0, max.Value);
            }
        }

        public void AddValue(QuestData.eType eType, City city, long value)
        {
            if (qid.Value == eType
                && city != null
                && fid.Value == city.data.id)
            {
                count.Value += value;
//                count.Value = LongClamp(count.Value + value, 0, max.Value);
            }
        }

        public bool IsSuccess()
        {
            return count.Value >= max.Value;
        }

        public bool IsMissionCargo(Cargo cargo, City city)
        {
            if (qid.Value == QuestData.eType.to_city_cargo_id
                && fid.Value == cargo.model.id.Value
                && cargo.to.Value == city)
            {
                return true;
            }

            if (qid.Value == QuestData.eType.from_city_cargo_id
                && fid.Value == cargo.model.id.Value
                && cargo.from.Value == city)
            {
                return true;
            }

            if (qid.Value == QuestData.eType.cargo_id
                && fid.Value == cargo.model.id.Value
                && cargo.to.Value == city)
            {
                return true;
            }

            if (qid.Value == QuestData.eType.to_city_cargo
                && cargo.to.Value == city)
            {
                return true;
            }

            if (qid.Value == QuestData.eType.from_city_cargo
                && cargo.from.Value == city)
            {
                return true;
            }

            return false;
        }
    }
}