using System;
using UniRx;
using UnityEngine;

namespace Trucking.Common
{
    public static class ResourceLoader
    {
        public static T Load<T>(string path) where T : UnityEngine.Object
        {
            return Resources.Load<T>(path);
        }

        public static IObservable<T> LoadAsync<T>(string path) where T : UnityEngine.Object
        {
            return Observable.Create<T>(obs =>
            {
                return Resources.LoadAsync<T>(path).AsAsyncOperationObservable().Last()
                    .Subscribe(x =>
                    {
                        var obj = x.asset as T;
                        if (obj != null)
                        {
                            obs.OnNext(obj);
                            obs.OnCompleted();
                        }
                        else
                        {
                            var msg = $"Not found Resource Path : {path}({typeof(T)})";
                            Debug.LogError(msg);
                            obs.OnError(new Exception(msg));
                        }
                    });
            });
        }

        public static IObservable<UnityEngine.Object> LoadAsync(string path)
        {
            return LoadAsync<UnityEngine.Object>(path);
        }

        public static UnityEngine.Object Load(string path)
        {
            return Load<UnityEngine.Object>(path);
        }
    }
}