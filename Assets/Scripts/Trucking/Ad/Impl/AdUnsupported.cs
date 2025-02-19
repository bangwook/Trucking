/*
 * Created on 2019년 3월 13일 수요일
 * Creator jhlee5@cookapps.com
 * Copyright (c) 2019 CookAppsPlay
 */

using System;
using UniRx;

namespace Trucking.Ad
{
    public sealed class AdUnsupported : IAdNetwork
    {
        private readonly ReactiveProperty<bool> _isLoadedReward = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> IsLoadedReward => _isLoadedReward;

        public void Init(Action open, Action close)
        {
            // none
        }

        public void ShowRewardAd(AdUnit adUnit, Action<AdResult> rewardCallBack)
        {
            Observable.NextFrame()
                .Subscribe(_ =>
                {
                    rewardCallBack?.Invoke(AdResult.NoFill);
                });
        }

        public void Setting(object[] setting)
        {
            // none
        }
    }
}