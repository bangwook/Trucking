/*
 * Created on 2019년 3월 13일 수요일
 * Creator jhlee5@cookapps.com
 * Copyright (c) 2019 CookAppsPlay
 */

using System;
using Trucking.Common;
using Trucking.Manager;
using UniRx;

namespace Trucking.Ad
{
    public sealed class AdEditor : IAdNetwork
    {
        private Action _open, _close;

//        public IReadOnlyReactiveProperty<bool> IsLoadedReward { get; }
        public ReactiveProperty<bool> IsLoadedReward
        {
            get
            {
                if (LunarConsoleVariables.isADLoading)
                {
                    return new ReactiveProperty<bool>(false);
                }

                return new ReactiveProperty<bool>(true);
            }
        }

        public void Init(Action open, Action close)
        {
            _open = open;
            _close = close;
        }

        public void ShowRewardAd(AdUnit adUnit, Action<AdResult> rewardCallBack)
        {
            _open?.Invoke();
            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(0.2f))
                .Subscribe(_ =>
                {
                    _close?.Invoke();

                    if (LunarConsoleVariables.isADLoading)
                    {
                        rewardCallBack?.Invoke(AdResult.NoFill);
                    }
                    else
                    {
                        rewardCallBack?.Invoke(AdResult.Success);
                    }
                });
        }

        public void Setting(object[] setting)
        {
            // none
        }
    }
}