using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using EnhancedUI.EnhancedScroller;
using Trucking.Common;
using Trucking.UI.CellView;
using Trucking.UI.Mission;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI
{
    public class AchievementView : MonoSingleton<AchievementView>, IEnhancedScrollerDelegate
    {
        public EnhancedScroller scroller;
        public EnhancedScrollerCellView cellViewPrefab;
        public Image blackPanel;

        private List<AchievementCellModel> datas = new List<AchievementCellModel>();

        private void Start()
        {
            scroller.Delegate = this;
            scroller.cellViewVisibilityChanged = CellViewVisibilityChanged;
        }

        public void Show()
        {
            GameManager.Instance.fsm.PushState(GameManager.Instance.acheivementState);
            gameObject.SetActive(true);
            
            datas.Clear();
            datas = AchievementManager.Instance.model.model.ToList().OrderByDescending(x => x.quest.Value.IsSuccess()).ToList();
            scroller.ReloadData();
            
            blackPanel.color = new Color32(0, 0, 0, 0);
            blackPanel.DOFade(0.7f, 0.5f);
//            SetAnimation();
        }

        public void Close()
        {
            gameObject.SetActive(false);
//            GameManager.Instance.fsm.PopState();
        }
        
        void SetAnimation()
        {
            for (int i = scroller.StartCellViewIndex; i < datas.Count; i++)
            {
                EnhancedScrollerCellView cellView = scroller.GetCellViewAtDataIndex(i);

                if (cellView != null)
                {
                    cellView.transform.localScale = new Vector3(0, 1, 1);
                }
            }
            
            Observable.NextFrame().Subscribe(_ =>
            {
                for (int i = scroller.StartCellViewIndex; i < datas.Count; i++)
                {
                    EnhancedScrollerCellView cellView = scroller.GetCellViewAtDataIndex(i);

                    if (cellView != null)
                    {
                        cellView.transform.localScale = new Vector3(0, 1, 1);
                        cellView.transform.DOScaleX(1, 0.3f)
                            .SetDelay((i - scroller.StartCellViewIndex) * 0.05f);    
                    }
                }
            });
        }


        public void OnClickConfirm(AchievementCellView cell)
        {
            scroller.RefreshActiveCellViews();
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return Datas.achievementData.Length;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 300;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            AchievementCellView cellView = scroller.GetCellView(cellViewPrefab) as AchievementCellView;
            cellView.name = "Cell " + dataIndex;
            cellView.gameObject.SetActive(true);
            return cellView;
        }

        private void CellViewVisibilityChanged(EnhancedScrollerCellView cellView)
        {
            // cast the cell view to our custom view
            AchievementCellView view = cellView as AchievementCellView;

            // if the cell is active, we set its data, 
            // otherwise we will clear the image back to 
            // its default state
            if (cellView.active)
            {
                cellView.gameObject.SetActive(true);
                view.SetData(datas[view.dataIndex],
                    OnClickConfirm);

//                OnCellRefresh(view);
                

            }

        }
    }
}