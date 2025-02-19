using System.Collections.Generic;
using DatasTypes;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace Trucking.UI.Craft
{
    public class CraftView_Crate : MonoBehaviour, IEnhancedScrollerDelegate
    {
        public EnhancedScroller scroller;
        public EnhancedScrollerCellView crateCellViewPrefab;

        private List<Crate> datas = new List<Crate>();

        public void Show()
        {
            gameObject.SetActive(true);

            for (int i = 0; i < Datas.crate.Length; i++)
            {
                datas.Add(Datas.crate[i]);
            }

            if (scroller.Delegate == null)
            {
                scroller.Delegate = this;
                scroller.cellViewVisibilityChanged = CellViewVisibilityChanged;
            }

            scroller.ReloadData();
        }

        public void Close()
        {
            gameObject.SetActive(false);
            datas?.Clear();
            scroller.ClearAll();

//            for (int i = 0; i < UserDataManager.Instance.crateNoti.Count; i++)
//            {
//                UserDataManager.Instance.crateNoti[i].Value = false;
//            }
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return datas.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 400;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            CraftCellView_Crate cellView = scroller.GetCellView(crateCellViewPrefab) as CraftCellView_Crate;
            cellView.name = "Cell " + dataIndex;
            cellView.gameObject.SetActive(true);

            return cellView;
        }

        private void CellViewVisibilityChanged(EnhancedScrollerCellView cellView)
        {
            // cast the cell view to our custom view
            CraftCellView_Crate view = cellView as CraftCellView_Crate;

            // if the cell is active, we set its data, 
            // otherwise we will clear the image back to 
            // its default state
            if (cellView.active)
            {
                cellView.gameObject.SetActive(true);
                view.SetData(datas[cellView.dataIndex]);
            }
        }
    }
}