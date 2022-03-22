using System.Collections.Immutable;

namespace InventoryPreset.InventoryPresetSystem;

public class InventoryPreset
{
    public readonly string Name;

    public readonly string NormalizedName;

    public readonly ImmutableArray<GoodPresetRow> GoodSpecifications;

    public bool IsDefault = false;

    public InventoryPreset(string name, string normalizedName, ImmutableArray<GoodPresetRow> goodSpecifications, bool isDefault = false)
    {
        GoodSpecifications = goodSpecifications;
        Name = name;
        NormalizedName = normalizedName;
        IsDefault = isDefault;
    }
}