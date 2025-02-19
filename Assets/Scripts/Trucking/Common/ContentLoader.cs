/*
 * Created on 2019년 2월 12일 화요일
 * Creator jhlee5@cookapps.com
 * Copyright (c) 2019 CookAppsPlay
 */

using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Trucking.Common
{
    public static partial class ContentLoader
    {
        public static IObservable<Transform> LoadTruckUIObject3DAsync(string head, string cargo)
        {
            return Observable.FromCoroutine<Transform>((observer, cancellation) =>
                ImplLoadTruckHeadCargoAsync(observer, head, cargo, cancellation, LayerMask.NameToLayer("UIObject3D")));
        }

        public static IObservable<Unit> LoadParentTruckHeadCargoAsync(Transform parent, string head, string cargo)
        {
            return Observable.FromCoroutine<Unit>((observer, cancellation) =>
                ImplLoadParentTruckHeadCargoAsync(observer, parent, head, cargo, cancellation));
        }

        private static string PathTruckHead(string head) => $"Prefab/Truck_new/Head/{head}";
        private static string PathTruckCargo(string cargo) => $"Prefab/Truck_new/Cargo/{cargo}";

        private static IObservable<IList<UnityEngine.Object>> LoadTruckHeadCargoAsync(string head, string cargo)
        {
            var path_head = PathTruckHead(head);
            var obs_head = ResourceLoader.LoadAsync(path_head);

            if (cargo.Equals("0"))
            {
                return Observable.Zip(obs_head);
            }

            var path_cargo = PathTruckCargo(cargo);
            var obs_cargo = ResourceLoader.LoadAsync(path_cargo);
            return Observable.Zip(obs_head, obs_cargo);
        }
    }
}