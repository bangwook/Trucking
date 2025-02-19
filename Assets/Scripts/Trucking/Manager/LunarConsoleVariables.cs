using LunarConsolePlugin;

namespace Trucking.Manager
{
    [CVarContainer]
    public class LunarConsoleVariables
    {
        public static readonly CVar isTruckSpeedHack = new CVar("Truck Speed Hack", false);
        public static readonly CVar userLv = new CVar("User Lv", 1);
        public static readonly CVar isDeliveryHack = new CVar("Delivery Service Hack", false);
        public static readonly CVar isNewDailyMsiion = new CVar("New Daily Mission", false);
        public static readonly CVar isLevelMission = new CVar("Level Mission", false);
        public static readonly CVar isEventPack = new CVar("Event Package", false);
        public static readonly CVar isCargoSlotTime = new CVar("Cargo Slot Refresh Time Test", false);
        public static readonly CVar isPlaneCash = new CVar("Plane Cash Only", false);
        public static readonly CVar isADBooster = new CVar("Truck Boost AD", false);
        public static readonly CVar isCraft = new CVar("Part Craft", false);
        public static readonly CVar isADLoading = new CVar("AD Loading", false);
        public static readonly CVar isDouble = new CVar("Job Double", false);
        public static readonly CVar isTutorialSkip = new CVar("Tutorial Skip", false);
        public static readonly CVar isTree = new CVar("Map Tree", false);
    }
}