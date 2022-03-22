using System;
using System.Reflection;
using HarmonyLib;
using Timberborn.TemplateSystem;

namespace InventoryPreset.HarmonyPatches;

[HarmonyPatch]
public static class WareHouseTemplateModuleProviderPatch
{
    static MethodInfo TargetMethod()
    {
        
        Type t = Type.GetType("Timberborn.Warehouses.WarehousesTemplateModuleProvider,Timberborn.Warehouses");
        MethodInfo methodInfo = t.GetMethod("InitializeBehaviors", BindingFlags.Static | BindingFlags.NonPublic);
        ModLogger.Log.LogFatal(methodInfo);
        return methodInfo;
    }

    static void Postfix(TemplateModule.Builder builder)
    {
        ModLogger.Log.LogFatal("Pogger");
    }
}