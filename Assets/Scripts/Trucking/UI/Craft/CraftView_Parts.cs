using System;
using EnhancedUI.EnhancedScroller;
using TMPro;
using Trucking.Common;
using Trucking.Model;
using UniRx;
using UnityEngine;

namespace Trucking.UI.Craft
{
    public class CraftView_Parts : MonoBehaviour, IEnhancedScrollerDelegate
    {
        public EnhancedScroller scroller;
        public EnhancedScrollerCellView partCellViewPrefab;
        public Transform trsParts;
        public Transform trsMaterial;

        private CompositeDisposable _disposable = new CompositeDisposable();

        public void Show(int index = 0)
        {
            gameObject.SetActive(true);

            if (scroller.Delegate == null)
            {
                scroller.Delegate = this;
                scroller.cellViewVisibilityChanged = CellViewVisibilityChanged;
            }

            scroller.ReloadData();
            scroller.JumpToDataIndex(index, 0.5f, 0.5f);

            for (int i = 0; i < UserDataManager.Instance.data.truckParts.Count; i++)
            {
                int partsIndex = i;
                UserDataManager.Instance.data.truckParts[i].Subscribe(part =>
                {
                    trsParts.GetChild(partsIndex).GetComponentInChildren<TextMeshProUGUI>().text =
                        Utilities.GetThousandCommaText(part);
                }).AddTo(_disposable);
            }

            for (int i = 0; i < UserDataManager.Instance.data.cityMaterials.Count; i++)
            {
                int matIndex = i;
                UserDataManager.Instance.data.cityMaterials[i].Subscribe(mat =>
                {
                    trsMaterial.GetChild(matIndex).GetComponentInChildren<TextMeshProUGUI>().text =
                        Utilities.GetThousandCommaText(mat);
                }).AddTo(_disposable);
            }
        }

        public void Close()
        {
            _disposable.Clear();
            gameObject.SetActive(false);
            scroller.ClearAll();
//            for (int i = 0; i < UserDataManager.Instance.partsNoti.Count; i++)
//            {
//                UserDataManager.Instance.partsNoti[i].Value = false;
//            }
        }

        public static long GetBoostCost(City city)
        {
            long boostCost = 0;

            if (city.model.state.Value == CityModel.State.Craft)
            {
                boostCost = (int) Math.Pow((city.model.productTime.Value - DateTime.Now).TotalSeconds / 60,
                    1 / Datas.partsProduction[0].pd_boost_cash); //남은시간/60^(1/PartsProduction: pd_boost_cash)    
            }
            else if (city.model.state.Value == CityModel.State.Upgrade)
            {
                boostCost = (int) Math.Pow((city.model.productTime.Value - DateTime.Now).TotalSeconds / 60,
                    1 / Datas.partsProduction[0].up_boost_cash); //남은시간/60^(1/PartsProduction: up_boost_cash)
            }

            if (boostCost < 1)
            {
                boostCost = 1;
            }

            return boostCost;
        }

        public void OnClickCraft(CraftCellView_Part cell)
        {
        }

        public void OnClickUpgrade(CraftCellView_Part cell)
        {
        }

        public void OnClickBoost(CraftCellView_Part cell)
        {
        }


        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return Datas.partsProduction.Length;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 300;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            CraftCellView_Part cellView = scroller.GetCellView(partCellViewPrefab) as CraftCellView_Part;
            cellView.name = "Cell " + dataIndex;
            cellView.gameObject.SetActive(true);
            return cellView;
        }

        private void CellViewVisibilityChanged(EnhancedScrollerCellView cellView)
        {
            // cast the cell view to our custom view
            CraftCellView_Part view = cellView as CraftCellView_Part;

            // if the cell is active, we set its data, 
            // otherwise we will clear the image back to 
            // its default state
            if (cellView.active)
            {
                cellView.gameObject.SetActive(true);
                view.SetData(Datas.partsProduction[cellView.dataIndex],
                    OnClickCraft,
                    OnClickUpgrade,
                    OnClickBoost
                );
            }
        }
    }
}