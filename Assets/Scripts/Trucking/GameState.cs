using System;
using Trucking.Common;
using Trucking.FSM;
using Trucking.Model;
using Trucking.UI;
using Trucking.UI.Craft;
using Trucking.UI.Popup;
using Trucking.UI.Shop;
using Trucking.UI.ToolTip;
using UniRx;
using UnityEngine;

namespace Trucking
{
    public abstract class GameState : FSMState<GameManager>
    {
        virtual public void OnClickStation(GameManager game, City city)
        {
        }

        virtual public void OnClickRoad(GameManager game, Road road)
        {
        }

//    virtual public void OnClickTruck(GameManager game, Truck truck){}
        virtual public void OnBackButton(GameManager game)
        {
            game.fsm.PopState();
        }

        public override void Push(GameManager game)
        {
            UIToastMassage.Instance.Close();
        }

        public override void Pop(GameManager game)
        {
            Enter(game);
        }
    }

    public class WorldMapState : GameState
    {
        public override void Enter(GameManager game)
        {
            Debug.Log($"WorldMapState Enter =========================");
            UIMain.Instance.SetMain();

            WorldMap.Instance.SetWorldMapView();

            if (!AudioManager.Instance.IsPlaying("bgm_1"))
            {
                AudioManager.Instance.PlayMusic("bgm_1", true);
            }

            if (UserDataManager.Instance.data.levelUpReward.Value)
            {
                UnirxExtension.DateTimer(DateTime.Now.AddSeconds(0.3f)).Subscribe(_ =>
                {
                    if (game.fsm.GetCurrentState() == this)
                    {
                        Popup_LevelUp.Instance.Show();
                    }
                });
            }
        }

        public override void Execute(GameManager game)
        {
            base.Execute(game);
        }

        public override void Push(GameManager game)
        {
            base.Push(game);
            Popup_ToolTip_Boost.Instance.Close();
            Popup_ToolTip_Level.Instance.Close();
        }

//    public override void OnClickTruck(GameManager game, Truck truck)
//    {
//        Debug.Log($"WorldMapState OnClickTruck : {truck.name}");      
//        HQView.Instance.Show(truck);
//    }

        public override void OnClickRoad(GameManager game, Road road)
        {
            Debug.Log($"WorldMapState OnClickRoad : {road.name}");

            if (road?.truck.Value != null && road?.truck.Value.model.state.Value == TruckModel.State.Move)
            {
                HQView.Instance.Show(road?.truck.Value);
            }
        }


        public override void OnClickStation(GameManager game, City city)
        {
            if (city != null)
            {
                Debug.LogWarning($"WorldMapState OnClickStation : {city.name}, {city.GetHashCode()}");
                Debug.Assert(city.model != null);

                if (!city.IsOpen())
                {
                    AudioManager.Instance.PlaySound("sfx_require");
                    UIToastMassage.Instance.Show(20077);
                }
                else if (city.completeCargoModels.Count > 0)
                {
                    UIRewardManager.Instance.Show(city);
                }
                else if (city.model.state.Value == CityModel.State.Craft_Collect)
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    city.CraftCollect(true);
                }
                else if (city.model.state.Value == CityModel.State.Upgrade_Complete)
                {
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    CraftView.Instance.Show(CraftView.Type.Parts, city.productIndex);
                }
                else
                {
                    FBAnalytics.FBAnalytics.LogCityClickEvent(UserDataManager.Instance.data.lv.Value, city.name);
                    AudioManager.Instance.PlaySound("sfx_button_main");
                    WorldMap.Instance.SetCamera(city.transform.position);
                    UICityMenu.Instance.Show(city);
                }
            }
        }

