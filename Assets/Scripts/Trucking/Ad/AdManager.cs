/*
 * Created on 2019년 3월 12일 화요일
 * Creator jhlee5@cookapps.com
 * Copyright (c) 2019 CookAppsPlay
 */

using System;
using Newtonsoft.Json;
using Trucking.Common;
using UnityEngine;
using UniRx;
using RS = UnityEngine.RemoteSettings;

namespace Trucking.Ad
{
    /// <summary>
    /// 광고 메니져
    /// </summary>
    public sealed class AdManager
    {
        private static AdManager _instance;
        public static AdManager Instance => _instance ?? (_instance = new AdManager());
        public ReactiveProperty<bool> IsLoadedReward;
        
        private IAdNetwork _adNetwork;
        private bool _isShowing;

        /// <summary>
        /// 광고 지원하는가?
        /// </summary>
        public bool SupportAd { get; private set; } = true;

        public bool IsShowing
        {
            get { return _isShowing; }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void RuntimeInit()
        {
            Instance.Init();
        }

        private void Init()
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        _adNetwork = new AdAdmob();
#elif UNITY_EDITOR
            _adNetwork = new AdEditor();
#else
        _adNetwork = new AdUnsupported();
        SupportAd = false;
#endif
            _adNetwork.Init(AudioManager.Instance.MusicPause, AudioManager.Instance.MusicUnpause);
            Debug.Log("AdManager Init");
            RemoteSettingsUpdated();
            RS.Updated += RemoteSettingsUpdated;
            IsLoadedReward = _adNetwork.IsLoadedReward;

            IsLoadedReward.Subscribe(load =>
            {
                Debug.Log("AdManager IsLoadedReward Change : " + load);    
            });
        }

        private void RemoteSettingsUpdated()
        {
            const string key = "ad_cfg";
            if (RS.HasKey(key))
            {
                var strJson = RS.GetString(key);
                try
                {
                    Debug.Log("ad_cfg : " + strJson);
                    var json = JsonConvert.DeserializeObject<object[]>(strJson);
                    _adNetwork.Setting(json);
                }
                catch (Exception e)
                {
                    Debug.LogWarning("ad_cfg : " + e);
                }
            }
        }

        /// <summary>
        /// 보상형 광고 보기
        /// </summary>
        /// <param name="adUnit"></param>
        /// <param name="callback"></param>
        public void ShowReward(AdUnit adUnit, Action<AdResult> callback)
        {
            _adNetwork.ShowRewardAd(adUnit, callback);
        }

        /// <summary>
        /// 보상형 광고 보기(rx)
        /// </summary>
        /// <param name="adUnit"></param>
        /// <returns></returns>
        public IObservable<AdResult> ShowReward(AdUnit adUnit)
        {
            return Observable.Create<AdResult>((obs) =>
            {
                _isShowing = true;
                ShowReward(adUnit, (result) =>
                {
                    _isShowing = false;
                    obs.OnNext(result);
                    obs.OnCompleted();
                });
                return Disposable.Empty;
            });
        }

        public IObservable<AdResult> ShowRewardLoad(AdUnit adUnit, int step = 0)
        {
            Debug.Log("load start");
            FBAnalytics.FBAnalytics.LogWatchAdEvent(UserDataManager.Instance.data.lv.Value,
                step == 0 ? adUnit.ToString() : adUnit + "_" + step);

            return ShowReward(adUnit)
                .DoOnCompleted(() => { Debug.Log("load end"); });
        }

        
    }
}