using System;
using System.Linq;
using DatasTypes;
using EnhancedUI.EnhancedScroller;
using TMPro;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.Mission;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.CellView
{
    public class CargoCellView : EnhancedScrollerCellView
    {
        public Button btnClick;
        public Button btnDelete;
        public Button btnDeleteContract;

        public TextMeshProUGUI count;
        public TextMeshProUGUI targetCity;
        public Transform trsStar;

        public Image imgReward;
        public Image imgRewardMaterial;
        public TextMeshProUGUI txtReward;
        public TextMeshProUGUI txtReward2;
        public Image icon;
        public GameObject stateCargo;
        public GameObject stateSlot;
        public GameObject stateMoreJob;
        public GameObject contract;
        public GameObject missionIcon;
        public Image imgIconBack;
        public Image imgCargoBack;

        public TextMeshProUGUI txtSlot;
        public TextMeshProUGUI txtSlotADTime;

        [HideInInspector] public CargoCellData data;

        private Action<CargoCellView> OnClick;
        private Action<CargoCellView> OnClickDelete;
        private Action<CargoCellView> OnRefresh;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private Truck truck;

        private void Start()
        {
            btnClick.OnClickAsObservable()
                .Subscribe(_ => { OnClick?.Invoke(this); })
                .AddTo(this);

            btnDelete.OnClickAsObservable()
                .Subscribe(_ => { OnClickDelete?.Invoke(this); })
                .AddTo(this);

            btnDeleteContract.OnClickAsObservable()
                .Subscribe(_ => { OnClickDelete?.Invoke(this); })
                .AddTo(this);

            _compositeDisposable.AddTo(this);
        }

        private void OnDisable()
        {
            _compositeDisposable.Clear();
        }

        public void SetData(CargoCellData _data,
            Truck _truck,
            Action<CargoCellView> _onClick,
            Action<CargoCellView> _onClickDelete,
            Action<CargoCellView> _onRefresh
        )
        {
            data = _data;
            OnClick = _onClick;
            OnClickDelete = _onClickDelete;
            OnRefresh = _onRefresh;
            truck = _truck;

            _compositeDisposable.Clear();

            contract.SetActive(data.cargo.model.isContract.Value);
            stateCargo.SetActive(data.type == CargoCellData.CargoCellType.CARGO);
            stateSlot.SetActive(data.type == CargoCellData.CargoCellType.SLOT);
            stateMoreJob.SetActive(data.type == CargoCellData.CargoCellType.MORE_JOB);
            gameObject.SetActive(true);
            btnDelete.gameObject.SetActive(false);
            btnDeleteContract.gameObject.SetActive(false);

            imgCargoBack.color = Color.white;

            switch (data.type)
            {
                case CargoCellData.CargoCellType.CARGO:

                    bool isMissionCargo = NewDailyMissionManager.Instance.IsMissionCargo(data.cargo);
                    missionIcon.SetActive(isMissionCargo);
                    imgIconBack.color = Utilities.GetColorByHtmlString(isMissionCargo ? "FFBB31" : "6DD4FF");
                    count.text = data.cargo.model.weight.Value.ToString();
                    targetCity.text = data.cargo.to.Value.name;


                    for (int i = 0; i < Cargo.MAX_LEVEL; i++)
                    {
                        trsStar.GetChild(i).gameObject.SetActive(data.cargo.model.grade.Value > i);
                    }

                    var rewardModel = data.cargo.model.rewardModels.FirstOrDefault(x =>
                        x.type.Value == RewardData.eType.gold || x.type.Value == RewardData.eType.cash ||
                        x.type.Value == RewardData.eType.crate);
                    txtReward.text = Utilities.GetNumberKKK(rewardModel.count.Value);
                    imgReward.sprite = GameManager.Instance.GetRewardImage(rewardModel);

                    var rewardMaterialModel =
                        data.cargo.model.rewardModels.FirstOrDefault(x => x.type.Value == RewardData.eType.material);
                    imgRewardMaterial.gameObject.SetActive(rewardMaterialModel != null);
                    txtReward2.gameObject.SetActive(rewardMaterialModel != null);

                    if (rewardMaterialModel != null)
                    {
                        imgRewardMaterial.sprite = GameManager.Instance.GetRewardImage(rewardMaterialModel);
                        txtReward2.text = Utilities.GetNumberKKK(rewardMaterialModel.count.Value);
                    }

                    icon.sprite = GameManager.Instance.GetCargoSprite(data.cargo.model.id.Value);
                    btnDelete.gameObject.SetActive(_onClickDelete != null && !data.cargo.model.isContract.Value);
                    btnDeleteContract.gameObject.SetActive(_onClickDelete != null && data.cargo.model.isContract.Value);

                    break;
                case CargoCellData.CargoCellType.SLOT:

                    Observable.Interval(TimeSpan.FromSeconds(1)).StartWith(0).Subscribe(_ =>
                    {
                        if (data.cargo.model.refreshTime.Value == DateTime.MinValue
                            || data.cargo.model.refreshTime.Value <= DateTime.Now)
                        {
                            _compositeDisposable.Clear();
                        }
                        else
                        {
//                            txtSlot.text = "Slots " + (dataIndex + 1) + " : ";
                            txtSlot.text = string.Format(Utilities.GetStringByData(20073), data.slotIndex + 1);
                            txtSlotADTime.text =
                                Utilities.GetTimeString(data.cargo.model.refreshTime.Value - DateTime.Now);
                        }
                    }).AddTo(_compositeDisposable);

                    break;
            }
        }

        public override void RefreshCellView()
        {
            RefreshData();
        }

        public void RefreshData()
        {
            SetData(data,
                truck,
                OnClick,
                OnClickDelete,
                OnRefresh
            );

            OnRefresh(this);
        }
    }

    public class CargoCellData
    {
        public Cargo cargo;
        public CargoCellType type;
        public int slotIndex;

        public enum CargoCellType
        {
            CARGO = 0,
            SLOT,
            MORE_JOB
        }
    }
}