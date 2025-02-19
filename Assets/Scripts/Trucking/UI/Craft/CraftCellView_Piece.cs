using System;
using System.Linq;
using DatasTypes;
using EnhancedUI.EnhancedScroller;
using Newtonsoft.Json.Utilities;
using TMPro;
using Trucking.Common;
using Trucking.UI.Popup;
using Trucking.UI.ThreeDimensional;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.Craft
{
    public class CraftCellView_Piece : EnhancedScrollerCellView
    {
        public Button btnCraft;
        public Button btnAddPiece;
        public GameObject grayCraft;

        public TextMeshProUGUI txtName;
        public TextMeshProUGUI txtGrade;
        public TextMeshProUGUI txtGrade2;
        public TextMeshProUGUI truckSpeed;
        public TextMeshProUGUI truckSpeed2;
        public TextMeshProUGUI truckFuel;
        public TextMeshProUGUI truckFuel2;
        public TextMeshProUGUI truckCargo;
        public TextMeshProUGUI truckCargo2;
        public TextMeshProUGUI txtPiece;
        public UIObject3D truckUI3D;
        public GameObject noti;

        [HideInInspector] public TruckData data;

        private Action<CraftCellView_Piece> OnClickCraft;
        private CompositeDisposable _disposable = new CompositeDisposable();
        public int dataID;

        private void Start()
        {
            btnCraft.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (UserDataManager.Instance.data.truckPieces[dataID].Value < Datas.truckData[dataID].pieces)
                    {
                        Popup_GuideMain.Instance.Show(9);
                    }
                    else
                    {
                        OnClickCraft?.Invoke(this);
                    }
                })
                .AddTo(this);

            btnAddPiece.OnClickAsObservable()
                .Subscribe(_ => { CraftView.Instance.Show(CraftView.Type.Crates); })
                .AddTo(this);

            _disposable.AddTo(this);
        }

        private void OnDisable()
        {
            UserDataManager.Instance.pieceNoti[dataID].Value = false;
            _disposable.Clear();
        }


        public void SetData(TruckData _data, Action<CraftCellView_Piece> _onClickCraft
        )
        {
            _disposable.Clear();
            OnClickCraft = _onClickCraft;
            data = _data;
            dataID = Array.IndexOf(Datas.truckData.ToArray(), data);

            txtName.text = Utilities.GetStringByData(data.name_id);
            txtGrade.text = "1";
            txtGrade2.text = data.max_lv.ToString();

            ContentLoader.LoadTruckUIObject3DAsync(data.model_h, data.model_c)
                .TakeUntilDisable(this)
                .Subscribe(tran =>
                {
                    tran.SetParent(GameManager.Instance.trsUIObject3D);
                    truckUI3D.ObjectPrefab = tran;
                    truckUI3D.ImageColor = Color.white;
                    truckUI3D.TargetOffset = new Vector2(data.offset_x2, data.offset_y2);
                    truckUI3D.TargetRotation = new Vector3(3, 140, 0);
                    truckUI3D.CameraFOV = 30;
                    truckUI3D.CameraDistance = data.cam_dis_big;
                }).AddTo(_disposable);

            UserDataManager.Instance.data.truckPieces[dataID].Subscribe(piece =>
            {
                grayCraft.SetActive(piece < Datas.truckData[dataID].pieces);
                txtPiece.text = $"{piece} / {Datas.truckData[dataID].pieces}";

                if (piece < Datas.truckData[dataID].pieces)
                {
                    btnCraft.GetComponentInChildren<TextMeshProUGUI>().text = Utilities.GetStringByData(20186);
                }
                else
                {
                    btnCraft.GetComponentInChildren<TextMeshProUGUI>().text = Utilities.GetStringByData(20128);
                }
            }).AddTo(_disposable);

            UserDataManager.Instance.pieceNoti[dataID].Subscribe(_noti => { noti.SetActive(_noti); })
                .AddTo(_disposable);

            int speed = data.speed[0];
            int speedMax = data.speed[data.max_lv - 1];

            truckSpeed.text = speed.ToString();
            truckSpeed2.text = speedMax.ToString();

            int fuel = data.fuel[0];
            int fuelMax = data.fuel[data.max_lv - 1];

            truckFuel.text = fuel.ToString();
            truckFuel2.text = fuelMax.ToString();

            int cargo = data.cargo[0];
            int cargoMax = data.cargo[data.max_lv - 1];

            truckCargo.text = cargo.ToString();
            truckCargo2.text = cargoMax.ToString();

            txtPiece.text = $"{UserDataManager.Instance.data.truckPieces[dataID]} / {data.pieces}";
        }
    }
}