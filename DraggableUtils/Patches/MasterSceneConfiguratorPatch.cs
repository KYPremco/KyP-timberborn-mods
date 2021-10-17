using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bindito.Core;
using DraggableUtils.Configurators;
using DraggableUtils.Localization;
using HarmonyLib;
using Timberborn.Common;
using Timberborn.Localization;
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
    
    [HarmonyPatch]
    public static class LocPatch
    {
        private static MethodInfo TargetMethod()
        {
            return AccessTools.TypeByName("Timberborn.Localization.Loc").GetMethod("Initialize");
        }
        
        private static void Prefix(ref Dictionary<string, string> localization)
        {
            localization.AddRange(LocalizationList.English);
        }
    }
}