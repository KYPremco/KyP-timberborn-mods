using System.IO;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using TimberbornAPI;
using UnityEngine;
using UnityEditor;
using TimberbornAPI.Common;

namespace TimberDebug
{
    [BepInDependency("com.timberapi.timberapi")]
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class DebugPlugin : BaseUnityPlugin
    {
        public static string FilePath = Path.Combine("TimberApi", "Debug");

        private void Awake()
        {
            TimberAPI.DependecyRegistry.AddConfigurator(new DebugPanelConfigurator(), SceneEntryPoint.Global);
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
