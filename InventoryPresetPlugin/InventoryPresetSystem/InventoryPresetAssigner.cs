using Bindito.Core;
using Timberborn.InventorySystem;
using Timberborn.StockKeeping;
using UnityEngine;

namespace InventoryPreset.InventoryPresetSystem;

public class InventoryPresetAssigner : MonoBehaviour
{
    // public static readonly ComponentKey InventoryPresetInitializerKey = new ComponentKey(nameof (InventoryPresetAssigner));
    //
    // public static readonly PropertyKey<string> SelectedPresetNameKey = new PropertyKey<string>(nameof(SelectedPresetName));
    //
    // public string SelectedPresetName { get; set; }
    
    private InventoryPresetRepository _inventoryPresetRepository;

    private GoodDesirer _goodDesirer;

    private ToggleableGoodDisallower _toggleableGoodDisallower;

    private int _maxAmount;

    private void Awake()
    {
        _goodDesirer = GetComponent<GoodDesirer>();
        _toggleableGoodDisallower = GetComponent<ToggleableGoodDisallower>();
        foreach (Inventory inventory in GetComponents<Inventory>())
        {
            if (inventory.ComponentName.Equals("Stockpile"))
                _maxAmount = inventory.Capacity;
        }
    }
    
    [Inject]
    public void Inject(InventoryPresetRepository inventoryPresetRepository)
    {
        _inventoryPresetRepository = inventoryPresetRepository;
    }
    
    public void SetInventoryToPreset(InventoryPreset inventoryPreset)
    {
        foreach (GoodPresetRow row in inventoryPreset.GoodSpecifications)
        {
            
            if (row.IsDesired)
            {
                _goodDesirer.SetDesiredAmount(row.GoodSpecification, row.DesiredAmount >= _maxAmount ? _maxAmount : row.DesiredAmount);
                _toggleableGoodDisallower.Allow(row.GoodSpecification);
            }
            else
            {
                _toggleableGoodDisallower.Disallow(row.GoodSpecification);
            }
        }
    }



    // public void Save(IEntitySaver entitySaver)
    // {
    //     if(SelectedPresetName == null)
    //         return;
    //     
    //     entitySaver.GetComponent(InventoryPresetInitializerKey).Set<string>(SelectedPresetNameKey, SelectedPresetName);
    // }
    //
    // public void Load(IEntityLoader entityLoader)
    // {
    //     if(!entityLoader.HasComponent(InventoryPresetInitializerKey))
    //         return;
    //     SelectedPresetName = entityLoader.GetComponent(InventoryPresetInitializerKey).Get<string>(SelectedPresetNameKey);
    // }
}