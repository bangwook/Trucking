using DatasTypes;
using TMPro;
using Trucking.Common;
using Trucking.UI.ThreeDimensional;
using UniRx;
using UnityEngine;

namespace Trucking.UI.Popup
{
    public class Popup_PieceRewardCellView : MonoBehaviour
    {
        public TruckData data;
        public UIObject3D uiObject3D;
        public TextMeshProUGUI txtName;
        public TextMeshProUGUI truckSpeed;
        public TextMeshProUGUI truckFuel;
        public TextMeshProUGUI truckCargo;

        private CompositeDisposable disposable = new CompositeDisposable();

        public void SetData(TruckData _data)
        {
            disposable.Clear();
            data = _data;

            txtName.text = Utilities.GetStringByData(data.name_id);

            ContentLoader.LoadTruckUIObject3DAsync(data.model_h, data.model_c)
                .TakeUntilDisable(this)
                .Subscribe(tran =>
                {
                    tran.SetParent(GameManager.Instance.trsUIObject3D);
                    uiObject3D.ObjectPrefab = tran;
                    uiObject3D.ImageColor = Color.white;
                    uiObject3D.TargetOffset = new Vector2(data.offset_x2, data.offset_y2);
                    uiObject3D.TargetRotation = new Vector3(3, 140, 0);
                    uiObject3D.CameraFOV = 30;
                    uiObject3D.CameraDistance = data.cam_dis_big;
                }).AddTo(disposable);


            int speed = data.speed[0];
            int speedMax = data.speed[data.max_lv - 1];

            truckSpeed.text = $"<color=#197CB4>{speed} </color>({speedMax})";

            int fuel = data.fuel[0];
            int fuelMax = data.fuel[data.max_lv - 1];

            truckFuel.text = $"<color=#197CB4>{fuel} </color>({fuelMax})";

            int cargo = data.cargo[0];
            int cargoMax = data.cargo[data.max_lv - 1];

            truckCargo.text = $"<color=#197CB4>{cargo} </color>({cargoMax})";
        }

        private void OnDisable()
        {
            disposable.Clear();
        }
    }
}