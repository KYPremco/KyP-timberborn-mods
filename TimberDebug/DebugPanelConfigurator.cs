using Bindito.Core;
using HarmonyLib;
using Timberborn.CoreUI;
using Timberborn.MainMenuScene;
using Timberborn.Options;
using TimberbornAPI.UIBuilderSystem;
using TimberbornAPI.UIBuilderSystem.ElementSystem;
using TimberDebug.DebugPanels;
using UnityEngine.UIElements;

namespace TimberDebug
{
    public class DebugPanelConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.MultiBind<IDebugPanel>().To<SpriteExportPanel>().AsSingleton();
            containerDefinition.Bind<DebugPanel>().AsSingleton();
        }
    }
    
    [HarmonyPatch(typeof(OptionsBox), "GetPanel")]
    public static class InGamePanelPatch
    {
        private static void Postfix(ref VisualElement __result)
        {
            
            ElementFactory elementFactory = new ElementFactory();
            VisualElement root = __result.Query("OptionsBox");
            Button button = elementFactory.Button(new[] {"menu-button"}, "UI Previews");
            button.text = "Debug panel";
            button.clicked += DebugPanel.OpenPreviewBoxDelegate;
            root.Insert(6, button);
        }
    }
    
    [HarmonyPatch(typeof(MainMenuPanel), "GetPanel")]
    public static class MainMenuPanelPatch
    {
        private static void Postfix(ref VisualElement __result)
        {
            ElementFactory elementFactory = new ElementFactory();
            VisualElement root = __result.Query("MainMenuPanel");
            Button button = elementFactory.Button(new[] {"menu-button"}, "UI Previews");
            button.text = "Debug panel";
            button.clicked += DebugPanel.OpenPreviewBoxDelegate;
            root.Insert(6, button);
        }
    }
}