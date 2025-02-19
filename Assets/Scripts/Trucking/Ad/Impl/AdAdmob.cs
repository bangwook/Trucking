/*
 * Created on 2019년 3월 12일 화요일
 * Creator jhlee5@cookapps.com
 * Copyright (c) 2019 CookAppsPlay
 */

using System;
using GoogleMobileAds.Api;
using UniRx;
using UnityEngine;

namespace Trucking.Ad
{
    public sealed class AdAdmob : AdNetwork
    {
#if UNITY_IOS
        private const string appId = "ca-app-pub-9430141546173788~4389596916";
        private const string adUnitIdReward = "ca-app-pub-9430141546173788/5838409959";
#else
        private const string appId = "ca-app-pub-9430141546173788~5170473341";
        private const string adUnitIdReward = "ca-app-pub-9430141546173788/5607860496";
#endif

        private RewardedAd rewardBasedVideo = null;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        protected override void Init()
        {
            MobileAds.SetiOSAppPauseOnBackground(true);
            MobileAds.Initialize(appId);
        }

        protected override void AdShowReward(AdUnit adUnit)
        {
            _disposable.Clear();

            Observable.FromEventPattern<EventHandler<Reward>, EventArgs>(
                    h => (sender, args) => OnSuccessReward(rewardBasedVideo.MediationAdapterClassName(), adUnit),
                    h => rewardBasedVideo.OnUserEarnedReward += h, h => rewardBasedVideo.OnUserEarnedReward -= h)
                .Subscribe()
                .AddTo(_disposable);

            Observable.FromEventPattern<EventHandler<EventArgs>, EventArgs>(
                    h => (sender, args) => OnCloseReward(rewardBasedVideo.MediationAdapterClassName(), adUnit),
                    h => rewardBasedVideo.OnAdClosed += h, h => rewardBasedVideo.OnAdClosed -= h)
                .Subscribe()
                .AddTo(_disposable);

            Observable.FromEventPattern<EventHandler<EventArgs>, EventArgs>(
                    h => (sender, args) => OnOpenReward(rewardBasedVideo.MediationAdapterClassName(), adUnit),
                    h => rewardBasedVideo.OnAdOpening += h, h => rewardBasedVideo.OnAdOpening -= h)
                .Subscribe()
                .AddTo(_disposable);

            Observable.FromEventPattern<EventHandler<AdErrorEventArgs>, EventArgs>(
                    h => (sender, args) => OnAdFailedToShow(rewardBasedVideo.MediationAdapterClassName(), adUnit, args),
                    h => rewardBasedVideo.OnAdFailedToShow += h, h => rewardBasedVideo.OnAdFailedToShow -= h)
                .Subscribe()
                .AddTo(_disposable);


            if (rewardBasedVideo != null && rewardBasedVideo.IsLoaded())
            {
                Debug.Log(
                    "AdNetwork AdShowReward !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                rewardBasedVideo.Show();
            }
            else
            {
                Debug.Log(
                    "AdNetwork Error AdShowReward !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
        }

        protected override void AdLoadReward()
        {
            Debug.Log(
                "AdLoadReward !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!       :    " +
                rewardBasedVideo?.IsLoaded());
            rewardBasedVideo = new RewardedAd(adUnitIdReward);
            rewardBasedVideo.OnAdLoaded += (sender, args) =>
                OnLoadReward(rewardBasedVideo.MediationAdapterClassName(), args.ToString());
            rewardBasedVideo.OnAdFailedToLoad += (sender, args) =>
                OnFailedLoadReward(rewardBasedVideo.MediationAdapterClassName(), args);

            var request = CreateAdRequestBuilder().AddExtra("npa", "1").Build();
            rewardBasedVideo.LoadAd(request);
        }

        private AdRequest.Builder CreateAdRequestBuilder()
        {
            return new AdRequest.Builder()
                    .AddTestDevice("8761F3DCD5811E79648FC38F7544ED54") // android mi8 // jh
                    .AddTestDevice("8DE74CF27C7CF9C2B8C2D720E80CEF05") // android nexus5
                    .AddTestDevice("0330ab2fd1691fb287a31e4d274fec60") // ios ipad mini 3
                    .AddTestDevice("6f317197b9911c9f337e2301322ee7ea") // ios ipad mini 3
                    .AddTestDevice("3F7D313E8445AA88AE1DB36B1A76561C") // note 8 // si
                    .AddTestDevice("577BE9A403AB2789E4E7C31341237819") // android gallexy s9+ //brandon
                    .AddTestDevice("AE34711C0A70FB89D7181EB8E2FC1CB0") // androiid lg v30 // kj
                    .AddTestDevice("D8D03979DBEDA5E71F23C1C83B5C2A80") // android gallexy s10 5g //wook)
                ;
        }
    }
}