using Bindito.Core;
using HarmonyLib;
using PauseConfigurator.Configurators;
using Timberborn.MasterScene;

namespace PauseConfigurator.Patches
{
    [HarmonyPatch(typeof(MasterSceneConfigurator), "Configure", typeof(IContainerDefinition))]
    public static class MasterSceneConfiguratorPatch
    {
        private static void Postfix(IContainerDefinition containerDefinition)
        {
            containerDefinition.Install((IConfigurator) new DemoConfigurator());
        }
    }
}