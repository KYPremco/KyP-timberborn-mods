using System;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using Timberborn.WaterBuildings;
using UnityEngine;

namespace WirelessPumps
{
    [BepInPlugin("com.kyp.plugin.wirelesspumps", "Wireless Pumps", "1.0.0")]
    public class WirelessPumpsPlugin : BaseUnityPlugin
    {
        private static ConfigEntry<int> _bonusHeight;

        private static ConfigEntry<int> _bonusHeightMultiplier;
        
        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            
            _bonusHeight = Config.Bind("_General Settings", "bonusHeight", 0, "Bonus height to all pumps.");
            _bonusHeightMultiplier = Config.Bind("_General Settings", "bonusHeightMultiplier", 1, "Bonus height multiplier to all pumps.");

            foreach (var prefab in Resources.LoadAll<WaterInputSpecification>("Buildings"))
            {
                var pump = ExtractPump(prefab.name);

                var pumpConfig = Config.Bind(pump.Faction, pump.Name, prefab.MaxDepth, $"Maximum height for {pump.Name}.");
                
                var maxDepth = Math.Clamp(pumpConfig.Value, 0, 1000);
                
                prefab._maxDepth = maxDepth;
            }
            
            Logger.LogInfo($"Wireless Pumps is loaded!");
        }
        
        private static Pump ExtractPump(string prefabName)
        {
            var factionSeparatorIndex = prefabName.LastIndexOf('.');
            
            var pumpName = prefabName[..factionSeparatorIndex];
            var faction = prefabName[(factionSeparatorIndex + 1)..];

            return new Pump(pumpName, faction);
        }
        
        [HarmonyPatch]
        internal static class Patches
        {
            [HarmonyPrefix]
            [HarmonyPatch(typeof(WaterInputSpecification), "Awake")]
            private static bool DepthPatcher(ref int ____maxDepth)
            {
                ____maxDepth = ____maxDepth * _bonusHeightMultiplier.Value + _bonusHeight.Value;

                return false;
            }

        }
    }
}
