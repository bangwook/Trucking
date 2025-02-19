/*
 * Created on 2019년 3월 19일 화요일
 * Creator jhlee5@cookapps.com
 * Copyright (c) 2019 CookAppsPlay
 */

using Trucking.Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.MyComponent
{
    /// <summary>
    /// 앱스토어 이동
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class ButtonAppStore : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().OnClickAsObservable()
                .Subscribe(_ => Application.OpenURL(CommonDefine.AppLink)).AddTo(this);
        }
    }
}