using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModSettings.Common;
using ModSettings.Core;
using Timberborn.AssetSystem;
using Timberborn.Modding;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using Timberborn.WaterBuildings;

namespace PumpExtender;

public class ExtendablePumpSettings(
    ISettings settings,
    ModSettingsOwnerRegistry modSettingsOwnerRegistry,
    ModRepository modRepository,
    IAssetLoader assetLoader)
    : ModSettingsOwner(settings, modSettingsOwnerRegistry, modRepository), IUnloadableSingleton
{
    private bool _firstLoad;

    private static Dictionary<string, int> _pipeDepthDefaults = new();
    
    private readonly Dictionary<string, ModSetting<int>> _settings = new();

    protected override string ModId => "KyP.PumpExtender";

    protected override void OnAfterLoad()
    {
        var waterInputSpecifications = assetLoader.LoadAll<WaterInputSpecification>("").ToList();

        SaveDefaultValues(waterInputSpecifications);

        foreach (var specification in waterInputSpecifications)
        {
            var setting = new RangeIntModSetting(
                ExtractPumpName(specification.Asset.name),
                _pipeDepthDefaults[specification.Asset.name],
                1,
                30
            );
            
            AddCustomModSetting(setting, specification.Asset.name);
            
            _settings.Add(specification.Asset.name, setting);
        }
    }
    
    public void Unload()
    {
        foreach (var specification in assetLoader.LoadAll<WaterInputSpecification>(""))
        {
            specification.Asset._maxDepth = _settings[specification.Asset.name].Value;
        }
    }

    private void SaveDefaultValues(IEnumerable<LoadedAsset<WaterInputSpecification>> waterInputSpecifications)
    {
        if (_firstLoad)
        {
            return;
        }

        foreach (var specification in waterInputSpecifications)
        {
            if(! _pipeDepthDefaults.ContainsKey(specification.Asset.name))
            {
                _pipeDepthDefaults.TryAdd(specification.Asset.name, specification.Asset.MaxDepth);
            }
        }
        
        _firstLoad = true;
    }

    private static string ExtractPumpName(string prefabName)
    {
        var factionSeparatorIndex = prefabName.LastIndexOf('.');

        var pumpName = AddSpacesToSentence(prefabName[..factionSeparatorIndex]);
        var faction = prefabName[(factionSeparatorIndex + 1)..];

        return $"{pumpName} ({faction})";
    }
    
    private static string AddSpacesToSentence(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return "";
        StringBuilder newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);
        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                newText.Append(' ');
            newText.Append(text[i]);
        }
        return newText.ToString();
    }
}