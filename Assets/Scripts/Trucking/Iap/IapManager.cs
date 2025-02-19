/*
 * Created on 2019년 3월 18일 월요일
 * Creator jhlee5@cookapps.com
 * Copyright (c) 2019 CookAppsPlay
 */

using System;
using System.Linq;
using Trucking.Common;
using Trucking.UI.Popup;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;

namespace Trucking.Iap
{
    public class IapManager : Singleton<IapManager>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void RuntimeInit()
        {
            // 게임신 진입시 자동으로 초기화
            Observable.FromEvent<UnityAction<Scene, LoadSceneMode>, Scene>
                (
                    h => (s, l) => h.Invoke(s),
                    h => SceneManager.sceneLoaded += h,
                    h => SceneManager.sceneLoaded -= h)
                .Where(scene => scene.name.ToLower().Contains("gamescene"))
                .First()
                .Subscribe(_ => Instance.Init());
        }

        private void Init()
        {
            SetInfoOfficial();
            CPIapManager.Instance.Init();
        }

        private void SetInfoOfficial()
        {
            var info = new IapInfoOfficial();

            // 성공시 콜백
            info.Success += CallbackSuccess;

            // 상품 정보 등록
            info.Products = StoreData.StoreDatas.Select(value => new IapInfoOfficial.Product()
            {
                id = value.id,
                type = value.type,
                ids = new IDs()
                {
                    {value.google, GooglePlay.Name},
                    {value.apple, AppleAppStore.Name},
                    {value.amazon, AmazonApps.Name},
                    {value.mac, MacAppStore.Name},
                    {value.facebook, FacebookStore.Name}
                }
            });

            info.VerifyPeceipt = VerifyPeceiptOfficial;

            CPIapManager.Instance.SetInfo(info);
        }

        private void CallbackSuccess(string id)
        {
            Debug.Log($"Iap success : {id}");
            var storeData = StoreData.GetById(id);
        }

        /// <summary>
        /// 영수증 검증
        /// </summary>
        /// <param name="product"></param>
        /// <param name="result"></param>
        private void VerifyPeceiptOfficial(PurchaseEventArgs args, Product product, VerifyPeceiptResultOfficial result)
        {
            //        NetManager.Instance.Post(new net.req.ValidatePurchases(product.receipt))
            //            .Subscribe(recv => result.Invoke(product, recv.Success));

            var data = StoreData.GetById(product.definition.id);
            var success = new Action(() =>
            {
                var packagename = data != null ? data.id : product.definition.id;
                string currency = product.metadata.isoCurrencyCode;
                FBAnalytics.FBAnalytics.LogPurchase(decimal.ToSingle(product.metadata.localizedPrice),
                    currency,
                    packagename,
                    UserDataManager.Instance.data.lv.Value,
                    UserDataManager.Instance.data.hasPurchase.Value);
                result.Invoke(product, true);
            });

            Popup_Loading.Instance.gameObject.SetActive(false);
            IapCallback.OnIap(data, success);
        }

        /// <summary>
        /// rx 현지화 가격
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IReadOnlyReactiveProperty<string> ObsLocalizedPrice(string id)
        {
            return CPIapManager.Instance.ObsLocalizedPrice(id);
        }

        /// <summary>
        /// 결제
        /// </summary>
        /// <param name="id"></param>
        /// <param name="callback"></param>
        public void Purchase(string id)
        {
            IapResult func = (result) =>
            {
                if (!result) Popup_Loading.Instance.gameObject.SetActive(false);
            };
            Popup_Loading.Instance.gameObject.SetActive(true);
            CPIapManager.Instance.Purchase(id, func);
        }

        /// <summary>
        /// 복구 ios만
        /// </summary>
        public void RestoreApple(Action<bool> callback)
        {
            CPIapManager.Instance.Restore(callback);
        }
    }
}