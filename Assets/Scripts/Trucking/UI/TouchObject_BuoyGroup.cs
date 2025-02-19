using System;
using DatasTypes;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.Mission;
using UniRx;
using Random = UnityEngine.Random;

namespace Trucking.UI
{
    public class TouchObject_BuoyGroup : MonoSingleton<TouchObject_BuoyGroup>
    {
        public ReactiveProperty<bool> isShow = new ReactiveProperty<bool>();

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private CompositeDisposable _disposableIntaval = new CompositeDisposable();
        private bool isFirst = true;
        private TouchObject_Buoy[] arrBuoy;
        private RewardModel rewardModel;

        private void Start()
        {
            _compositeDisposable.Clear();
            arrBuoy = GetComponentsInChildren<TouchObject_Buoy>();

            for (int i = 0; i < arrBuoy.Length; i++)
            {
                arrBuoy[i].gameObject.SetActive(false);
            }

            isShow.Subscribe(show =>
            {
                if (show)
                {
                    MakeRewardModel();
                    int rand = Utilities.RandomRange(0, arrBuoy.Length - 1);
                    arrBuoy[rand].Show();
                }
            }).AddTo(this);

            Observable.CombineLatest(
                    isShow,
                    UserDataManager.Instance.data.hasTutorial,
                    (show, isTutorial) =>
                    {
                        if (!isTutorial && !show)
                        {
                            return true;
                        }

                        return false;
                    })
                .Subscribe(value =>
                {
                    if (value)
                    {
                        _disposableIntaval.Clear();

                        if (isFirst)
                        {
                            isShow.Value = true;
                            isFirst = false;
                        }
                        else
                        {
                            int intavalSec = Random.Range(Datas.touchObjectData.buoy.delay_min,
                                Datas.touchObjectData.buoy.delay_max);

                            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(intavalSec)).Subscribe(_ =>
                            {
                                if (!isShow.Value)
                                {
                                    isShow.Value = true;
                                }
                            }).AddTo(_disposableIntaval);
                        }
                    }
                }).AddTo(_compositeDisposable);
        }

        public void Touch(TouchObject_Buoy buoy)
        {
            isShow.Value = false;

            RewardModel rewardModel = MakeRewardModel();
            FBAnalytics.FBAnalytics.LogClickObjectEvent(UserDataManager.Instance.data.lv.Value, "Buoy",
                rewardModel.type.Value.ToString());
            UIRewardManager.Instance.Show(rewardModel, buoy.transform.position);
            MissionManager.Instance.AddValue(QuestData.eType.map_object, 1);
        }

        public RewardModel MakeRewardModel()
        {
            RewardModel rewardModel = new RewardModel(Datas.touchObjectData.buoy.reward_type[0].type,
                MissionManager.Instance.CalcLevelMagReward(
                    Datas.touchObjectData.buoy.reward_type[0].type,
                    Datas.touchObjectData.buoy.reward_count[0] * 0.2f));

            return rewardModel;
        }
    }
}