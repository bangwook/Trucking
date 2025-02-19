using System.Collections.Generic;
using System.Linq;
using DatasTypes;
using EnhancedUI.EnhancedScroller;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.Popup;
using UnityEngine;

namespace Trucking.UI.Craft
{
    public class CraftView_Pieces : MonoBehaviour, IEnhancedScrollerDelegate
    {
        public EnhancedScroller scroller;
        public EnhancedScrollerCellView pieceCellViewPrefab;

        private List<TruckData> datas = new List<TruckData>();

        public void Show()
        {
            gameObject.SetActive(true);

            if (scroller.Delegate == null)
            {
                scroller.Delegate = this;
                scroller.cellViewVisibilityChanged = CellViewVisibilityChanged;
            }

            for (int i = 0; i < Datas.truckData.Length; i++)
            {
                Truck truck = GameManager.Instance.trucks.ToArray().FirstOrDefault(x => x.data == Datas.truckData[i]);

                if (truck != null || UserDataManager.Instance.data.truckPiecesUnlock[i].Value)
                {
                    datas.Add(Datas.truckData[i]);
                }
            }

            scroller.ReloadData();
        }

        public void Close()
        {
            gameObject.SetActive(false);
            datas?.Clear();
            scroller.ClearAll();
//            for (int i = 0; i < UserDataManager.Instance.pieceNoti.Count; i++)
//            {
//                UserDataManager.Instance.pieceNoti[i].Value = false;
//            }
        }

        public void OnClickCraft(CraftCellView_Piece cell)
        {
            if (UserDataManager.Instance.UsePiece(cell.dataID, Datas.truckData[cell.dataID].pieces))
            {
                AudioManager.Instance.PlaySound("sfx_button_main");
                Truck newTruck = Truck.AddNewTruck(Datas.truckData[cell.dataID].id, true);

                Popup_TruckInformation.Instance.ShowClaim(newTruck, () =>
                {
                    FBAnalytics.FBAnalytics.LogBuyTruckEvent(UserDataManager.Instance.data.lv.Value,
                        Datas.truckData[cell.dataID].id,
                        GameManager.Instance.trucks.Count,
                        GameManager.Instance.trucks.Count(x => x.model.hasRoute.Value));
                });
            }
            else
            {
                AudioManager.Instance.PlaySound("sfx_require");
            }
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return datas.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 300;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            CraftCellView_Piece cellView = scroller.GetCellView(pieceCellViewPrefab) as CraftCellView_Piece;
            cellView.name = "Cell " + dataIndex;
            cellView.gameObject.SetActive(true);
            return cellView;
        }

        private void CellViewVisibilityChanged(EnhancedScrollerCellView cellView)
        {
            // cast the cell view to our custom view
            CraftCellView_Piece view = cellView as CraftCellView_Piece;

            // if the cell is active, we set its data, 
            // otherwise we will clear the image back to 
            // its default state
            if (cellView.active)
            {
                cellView.gameObject.SetActive(true);
                view.SetData(datas[cellView.dataIndex], OnClickCraft);
            }
        }
    }
}