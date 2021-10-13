using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace DraggableUtils
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        
        private void Awake()
        {
            Log = Logger;
            new Harmony("com.kyp.plugin.draggableutils").PatchAll();
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
