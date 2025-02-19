using Coffee.UIExtensions;
using DatasTypes;
using UnityEngine;
using DG.Tweening;
using Trucking.UI.Mission;
using UniRx;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_LevelMission : Popup_Base<Popup_LevelMission>
    {
        public Button btnRewardOpen;
        public RectTransform trsContents;
        public Popup_LevelMissionCellView[] cells;
        public Image imgRandomBox;

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private Tweener tweener;

        private void Start()
        {
            disposableClose.Clear();
            disposableBlack.Clear();

            btnCloseX.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (tweener == null)
                    {
                        GameManager.Instance.fsm.PopState();
                    }
                })
                .AddTo(this);

            btnBlackPanel.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (tweener == null)
                    {
                        GameManager.Instance.fsm.PopState();
                    }
                })
                .AddTo(this);

            btnRewardOpen.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (LevelMissionManager.Instance.model.isSuccess.Value)
                    {
                        GameManager.Instance.fsm.PopState();

                        Popup_RandomBoxOpenEffect.Instance.Show(LevelMissionManager.Instance.model.rewardIndex.Value,
                            () =>
                            {
                                MissionManager.Instance.AddValue(QuestData.eType.random_box, 1);
                                LevelMissionManager.Instance.model.hasMission.Value = false;
                            });
                    }
                })
                .AddTo(this);
        }

        public override void Show()
        {
            tweener?.Kill();
            base.Show();

            _compositeDisposable.Clear();
            btnRewardOpen.gameObject.SetActive(true);

            LevelMissionManager.Instance.model.isSuccess.Subscribe(suceess =>
            {
                btnRewardOpen.GetComponent<UIEffect>().enabled = !suceess;
            }).AddTo(_compositeDisposable);

            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].SetModel(LevelMissionManager.Instance.model.models[i]);
            }

            imgRandomBox.sprite = GameManager.Instance.GetRewardImage(RewardData.eType.random_box,
                LevelMissionManager.Instance.model.rewardIndex.Value);

            trsContents.anchoredPosition = new Vector3(0, -30, 0);
            tweener = trsContents.DOAnchorPosY(266, 0.3f).SetEase(Ease.OutQuart).OnComplete(() => { tweener = null; });
        }

        public override void Close()
        {
            tweener?.Kill();
            _compositeDisposable.Clear();
            base.Close();
            gameObject.SetActive(true);

            tweener = trsContents.DOAnchorPosY(-30, 0.3f).OnComplete(() =>
            {
                gameObject.SetActive(false);
                tweener = null;
            }).SetEase(Ease.OutQuart);
        }
    }
}