using Trucking.Common;
using Trucking.Model;
using Trucking.UI.ThreeDimensional;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Trucking.UI.CellView
{
    public class TruckCellViewMini : MonoBehaviour
    {
        public Button btnTruck;
        public UIObject3D truckUI3D;
        public Image imgSelect;
        public Image imgTruckColor;
        public GameObject hasCompleteCargo;

        private Truck truck;
        private CompositeDisposable disposable = new CompositeDisposable();

        public void SetTruck(Truck _truck)
        {
            truck = _truck;
            hasCompleteCargo.SetActive(false);

            imgSelect.gameObject.SetActive(truck == null);
            imgTruckColor.gameObject.SetActive(truck != null);
            truckUI3D.gameObject.SetActive(truck != null);

            if (truck != null)
            {
                imgTruckColor.color = truck.color;
                hasCompleteCargo.SetActive(truck.completeCargos.Count > 0);
                ContentLoader.LoadTruckUIObject3DAsync(truck.data.model_h, truck.data.model_c)
                    .TakeUntilDisable(this)
                    .Select(tran =>
                    {
                        if (truck != null)
                        {
                            return tran;
                        }

                        return null;
                    })
                    .Subscribe(tran =>
                    {
                        if (tran != null)
                        {
                            tran.SetParent(GameManager.Instance.trsUIObject3D);
                            truckUI3D.ObjectPrefab = tran;
                            truckUI3D.ImageColor = Color.white;
                            truckUI3D.TargetOffset = new Vector2(truck.data.offset_x, truck.data.offset_y);
                            truckUI3D.TargetRotation = new Vector3(6, 155, 0);
                            truckUI3D.CameraFOV = 25;
                            truckUI3D.CameraDistance = truck.data.cam_dis; //, -26, -28.5;     
                        }
                    }).AddTo(disposable);
            }
        }

        public void SetSelect(bool isSelect)
        {
            imgSelect.gameObject.SetActive(!isSelect);
        }

        public Truck GetTruck()
        {
            return truck;
        }

        private void OnDisable()
        {
            disposable.Clear();
        }
    }
}