        public override void OnBackButton(GameManager game)
        {
            Utilities.MoveTaskToBack();
        }
    }

    public class JobState : GameState
    {
        public override void Enter(GameManager game)
        {
            Debug.Log($"JobState Enter =========================");

            UIMain.Instance.SetBackTitle(GameManager.Instance.selectedCity.Value.name);
//        GameManager.Instance.camera.ResetCameraBoundaries();
            WorldMap.Instance.SetJobView(true);
            JobView.Instance.RefreshSelectedTruck();
        }

        public override void Execute(GameManager game)
        {
            base.Execute(game);
        }

        public override void Exit(GameManager game)
        {
            base.Exit(game);
            JobView.Instance.Close();
            WorldMap.Instance.SetJobView(false);
        }

        public override void OnClickStation(GameManager game, City city)
        {
//        Debug.Log($"JobState OnClickStation : {city.name}");
            //JobView.Instance.Button_Focus(city);
            //JobView.Instance.selectedTruck.Value.UndoPath();
            if (city != null)
            {
                JobView.Instance.Button_City(city);
            }
        }

        public override void Pop(GameManager game)
        {
            UIMain.Instance.SetBackTitle(GameManager.Instance.selectedCity.Value.name);
        }
    }

    public class MapEditState : GameState
    {
        public override void Enter(GameManager game)
        {
            Debug.Log($"MapEditState Enter =========================");
            UIMain.Instance.SetBackTitle(Utilities.GetStringByData(20005));
//            UIMain.Instance.ShowRouteCount();
            WorldMap.Instance.SetEditView(true);
            EditView.Instance.ShowTween(true);
            UIToastMassage.Instance.Show(30039, true);
        }

        public override void Exit(GameManager game)
        {
            base.Exit(game);
            EditView.Instance.Close();
            UIToastMassage.Instance.Close();
            WorldMap.Instance.SetEditView(false);
        }

        public override void Pop(GameManager game)
        {
            //base.Pop(game);
            UIMain.Instance.SetBackTitle(Utilities.GetStringByData(20005));
            EditView.Instance.SelectTruckLane(EditView.Instance.selectedTruck.Value);
        }

        public override void OnClickRoad(GameManager game, Road road)
        {
            base.OnClickRoad(game, road);

            EditView.Instance.SelectRoadCollider(road);
        }

        public override void OnClickStation(GameManager game, City city)
        {
            base.OnClickStation(game, city);

            if (city == null)
            {
                AudioManager.Instance.PlaySound("sfx_button_cancle");
                UIToastMassage.Instance.Show(30039, true);
            }

            EditView.Instance.ResetView();
        }
    }

    public class AllocateTruckState : GameState
    {
        public override void Enter(GameManager game)
        {
            Debug.Log($"AllocateTruckState Enter =========================");
            UIMain.Instance.SetBackTitle(Utilities.GetStringByData(20043));
            EditView.Instance.SelectTruckCell(EditView.Instance.selectedTruck.Value);
            EditView.Instance.editVeiwAllocate.ShowDraggableCities();
            UIToastMassage.Instance.Show(30041, true);
        }

        public override void OnClickStation(GameManager game, City city)
        {
            base.OnClickStation(game, city);

            if (city == null)
            {
                EditView.Instance.ResetView();
                UIToastMassage.Instance.Show(30041, true);
            }
        }

        public override void Exit(GameManager game)
        {
            base.Exit(game);
            EditView.Instance.editVeiwAllocate.Close();
            game.mapEditState.Enter(game);
        }

        public override void OnBackButton(GameManager game)
        {
            EditView.Instance.editVeiwAllocate.BackKey();
        }
    }

    public class ChangeTruckState : GameState
    {
        public override void Enter(GameManager game)
        {
            Debug.Log($"ChangeTruckState Enter =========================");
            UIMain.Instance.SetBackTitle(Utilities.GetStringByData(20041));
            EditView.Instance.editViewChange.MakeList();
            UIToastMassage.Instance.Show(30046, true);
        }

        public override void Exit(GameManager game)
        {
            base.Exit(game);
            EditView.Instance.editViewChange.Close();
            game.mapEditState.Enter(game);
        }

        public override void OnClickRoad(GameManager game, Road road)
        {
            base.OnClickRoad(game, road);

            if (road.truck.Value != null)
            {
                EditView.Instance.SelectTruckLane(road.truck.Value);
            }
        }
    }

    public class AddTruckState : GameState
    {
        public override void Enter(GameManager game)
        {
            Debug.Log($"AddTruckState Enter =========================");
            UIMain.Instance.SetBackTitle(Utilities.GetStringByData(20042));

            EditView.Instance.editViewAddTruck.MakeList();
            UIToastMassage.Instance.Show(30044, true);
        }

        public override void Exit(GameManager game)
        {
            base.Exit(game);
            EditView.Instance.editViewAddTruck.Close();
            game.mapEditState.Enter(game);
        }

        public override void OnBackButton(GameManager game)
        {
            EditView.Instance.editViewAddTruck.BackKey();
        }
    }

    public class SettingTruckState : GameState
    {
        public override void Enter(GameManager game)
        {
            Debug.Log($"ColorSettingTruckState Enter =========================");
            UIMain.Instance.SetBackTitle(Utilities.GetStringByData(20087));
        }

        public override void Exit(GameManager game)
        {
            base.Exit(game);
            EditView.Instance.editViewSetting.Close();
            game.mapEditState.Enter(game);
        }
    }


    public class ShopState : GameState
    {
        public override void Enter(GameManager game)
        {
            Debug.Log($"ShopState Enter =========================");
            UIMain.Instance.SetBackTitle(Utilities.GetStringByData(20032));
            ShopView.Instance.SetRefresh();
        }

        public override void Execute(GameManager game)
        {
            base.Execute(game);
        }

        public override void Exit(GameManager game)
        {
            base.Exit(game);
            ShopView.Instance.Close();
        }

        public override void OnBackButton(GameManager game)
        {
            game.fsm.PopState();
        }
    }

    public class CraftState : GameState
    {
        public override void Enter(GameManager game)
        {
            Debug.Log($"CraftState Enter =========================");
            UIMain.Instance.SetBackTitle(Utilities.GetStringByData(20128));
            CraftView.Instance.SetRefresh();
        }

        public override void Execute(GameManager game)
        {
            base.Execute(game);
        }

        public override void Exit(GameManager game)
        {
            base.Exit(game);
            CraftView.Instance.Close();
        }

        public override void OnBackButton(GameManager game)
        {
            game.fsm.PopState();
        }
    }

    public class HQState : GameState
    {
        public override void Enter(GameManager game)
        {
            Debug.Log($"HQState Enter =========================");
            UIMain.Instance.SetBackTitle(Utilities.GetStringByData(20004));
            HQView.Instance.ShowTween(true);
        }

        public override void Execute(GameManager game)
        {
            base.Execute(game);
        }

        public override void OnClickStation(GameManager game, City city)
        {
            game.worldMapState.OnClickStation(game, city);
        }

        public override void OnClickRoad(GameManager game, Road road)
        {
            if (road?.truck.Value != null)
            {
                HQView.Instance.SelectRoadCollider(road);
            }
        }

        public override void Exit(GameManager game)
        {
            base.Exit(game);
            HQView.Instance.Close();
            HQView.Instance.ShowTween(false);
        }

        public override void Push(GameManager game)
        {
            base.Push(game);
            HQView.Instance.ShowTween(false);
        }
    }

    public class AcheivementState : GameState
    {
        public override void Enter(GameManager game)
        {
            Debug.Log($"AcheivementState Enter =========================");
            UIMain.Instance.SetBackTitle(Utilities.GetStringByData(20009));
        }

        public override void Exit(GameManager game)
        {
            base.Exit(game);
            AchievementView.Instance.Close();
        }
    }

    public class BoosterState : GameState
    {
        public override void Enter(GameManager game)
        {
            Debug.Log($"BoosterState Enter =========================");
            WorldMap.Instance.gameObject.SetActive(false);
            GameManager.Instance.canvas.gameObject.SetActive(false);
            GameManager.Instance.canvasWorld.gameObject.SetActive(false);
            GameManager.Instance.trsTrucks.gameObject.SetActive(false);
        }


        public override void Exit(GameManager game)
        {
            base.Exit(game);
            WorldMap.Instance.gameObject.SetActive(true);
            GameManager.Instance.canvas.gameObject.SetActive(true);
            GameManager.Instance.canvasWorld.gameObject.SetActive(true);
            GameManager.Instance.trsTrucks.gameObject.SetActive(true);
        }

        public override void OnBackButton(GameManager game)
        {
            //nothing
        }
    }

    public class CityMenuState : GameState
    {
        public override void Enter(GameManager game)
        {
            Debug.Log($"CityMenuState Enter =========================");
            UIMain.Instance.SetBackTitle(GameManager.Instance.selectedCity.Value.name);
        }

        public override void Exit(GameManager game)
        {
            base.Exit(game);
            UICityMenu.Instance.Close();
        }

        public override void Push(GameManager game)
        {
            base.Push(game);
            UICityMenu.Instance.Close();
        }

        public override void Pop(GameManager game)
        {
            base.Pop(game);
            UICityMenu.Instance.Show(GameManager.Instance.selectedCity.Value, true);
        }

        public override void OnClickStation(GameManager game, City city)
        {
            if (city == null)
            {
                GameManager.Instance.fsm.PopState();
            }
        }
    }


    public class PopupState<T> : GameState where T : MonoSingleton<T>
    {
        private Popup_Base<T> popup;

        public PopupState(Popup_Base<T> _popup)
        {
            popup = _popup;
        }

        public override void Enter(GameManager game)
        {
            Debug.Log($"PopupState Enter =========================");
            UIMain.Instance.SetOnlyResource();
        }

        public override void Exit(GameManager game)
        {
            base.Exit(game);
            popup.Close();
        }

        public override void OnBackButton(GameManager game)
        {
            popup.BackKey();
        }
    }
}