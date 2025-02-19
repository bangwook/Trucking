using System.Collections.Generic;
using DatasTypes;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.Mission;
using UniRx;

namespace Trucking.Manager
{
    public class GuideQuestManager : Singleton<GuideQuestManager>
    {
        public GuideQuestModel model;


        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private CompositeDisposable _compositeDisposableMission = new CompositeDisposable();

        public void SetModel(GuideQuestModel _model)
        {
            model = _model;

            if (model == null)
            {
                model = new GuideQuestModel();
                UserDataManager.Instance.data.guideQuestData.Value = model;
            }

            Observable.CombineLatest(model.isFirst,
                    UserDataManager.Instance.data.hasTutorial,
                    (first, isTutorial) => first && !isTutorial)
                .Subscribe(value =>
                {
                    if (value)
                    {
                        model.index.Value = 0;
                        model.hasMission.Value = true;
                        model.isFirst.Value = false;
                    }
                }).AddTo(_compositeDisposable);

            model.index.Subscribe(index =>
            {
                if (index < Datas.guideQuestData.Length)
                {
                    if (model.questModel.Value != null)
                    {
                        model.questModel.Value = QuestModel.Make(GetData(), model.questModel.Value.count.Value);
                    }
                    else
                    {
                        model.questModel.Value = QuestModel.Make(GetData());
                    }
                }
            }).AddTo(_compositeDisposable);

            model.questModel.Subscribe(qm =>
            {
                _compositeDisposableMission.Clear();

                qm?.count.Subscribe(_ => { model.isSuccess.Value = qm.IsSuccess(); })
                    .AddTo(_compositeDisposableMission);
            }).AddTo(_compositeDisposable);
        }

        public void SetNext()
        {
            model.questModel.Value.count.Value = 0;
            model.index.Value++;
            model.isCharacterText.Value = true;
            model.hasMission.Value = model.index.Value < Datas.guideQuestData.Length;
            UserDataManager.Instance.SaveData();
        }

        public GuideQuestData GetData()
        {
            if (model.index.Value < Datas.guideQuestData.Length)
            {
                return Datas.guideQuestData[model.index.Value];
            }

            return Datas.guideQuestData[0];
        }

        public List<RewardModel> GetReward()
        {
            if (model.index.Value < Datas.guideQuestData.Length)
            {
                GuideQuestData rewadData = GetData();
                List<RewardModel> listRewardModels = new List<RewardModel>();

                RewardModel rewardModel =
                    new RewardModel(rewadData.reward_type.type, rewadData.reward_count, rewadData.reward_index);
                listRewardModels.Add(rewardModel);

                return listRewardModels;
            }

            return null;
        }
    }

    public class GuideQuestModel
    {
        public ReactiveProperty<bool> hasMission = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> isSuccess = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> isCharacterText = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> isFirst = new ReactiveProperty<bool>(true);
        public ReactiveProperty<int> index = new ReactiveProperty<int>();

        public ReactiveProperty<QuestModel> questModel = new ReactiveProperty<QuestModel>();
    }
}