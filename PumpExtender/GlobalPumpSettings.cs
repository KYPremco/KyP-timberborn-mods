using System;
using HarmonyLib;
using ModSettings.Common;
using ModSettings.Core;
using Timberborn.Modding;
using Timberborn.SettingsSystem;
using Timberborn.SingletonSystem;
using Timberborn.WaterBuildings;

namespace PumpExtender;

[HarmonyPatch(typeof(WaterInputSpec), "Awake")]
public class GlobalPumpSettings(
    ISettings settings,
    ModSettingsOwnerRegistry modSettingsOwnerRegistry,
    ModRepository modRepository) : ModSettingsOwner(settings, modSettingsOwnerRegistry, modRepository), IUnloadableSingleton
{
    public override string ModId => "KyP.PumpExtender";
    
    public override string HeaderLocKey => "KyP.PumpExtender.GlobalSettingsHeader";

    public override int Order => 10;

    private static readonly Harmony GlobalPumpHarmony = new("KyP.PumpExtender");

    private static bool _loaded = false;
    
    private static ModSetting<bool> UseGlobalModifier { get; } = new(false, ModSettingDescriptor.CreateLocalized("KyP.PumpExtender.UseGlobalModifier").SetLocalizedTooltip("KyP.PumpExtender.UseGlobalModifierToolTip"));

    private static RangeIntModSetting GlobalModifier { get; } = new(0, -45, 45, ModSettingDescriptor.CreateLocalized("KyP.PumpExtender.GlobalModifier"));

    public override void OnAfterLoad()
    {
        AddCustomModSetting(UseGlobalModifier, "Use.GlobalModifier");
        AddCustomModSetting(GlobalModifier, "GlobalModifier");
    }
    
    public void Unload()
    {
        switch (UseGlobalModifier.Value)
        {
            case true when ! _loaded:
                GlobalPumpHarmony.PatchAll();
                _loaded = true;
                break;
            case false when _loaded:
                GlobalPumpHarmony.UnpatchAll();
                _loaded = false;
                break;
        }
    }
    
    [HarmonyPrefix]
    public static void ChangePipeDepth(WaterInputSpec __instance)
    {
        __instance._maxDepth = Math.Clamp(__instance._maxDepth + GlobalModifier.Value, 1, int.MaxValue);
    }
}