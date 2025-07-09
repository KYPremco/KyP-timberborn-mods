using HarmonyLib;
using ModSettings.Common;
using ModSettings.Core;
using Timberborn.Modding;
using Timberborn.SettingsSystem;
using Timberborn.WaterBuildings;

namespace PumpExtender;

[HarmonyPatchCategory("SwapGlobalPumps")]
[HarmonyPatch(typeof(WaterInputSpec), MethodType.Constructor)]
public class GlobalPumpSettings(
    ISettings settings,
    ModSettingsOwnerRegistry modSettingsOwnerRegistry,
    ModRepository modRepository) : ModSettingsOwner(settings, modSettingsOwnerRegistry, modRepository)
{
    public override string ModId => "KyP.PumpExtender";
    
    public override string HeaderLocKey => "KyP.PumpExtender.GlobalSettingsHeader";

    public override int Order => 10;

    public static ModSetting<bool> UseGlobalModifier { get; } = new(false, ModSettingDescriptor.CreateLocalized("KyP.PumpExtender.UseGlobalModifier").SetLocalizedTooltip("KyP.PumpExtender.UseGlobalModifierToolTip"));

    public static RangeIntModSetting GlobalModifier { get; } = new(0, -45, 45, ModSettingDescriptor.CreateLocalized("KyP.PumpExtender.GlobalModifier"));

    public override void OnAfterLoad()
    {
        AddCustomModSetting(UseGlobalModifier, "Use.GlobalModifier");
        AddCustomModSetting(GlobalModifier, "GlobalModifier");
    }
}