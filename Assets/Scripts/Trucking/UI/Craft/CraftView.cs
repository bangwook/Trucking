using System;
using System.Linq;
using DG.Tweening;
using Trucking.Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Craft
{
    public class CraftView : MonoSingleton<CraftView>
    {
        public Button btnCrate;
        public Button btnPiece;
        public Button btnPart;

        public GameObject crateGray;
        public GameObject pieceGray;
        public GameObject partGray;

        public CraftView_Crate crateView;
        public CraftView_Pieces pieceView;
        public CraftView_Parts partView;

        public Image blackPanel;
        public GameObject notiCrate;
        public GameObject notiPiece;
        public GameObject notiPart;


        public enum Type
        {
            Crates = 0,
            Pieces,
            Parts
        }

        private void Start()
        {
            btnCrate.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    SetType(Type.Crates);
                });

            btnPiece.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    SetType(Type.Pieces);
                });

            btnPart.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    SetType(Type.Parts);
                });

            UserDataManager.Instance.crateNoti.Merge().Subscribe(x =>
            {
                notiCrate.SetActive(UserDataManager.Instance.crateNoti.Count(y => y.Value) > 0);
            }).AddTo(this);

            UserDataManager.Instance.pieceNoti.Merge().Subscribe(x =>
            {
                notiPiece.SetActive(UserDataManager.Instance.pieceNoti.Count(y => y.Value) > 0);
            }).AddTo(this);

            UserDataManager.Instance.partsNoti.Merge().Subscribe(x =>
            {
                notiPart.SetActive(UserDataManager.Instance.partsNoti.Count(y => y.Value) > 0);
            }).AddTo(this);
        }

        public void Show(Type _type, int index = 0)
        {
            gameObject.SetActive(true);
            SetType(_type, index);
            GameManager.Instance.fsm.PushState(GameManager.Instance.craftState);

            blackPanel.color = new Color32(0, 0, 0, 0);
            blackPanel.DOFade(0.7f, 0.5f);
            UIToastMassage.Instance.Hide();
        }

        void SetType(Type _type, int index = 0)
        {
            crateGray.SetActive(_type != Type.Crates);
            pieceGray.SetActive(_type != Type.Pieces);
            partGray.SetActive(_type != Type.Parts);

            crateView.Close();
            pieceView.Close();
            partView.Close();

            if (_type == Type.Crates)
            {
                crateView.Show();
            }
            else if (_type == Type.Pieces)
            {
                pieceView.Show();
            }
            else if (_type == Type.Parts)
            {
                partView.Show(index);
            }
        }

        public void SetRefresh()
        {
            if (pieceView.gameObject.activeSelf)
            {
                pieceView.scroller.RefreshActiveCellViews();
            }
            else if (partView.gameObject.activeSelf)
            {
                partView.scroller.RefreshActiveCellViews();
            }
            else if (crateView.gameObject.activeSelf)
            {
                crateView.scroller.RefreshActiveCellViews();
            }
        }

        public void Close()
        {
            gameObject.SetActive(false);

            crateView.scroller.ClearAll();
            pieceView.scroller.ClearAll();
            partView.scroller.ClearAll();

/*            if (!notiCrate.activeSelf)
            {
                for (int i = 0; i < UserDataManager.Instance.crateNoti.Count; i++)
                {
                    UserDataManager.Instance.crateNoti[i].Value = false;
                }    
            }

            if (!notiPiece.activeSelf)
            {
                for (int i = 0; i < UserDataManager.Instance.pieceNoti.Count; i++)
                {
                    UserDataManager.Instance.pieceNoti[i].Value = false;
                }
            }

            if (!notiPart.activeSelf)
            {
                for (int i = 0; i < UserDataManager.Instance.partsNoti.Count; i++)
                {
                    UserDataManager.Instance.partsNoti[i].Value = false;
                }
            }*/


            GameManager.Instance.ClearUIObject3D();
            GC.Collect();
        }
    }
}