using System;
using DG.Tweening;
using Trucking.Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Shop
{
    public class ShopView : MonoSingleton<ShopView>
    {
        public Button btnCrate;
        public Button btnCoin;
        public Button btnCash;
        public Button btnBooster;

        public GameObject crateGray;
        public GameObject coinGray;
        public GameObject cashGray;
        public GameObject boosterGray;

        public ShopView_Crate crateView;
        public ShopView_Coin coinView;
        public ShopView_Cash cashView;

        public Image blackPanel;

        public enum Type
        {
            Crate = 0,
            Cash,
            Coin
        }

        private void Start()
        {
            btnCrate.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    SetType(Type.Crate);
                });

            btnCoin.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    SetType(Type.Coin);
                });

            btnCash.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    SetType(Type.Cash);
                });

//            btnBooster.OnClickAsObservable()
//                .Subscribe(_ =>
//                {
//                    AudioManager.Instance.PlaySound("sfx_button_main");
//                    SetType(Type.Booster);
//                });
        }

        public void Show(Type _type)
        {
            gameObject.SetActive(true);
            SetType(_type);
            GameManager.Instance.fsm.PushState(GameManager.Instance.shopState);

            blackPanel.color = new Color32(0, 0, 0, 0);
            blackPanel.DOFade(0.7f, 0.5f);
            UIToastMassage.Instance.Hide();
        }

        void SetType(Type _type)
        {
            crateGray.SetActive(_type != Type.Crate);
            coinGray.SetActive(_type != Type.Coin);
            cashGray.SetActive(_type != Type.Cash);
//            boosterGray.SetActive(_type != Type.Booster);

            crateView.Close();
            coinView.Close();
            cashView.Close();
//            boosterView.Close();

            if (_type == Type.Crate)
            {
                crateView.Show();
            }
            else if (_type == Type.Coin)
            {
                coinView.Show();
            }
            else if (_type == Type.Cash)
            {
                cashView.Show();
            }

//            else if (_type == Type.Booster)
//            {
//                boosterView.Show();
//            }
        }

        public void SetRefresh()
        {
            if (crateView.gameObject.activeSelf)
            {
                crateView.scroller.RefreshActiveCellViews();
            }
            else if (coinView.gameObject.activeSelf)
            {
                coinView.scroller.RefreshActiveCellViews();
            }
            else if (cashView.gameObject.activeSelf)
            {
                cashView.scroller.RefreshActiveCellViews();
            }

//            else if (boosterView.gameObject.activeSelf)
//            {
//                boosterView.scroller.RefreshActiveCellViews();
//            }
        }

        public void Close()
        {
            gameObject.SetActive(false);

            crateView.scroller.ClearAll();
            coinView.scroller.ClearAll();
            cashView.scroller.ClearAll();
//            boosterView.scroller.ClearAll();

            GameManager.Instance.ClearUIObject3D();
            GC.Collect();
        }
    }
}