using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using Bindito.Core.Internal;
using HarmonyLib;
using InventoryPreset.InventoryPresetSystem;
using InventoryPreset.InventoryPresetSystemUI;
using InventoryPreset.VisualPresetSystem;
using TimberbornAPI;
using TimberbornAPI.Common;

namespace InventoryPreset;

internal static class ModLogger
{
    public static ManualLogSource Log;
}

[BepInDependency("com.timberapi.timberapi")]
[BepInPlugin("com.kyp.plugin.inventory.preset", "Inventory preset", "1.0.0")]
public class InventoryPresetPlugin : BaseUnityPlugin
{
    public static Container Container;
        
    private void Awake()
    {
        ModLogger.Log = Logger;
        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        
        TimberAPI.DependencyRegistry.AddConfigurator(new VisualPresetSystemConfigurator(), SceneEntryPoint.MapEditor);
        TimberAPI.DependencyRegistry.AddConfigurator(new VisualPresetSystemConfigurator(), SceneEntryPoint.InGame);
        
        TimberAPI.DependencyRegistry.AddConfigurator(new InventoryPresetSystemConfigurator(), SceneEntryPoint.MapEditor);
        TimberAPI.DependencyRegistry.AddConfigurator(new InventoryPresetSystemConfigurator(), SceneEntryPoint.InGame);
        
        TimberAPI.DependencyRegistry.AddConfigurator(new InventoryPresetSystemUIConfigurator(), SceneEntryPoint.MapEditor);
        TimberAPI.DependencyRegistry.AddConfigurator(new InventoryPresetSystemUIConfigurator(), SceneEntryPoint.InGame);
    }
}