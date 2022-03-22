using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using Timberborn.Autosaving;
using Timberborn.GameSaveRepositorySystem;
using Timberborn.GameSaveRuntimeSystem;
using Timberborn.GameSaveRuntimeSystemUI;
using Timberborn.SettlementNameSystem;
using Timberborn.SingletonSystem;

namespace SaveConfig
{
    [BepInPlugin("com.kyp.plugin.saveconfig", "Save config", "1.2.0")]
    public class SaveConfig : BaseUnityPlugin
    {
        private static ConfigEntry<int> _autoSavesPerSettlement;
        private static ConfigEntry<float> _frequencyInMinutes;

        private void Awake()
        {
            _autoSavesPerSettlement = Config.Bind("Save settings", "SettlementSaveAmount", 3, "The amount of saves before overwriting the old one's. Min: 2, Max: 50");
            _frequencyInMinutes = Config.Bind("Save settings", "SaveFrequency", 10f, "How many minutes passes before auto saving (1.5 = 1 minute 30 seconds). Min: 1, Max: 240");
            
            new Harmony("com.kyp.plugin.saveconfig").PatchAll();
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
        
        [HarmonyPatch(typeof(Autosaver), "InjectDependencies", 
            typeof(SaveTimestampFormatter),
            typeof(GameSaver),
            typeof(EventBus),
            typeof(GameSaveRepository),
            typeof(SettlementNameService))]
        private static class AutoSavePatch
        {
            private static void Postfix(Autosaver __instance)
            {
                __instance.AutosavesPerSettlement = _autoSavesPerSettlement.Value is > 2 and < 50 ? _autoSavesPerSettlement.Value : 3;
                __instance.FrequencyInMinutes = _frequencyInMinutes.Value is > 1f and < 240f ? _frequencyInMinutes.Value : 10f;
            }
        }
    }
}
