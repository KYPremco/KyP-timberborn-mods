using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using TimberbornAPI;
using TimberbornAPI.Common;

namespace DemoCode
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("com.kyp.utils.customassetloader")]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        
        private void Awake()
        {
            Log = Logger;
            new Harmony("com.kyp.plugin.demo").PatchAll();
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            
            TimberAPI.AssetRegistry.AddSceneAssets("com.kyp.plugin.demo", SceneEntryPoint.Global);
            TimberAPI.AssetRegistry.AddSceneAssets("com.kyp.plugin.demo", SceneEntryPoint.MainMenu);
            TimberAPI.AssetRegistry.AddSceneAssets("com.kyp.plugin.demo", SceneEntryPoint.MapEditor);
            TimberAPI.AssetRegistry.AddSceneAssets("com.kyp.plugin.demo", SceneEntryPoint.InGame);
        }
    }
}
