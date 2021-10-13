using Bindito.Core;
using DraggableUtils.Configurators;
using HarmonyLib;
using Timberborn.MasterScene;

namespace DraggableUtils.Patches
{
    [HarmonyPatch(typeof(MasterSceneConfigurator), "Configure", typeof(IContainerDefinition))]
    public static class MasterSceneConfiguratorPatch
    {
        private static void Postfix(IContainerDefinition containerDefinition)
        {
            containerDefinition.Install((IConfigurator) new DraggableUtilsConfigurator());
        }
    }
}