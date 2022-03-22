using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Timberborn.Persistence;

namespace InventoryPreset;

internal static class ModLogger
{
    public static ManualLogSource Log;
}

[BepInDependency("com.timberapi.timberapi")]
[BepInPlugin("com.kyp.plugin.science.tree", "Science tree", "1.0.0")]
public class ScienceTree : BaseUnityPlugin
{
    private void Awake()
    {
        ModLogger.Log = Logger;
        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        
        ModLogger.Log.LogFatal("AAAAAAAAAAAAAAAAAAAAAAAA");
        
        Timberborn.Attractions

        // TimberAPI.DependencyRegistry.AddConfigurator(new VisualPresetSystemConfigurator(), SceneEntryPoint.MapEditor);
        // TimberAPI.DependencyRegistry.AddConfigurator(new VisualPresetSystemConfigurator(), SceneEntryPoint.InGame);
        //
        // TimberAPI.DependencyRegistry.AddConfigurator(new InventoryPresetSystemConfigurator(), SceneEntryPoint.MapEditor);
        // TimberAPI.DependencyRegistry.AddConfigurator(new InventoryPresetSystemConfigurator(), SceneEntryPoint.InGame);
        //
        // TimberAPI.DependencyRegistry.AddConfigurator(new InventoryPresetSystemUIConfigurator(), SceneEntryPoint.MapEditor);
        // TimberAPI.DependencyRegistry.AddConfigurator(new InventoryPresetSystemUIConfigurator(), SceneEntryPoint.InGame);
    }
}