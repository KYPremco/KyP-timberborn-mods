using BepInEx;
using BepInEx.Logging;
using DraggableUtils.Configurators;
using TimberbornAPI;
using TimberbornAPI.Common;

namespace DraggableUtils
{
    [BepInPlugin("com.kyp.plugin.draggableutils", "Draggable Utilities", "1.4.0")]
    [BepInDependency("com.timberapi.timberapi")]
    public class DraggableUtilsPlugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        
        private void Awake()
        {
            Log = Logger;
            Logger.LogInfo($"DraggableUtils is loaded!");
            TimberAPI.AssetRegistry.AddSceneAssets("DraggableUtils", SceneEntryPoint.InGame, new [] { "Assets" });
            TimberAPI.DependencyRegistry.AddConfigurator(new DraggableUtilsConfigurator());
        }
    }
}
