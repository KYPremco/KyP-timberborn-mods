using System.Collections.Generic;
using System.Linq;
using Bindito.Core;
using DemoCode.Configurators;
using HarmonyLib;
using Timberborn.MasterScene;
using Timberborn.Options;
using UnityEngine;
using UnityEngine.UIElements;


namespace DemoCode.Patches
{
    [HarmonyPatch(typeof(MasterSceneConfigurator), "Configure", typeof(IContainerDefinition))]
    public static class MasterSceneConfiguratorPatch
    {
        private static void Postfix(IContainerDefinition containerDefinition)
        {
            containerDefinition.Install((IConfigurator) new DemoConfigurator());
            containerDefinition.Install((IConfigurator) new DemoFragmentConfigurator());
        }
    }
    
    [HarmonyPatch(typeof(OptionsBox), "GetPanel")]
    public static class HawsCreativeModTest
    {
        private static void Postfix(ref VisualElement __result)
        {
            VisualElement root = __result.Children().First();

            root.Insert(2,CreateButton("Creative mod 1",  new []{ "settings-toggle" }));
            root.Insert(2,TextField(new [] { "unity-base-field", "unity-base-field--no-label", "unity-base-text-field", "unity-text-field", "new-game-mode-panel__setting-input", "text-field" }));
            root.Insert(4,CreateButton("Creative mod 2",  new []{ "tool-panel-toggle" }));
            root.Insert(6,CreateButton("Creative mod 3",  new []{ "game-toggle" }));

        }
        private static VisualElement CreateButton(string name, IEnumerable<string> classes)
        {
            Toggle button = new Toggle();
            button.text = name;
            button.classList.AddRange(classes);
            return button;
        }
        
        public static TextField TextField(IEnumerable<string> classes = null, string name = null)
        {
            TextField element = new TextField();
            if(name != null)
                element.name = name;
            if(classes != null)
                element.classList.AddRange(classes);
            return element;
        }
    }
    
}