﻿/**
 * iap manager - jhlee5
 * */

using System;
using Trucking.Common;
using Trucking.Iap.Impl;
using UniRx;
using UnityEngine.Assertions;

namespace Trucking.Iap
{
    /// <summary>
    /// iap manager
    /// </summary>
    public sealed class CPIapManager : Singleton<CPIapManager>
    {
        private IIap iapImpl = null;

        private IIap Iap
        {
            get
            {
                Assert.IsNotNull(iapImpl);
                return iapImpl;
            }
        }

        private void CreateImpl(IapType type)
        {
            switch (type)
            {
                case IapType.Official:
                    iapImpl = new IapOfficial();
                    break;
            }
            Assert.IsNotNull(iapImpl, string.Format("can not create iap {0}", type.ToString()));
        }

        /// <summary>
        /// 초기화 되었는가?
        /// </summary>
        public bool IsInit
        {
            get { return Iap.IsInit(); }
        }

        /// <summary>
        /// iap 정보
        /// </summary>
        /// <param name="info"></param>
        public void SetInfo(IIapInfo info)
        {
            CreateImpl(info.Type);
            Iap.SetInfo(info);
        }

        /// <summary>
        /// 초기화
        /// </summary>
        public void Init()
        {
            Iap.Init();
        }

        /// <summary>
        /// 결제
        /// </summary>
        /// <param name="id"></param>
        /// <param name="callback"></param>
        public void Purchase(string id, IapResult callback = null)
        {
            Iap.Purchase(id, callback);
        }

        /// <summary>
        /// 현지화 가격 얻기
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string LocalizedPrice(string id)
        {
            return Iap.LocalizedPrice(id);
        }

        /// <summary>
        /// rx 현지화 가격 얻기
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IReadOnlyReactiveProperty<string> ObsLocalizedPrice(string id)
        {
            return Iap.ObsLocalizedPrice(id);
        }

        public void Restore(Action<bool> callback)
        {
            Iap.Restore(callback);
        }
    }
}