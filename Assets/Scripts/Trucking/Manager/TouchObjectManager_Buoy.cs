using System;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.Mission;
using UniRx;
using Random = UnityEngine.Random;

namespace Trucking.Manager
{
    public class TouchObjectManager_Buoy : Singleton<TouchObjectManager_Buoy>
    {
        public ReactiveProperty<bool> isShow = new ReactiveProperty<bool>();
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private CompositeDisposable _disposableIntaval = new CompositeDisposable();
        private RewardModel rewardModel;
        private bool isFirst = true;

        public void Init()
        {
            _compositeDisposable.Clear();

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
                            rewardModel = MakeRewardModel();
                            isShow.Value = true;
                            isFirst = false;
                        }
                        else
                        {
                            int intavalSec = Random.Range(Datas.touchObjectData.plane.delay_min,
                                Datas.touchObjectData.plane.delay_max);

                            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(intavalSec)).Subscribe(_ =>
                            {
                                if (!isShow.Value)
                                {
                                    rewardModel = MakeRewardModel();
                                    isShow.Value = true;
                                }
                            }).AddTo(_disposableIntaval);
                        }
                    }
                }).AddTo(_compositeDisposable);
        }

        public RewardModel GetReward()
        {
            return rewardModel;
        }
        
        public RewardModel MakeRewardModel()
        {                        
            RewardModel rewardModel = new RewardModel(Datas.touchObjectData.buoy.reward_type[0].type, 
                MissionManager.Instance.CalcLevelMagReward(
                    Datas.touchObjectData.plane.reward_type[0].type,
                    Datas.touchObjectData.plane.reward_count[0] * 0.2f));
            
            return rewardModel;
        }
    }    
}