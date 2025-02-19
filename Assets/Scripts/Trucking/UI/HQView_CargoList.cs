using System.Collections.Generic;
using System.Linq;
using DatasTypes;
using DG.Tweening;
using EnhancedUI.EnhancedScroller;
using TMPro;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.CellView;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI
{
    public class HQView_CargoList : MonoSingleton<HQView_CargoList>, IEnhancedScrollerDelegate
    {
        public TextMeshProUGUI txtTruckRewardCash;
        public TextMeshProUGUI txtTruckRewardGold;
        public TextMeshProUGUI txtTruckRewardCrate;
        public TextMeshProUGUI txtTruckRewardMaterial;
        public Image imgTruckRewardMaterial;
        
        public EnhancedScroller scroller;
        private List<CargoCellData> _cargoCellDatas = new List<CargoCellData>();
        
        private CompositeDisposable _disposable = new CompositeDisposable();        
        private ReactiveProperty<Truck> truck = new ReactiveProperty<Truck>();

        private void Start()
        {
            scroller.Delegate = this;
            scroller.cellViewVisibilityChanged = CellViewVisibilityChanged;
            
            truck.Subscribe(tr =>
            {
                _disposable.Clear();
                _cargoCellDatas.Clear();
                
                if (tr != null)
                {
                    tr.model.state.Subscribe(value =>
                    {
                        if (value == TruckModel.State.Wait)
                        {
//                            HQView.Instance.Show(tr);
                            Close();    
                        }
                    }).AddTo(_disposable);
                    
                    foreach (var cargo in truck.Value.completeCargos)
                    {
                        CargoCellData cell = new CargoCellData();
                        cell.cargo = cargo;
                        cell.type = CargoCellData.CargoCellType.CARGO;
                        _cargoCellDatas.Add(cell);
                    }
                    
                    foreach (var cargo in truck.Value.cargos)
                    {
                        CargoCellData cell = new CargoCellData();
                        cell.cargo = cargo;
                        cell.type = CargoCellData.CargoCellType.CARGO;
                        _cargoCellDatas.Add(cell);
                    }
                    
                    scroller.ReloadData();
                    
                    UpdateTruckText();
                }
                else
                {
                    scroller.ClearAll();
                }
            }).AddTo(this);
        }

        public void Show(Truck _truck)
        {
            if (_truck.model.state.Value == TruckModel.State.Move)
            {
                gameObject.SetActive(true);
                truck.Value = _truck;
            
                GetComponent<RectTransform>().DOAnchorPosX(-5, 0.3f);
            }
        }

        public void Close()
        {
            if (gameObject.activeSelf)
            {   
                GetComponent<RectTransform>().DOAnchorPosX(300, 0.3f)
                    .OnComplete(() =>
                    {
                        truck.Value = null;
                        gameObject.SetActive(false);
                    });
            }
        }

        void UpdateTruckText()
        {
            var cargoModels = _cargoCellDatas.Where(x => truck.Value.pathStation.IndexOf(x.cargo.to.Value) >= 0).Select(x => x.cargo.model);
            
            long gold = cargoModels.Sum(x => x.rewardModels.Where(y => y.type.Value == RewardData.eType.gold).Sum(y => y.count.Value));
            long cash = cargoModels.Sum(x => x.rewardModels.Where(y => y.type.Value == RewardData.eType.cash).Sum(y => y.count.Value));
            long material = cargoModels.Sum(x => x.rewardModels.Where(y => y.type.Value == RewardData.eType.material).Sum(y => y.count.Value));
            long crate = cargoModels.Sum(x => x.rewardModels.Where(y => y.type.Value == RewardData.eType.crate).Sum(y => y.count.Value));
            
            txtTruckRewardGold.text = Utilities.GetNumberKKK(gold);
            txtTruckRewardCash.text = Utilities.GetNumberKKK(cash);
            txtTruckRewardMaterial.text = Utilities.GetNumberKKK(material);
            txtTruckRewardCrate.text = Utilities.GetNumberKKK(crate);

            if (material > 0)
            {
                int index = cargoModels.FirstOrDefault(x => x.rewardModels.FirstOrDefault(y => y.type.Value == RewardData.eType.material) != null)
                    .rewardModels.FirstOrDefault(x => x.type.Value == RewardData.eType.material).index.Value;
                imgTruckRewardMaterial.GetComponent<Image>().sprite =
                    GameManager.Instance.GetRewardImage(RewardData.eType.material, index);
            }
            else
            {
                imgTruckRewardMaterial.GetComponent<Image>().sprite = GameManager.Instance.atlasUI.GetSprite("goods_parts");
            }
        }

        #region cellEvent
        
        void OnCellRefresh(CargoCellView cell)
        {
            if (cell == null)
            {
                return;
            }

            if (cell.data.type == CargoCellData.CargoCellType.CARGO)
            {
                cell.targetCity.color = Utilities.GetColorByHtmlString("#7F7F7F");
                
                if (truck.Value != null)
                {
                    List<City> linkedCities = GameManager.Instance.FindLinkedStationWithTruck(truck.Value);
                    
                    foreach (var ct in linkedCities)
                    {
                        if (cell.data.cargo.to.Value == ct)
                        {
                            cell.targetCity.color = Utilities.GetColorByHtmlString("#FF9200");
                            break;
                        }
                    }
                }                
            }
        }
        


        #endregion
        
        

        #region scroll

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _cargoCellDatas.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 96;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            CargoCellView cellView = scroller.GetCellView(GameManager.Instance.cargoCellViewPrefab) as CargoCellView;
            cellView.gameObject.name = "Cell " + dataIndex;

            return cellView;
        }
        
        private void CellViewVisibilityChanged(EnhancedScrollerCellView cellView)
        {
            // cast the cell view to our custom view
            CargoCellView view = cellView as CargoCellView;

            // if the cell is active, we set its data, 
            // otherwise we will clear the image back to 
            // its default state
            if (cellView.active)
            {
                view.SetData(_cargoCellDatas[cellView.dataIndex], 
                    truck.Value,
                    null,
                    null,
                    OnCellRefresh);
                
                OnCellRefresh(view);
            }
                
        }

        #endregion
        
    }
}