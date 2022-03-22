using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.InventorySystemUI;
using Timberborn.WarehousesUI;
using TimberbornAPI;
using UnityEngine.UIElements;

namespace CategorizedInventory
{
    public static class ModLogger
    {
        public static ManualLogSource Log;
    }
    
    [BepInPlugin("com.kyp.plugin.categorizedinventory", "Categorized Inventory", "1.0.0")]
    public class CategorizedInventoryPlugin : BaseUnityPlugin
    {
        
        private void Awake()
        {
            Paths.Location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ModLogger.Log = Logger;
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            Logger.LogInfo($"Categorized Inventory loaded!");
            TimberAPI.DependencyRegistry.AddConfigurator(new CategorizedInventoryConfigurator());
        }

        [HarmonyPatch(typeof(StockpileInventoryFragment), "AddRows")]
        static class InfinityDepthPumps2
        {
            public static bool Prefix(StockpileInventoryFragment __instance, Inventory ____inventory, List<GoodSpecification> ____goodSpecifications, List<AdjustableRow> ____rows, VisualElement ____root, VisualElement ____content)
            {
                StockpileInventoryCategorize.AddRows(__instance, ____inventory, ____goodSpecifications, ____rows, ____root, ____content);
                return false;
            }
        }
    }
}
