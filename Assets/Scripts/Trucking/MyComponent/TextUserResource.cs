/*
 * Created on 2019년 3월 20일 수요일
 * Creator jhlee5@cookapps.com
 * Copyright (c) 2019 CookAppsPlay
 */

using System;
using DG.Tweening;
using TMPro;
using Trucking.Common;
using UniRx;
using UnityEngine;

namespace Trucking.MyComponent
{
    /// <summary>
    /// 유저 자원 텍스트 갱신 에니메이션
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextUserResource : MonoBehaviour
    {
        private enum eType
        {
            Gold,
            Cash
        }

        [SerializeField] private eType _type;

        private void Start()
        {
            var text = GetComponent<TextMeshProUGUI>();
            var userResource = ObsUserResource();
            text.text = userResource.Value.ToString("N0");

            userResource.Pairwise()
                .Subscribe(pair =>
                {
                    DOTween.Kill(this);
                    var start = pair.Previous;
                    var to = pair.Current;
                    DOTween.To(() => start, x => text.text = x.ToString("N0"), to, 0.5f)
                        .OnComplete(() => text.text = to.ToString("N0")).SetTarget(this);
                }).AddTo(this);
        }

        private IReadOnlyReactiveProperty<long> ObsUserResource()
        {
            switch (_type)
            {
                case eType.Cash: return UserDataManager.Instance.data.cash;
                case eType.Gold: return UserDataManager.Instance.data.gold;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}