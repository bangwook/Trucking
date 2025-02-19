using System.Collections.Generic;
using DG.Tweening;
using EnhancedUI.EnhancedScroller;
using TMPro;
using Trucking.Common;
using Trucking.Model;
using Trucking.UI.CellView;
using Trucking.UI.Popup;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI
{
    public class EditView_AddTruck : MonoBehaviour, IEnhancedScrollerDelegate
    {
        public EnhancedScroller scroller;
        public Button btnClose;
        public TextMeshProUGUI title;

        private List<EditCellData> _data = new List<EditCellData>();
        private int selectedCellIndex;
        
        private void Start()
        {
            scroller.Delegate = this;
            scroller.cellViewVisibilityChanged = CellViewVisibilityChanged;
        }

        public void Show()
        {
            GameManager.Instance.fsm.PushState(GameManager.Instance.addTruckState);
            
            
            gameObject.SetActive(true);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(-600, GetComponent<RectTransform>().anchoredPosition.y);
            GetComponent<RectTransform>().DOAnchorPosX(0, 0.3f);

            MakeList();
        }

        public void MakeList()
        {
            selectedCellIndex = -1;
            _data.Clear();
            
            foreach (var tr in GameManager.Instance.trucks)
            {
                if (tr.currentStation.Value == null
                    && !tr.model.hasRoute.Value)
                {
                    EditCellData changeCell = new EditCellData();
                    changeCell.truck = tr;
                    changeCell.type = EditCellData.EditCellType.TRUCK;

                    _data.Add(changeCell);    
                }
            }
            
//            // New Truck cell
//            EditCellData cellAddTruck = new EditCellData();
//            cellAddTruck.type = EditCellData.EditCellType.NEW_TRUCK;
//            _data.Add(cellAddTruck);    
            
            scroller.ReloadData();
        }

        public void Close()
        {
            GetComponent<RectTransform>().DOAnchorPosX(-600, 0.3f).OnComplete(() =>
            {
                gameObject.SetActive(false);
                scroller.ClearAll();
            });            
        }

        public void BackKey()
        {
            Popup_Common.Instance.Show(Utilities.GetStringByData(20064), 
                Utilities.GetStringByData(30011), false)
                .SetLeft(Utilities.GetStringByData(20066))
                .SetRight(Utilities.GetStringByData(20065), Popup_Common.ButtonColor.Blue, () =>
            {
                GameManager.Instance.fsm.PopState();
                EditView.Instance.MakeList();
                EditView.Instance.SelectTruckLane(null);
            });
        }

        public void OnCellRefresh(EditCellView cell)
        {
            cell.HideAllButton();
            
            switch (cell.data.type)
            {
                case EditCellData.EditCellType.TRUCK:
                    cell.goMaxSpec.SetActive(true);
                    
                    if (cell.dataIndex == selectedCellIndex)
                    {
                        cell.btnConfirm.gameObject.SetActive(true);
                        cell.btnTruck.GetComponent<RectTransform>().DOAnchorPosX(15, 0.3f);
                    }
                    else
                    {
                        cell.btnTruck.GetComponent<RectTransform>().DOAnchorPosX(0, 0.3f);
                    }
                    break;
                case EditCellData.EditCellType.ADD_ROUTE:
                    
                    break;
//                case EditCellData.EditCellType.ADD_TRUCK:
//                    break;
                case EditCellData.EditCellType.NEW_TRUCK:
                    break;
            }
        }

        public void OnClickItem(EditCellView cell)
        {
            AudioManager.Instance.PlaySound("sfx_button_main");

            selectedCellIndex = cell.dataIndex;

            for (int i = scroller.StartCellViewIndex; i < _data.Count; i++)
            {
                EnhancedScrollerCellView cellView = scroller.GetCellViewAtDataIndex(i);
                EditCellView view = cellView as EditCellView;

                if (view != null)
                {
                    OnCellRefresh(view);    
                }
            }

            UIToastMassage.Instance.Show(30045, true);
//            if (selectedCellIndex <= _data.Count - 1)
//            {
//                    
//            }
//            else
//            {
//                // new truck
//                CraftView.Instance.Show(CraftView.Type.Pieces);
//                UIToastMassage.Instance.Hide();
//            }
        }

        public void SelectTruck(Truck tr)
        {
            for (int i = 0; i < _data.Count; i++)
            {
                if (_data[i].truck == tr)
                {
                    scroller.JumpToDataIndex(i);
                    OnClickItem(scroller.GetCellViewAtDataIndex(i) as EditCellView);
                    break;
                }
                
            }
        }
        
        public void OnClickConfirm(EditCellView cell)
        {
//            _data[selectedCellIndex].truck.model.hasRoute.Value = true;
//            EditView.Instance.MakeList();
            EditView.Instance.SelectTruckCell(_data[selectedCellIndex].truck);

            GameManager.Instance.fsm.PopState();
            EditView.Instance.ShowTween(false);
            EditView.Instance.editVeiwAllocate.Show(_data[selectedCellIndex]);
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _data.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 104;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            EditCellView cellView = scroller.GetCellView(GameManager.Instance.editCellViewPrefab) as EditCellView;
            cellView.name = "Cell " + dataIndex;

            return cellView;
        }
        
        private void CellViewVisibilityChanged(EnhancedScrollerCellView cellView)
        {
            // cast the cell view to our custom view
            EditCellView view = cellView as EditCellView;

            // if the cell is active, we set its data, 
            // otherwise we will clear the image back to 
            // its default state

            if (cellView.active)
            {
                view.SetData(_data[cellView.dataIndex], 
                    OnCellRefresh,
                    OnClickItem,
                    OnClickConfirm);
                OnCellRefresh(view);
            }
                
        }
    }
}