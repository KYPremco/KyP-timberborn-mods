using BepInEx;
using BepInEx.Logging;
using CustomAssetLoader.RegisterSystem;
using HarmonyLib;

namespace DraggableUtils
{
    [BepInPlugin("com.kyp.plugin.draggableutils", "Draggable Utilities", "1.0.1")]
    [BepInDependency("com.kyp.utils.customassetloader")]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        
        private void Awake()
        {
            Log = Logger;
            new Harmony("com.kyp.plugin.draggableutils").PatchAll();
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            AssetRegisterService.RegisterInGameAssets("com.kyp.plugin.draggableutils", "DraggableUtils", new []{ "Assets" });
        }
    }
}
