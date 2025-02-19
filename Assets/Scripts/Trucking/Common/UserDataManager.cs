using UnityEngine;
using System;
using System.Linq;
using DatasTypes;
using Newtonsoft.Json.Utilities;
using Trucking.Manager;
using Trucking.Model;
using Trucking.UI.Mission;
using UniRx;


namespace Trucking.Common
{
    [Serializable]
    public class UserDataManager : Singleton<UserDataManager>, IDisposable
    {
        public enum BoosterType
        {
            XP,
            GOLD,
            SPEED,
            GAS
        }


        public const string SaveKey = "savedata";
        const int AutoSaveMinInterval = 5;

        public UserData data = new UserData();
        public bool isFirst = true;
        public ReactiveCollection<ReactiveProperty<bool>> crateNoti = new ReactiveCollection<ReactiveProperty<bool>>();
        public ReactiveCollection<ReactiveProperty<bool>> pieceNoti = new ReactiveCollection<ReactiveProperty<bool>>();
        public ReactiveCollection<ReactiveProperty<bool>> partsNoti = new ReactiveCollection<ReactiveProperty<bool>>();


        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private CompositeDisposable _disposableDelivery = new CompositeDisposable();

        static UserDataManager()
        {
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnRuntimeInit()
        {
            Instance.Init();
        }

        public void Init()
        {
            data.cash.Value = Datas.baseData[0].start_cg[0];
            data.gold.Value = Datas.baseData[0].start_cg[1];
            data.exp.Value = 0;
            data.lv.Value = 1;
            data.truckColorIndex.Value = 0;

            data.truckData.Clear();
            foreach (var roadModel in data.roadData)
            {
                roadModel.isOpen.Value = false;
                roadModel.truckBirthID.Value = 0;
            }

            data.crate.Clear();

            for (int i = 0; i < 2; i++)
            {
                data.crate.Add(new IntReactiveProperty(0));
            }

            data.boosterShopData.Clear();
            for (int i = 0; i < 4; i++)
            {
                data.boosterShopData.Add(new DateTime());
            }

            data.cityMaterials.Clear();
            for (int i = 0; i < 4; i++)
            {
                data.cityMaterials.Add(new IntReactiveProperty(0));
            }

            data.truckParts.Clear();
            for (int i = 0; i < 4; i++)
            {
                data.truckParts.Add(new IntReactiveProperty(0));
            }

            data.truckPieces.Clear();
            for (int i = 0; i < Datas.truckData.Length; i++)
            {
                data.truckPieces.Add(new IntReactiveProperty(0));
                data.truckPiecesUnlock.Add(new BoolReactiveProperty(false));
            }

            data.cloudOpen.Clear();
            for (int i = 0; i < 7; i++)
            {
                data.cloudOpen.Add(new BoolReactiveProperty(false));
            }

            data.cloudOpen[0].Value = true;

            Subscribe();
            Debug.Log("UserDataManager Create");
        }

        public void Subscribe()
        {
            _compositeDisposable.Clear();

            var obsInterval = Observable.Interval(TimeSpan.FromMinutes(AutoSaveMinInterval)).AsUnitObservable();
            var obsSignal = MessageBroker.Default.Receive<SaveUserDataLocalSignal>().AsUnitObservable();

            obsInterval.Merge(obsSignal)
                .BatchFrame(0, FrameCountType.EndOfFrame)
                .Skip(1)
                .Subscribe(_ => Save())
                .AddTo(_compositeDisposable);

            crateNoti.Clear();
            for (int i = 0; i < 2; i++)
            {
                crateNoti.Add(new BoolReactiveProperty(false));
                int index = i;
                data.crate[index].Pairwise().Subscribe(x => { crateNoti[index].Value = x.Current > x.Previous; })
                    .AddTo(_compositeDisposable);
            }

            pieceNoti.Clear();
            for (int i = 0; i < Datas.truckData.Length; i++)
            {
                pieceNoti.Add(new BoolReactiveProperty(false));

                int index = i;
                data.truckPieces[index].Subscribe(x =>
                {
                    pieceNoti[index].Value = x >= Datas.truckData[index].pieces;

                    // 조합가능
                    if (x >= Datas.truckData[index].pieces)
                    {
                        Debug.Log(
                            $"Piece Noti : {Utilities.GetStringByData(Datas.truckData[index].name_id)} ({x}/{Datas.truckData[index].pieces})");
                    }

                    if (!data.truckPiecesUnlock[index].Value && x > 0)
                    {
//                        pieceNoti[index].Value = true;
                        data.truckPiecesUnlock[index].Value = true;
                    }
                }).AddTo(_compositeDisposable);
            }


            if (data.cityData.Count > 0)
            {
                partsNoti.Clear();

                for (int i = 0; i < Datas.partsProduction.Length; i++)
                {
                    partsNoti.Add(new BoolReactiveProperty(false));

                    int index = i;
                    int cityIndex = data.cityData.IndexOf(x => x.id.Value == Datas.partsProduction[i].city);

                    data.cityData[cityIndex].state.Pairwise().Subscribe(state =>
                    {
                        if (state.Current == CityModel.State.Wait
                            && state.Previous == CityModel.State.Lock)
                        {
                            partsNoti[index].Value = true;
                        }
                    }).AddTo(_compositeDisposable);
                }
            }
        }

        public bool Load()
        {
            if (!IsFirst())
            {
                string json = PlayerPrefs.GetString(SaveKey);
                Debug.Log("Userdata Load : " + json);

                try
                {
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<UserData>(json);
                }
                catch (Exception e)
                {
                    Debug.Log("load fail !!!! : " + e);
                    return false;
                }

                isFirst = false;
                Subscribe();

                return true;
            }

            return false;
        }

        public bool IsFirst()
        {
            return !PlayerPrefs.HasKey(SaveKey);
        }

        private void Save()
        {
            isFirst = false;
            data.savedTime.Value = DateTime.Now;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
//        Debug.Log("UserDataManager Save : " + json);

            if (data.truckData.Count == 0)
            {
                Debug.LogError("UserDataManager Save Error : " + json);
            }
            else
            {
                PlayerPrefs.SetString(SaveKey, json);
            }
        }

        public void SaveData()
        {
//        Debug.Log("Userdata Save!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            MessageBroker.Default.Publish(new SaveUserDataLocalSignal());
        }

        public void Delete()
        {
            PlayerPrefs.DeleteKey(SaveKey);
            //Reload();
        }

        class SaveUserDataLocalSignal
        {
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }

        public void AddRewardType(RewardData.eType type, long value, int index = 0)
        {
            if (value > 0)
            {
                MissionManager.Instance.AddValue(QuestData.eType.reward_id,
                    type,
                    value);
            }

            switch (type)
            {
                case RewardData.eType.gold:
                    AddGold(value);
                    break;
                case RewardData.eType.cash:
                    AddCash(value);
                    break;
                case RewardData.eType.exp:
                    AddExp(value);
                    break;
                case RewardData.eType.crate:
                    data.crate[index].Value += (int) value;
                    break;
                case RewardData.eType.material:
                    data.cityMaterials[index].Value += (int) value;
                    break;
                case RewardData.eType.parts:
                    data.truckParts[index].Value += (int) value;
                    break;
                case RewardData.eType.truck_pc:
                    data.truckPieces[index].Value += (int) value;
                    break;
                case RewardData.eType.booster:
                    AddBooster(index, (int) value);
                    break;
            }
        }

        public void AddExp(long value)
        {
            if (data.lv.Value < Datas.levelData.Length)
            {
                if (data.exp.Value + value > Datas.levelData[data.lv.Value].request_xp)
                {
                    data.exp.Value = data.exp.Value + value - Datas.levelData[data.lv.Value].request_xp;
                    data.lv.Value++;
                }
                else
                {
                    data.exp.Value += value;
                }
            }
        }

        public long GetNextExp()
        {
            if (data.lv.Value < Datas.levelData.Length)
                return Datas.levelData[data.lv.Value].request_xp;

            return Datas.levelData.ToArray().Last().request_xp;
        }

        public bool CheckExp()
        {
            if (data.lv.Value < Datas.levelData.Length)
            {
                if (data.exp.Value > Datas.levelData[data.lv.Value].request_xp)
                {
                    data.exp.Value -= Datas.levelData[data.lv.Value].request_xp;
                    data.lv.Value++;
                    return true;
                }
            }

            return false;
        }

        public void AddGold(long value)
        {
            data.gold.Value += value;
        }

        public bool UseGold(long value)
        {
            if (data.gold.Value >= value)
            {
                data.gold.Value -= value;
//            UIMain.Instance.AddGold(-value);
                return true;
            }

            return false;
        }

        public void AddCash(long value)
        {
//        UIMain.Instance.AddCash(value);
            data.cash.Value += value;
        }

        public bool UseCash(long value)
        {
            if (data.cash.Value >= value)
            {
                data.cash.Value -= value;
//            UIMain.Instance.AddCash(-value);
                return true;
            }

            return false;
        }

        public int GetBooster(BoosterType type)
        {
            if (data.boosterShopData[(int) type] > DateTime.Now)
            {
                return 100;
            }

            return 0;
        }

        public void AddBooster(int index, int minValue)
        {
            if (data.boosterShopData[index] < DateTime.Now)
            {
                data.boosterShopData[index] = DateTime.Now.AddMinutes(minValue);
            }
            else
            {
                data.boosterShopData[index] = data.boosterShopData[index].AddMinutes(minValue);
            }
        }

        public bool UseMaterial(int index, int value)
        {
            if (data.cityMaterials[index].Value >= value)
            {
                data.cityMaterials[index].Value -= value;

                return true;
            }

            return false;
        }

        public bool UsePart(int index, int value)
        {
            if (data.truckParts[index].Value >= value)
            {
                data.truckParts[index].Value -= value;

                return true;
            }

            return false;
        }

        public bool UsePiece(int index, int value)
        {
            if (data.truckPieces[index].Value >= value)
            {
                data.truckPieces[index].Value -= value;

                return true;
            }

            return false;
        }

        public bool UseCrate(int index, int value)
        {
            if (data.crate[Convert.ToInt32(index)].Value >= value)
            {
                data.crate[Convert.ToInt32(index)].Value -= value;
                return true;
            }

            return false;
        }

        public bool UseResource(long goldValue, long cashValue)
        {
            if (data.gold.Value >= goldValue
                && data.cash.Value >= cashValue)
            {
                UseGold(goldValue);
                UseCash(cashValue);
                return true;
            }

            return false;
        }

        public int GetNextTruckColor()
        {
            data.truckColorIndex.Value += 1;

            if (data.truckColorIndex.Value <= 0)
            {
                data.truckColorIndex.Value = 1;
            }
            else if (data.truckColorIndex.Value > 10)
            {
                data.truckColorIndex.Value = 1;
            }

            return data.truckColorIndex.Value;
        }

        public LevelMag GetLevelMag(int lv = 0)
        {
            if (lv == 0)
            {
                lv = data.lv.Value;
            }

            return Datas.levelMag.ToArray().Where(x => x.lv <= lv)
                .OrderByDescending(x => x.lv)
                .FirstOrDefault();
        }

        public class UserData
        {
            public ReactiveProperty<int> lv = new ReactiveProperty<int>(1);
            public ReactiveProperty<long> gold = new ReactiveProperty<long>(0);
            public ReactiveProperty<long> cash = new ReactiveProperty<long>(0);
            public ReactiveProperty<long> exp = new ReactiveProperty<long>(0);
            public ReactiveProperty<bool> gdpr = new ReactiveProperty<bool>();

            public ReactiveCollection<ReactiveProperty<bool>> cloudOpen =
                new ReactiveCollection<ReactiveProperty<bool>>();

            public ReactiveCollection<ReactiveProperty<int>> crate = new ReactiveCollection<ReactiveProperty<int>>();

            public ReactiveCollection<ReactiveProperty<int>> cityMaterials =
                new ReactiveCollection<ReactiveProperty<int>>();

            public ReactiveCollection<ReactiveProperty<int>> truckParts =
                new ReactiveCollection<ReactiveProperty<int>>();

            public ReactiveCollection<ReactiveProperty<int>> truckPieces =
                new ReactiveCollection<ReactiveProperty<int>>();

            public ReactiveCollection<ReactiveProperty<bool>> truckPiecesUnlock =
                new ReactiveCollection<ReactiveProperty<bool>>();

            public ReactiveProperty<DateTime> savedTime = new ReactiveProperty<DateTime>(DateTime.Now);

            public ReactiveProperty<bool> levelUpReward = new ReactiveProperty<bool>();

            public ReactiveProperty<bool> settingSound = new ReactiveProperty<bool>(true);
            public ReactiveProperty<bool> settingMusic = new ReactiveProperty<bool>(true);

            public ReactiveCollection<CityModel> cityData = new ReactiveCollection<CityModel>();
            public ReactiveCollection<RoadModel> roadData = new ReactiveCollection<RoadModel>();
            public ReactiveCollection<TruckModel> truckData = new ReactiveCollection<TruckModel>();
            public ReactiveCollection<DateTime> boosterShopData = new ReactiveCollection<DateTime>();
            public ReactiveProperty<LevelMissionModel> levelMissionData = new ReactiveProperty<LevelMissionModel>();

            public ReactiveProperty<NewDailyMissionModel> newDailyMissionData =
                new ReactiveProperty<NewDailyMissionModel>();

            public ReactiveProperty<AchievementModel> achievementData = new ReactiveProperty<AchievementModel>();
            public ReactiveProperty<GuideQuestModel> guideQuestData = new ReactiveProperty<GuideQuestModel>();

            public ReactiveProperty<DeliveryServiceModel> deliveryData = new ReactiveProperty<DeliveryServiceModel>();
            public ReactiveProperty<FreeCashModel> freeCashData = new ReactiveProperty<FreeCashModel>();

            public ReactiveProperty<OperationManagerModel>
                operationData = new ReactiveProperty<OperationManagerModel>();

            public ReactiveProperty<TouchObject_PlaneModel> planeObjectData =
                new ReactiveProperty<TouchObject_PlaneModel>();

            public ReactiveProperty<int> truckUpgradeCount = new ReactiveProperty<int>();
            public ReactiveProperty<int> cityUpgradeCount = new ReactiveProperty<int>();

            public ReactiveProperty<bool> notiAllTruckArrive = new ReactiveProperty<bool>(true);
            public ReactiveProperty<bool> notiEachTruckArrive = new ReactiveProperty<bool>(false);
            public ReactiveProperty<bool> notiAllRefuelingComplete = new ReactiveProperty<bool>(true);
            public ReactiveProperty<bool> notiEachRefuelingComplete = new ReactiveProperty<bool>(false);
            public ReactiveProperty<bool> notiEventStart = new ReactiveProperty<bool>(true);
            public ReactiveProperty<bool> notiEventDelivery = new ReactiveProperty<bool>(true);
            public ReactiveProperty<bool> notiPartProduction = new ReactiveProperty<bool>(true);
            public ReactiveProperty<bool> notiProductionUpgrade = new ReactiveProperty<bool>(true);

            public ReactiveProperty<int> optionLanguage = new ReactiveProperty<int>();
            public ReactiveProperty<int> truckColorIndex = new ReactiveProperty<int>();
            public ReactiveProperty<bool> hasTutorial = new ReactiveProperty<bool>(true);
            public ReactiveProperty<bool> hasPurchase = new ReactiveProperty<bool>();
            public ReactiveProperty<int> maxFps = new ReactiveProperty<int>(0);
        }
    }
}