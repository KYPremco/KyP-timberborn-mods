using System;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using Timberborn.WaterBuildings;
using Timberborn.Workshops;
using UnityEngine;

namespace WirelessPumps
{
    [BepInPlugin("com.kyp.plugin.wirelesspumps", "Wireless Pumps", "1.0.0")]
    public class WirelessPumpsPlugin : BaseUnityPlugin
    {
        private static ConfigEntry<int> _bonusHeightPump;
        private static ConfigEntry<int> _bonusHeightMover;
        
        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            _bonusHeightPump = Config.Bind("Settings", "bonusHeightPump", 0, "Addition height given to water pumps. Min: 0, Max: 25");
            _bonusHeightMover = Config.Bind("Settings", "bonusHeightMover", 0, "Addition height given to water movers. Min: 0, Max: 25");
            
            Logger.LogInfo($"Wireless Pumps is loaded!");
        }
        
        [HarmonyPatch(typeof(WaterManufactory), "Start")]
        static class InfinityDepthPumps
        {
            public static void Postfix(ref WaterManufactory __instance, ref Vector3 ____waterCoordinates)
            {
                if(__instance.gameObject.name.StartsWith("WaterDump"))
                    return;
                
                __instance.WaterCoordinates.z -= Math.Clamp(_bonusHeightPump.Value, 0, 25);
                ____waterCoordinates.z -= Math.Clamp(_bonusHeightPump.Value, 0, 25);
            }
        }
        
        [HarmonyPatch(typeof(WaterMover), "Awake")]
        static class InfinityDepthWaterMover
        {
            public static void Postfix(ref WaterMover __instance)
            {
                __instance.Input.z -= Math.Clamp(_bonusHeightMover.Value, 0, 25);
            }
        }
    }
}
