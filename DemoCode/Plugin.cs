using BepInEx;
using BepInEx.Logging;
using CustomAssetLoader.ModPluginSystem;
using CustomAssetLoader.RegisterSystem;
using HarmonyLib;

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
            
            AssetRegisterService.RegisterAssets("com.kyp.plugin.demo", "Demo", new []{ "Assets" }, EScene.Global);
            AssetRegisterService.RegisterAssets("com.kyp.plugin.demo", "Demo", new []{ "Assets" }, EScene.MainMenu);
            AssetRegisterService.RegisterAssets("com.kyp.plugin.demo", "Demo", new []{ "Assets" }, EScene.MapEditor);
            AssetRegisterService.RegisterAssets("com.kyp.plugin.demo", "Demo", new []{ "Assets" }, EScene.InGame);
        }
    }
}
