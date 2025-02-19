using System.Linq;
using DatasTypes;
using Newtonsoft.Json;
using Trucking.Common;
using Trucking.Model;
using UniRx;

namespace Trucking.UI.Mission
{
    public class AchievementManager : Singleton<AchievementManager>
    {
        public AchievementModel model;
        public ReactiveProperty<bool> hasReward = new BoolReactiveProperty();
        
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private CompositeDisposable _disposableQuest = new CompositeDisposable();

        
        void Subscribe()
        {
            _compositeDisposable.Clear();
            
            model.model.Select(value => value.quest).Merge()
                .Subscribe(quest =>
                {
                    _disposableQuest.Clear();
                    
                    model.model.Select(value => value.quest.Value.ObsChanged).Merge()
                        .Subscribe(q =>
                        {
                            CheckHasReward();
                        }).AddTo(_disposableQuest);

                }).AddTo(_compositeDisposable);
            

            model.model.Select(value => value.step).Merge()
                .Subscribe(step =>
                {
                    CheckHasReward();
                    
                }).AddTo(_compositeDisposable);

        }

        void CheckHasReward()
        {
            hasReward.Value = false;
                    
            foreach (var cellModel in model.model)
            {
                if (cellModel.step.Value < cellModel.data.reward_type.Length && cellModel.quest.Value.IsSuccess())
                {
                    hasReward.Value = true;
                    break;
                }
            }

        }
        
        public void SetModel(AchievementModel _model)
        {
            model = _model;
            
            if (model == null)
            {
                model = new AchievementModel();
                UserDataManager.Instance.data.achievementData.Value = model;
                
                foreach (AchievementData data in Datas.achievementData)
                {
                    QuestModel quest = QuestModel.Make(data, 0);
                    AchievementCellModel cell = new AchievementCellModel();
                    cell.data = data;
                    cell.quest.Value = quest;
                    model.model.Add(cell);
                }
            }

            for (int i = 0; i < model.model.Count; i++)
            {
                model.model[i].data = Datas.achievementData[i];
            }

            Subscribe();
        }
    }
    
    public class AchievementModel
    {
        public ReactiveCollection<AchievementCellModel> model = new ReactiveCollection<AchievementCellModel>();
    }

    public class AchievementCellModel
    {
        public ReactiveProperty<QuestModel> quest = new ReactiveProperty<QuestModel>();
        public ReactiveProperty<int> step = new ReactiveProperty<int>();

        [JsonIgnore]
        public AchievementData data;

        public RewardModel GetRewardModel()
        {
            RewardModel rewardModel = new RewardModel(data.reward_type[step.Value].type,
                data.reward_count[step.Value], data.reward_index[step.Value]);

            return rewardModel;
        }
    }

}