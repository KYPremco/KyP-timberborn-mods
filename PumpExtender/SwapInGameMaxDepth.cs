using System.Linq;
using HarmonyLib;
using Timberborn.AssetSystem;
using Timberborn.SingletonSystem;
using Timberborn.WaterBuildings;

namespace PumpExtender;

[HarmonyPatchCategory("SwapIndividualPumps")]
[HarmonyPatch(typeof(SingletonLifecycleService), "LoadAll")]
public class SwapInGameMaxDepth
{
    private static IAssetLoader _assetLoader;

    public SwapInGameMaxDepth(IAssetLoader assetLoader)
    {
        _assetLoader = assetLoader;
    }

    [HarmonyPrefix]
    public static void Load()
    {
        var waterInputSpecifications = _assetLoader.LoadAll<WaterInputSpec>("buildings").ToList();

        if (GlobalPumpSettings.UseGlobalModifier.Value)
        {
            foreach (var specification in waterInputSpecifications)
            {
                specification.Asset._maxDepth = GlobalPumpSettings.GlobalModifier.Value + specification.Asset._maxDepth;
            }
        }

        if (ExtendablePumpSettings.UseIndividualPumpSettings.Value)
        {
            foreach (var specification in waterInputSpecifications)
            {
                specification.Asset._maxDepth = ExtendablePumpSettings.PumpSettings[specification.Asset.name].Value;
            }
        }
    }
}