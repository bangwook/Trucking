using System;
using Trucking.Common;
using Trucking.UI;
using Trucking.UI.CellView;
using UniRx;
using UnityEngine;

namespace Trucking.Manager
{
    public class TutorialManager : Singleton<TutorialManager>
    {
        public TutorialData[] datas;

        public delegate Transform findTarget();

        public ReactiveProperty<int> tutorialIndex = new ReactiveProperty<int>(0);

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnRuntimeInit()
        {
            Instance.Init();
        }

        void Init()
        {
            datas = new TutorialData[]
            {
                new TutorialData(12121, eCusor.LEFT_TOP, eMessage.BOTTOM, FindTarget_1, EventTarget_1, true),
                new TutorialData(12122, eCusor.LEFT_TOP, eMessage.BOTTOM, FindTarget_2, EventTarget_2, true),
                new TutorialData(12123, eCusor.RIGHT_TOP, eMessage.BOTTOM, FindTarget_3, EventTarget_3),
                new TutorialData(12124, eCusor.LEFT_BOTTOM, eMessage.BOTTOM, FindTarget_4, EventTarget_4, true),
                new TutorialData(12125, eCusor.LEFT_BOTTOM, eMessage.TOP, FindTarget_5, EventTarget_5),
                new TutorialData(0, eCusor.LEFT_TOP, eMessage.NONE, FindTarget_6, EventTarget_6),
                new TutorialData(12126, eCusor.NONE, eMessage.BOTTOM, null, null),
            };
        }

        public void SetNext()
        {
            tutorialIndex.Value++;

            FBAnalytics.FBAnalytics.LogTutorialEvent(tutorialIndex.Value,
                UserDataManager.Instance.data.hasTutorial.Value);

            if (tutorialIndex.Value >= datas.Length)
            {
                UserDataManager.Instance.data.hasTutorial.Value = false;
            }
        }

        public TutorialData GetData()
        {
            if (tutorialIndex.Value < datas.Length)
            {
                return datas[tutorialIndex.Value];
            }

            return null;
        }

        public Transform GetTarget()
        {
            if (tutorialIndex.Value < datas.Length)
            {
                return datas[tutorialIndex.Value].funcFindTarget();
            }

            return null;
        }

        Transform FindTarget_1()
        {
            Observable.NextFrame().Subscribe(_ =>
            {
                WorldMap.Instance.SetCamera(GameManager.Instance.FindStation("MONTPELIER").transform.position,
                    true);
            });

            return GameManager.Instance.FindStation("MONTPELIER").transform;
        }

        void EventTarget_1()
        {
            GameManager.Instance.ClickStation("MONTPELIER");
        }


        Transform FindTarget_2()
        {
            return UICityMenu.Instance.btnJob.transform;
        }

        void EventTarget_2()
        {
            UICityMenu.Instance.btnJob.onClick.Invoke();
        }

        Transform FindTarget_3()
        {
            var cell = JobView.Instance.scrollerPlatform.GetCellViewAtDataIndex(0) as CargoCellView;
            return cell?.icon.transform;
        }

        void EventTarget_3()
        {
            var cell = JobView.Instance.scrollerPlatform.GetCellViewAtDataIndex(0) as CargoCellView;
            cell?.btnClick.onClick.Invoke();
            WorldMap.Instance.SetCamera(GameManager.Instance.FindStation("NEW YORK").transform.position +
                                        new Vector3(170, 0, 0));
        }

        Transform FindTarget_4()
        {
            return GameManager.Instance.FindStation("NEW YORK").transform;
        }

        void EventTarget_4()
        {
            GameManager.Instance.ClickStation("NEW YORK");
        }

        Transform FindTarget_5()
        {
            return JobView.Instance.txtTruckTime.transform;
        }

        void EventTarget_5()
        {
            JobView.Instance.btnDepart.onClick.Invoke();
        }

        Transform FindTarget_6()
        {
            return UIMain.Instance.buttonBack.transform;
        }

        void EventTarget_6()
        {
            UIMain.Instance.buttonBack.onClick.Invoke();
        }
    }


    public class TutorialData
    {
        public int textId;
        public eCusor cusorPos;
        public eMessage messagePos;
        public TutorialManager.findTarget funcFindTarget;
        public bool isWorldCanvas;
        public bool keepCopyTarget;
        public Action funcEvent;

        public TutorialData(int _textId,
            eCusor _cusorPos,
            eMessage _messagePos,
            TutorialManager.findTarget _funcFindTarget = null,
            Action _funcEvent = null,
            bool _isWorldCanvas = false,
            bool _keepCopyTarget = false)
        {
            textId = _textId;
            cusorPos = _cusorPos;
            messagePos = _messagePos;
            funcFindTarget = _funcFindTarget;
            isWorldCanvas = _isWorldCanvas;
            keepCopyTarget = _keepCopyTarget;
            funcEvent = _funcEvent;
        }

        public Transform GetTarget()
        {
            return funcFindTarget?.Invoke();
        }

        public void InvokeEvent()
        {
            funcEvent?.Invoke();
        }
    }

    public enum eCusor
    {
        LEFT_TOP,
        LEFT_CENTER,
        LEFT_BOTTOM,
        RIGHT_TOP,
        RIGHT_CENTER,
        RIGHT_BOTTOM,
        NONE,
    }

    public enum eMessage
    {
        TOP,
        CENTER,
        BOTTOM,
        NONE
    }
}