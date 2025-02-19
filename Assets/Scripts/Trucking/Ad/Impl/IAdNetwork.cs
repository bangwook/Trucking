/*
 * Created on 2019년 3월 12일 화요일
 * Creator jhlee5@cookapps.com
 * Copyright (c) 2019 CookAppsPlay
 */

using System;
using System.Collections.Generic;
using Facebook.Unity;
using GoogleMobileAds.Api;
using UniRx;
using UnityEngine;

namespace Trucking.Ad
{
    public interface IAdNetwork
    {
        void Init(Action open, Action close);
        void ShowRewardAd(AdUnit adUnit, Action<AdResult> rewardCallBack);
        void Setting(object[] param);
        ReactiveProperty<bool> IsLoadedReward { get; }
    }

    public abstract class AdNetwork : IAdNetwork
    {
        private enum eAppEvent
        {
            reward_request_failed,
            reward_request_success,
            reward_start,
            reward_complete,
        }

        private Action _actionOpen;
        private Action _actionClose;
        private Action<AdResult> _actionReward;
        private bool _successReward;

        protected int _delayFailed = 10;
        protected int _delayCompleted = 3;
        private int _retryCount = int.MaxValue;
        private int _retryReward = 0;
        private readonly ReactiveProperty<bool> _isLoadedReward = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> IsLoadedReward => _isLoadedReward;

        protected abstract void Init();
        protected abstract void AdLoadReward();
        protected abstract void AdShowReward(AdUnit adUnit);


        public void Init(Action open, Action close)
        {
            _actionOpen = open;
            _actionClose = close;
            Init();

            Observable.NextFrame()
                .Subscribe(_ => AdLoadReward());
        }

        public void Setting(object[] param)
        {
            if (param != null)
            {
                if (param.Length > 0)
                    _retryCount = Convert.ToInt32(param[0]);
                if (param.Length > 1)
                    _delayFailed = Convert.ToInt32(param[1]);
                if (param.Length > 2)
                    _delayCompleted = Convert.ToInt32(param[2]);

                if (_retryCount == -1)
                    _retryCount = int.MaxValue;
            }
        }

        #region Reward

        public void ShowRewardAd(AdUnit adUnit, Action<AdResult> rewardCallBack)
        {
            _successReward = false;
            _actionReward = rewardCallBack;
            if (IsLoadedReward.Value)
            {
                AdShowReward(adUnit);
            }
            else
            {
                Observable.NextFrame()
                    .Subscribe(_ => { rewardCallBack?.Invoke(AdResult.NoFill); });
            }
        }

        protected void OnOpenReward(string mediation, AdUnit adUnit)
        {
            Debug.Log("AdNetwork OnOpenReward");
            Observable.NextFrame()
                .Subscribe(_ =>
                {
                    _isLoadedReward.Value = false;
                    _actionOpen?.Invoke();
                    LogAppEvent(eAppEvent.reward_start, mediation, adUnit);
                });
        }

        protected void OnCloseReward(string mediation, AdUnit adUnit)
        {
            Debug.Log("AdNetwork OnCloseReward");
            Observable.NextFrame()
                .Subscribe(_ =>
                {
                    Debug.Log("AdNetwork OnCloseReward 1");

                    Debug.Log("AdNetwork OnCloseReward 2");
                    _actionReward?.Invoke(_successReward ? AdResult.Success : AdResult.Cancle);
                    Debug.Log("AdNetwork OnCloseReward 3");
                    _successReward = false;
                    Debug.Log("AdNetwork OnCloseReward 4");
                    _actionReward = null;
                    Debug.Log("AdNetwork OnCloseReward 5");

                    Observable.NextFrame()
                        .Subscribe(__ => _actionClose?.Invoke());
                    Debug.Log("AdNetwork OnCloseReward 6");
                    Observable.Timer(TimeSpan.FromSeconds(_delayCompleted + float.Epsilon),
                            Scheduler.MainThreadIgnoreTimeScale)
                        .Subscribe(__ =>
                        {
                            Debug.Log("AdNetwork OnCloseReward 7");
                            AdLoadReward();
                            Debug.Log("AdNetwork OnCloseReward 8");
                        });
                });
        }

        protected void OnAdFailedToShow(string mediation, AdUnit adUnit, AdErrorEventArgs errorEvent)
        {
            Debug.Log($"AdNetwork OnAdFailedToShow : {mediation}, {adUnit}, {errorEvent.Message}|");
            Observable.NextFrame()
                .Subscribe(_ =>
                {
                    _isLoadedReward.Value = false;
                    _actionReward?.Invoke(AdResult.NoFill);
                    _successReward = false;
                    ReTryLoadReward();
                });
        }

        protected void OnLoadReward(string mediation, string message)
        {
            Debug.Log("AdNetwork OnLoadReward : " + message);
            Observable.NextFrame()
                .Subscribe(_ => { _isLoadedReward.Value = true; });

            LogAppEvent(eAppEvent.reward_request_success, mediation);
        }


        protected void OnFailedLoadReward(string mediation, AdErrorEventArgs errorEvent)
        {
            Debug.Log($"AdNetwork OnFailedLoadReward : {mediation}, {errorEvent.Message}");
            _isLoadedReward.Value = false;
            ReTryLoadReward();
        }

        void ReTryLoadReward()
        {
            _retryReward++;

            _successReward = false;
            _actionReward = null;
            //LogAppEvent(eAppEvent.reward_request_failed, mediation);

            if (_retryCount > _retryReward)
            {
                Observable.Timer(TimeSpan.FromSeconds(_delayFailed + float.Epsilon),
                        Scheduler.MainThreadIgnoreTimeScale)
                    .Subscribe(_ =>
                    {
                        Debug.Log("AdNetwork OnFailedLoadReward : Retry");
                        AdLoadReward();
                    });
            }
        }

        protected void OnSuccessReward(string mediation, AdUnit adUnit)
        {
            Debug.Log("AdNetwork OnSuccessReward");
            _successReward = true;
            LogAppEvent(eAppEvent.reward_complete, mediation, adUnit);
        }

        private void LogAppEvent(eAppEvent logEvent, string mediation, AdUnit? adUnit = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters["mediation"] = mediation ?? "none";

            if (adUnit.HasValue)
                parameters["adunit"] = adUnit.Value.ToString();

            FB.LogAppEvent(logEvent.ToString(), null, parameters);
        }

        #endregion
    }
}