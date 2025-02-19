//using System;
//using System.Collections.Generic;
//using System.Linq;
//using DatasTypes;
//using DG.Tweening;
//using EnhancedUI.EnhancedScroller;
//using TMPro;
//using Trucking.Common;
//using Trucking.Manager;
//using Trucking.Model;
//using UniRx;
//using UnityEngine;
//using UnityEngine.UI;
//using Random = UnityEngine.Random;
//
//namespace Trucking.UI.Popup
//{
//    public class Popup_LuckyBox : Popup_Base<Popup_LuckyBox>, IEnhancedScrollerDelegate
//    {
//        public Button btnBuy;
//        
//        public EnhancedScroller scroller;
//        public EnhancedScrollerCellView luckyBoxCellViewPrefab;
//
//        public TextMeshProUGUI txtTitle;
//        public TextMeshProUGUI txtDes;
//        public TextMeshProUGUI txtPrice;
//        public TextMeshProUGUI txtTime;
//
//        List<LuckyBoxRowData> luckyBoxRowDatas = new List<LuckyBoxRowData>();
//
//        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
//        DateTime startTime;
//        DateTime endTime;
//        private int[] reward_rate;
//        private LuckyBoxData data;
//        
//        private void Start()
//        {
//            scroller.Delegate = this;
//            scroller.cellViewVisibilityChanged = CellViewVisibilityChanged;
//
//            
//            btnBuy.OnClickAsObservable()
//                .Subscribe(_ =>
//                {
//                    if (UserDataManager.Instance.UseCash(LuckyBoxManager.Instance.data.Value.price_cash))
//                    {
//                        AudioManager.Instance.PlaySound("sfx_box_open");
//                        FBAnalytics.FBAnalytics.LogLuckyBoxEvent(UserDataManager.Instance.data.lv.Value,
//                            UserDataManager.Instance.data.hasPurchase.Value);
//                        Popup_RandomBoxOpenEffect.Instance.Show(GetRewardModels(), Refresh);                        
//                    }
//                    else
//                    {
//                        AudioManager.Instance.PlaySound("sfx_require");
//                        btnBuy.transform.DOShakePosition(0.3f, 3);
//                    }
//                })
//                .AddTo(this);
//
//            UserDataManager.Instance.data.lv.Subscribe(lv =>
//            {
//                Refresh();
//            }).AddTo(this);
//
//        }
//
//        public override void Show()
//        {
//            if (!Trucking.Common.Trucking.CloseLuckyBox)
//            {
//                base.Show();
//                UIMain.Instance.luckyBoxAlert.SetActive(false);
//                Refresh();
//            }
//        }
//        
//        public override void Close()
//        {
//            base.Close();
//            _compositeDisposable.Clear();            
//        }
//
//        void Refresh()
//        {
//            luckyBoxRowDatas.Clear();
//            
//            if (LuckyBoxManager.Instance.data.Value != null)
//            {
//                data = LuckyBoxManager.Instance.data.Value;
//                reward_rate = new int[data.reward_rate.Length];
//
//                for (int i = 0; i < data.reward_type.Length; i++)
//                {
//                    LuckyBoxRowData luckyBoxRowData = new LuckyBoxRowData();
//                    luckyBoxRowData.type = data.reward_type[i].type;
//                    luckyBoxRowData.count = data.reward_count[i];
//                    reward_rate[i] = data.reward_rate[i];
//
//                    if (luckyBoxRowData.type == RewardData.eType.gold)
//                    {
//                        luckyBoxRowData.count = data.reward_count[i] * (int)UserDataManager.Instance.GetLevelMag().lucky_gold;
//                    }
//                    
//                    if (data.reward_type[i].type == RewardData.eType.truck_id)
//                    {
//                        TruckData truckData = Datas.truckData.ToArray()
//                            .FirstOrDefault(x => x.id == data.reward_count[i]);
//
//                        if (truckData != null && GameManager.Instance.trucks.Count(x => x.data.id == truckData.id) == 0)
//                        {
//                            luckyBoxRowDatas.Add(luckyBoxRowData);
//                        }
//                        else
//                        {
//                            reward_rate[i] = 0;
//                        }
//                    }
//                    else
//                    {
//                        luckyBoxRowDatas.Add(luckyBoxRowData);
//                    }
//                }
//                
//                scroller.ReloadData();
//
//                UserDataManager.Instance.data.cash.Subscribe(cash =>
//                {
//                    txtPrice.text = Utilities.GetThousandCommaText(data.price_cash);
//                    txtPrice.color = Color.white;
//                    if (cash < data.price_cash)
//                    {
//                        txtPrice.color = Color.red;
//                    }    
//                }).AddTo(_compositeDisposable);
//                
//
//                Observable.Interval(TimeSpan.FromSeconds(1)).StartWith(0).Subscribe(_ =>
//                {
//                    txtTime.text = Utilities.GetTimeStringShort(LuckyBoxManager.Instance.data.Value.end_date - DateTime.Now);
//
//                }).AddTo(_compositeDisposable);
//
//
//                //truck test
//                //                reward_rate[luckyBoxRowDatas.Count - 1] = 100000;
//            }
//        }
//
//        public List<RewardModel> GetRewardModels()
//        {
//            int total = 0;
//            int resultIndex = 0;
//
//            if (reward_rate == null)
//            {
//                Refresh();
//            }
//            
//            for (int i = 0; i < reward_rate.Length; i++)
//            {
//                total += reward_rate[i];
//            }
//
//            List<RewardModel> rewardModels = new List<RewardModel>();
//            List<int> indexList = new List<int>();
//
//            for (int j = 0; j < 3; j++)
//            {
//                int random = Random.Range(0, total);
//                int rate = 0;
//                
//                for (int i = 0; i < reward_rate.Length; i++)
//                {
//                    if (indexList.Contains(i))
//                    {
//                        continue;
//                    }
//                    
//                    rate += reward_rate[i];
//                
//                    if (random < rate)
//                    {
//                        resultIndex = i;
//                        break;
//                    }
//                }
//            
//                indexList.Add(resultIndex);
//                total -= reward_rate[resultIndex];
//                RewardModel rewardModel = new RewardModel(data.reward_type[resultIndex].type, data.reward_count[resultIndex]);
//
//                if (rewardModel.type.Value == RewardData.eType.gold)
//                {
//                    rewardModel.count.Value = data.reward_count[resultIndex] * (int)UserDataManager.Instance.GetLevelMag().lucky_gold;
//                }
//
//                rewardModels.Add(rewardModel);
//            }
//            
//            return rewardModels;
//        }
//
//        
//        public int GetNumberOfCells(EnhancedScroller scroller)
//        {
//            return Mathf.CeilToInt(luckyBoxRowDatas.Count / (float)3);
//        }
//
//        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
//        {
//            return 110 + 12;
//        }
//
//        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
//        {
//            Popup_LuckyBoxCellView cellView = scroller.GetCellView(luckyBoxCellViewPrefab) as Popup_LuckyBoxCellView;
//            cellView.name = "Cell " + dataIndex * 3 + " to " + (dataIndex * 3 + 3 - 1);
//            cellView.gameObject.SetActive(true);
////            cellView.SetData(luckyBoxRowDatas, cellIndex * 3);
//
//            return cellView;
//        }
//
//        private void CellViewVisibilityChanged(EnhancedScrollerCellView cellView)
//        {
//            // cast the cell view to our custom view
//            Popup_LuckyBoxCellView view = cellView as Popup_LuckyBoxCellView;
//
//            // if the cell is active, we set its data, 
//            // otherwise we will clear the image back to 
//            // its default state
//            if (cellView.active)
//            {
//                cellView.gameObject.SetActive(true);
//                view.SetData(luckyBoxRowDatas, view.dataIndex * 3);
//            }
//                
//        }
//
//    }
//    
//    public class LuckyBoxRowData
//    {
//        public RewardData.eType type;
//        public int count;
//    }
//        
//}

