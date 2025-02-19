/*
 * Created on 2019년 4월 11일 목요일
 * Creator jhlee5@cookapps.com
 * Copyright (c) 2019 CookAppsPlay
 */

using System;
using System.Collections;
using System.Threading;
using UniRx;
using UnityEngine;

namespace Trucking.Common
{
    public static partial class ContentLoader
    {
        private static IEnumerator ImplLoadTruckHeadCargoAsync(IObserver<Transform> observer, string head, string cargo,
            CancellationToken cancel, int layer = 0)
        {
            var loadAsync = LoadTruckHeadCargoAsync(head, cargo).ToYieldInstruction(cancel);
            yield return loadAsync.ToYieldInstruction(cancel);

            if (cancel.IsCancellationRequested)
            {
                yield break;
            }

            var go = new GameObject();
//        go.layer = layer;
            go.transform.position = new Vector3(0, -10000, 0);
            var trans = go.transform;
            foreach (var prefab in loadAsync.Result)
            {
                GameObject.Instantiate(prefab, trans);
            }

            if (trans.childCount > 0 && trans.GetChild(0).childCount > 0)
            {
                trans.GetChild(0).GetChild(0).gameObject.SetActive(false);
            }

            if (trans.childCount > 1 && trans.GetChild(1).childCount > 0)
            {
                trans.GetChild(1).GetChild(0).gameObject.SetActive(false);
            }


            observer.OnNext(trans);
            observer.OnCompleted();
        }

        private static IEnumerator ImplLoadParentTruckHeadCargoAsync(IObserver<Unit> observer, Transform parent,
            string head, string cargo, CancellationToken cancel)
        {
            var loadAsync = LoadTruckHeadCargoAsync(head, cargo).ToYieldInstruction(cancel);
            yield return loadAsync.ToYieldInstruction(cancel);

            if (cancel.IsCancellationRequested)
            {
                yield break;
            }

            foreach (var prefab in loadAsync.Result)
            {
                GameObject.Instantiate(prefab, parent);
            }

            observer.OnNext(Unit.Default);
            observer.OnCompleted();
        }
    }
}