using System;
using DatasTypes;
using DG.Tweening;
using TMPro;
using Trucking.Ad;
using Trucking.Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Popup
{
    public class Popup_Common : Popup_Base<Popup_Common>
    {
        public Button btnLeft;
        public Button btnRight;
        public Button btnAD;

        public TextMeshProUGUI txtTitle;
        public TextMeshProUGUI txtDesc;
        public TextMeshProUGUI txtDescBig;
        public TextMeshProUGUI txtLeft;
        public TextMeshProUGUI txtRight;
        public TextMeshProUGUI txtResource;
        public TextMeshProUGUI txtAd;

        public Image imgLeft;
        public Image imgRight;
        public Image imgLeftGray;
        public Image imgRightGray;
        public Image imgAdGray;
        public Image imgResource;

        public GameObject formNormal;
        public GameObject formBig;

        public enum ButtonColor
        {
            Blue,
            Red,
            Green,
            Yellow,
            Orenge
        };

        protected Action onClickLeft;

        protected Action onClickRight;

//        protected Action onClickClose;
        protected Action onClickAD;

        protected bool cantBtnLeft;
        protected bool cantBtnRight;

        protected CompositeDisposable _compositeDisposable = new CompositeDisposable();

        private void Start()
        {
            btnLeft.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (!cantBtnLeft)
                    {
                        AudioManager.Instance.PlaySound("sfx_button_main");
                        GameManager.Instance.fsm.PopState();
                        onClickLeft?.Invoke();
                    }
                    else
                    {
                        AudioManager.Instance.PlaySound("sfx_require");
                        btnLeft.transform.DOShakePosition(0.3f, 3);
                    }
                }).AddTo(this);

            btnRight.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (!cantBtnRight)
                    {
                        AudioManager.Instance.PlaySound("sfx_button_main");

                        GameManager.Instance.fsm.PopState();
                        onClickRight?.Invoke();
                    }
                    else
                    {
                        AudioManager.Instance.PlaySound("sfx_require");
                        btnRight.transform.DOShakePosition(0.3f, 3);
                    }
                }).AddTo(this);

            btnAD.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");

                    GameManager.Instance.fsm.PopState();
                    onClickAD?.Invoke();
                }).AddTo(this);
        }

        public Popup_Common Show(string strTitle, string strDesc, bool hasCloseButton = true,
            Action _onClickClose = null)
        {
            base.Show();
            _compositeDisposable.Clear();

            OnClose = _onClickClose;
            txtTitle.text = strTitle;
            txtDesc.text = strDesc;
            txtDescBig.text = strDesc;

            txtResource.transform.parent.gameObject.SetActive(false);
            btnLeft.gameObject.SetActive(false);
            btnRight.gameObject.SetActive(false);
            btnAD.gameObject.SetActive(false);
            btnCloseX.gameObject.SetActive(hasCloseButton);
            cantBtnLeft = false;
            cantBtnRight = false;
            contents.GetComponent<RectTransform>().sizeDelta = new Vector2(700, 440);
            formNormal.SetActive(true);
            formBig.SetActive(false);

            return this;
        }

        public Popup_Common SetBigSize(int w, int h)
        {
            formNormal.SetActive(false);
            formBig.SetActive(true);
            contents.GetComponent<RectTransform>().sizeDelta = new Vector2(w, h);

            return this;
        }


        public Popup_Common SetLeft(string strLeft, ButtonColor btnColor = ButtonColor.Blue, Action _onClickLeft = null)
        {
            onClickLeft = _onClickLeft;
            txtLeft.color = Color.white;
            txtLeft.text = strLeft;
            txtLeft.transform.localPosition = Vector3.zero;
            imgLeft.gameObject.SetActive(false);
            imgLeftGray.gameObject.SetActive(false);
            btnLeft.gameObject.SetActive(true);

            SetButtonColor(btnLeft, btnColor);

            return this;
        }

        public Popup_Common SetRight(string strRight, ButtonColor btnColor = ButtonColor.Blue,
            Action _onClickRight = null)
        {
            onClickRight = _onClickRight;
            txtRight.color = Color.white;
            txtRight.text = strRight;
            txtRight.transform.localPosition = Vector3.zero;
            imgRight.gameObject.SetActive(false);
            imgRightGray.gameObject.SetActive(false);
            btnRight.gameObject.SetActive(true);
            SetButtonColor(btnRight, btnColor);

            return this;
        }

        public Popup_Common SetCenter(string strCenter, ButtonColor btnColor = ButtonColor.Blue,
            Action _onClickCenter = null)
        {
            return SetLeft(strCenter, btnColor, _onClickCenter);
        }

        public Popup_Common SetAD(Action _onClickAD)
        {
            onClickAD = _onClickAD;

            btnAD.gameObject.SetActive(true);

            AdManager.Instance.IsLoadedReward.Subscribe(load =>
            {
                imgAdGray.gameObject.SetActive(!load);
                txtAd.gameObject.SetActive(!load);
            }).AddTo(_compositeDisposable);

            return this;
        }

        public Popup_Common SetLeftReward(RewardData.eType type, long count, Action _onClickLeft)
        {
            onClickLeft = _onClickLeft;

            btnLeft.gameObject.SetActive(true);
            imgLeft.gameObject.SetActive(true);
            imgLeft.sprite = GameManager.Instance.GetRewardImage(type);
            txtLeft.text = Utilities.GetNumberKKK(count);
            txtLeft.color = Color.white;
            LayoutRebuilder.ForceRebuildLayoutImmediate(txtLeft.GetComponent<RectTransform>());

            switch (type)
            {
                case RewardData.eType.gold:
                    btnLeft.GetComponent<Image>().sprite = GameManager.Instance.atlasUI.GetSprite("button_edit_y");

                    UserDataManager.Instance.data.gold.Subscribe(gold =>
                    {
                        cantBtnLeft = gold < count;
                        txtLeft.color = gold < count ? Color.red : Color.white;
                        imgLeftGray.gameObject.SetActive(gold < count);
                    }).AddTo(_compositeDisposable);
                    break;
                case RewardData.eType.cash:
                    btnLeft.GetComponent<Image>().sprite = GameManager.Instance.atlasUI.GetSprite("button_edit_g");

                    UserDataManager.Instance.data.cash.Subscribe(cash =>
                    {
                        cantBtnLeft = cash < count;
                        txtLeft.color = cash < count ? Color.red : Color.white;
                        imgLeftGray.gameObject.SetActive(cash < count);
                    }).AddTo(_compositeDisposable);
                    break;
            }

            return this;
        }


        public Popup_Common SetRightReward(RewardData.eType type, long count, Action _onClickRight)
        {
            onClickRight = _onClickRight;
            btnRight.gameObject.SetActive(true);
            imgRight.gameObject.SetActive(true);
            imgRight.sprite = GameManager.Instance.GetRewardImage(type);
            txtRight.text = Utilities.GetNumberKKK(count);
            txtRight.color = Color.white;
            LayoutRebuilder.ForceRebuildLayoutImmediate(txtRight.GetComponent<RectTransform>());

            switch (type)
            {
                case RewardData.eType.gold:
                    btnRight.GetComponent<Image>().sprite = GameManager.Instance.atlasUI.GetSprite("button_edit_y");

                    UserDataManager.Instance.data.gold.Subscribe(gold =>
                    {
                        cantBtnRight = gold < count;
                        txtRight.color = gold < count ? Color.red : Color.white;
                        imgRightGray.gameObject.SetActive(gold < count);
                    }).AddTo(_compositeDisposable);

                    break;
                case RewardData.eType.cash:
                    btnRight.GetComponent<Image>().sprite = GameManager.Instance.atlasUI.GetSprite("button_edit_g");

                    UserDataManager.Instance.data.cash.Subscribe(cash =>
                    {
                        cantBtnRight = cash < count;
                        txtRight.color = cash < count ? Color.red : Color.white;
                        imgRightGray.gameObject.SetActive(cash < count);
                    }).AddTo(_compositeDisposable);
                    break;
            }

            return this;
        }


        public Popup_Common SetCenterReward(RewardData.eType type, long count, Action _onClickCenter)
        {
            return SetLeftReward(type, count, _onClickCenter);
        }

        public Popup_Common SetResource(RewardData.eType type, long count)
        {
            imgResource.sprite = GameManager.Instance.GetRewardImage(type);
            txtResource.transform.parent.gameObject.SetActive(true);
            txtResource.text = Utilities.GetThousandCommaText(count);
            txtResource.color = Color.white;

            return this;
        }

        void SetButtonColor(Button btn, ButtonColor btnColor)
        {
            switch (btnColor)
            {
                case ButtonColor.Blue:
                    btn.GetComponent<Image>().sprite = GameManager.Instance.atlasUI.GetSprite("button_edit_b");
                    break;
                case ButtonColor.Red:
                    btn.GetComponent<Image>().sprite = GameManager.Instance.atlasUI.GetSprite("button_edit_r");
                    break;
                case ButtonColor.Green:
                    btn.GetComponent<Image>().sprite = GameManager.Instance.atlasUI.GetSprite("button_edit_g");
                    break;
                case ButtonColor.Yellow:
                    btn.GetComponent<Image>().sprite = GameManager.Instance.atlasUI.GetSprite("button_edit_y");
                    break;
                case ButtonColor.Orenge:
                    btn.GetComponent<Image>().sprite = GameManager.Instance.atlasUI.GetSprite("button_edit_o");
                    break;
            }
        }

        public override void BackKey()
        {
            if (btnCloseX.gameObject.activeSelf)
            {
                btnCloseX.onClick.Invoke();
            }
        }
    }
}