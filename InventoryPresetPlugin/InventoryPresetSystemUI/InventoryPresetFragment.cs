using System;
using System.Collections.Generic;
using System.Linq;
using InventoryPreset.InventoryPresetSystem;
using InventoryPreset.VisualPresetSystem;
using Timberborn.ConstructibleSystem;
using Timberborn.ConstructionSites;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace InventoryPreset.InventoryPresetSystemUI;

public class InventoryPresetFragment : IEntityPanelFragment
{
    private readonly InventoryPresetRepository _inventoryPresetRepository;
    
    private readonly VisualPresetLoader _visualPresetLoader;

    private ListView _listView;

    private InventoryPresetAssigner _inventoryPresetAssigner;

    private VisualElement _root;

    public InventoryPresetFragment(VisualPresetLoader visualPresetLoader, InventoryPresetRepository inventoryPresetRepository)
    {
        _visualPresetLoader = visualPresetLoader;
        _inventoryPresetRepository = inventoryPresetRepository;
    }

    public VisualElement InitializeFragment()
    {
        _root = _visualPresetLoader.LoadVisualElement(InventoryPresetFragmentPreset.Key);
        _listView = _root.Q<ListView>("Presets");
        _listView.itemsSource = _inventoryPresetRepository.GetAll();
        _listView.bindItem = BindItem;
        _listView.onSelectionChange += ListViewOnSelectionChange;

        _root.ToggleDisplayStyle(false);
        return _root;
    }
    
    [OnEvent]
    public void OnInventoryPresetRepositoryChangedEvent(InventoryPresetRepositoryChangedEvent changedEvent)
    {
        _listView.RefreshItems();
    }

    private void ListViewOnSelectionChange(IEnumerable<object> obj)
    {
        InventoryPresetSystem.InventoryPreset preset = (InventoryPresetSystem.InventoryPreset)obj.FirstOrDefault(o => o is InventoryPresetSystem.InventoryPreset);
        if(preset == null)
            return;
        _inventoryPresetAssigner.SetInventoryToPreset(preset);
    }

    private void BindItem(VisualElement visualElement, int index)
    {
        InventoryPresetSystem.InventoryPreset preset = _inventoryPresetRepository.GetByIndex(index);
        Label visualPreset = visualElement.Q<Label>("ItemLabel");
        visualPreset.text = preset.Name;
    }

    public void ShowFragment(GameObject entity)
    {
        if(!entity.TryGetComponent(out _inventoryPresetAssigner))
            return;
        // _listView.SetSelection(_inventoryPresetRepository.GetIndexByName(_inventoryPresetAssigner.SelectedPresetName));
        _root.ToggleDisplayStyle(true);
    }

    public void ClearFragment()
    {
        _listView.ClearSelection();
        _root.ToggleDisplayStyle(false);
        _inventoryPresetAssigner = null;
    }

    public void UpdateFragment()
    {
    }
}