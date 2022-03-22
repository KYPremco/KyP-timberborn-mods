using System;
using System.Collections.Generic;
using System.Linq;
using InventoryPreset.InventoryPresetSystem;
using InventoryPreset.VisualPresetSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.Goods;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using Timberborn.ToolPanelSystem;
using Timberborn.ToolSystem;
using TimberbornAPI.EventSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace InventoryPreset.InventoryPresetSystemUI;

public class InventoryPresetPanel : EventListener, IToolFragment, IInputProcessor
{
    private readonly InventoryPresetRepository _inventoryPresetRepository;
    
    private readonly InputService _inputService;
    
    private readonly VisualPresetLoader _visualPresetLoader;
    
    private readonly IGoodService _goodService;

    private readonly ILoc _loc;

    private ListView _presetsUI;

    private IList<AdjustablePresetRow> _adjustablePresets;

    private VisualElement _root;

    private InventoryPresetTool _tool;

    public InventoryPresetPanel(VisualPresetLoader visualPresetLoader, IGoodService goodService, ILoc loc, InputService inputService, InventoryPresetRepository inventoryPresetRepository)
    {
        _goodService = goodService;
        _loc = loc;
        _inputService = inputService;
        _inventoryPresetRepository = inventoryPresetRepository;
        _visualPresetLoader = visualPresetLoader;
        _adjustablePresets = new List<AdjustablePresetRow>();
    }

    public VisualElement InitializeFragment()
    {
        _root = _visualPresetLoader.LoadVisualElement(InventoryPresetPanelPreset.Key);
        _presetsUI = _root.Q<ListView>("Presets");
        _root.Q<Button>("Add").clicked += () => OnAddButtonClicked(_root.Q<TextField>("NameInput").value);
        _root.Q<Button>("Load").clicked += OnLoadButtonClicked;
        _root.Q<Button>("Remove").clicked += OnRemoveButtonClicked;
        TextField textField = _root.Q<TextField>("NameInput");
        textField?.textInput.RegisterCallback<FocusInEvent>(evt => _inputService.AddInputProcessor(this));
        textField?.textInput.RegisterCallback<FocusOutEvent>(evt => _inputService.RemoveInputProcessor(this));

        _presetsUI.bindItem += BindItem;
        _presetsUI.itemsSource = _inventoryPresetRepository.GetAll();

        _adjustablePresets.AddRange(CreateRows(_goodService.GetGoodSpecifications().Where<GoodSpecification>((Func<GoodSpecification, bool>) (good => good.GoodContainer == GoodContainer.Box)), _root.Q<ScrollView>("Rows")));

        return _root;
    }
    
    [OnEvent]
    public void OnInventoryPresetRepositoryChangedEvent(InventoryPresetRepositoryChangedEvent changedEvent)
    {
        _presetsUI.RefreshItems();
    }

    private void OnLoadButtonClicked()
    {
        if(_presetsUI.selectedItem == null)
            return;
        InventoryPresetSystem.InventoryPreset preset = _inventoryPresetRepository.Get(((InventoryPresetSystem.InventoryPreset) _presetsUI.selectedItem).NormalizedName);

        foreach (GoodPresetRow goodPreset in preset.GoodSpecifications)
        {
            AdjustablePresetRow adjustablePresetRow = _adjustablePresets.FirstOrDefault(row => row.GoodSpecification.Equals(goodPreset.GoodSpecification));
            if(adjustablePresetRow == null)
                continue;
            adjustablePresetRow.Active = goodPreset.IsDesired;
            adjustablePresetRow.DesiredAmount = goodPreset.DesiredAmount;
        }
    }

    private void OnRemoveButtonClicked()
    {
        if(_presetsUI.selectedItem == null)
            return;
        
        _inventoryPresetRepository.Remove(((InventoryPresetSystem.InventoryPreset)_presetsUI.selectedItem).NormalizedName);
    }

    private void BindItem(VisualElement visualElement, int index)
    {
        InventoryPresetSystem.InventoryPreset preset = _inventoryPresetRepository.GetByIndex(index);
        Label visualPreset = visualElement.Q<Label>("ItemLabel");
        visualPreset.text = preset.Name;
        if(preset.IsDefault)
            visualPreset.text += $" (Default)";
    }

    private void OnAddButtonClicked(string name)
    {
        List<GoodPresetRow> presetRows = new();
        foreach (AdjustablePresetRow adjustablePresetRow in _adjustablePresets)
            presetRows.Add(new GoodPresetRow(adjustablePresetRow.DesiredAmount, adjustablePresetRow.GoodSpecification, adjustablePresetRow.Active));
        
        _inventoryPresetRepository.Add(name, presetRows);
    }

    [OnEvent]
    public void OnToolEntered(ToolEnteredEvent toolEnteredEvent)
    {
        _tool = toolEnteredEvent.Tool as InventoryPresetTool;
        _root.ToggleDisplayStyle(this._tool != null);
    }
    
    public bool ProcessInput()
    {
        return true;
    }

    #region AdjustablePresetRowFactory

    private IEnumerable<AdjustablePresetRow> CreateRows(
        IEnumerable<GoodSpecification> goodSpecifications,
        VisualElement parent)
    {
        foreach (GoodSpecification goodSpecification in goodSpecifications)
            yield return CreateRow(goodSpecification, parent);
    }
    
    private AdjustablePresetRow CreateRow(GoodSpecification goodSpecification, VisualElement parent)
    {
        AdjustablePresetRow adjustableRow = new AdjustablePresetRow(100, goodSpecification);
        VisualElement row = _visualPresetLoader.LoadVisualElement(InventoryPresetAdjustableRowPreset.Key);
        Toggle toggle = row.Q<Toggle>("Toggle");
        TextField limit = row.Q<TextField>("Limit");
        Button increase = row.Q<Button>("Increase");
        Button decrease = row.Q<Button>("Decrease");
        row.Q<VisualElement>("Image").style.backgroundImage = new StyleBackground(goodSpecification.Icon);
        row.Q<Label>("Name").text = _loc.T(goodSpecification.DisplayNameLocKey);
        TextFields.InitializeIntTextField(limit, 100, 0, 4000, newValue => OnTextFieldChanged(adjustableRow, newValue));
        toggle.RegisterValueChangedCallback(value => ToggleAllowedState(adjustableRow, value.newValue, limit, increase, decrease));
        toggle.SetValueWithoutNotify(true);
        increase.clicked += () => OnButtonClicked(10, 4000, limit, adjustableRow);
        decrease.clicked += () => OnButtonClicked(-10, 4000, limit, adjustableRow);
        parent.Add(row);
        return adjustableRow;
    }
    
    private static void OnButtonClicked(int change, int maxAmount, TextField textField, AdjustablePresetRow adjustablePresetRow)
    {
        int num = Mathf.Clamp(adjustablePresetRow.DesiredAmount + change, 0, maxAmount);
        adjustablePresetRow.DesiredAmount = num;
        textField.SetValueWithoutNotify(adjustablePresetRow.DesiredAmount.ToString());
    }
    
    private void OnTextFieldChanged(AdjustablePresetRow adjustablePresetRow, int newValue)
    {
        adjustablePresetRow.DesiredAmount = newValue;
    }
    
    private static void ToggleAllowedState(AdjustablePresetRow adjustablePresetRow, bool newState, TextField limit, Button increase, Button decrease)
    {
        limit.SetEnabled(newState);
        limit.visible = newState;
        increase.SetEnabled(newState);
        increase.visible = newState;
        decrease.SetEnabled(newState);
        decrease.visible = newState;
        adjustablePresetRow.Active = newState;
    }

    #endregion
}