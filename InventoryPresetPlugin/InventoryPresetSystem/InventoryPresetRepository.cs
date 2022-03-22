using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using InventoryPreset.Helpers;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;

namespace InventoryPreset.InventoryPresetSystem;

public class InventoryPresetRepository : ISaveableSingleton, ILoadableSingleton
{
    private static readonly SingletonKey InventoryPresetRepositoryKey = new SingletonKey(nameof (InventoryPresetRepository));
    
    private static readonly ListKey<InventoryPreset> InventoryPresetKey = new("InventoryPresets");
    
    private readonly ISingletonLoader _singletonLoader;

    private readonly EventBus _eventBus;
    
    private List<InventoryPreset> _inventoryPresets;
    
    public InventoryPresetRepository(ISingletonLoader singletonLoader, EventBus eventBus)
    {
        _singletonLoader = singletonLoader;
        _eventBus = eventBus;
        _inventoryPresets = new List<InventoryPreset>();
    }

    public List<InventoryPreset> GetAll()
    {
        return _inventoryPresets;
    }
    
    public InventoryPreset GetDefault()
    {
        return _inventoryPresets.SingleOrDefault(preset => preset.IsDefault);
    }

    public InventoryPreset Get(string normalizedName)
    {
        return _inventoryPresets.FirstOrDefault(preset => preset.NormalizedName.Equals(normalizedName));
    }
    
    public InventoryPreset GetByIndex(int index)
    {
        return _inventoryPresets[index];
    }
    
    public int GetIndexByName(string normalizedName)
    {
        return _inventoryPresets.IndexOf(Get(normalizedName));
    }
    
    public void Add(string name, IEnumerable<GoodPresetRow> presetRows)
    {
        string normalizedName = NormalizePresetName(name);
        if(_inventoryPresets.Any(preset => preset.NormalizedName.Equals(normalizedName)))
            return;
        
        _inventoryPresets.Add(new InventoryPreset(name.TrimBetween(), normalizedName, presetRows.ToImmutableArray()));
        _eventBus.Post(new InventoryPresetRepositoryChangedEvent());
    }
    
    public void Remove(string normalizedName)
    {
        InventoryPreset preset = _inventoryPresets.Find(inventoryPreset => inventoryPreset.NormalizedName.Equals(normalizedName));
        if(preset == null)
            return;
        _inventoryPresets.Remove(preset);
        _eventBus.Post(new InventoryPresetRepositoryChangedEvent());
    }
    
    private string NormalizePresetName(string name)
    {
        name = StringNormalizationExtensions.Normalize(name);
        name = name.TrimBetween();
        name = name.Replace(" ", "_");
        name = name.ToLower();
        return StringNormalizationExtensions.Normalize(name);
    }

    public void Save(ISingletonSaver singletonSaver)
    {
        singletonSaver.GetSingleton(InventoryPresetRepositoryKey).Set(InventoryPresetKey, _inventoryPresets);
            
    }

    public void Load()
    {
        if (!_singletonLoader.HasSingleton(InventoryPresetRepositoryKey))
            return;
        _inventoryPresets = _singletonLoader.GetSingleton(InventoryPresetRepositoryKey).Get(InventoryPresetKey);
    }
}