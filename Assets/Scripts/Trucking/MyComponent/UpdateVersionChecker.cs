/*
 * Created on 2019년 3월 19일 화요일
 * Creator jhlee5@cookapps.com
 * Copyright (c) 2019 CookAppsPlay
 */

using Trucking.Common;
using UniRx;
using UnityEngine;
using RS = UnityEngine.RemoteSettings;

namespace Trucking.MyComponent
{
    /// <summary>
    /// 앱 버전업 체크
    /// </summary>
    public class UpdateVersionChecker : MonoBehaviour
    {
        [SerializeField]
        private Canvas _parent;
        
        [SerializeField]
        private GameObject _prefabPopup;
        
        private void Start()
        {
            RS.Updated += RemoteSettingsUpdated;
            
            Observable.EveryApplicationFocus()
                .Subscribe(SetApplicationFocus).AddTo(this);

            RemoteSettingsUpdated();
        }

        private void OnDestroy()
        {
            RS.Updated -= RemoteSettingsUpdated;   
        }

        private void RemoteSettingsUpdated()
        {
            Debug.Log("Check Update");
            
            const string min_version = "min_ver_" + CommonDefine.Platform;
            if (RS.HasKey(min_version) && _parent != null && _prefabPopup != null)
            {
                var version = RS.GetInt(min_version);
                var current = Trucking.Common.Trucking.VERSION_CODE;
                
                if (current < version)
                {
                    Instantiate(_prefabPopup, _parent.transform);
                }
            }
        }

        void SetApplicationFocus(bool focus)
        {
            if (focus)
            {
                RS.ForceUpdate();
            }
        }
    }
}