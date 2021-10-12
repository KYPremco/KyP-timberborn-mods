using Bindito.Core;
using DemoCode.Configurators;
using HarmonyLib;
using Timberborn.MasterScene;

namespace DemoCode.Patches
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