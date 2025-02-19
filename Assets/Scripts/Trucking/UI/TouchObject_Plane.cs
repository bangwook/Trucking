using System;
using DatasTypes;
using Trucking.Common;
using Trucking.Manager;
using Trucking.Model;
using Trucking.UI.Mission;
using Trucking.UI.Popup;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Trucking.UI
{
    public class TouchObject_Plane : MonoSingleton<TouchObject_Plane>
    {
        public Transform trsPlanes;
        public Animator aniPlaneGold;
        public ParticleSystem particle;

        private bool hasGold;
        private bool hasSound;
        private CompositeDisposable _disposableIntaval = new CompositeDisposable();
        private CompositeDisposable _disposableSound = new CompositeDisposable();


        private void Start()
        {
            TouchObjectManager_Plane.Instance.isShow.Subscribe(show =>
            {
                if (show)
                {
                    Move();
                }
                else
                {
                    Hide();
                }
            }).AddTo(this);
        }

        void Move()
        {
            hasGold = true;
            hasSound = false;
            _disposableIntaval.Clear();

            trsPlanes.gameObject.SetActive(true);

            int type = (int) TouchObjectManager_Plane.Instance.GetReward().type.Value;

            for (int i = 0; i < trsPlanes.childCount; i++)
            {
                trsPlanes.GetChild(i).gameObject.SetActive(false);
            }

            trsPlanes.GetChild(type - 1).gameObject.SetActive(true);

            string aniName = "touch_object_Route0" + Random.Range(1, 5);

            Animator animator = trsPlanes.GetChild(type - 1)
                .GetComponent<Animator>();
            animator.speed = 0.1f;
            animator.Play(aniName, -1, 0);

            float length = Utilities.GetAnimatoionLength(animator, aniName) * 10;

            UnirxExtension.DateTimer(DateTime.Now.AddSeconds(length)).Subscribe(len => { Hide(); })
                .AddTo(_disposableIntaval);

            Observable.EveryUpdate().Subscribe(_ =>
            {
                Vector3 viewPos = Camera.main.WorldToViewportPoint(animator.transform.position);

                if (viewPos.x >= 0 && viewPos.x <= 1
                                   && viewPos.y >= 0 && viewPos.y <= 1
                                   && viewPos.z > 0)
                {
                    hasSound = true;
                    _disposableSound.Clear();
                    AudioManager.Instance.PlaySound("sfx_airplane");
                }
            }).AddTo(_disposableSound);
        }

        void Hide()
        {
            _disposableIntaval.Clear();
            TouchObjectManager_Plane.Instance.isShow.Value = false;
            trsPlanes.gameObject.SetActive(false);
        }

        public void Touch(Vector3 pos)
        {
            if (!hasGold)
            {
                return;
            }

            hasGold = false;
            AudioManager.Instance.PlaySound("sfx_button_main");
            AudioManager.Instance.Stop("sfx_airplane");
            MissionManager.Instance.AddValue(QuestData.eType.map_object, 1);

            RewardModel rewardModel = TouchObjectManager_Plane.Instance.GetReward();
            FBAnalytics.FBAnalytics.LogClickObjectEvent(UserDataManager.Instance.data.lv.Value, "Plane",
                rewardModel.type.Value.ToString());

            if (rewardModel.type.Value == RewardData.eType.gold)
//                || !AdManager.Instance.IsLoadedReward.Value)
            {
                aniPlaneGold.enabled = false;
                aniPlaneGold.transform.GetChild(0).gameObject.SetActive(false);

                particle.gameObject.SetActive(true);
                particle.Stop();
                particle.Clear();
                particle.Play();

                UIRewardManager.Instance.Show(rewardModel, pos);

                UnirxExtension.DateTimer(DateTime.Now.AddSeconds(1)).Subscribe(_ =>
                {
                    aniPlaneGold.enabled = true;
                    aniPlaneGold.transform.GetChild(0).gameObject.SetActive(true);
                    particle.gameObject.SetActive(false);
                    Hide();
                }).AddTo(this);
            }
            else          
            {
                Hide();
                Popup_TouchObject.Instance.Show(pos);
            }
        }

        private void OnDestroy()
        {
            base.OnDestroy();

            _disposableSound.Clear();
            _disposableIntaval.Clear();
        }
    }
}