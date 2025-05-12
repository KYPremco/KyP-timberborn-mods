using System.Linq;
using Timberborn.AssetSystem;
using Timberborn.SingletonSystem;
using Timberborn.WaterBuildings;

namespace PumpExtender;

public class SwapInGameMaxDepth(IAssetLoader assetLoader, ExtendablePumpSettings extendablePumpSettings) : ILoadableSingleton
{
    public void Load()
    {
        if (! ExtendablePumpSettings.UseIndividualPumpSettings.Value)
        {
            return;
        }
        
        var waterInputSpecifications = assetLoader.LoadAll<WaterInputSpec>("buildings").ToList();
        
        foreach (var specification in waterInputSpecifications)
        {
            specification.Asset._maxDepth = extendablePumpSettings.Settings[specification.Asset.name].Value;
        }
    }
}