using System;
using UniRx;
using UnityEngine.UI;

namespace Trucking.Common
{
    public static class UnirxExtension
    {
        public static IObservable<ReactiveDictionary<TKey, TValue>> ObsSomeChanged<TKey, TValue>(
            this ReactiveDictionary<TKey, TValue> dict, bool notifyCurrentCount = true)
        {
            return dict.ObserveAdd().AsUnitObservable()
                .Merge(dict.ObserveReset().AsUnitObservable())
                .Merge(dict.ObserveRemove().AsUnitObservable())
                .Merge(dict.ObserveReplace().AsUnitObservable())
                .Merge(dict.ObserveCountChanged(notifyCurrentCount).AsUnitObservable())
                .Select(_ => dict);
        }

        public static IObservable<IReactiveCollection<T>> ObsSomeChanged<T>(this IReactiveCollection<T> collection,
            bool notifyCurrentCount = true)
        {
            return collection.ObserveAdd().AsUnitObservable()
                .Merge(collection.ObserveReset().AsUnitObservable())
                .Merge(collection.ObserveMove().AsUnitObservable())
                .Merge(collection.ObserveRemove().AsUnitObservable())
                .Merge(collection.ObserveReplace().AsUnitObservable())
                .Merge(collection.ObserveCountChanged(notifyCurrentCount).AsUnitObservable())
                .Select(_ => collection);
        }

        public static IObservable<Unit> OnClickSoundAsObservable(this Button button)
        {
            return button.onClick.AsObservable();
        }

        public static IObservable<long> DateTimer(DateTime dateTime,
            FrameCountType frameCountType = FrameCountType.Update)
        {
            if (dateTime <= DateTime.Now)
            {
                return Observable.Return(0L);
            }

            if (frameCountType == FrameCountType.EndOfFrame)
            {
                return Observable.EveryEndOfFrame().Where(_ => dateTime <= DateTime.Now).First();
            }

            return Observable.EveryUpdate().Where(_ => dateTime <= DateTime.Now).First();
        }

//        public static IObservable<bool> DateTimerBoolean(DateTime dateTime)
//        {
//            if (dateTime <= DateTime.Now)
//            {
//                return Observable.Return(true);
//            }
//
//            return Observable.EveryUpdate().Select(_ => dateTime <= DateTime.Now).StartWith(false).DistinctUntilChanged();
//        }

        public static IObservable<Button> OnClickButtonAsObservable(this Button button)
        {
            return button.onClick.AsObservable().Select(_ => button);
        }
    }
}