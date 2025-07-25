using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
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
    public override string ModId => "KyP.PumpExtender";
    
    public override string HeaderLocKey => "KyP.PumpExtender.IndividualPumpSettings";

    public override int Order => 20;
    
    private bool _firstLoad;

    private static readonly Dictionary<string, int> PipeDepthDefaults = new();

    private static readonly Dictionary<string, ModSetting<int>> _pumpSettings = new();
    
    private static readonly Harmony IndividualPumpHarmony = new("KyP.PumpExtender.Individual");
    
    private static bool _loaded = false;
    
    public static ModSetting<bool> UseIndividualPumpSettings { get; } = new(true, ModSettingDescriptor.CreateLocalized("KyP.Use.IndividualPumpSettings").SetLocalizedTooltip("KyP.Use.IndividualPumpSettingsTooltip"));

    public static Dictionary<string, ModSetting<int>> PumpSettings => _pumpSettings;
    
    public override void OnAfterLoad()
    {
        AddCustomModSetting(UseIndividualPumpSettings, "KyP.Use.IndividualPumpSettings");
        
        if (UseIndividualPumpSettings.Value)
        {
            RegisterIndividualPumpSettings();
        }
    }
    
    private void RegisterIndividualPumpSettings()
    {
        var waterInputSpecifications = assetLoader.LoadAll<WaterInputSpec>("buildings").ToList();
        
        SaveDefaultValues(waterInputSpecifications);
        
        foreach (var specification in waterInputSpecifications)
        {
            if (_pumpSettings.ContainsKey(specification.Asset.name))
            {
                continue;
            }
        
            var setting = new RangeIntModSetting(
                PipeDepthDefaults[specification.Asset.name],
                1,
                45,
                ModSettingDescriptor.Create(ExtractPumpName(specification.Asset.name))
            );
            
            AddCustomModSetting(setting, specification.Asset.name);

            _pumpSettings.Add(specification.Asset.name, setting);
        }
    }
    
    public void Unload()
    {
        switch (UseIndividualPumpSettings.Value)
        {
            case true when ! _loaded:
                IndividualPumpHarmony.PatchCategory("SwapIndividualPumps");
                _loaded = true;
                break;
            case false when _loaded:
                IndividualPumpHarmony.UnpatchCategory("SwapIndividualPumps");
                _loaded = false;
                break;
        }
    }

    private void SaveDefaultValues(IEnumerable<LoadedAsset<WaterInputSpec>> waterInputSpecifications)
    {
        if (_firstLoad)
        {
            return;
        }
        
        foreach (var specification in waterInputSpecifications)
        {
            if (!PipeDepthDefaults.ContainsKey(specification.Asset.name))
            {
                PipeDepthDefaults.TryAdd(specification.Asset.name, specification.Asset.MaxDepth);
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
        {
            return "";
        }

        var newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);
        for (var i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]) && text[i - 1] != ' ')
            {
                newText.Append(' ');
            }

            newText.Append(text[i]);
        }

        return newText.ToString();
    }
}