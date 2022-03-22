using HarmonyLib;
using InventoryPreset.InventoryPresetSystemUI;
using Timberborn.WarehousesUI;
using TimberbornAPI;
using UnityEngine;
using UnityEngine.UIElements;

namespace InventoryPreset.HarmonyPatches;

public static class StockpileInventoryFragmentPatch
{
    private static InventoryAdditionFragment InventoryAdditionFragment => TimberAPI.DependencyContainer.GetInstance<InventoryAdditionFragment>();

    [HarmonyPatch(typeof(StockpileInventoryFragment), "InitializeFragment")]
    private static class InitializeFragment
    {
        private static void Postfix(VisualElement __result)
        {
            InventoryAdditionFragment.InitializeFragment(__result);
        }
    }
    
    [HarmonyPatch(typeof(StockpileInventoryFragment), "ShowFragment", typeof(GameObject))]
    private static class ShowFragment
    {
        private static void Postfix(GameObject entity)
        {
            InventoryAdditionFragment.ShowFragment(entity);
        }
    }
}