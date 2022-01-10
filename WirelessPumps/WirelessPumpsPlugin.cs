using System;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using Timberborn.Workshops;
using UnityEngine;

namespace WirelessPumps
{
    [BepInPlugin("com.kyp.plugin.wirelesspumps", "Wireless Pumps", "1.0.0")]
    public class WirelessPumpsPlugin : BaseUnityPlugin
    {
        private static ConfigEntry<int> _bonusHeight;
        
        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            _bonusHeight = Config.Bind("Settings", "bonusHeight", 0, "Addition height given to water pumps. Min: 0, Max: 25");
            Logger.LogInfo($"Wireless Pumps is loaded!");
        }
        
        [HarmonyPatch(typeof(WaterManufactory), "Start")]
        static class InfinityDepth
        {
            public static void Postfix(ref WaterManufactory __instance, ref Vector3 ____waterCoordinates)
            {
                __instance.WaterCoordinates.z -= Math.Clamp(_bonusHeight.Value, 0, 25);
                ____waterCoordinates.z -= Math.Clamp(_bonusHeight.Value, 0, 25);
            }
        }
    }
}
