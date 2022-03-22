using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Persistence;

namespace InventoryPreset.InventoryPresetSystem;

public class InventoryPresetObjectSerializer : IObjectSerializer<InventoryPreset>
{
    private static readonly PropertyKey<string> NameKey = new PropertyKey<string>("Name");
    
    private static readonly PropertyKey<string> NormalizedNameKey = new PropertyKey<string>("NormalizedName");

    private static readonly PropertyKey<bool> IsDefaultKey = new PropertyKey<bool>("IsDefault");

    private static readonly ListKey<GoodPresetRow> GoodPresetsKey = new ListKey<GoodPresetRow>("GoodPresets");

    public void Serialize(InventoryPreset inventoryPreset, IObjectSaver objectSaver)
    {
        objectSaver.Set<string>(NameKey, inventoryPreset.Name);
        objectSaver.Set<string>(NormalizedNameKey, inventoryPreset.NormalizedName);
        objectSaver.Set<bool>(IsDefaultKey, inventoryPreset.IsDefault);
        objectSaver.Set<GoodPresetRow>(GoodPresetsKey, inventoryPreset.GoodSpecifications);
    }

    public Obsoletable<InventoryPreset> Deserialize(IObjectLoader objectLoader)
    {
        string name = objectLoader.Get(NameKey);
        string normalizedName = objectLoader.Get(NormalizedNameKey);
        bool isDefaultKey = objectLoader.Get(IsDefaultKey);
        List<GoodPresetRow> goodPresetRows = objectLoader.Get(GoodPresetsKey);
        return new InventoryPreset(name, normalizedName, goodPresetRows.ToImmutableArray(), isDefaultKey);
    }
}