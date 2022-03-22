using System.Collections.Generic;
using InventoryPreset.InventoryPresetSystem;
using Timberborn.Goods;
using Timberborn.InputSystem;
using Timberborn.InventorySystem;
using Timberborn.SingletonSystem;
using Timberborn.StockKeeping;
using Timberborn.Warehouses;
using TimberbornAPI.Common;
using TimberbornAPI.UIBuilderSystem;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UIElements.Length.Unit;

namespace InventoryPreset.InventoryPresetSystemUI;

public class InventoryAdditionFragment : IInputProcessor
{
    private readonly InventoryPresetRepository _inventoryPresetRepository;
    
    private readonly UIBuilder _uiBuilder;

    private readonly InputService _inputService;

    private readonly IGoodService _goodService;

    private ToggleableGoodDisallower _toggleableGoodDisallower;

    private GoodDesirer _goodDesirer;

    private TextField _textField;

    public InventoryAdditionFragment(UIBuilder uiBuilder, InputService inputService, InventoryPresetRepository inventoryPresetRepository, IGoodService goodService)
    {
        _uiBuilder = uiBuilder;
        _inputService = inputService;
        _inventoryPresetRepository = inventoryPresetRepository;
        _goodService = goodService;
    }
    
    public void InitializeFragment(VisualElement root)
    {
        root.Insert(1, _uiBuilder.CreateComponentBuilder().CreateVisualElement()
            .SetFlexDirection(FlexDirection.Row).SetJustifyContent(Justify.SpaceBetween)
            .SetMargin(new Margin(new Length(10, Pixel), 0, 0, 0))
            .AddPreset(factory => factory.Labels().GameText("inventory.preset.input.name", builder: textBuilder => textBuilder.SetColor(new Color(0.66f, 0.58f, 0.39f))))
            .AddPreset(factory => factory.TextFields().InGameTextField(
                new Length(150, Pixel),
                name: "NameInput",
                builder: fieldBuilder => fieldBuilder.SetStyle(style => style.backgroundColor = new Color(0.1f, 0.19f, 0.17f))))
            .AddPreset(factory => factory.Buttons().ButtonGame( "inventory.preset.add", name: "Add",
                width: new Length(50, Pixel), height: new Length(24, Pixel))).BuildAndInitialize());
        
        root.Q<Button>("Add").clicked += OnAddButtonClicked;
        _textField = root.Q<TextField>("NameInput");
        _textField?.textInput.RegisterCallback<FocusInEvent>(evt => _inputService.AddInputProcessor(this));
        _textField?.textInput.RegisterCallback<FocusOutEvent>(evt => _inputService.RemoveInputProcessor(this));
    }

    private void OnAddButtonClicked()
    {
        List<GoodPresetRow> goodPresetRows = new ();
        
        foreach (GoodSpecification goodSpecification in _goodService.GetGoodSpecifications())
        {
            goodPresetRows.Add(new GoodPresetRow(_goodDesirer.DesiredAmount(goodSpecification), goodSpecification, !_toggleableGoodDisallower.IsDisallowed(goodSpecification)));
        }
        
        _inventoryPresetRepository.Add(_textField.value, goodPresetRows);
    }
    
    public bool ProcessInput()
    {
        return true;
    }
    
    public void ShowFragment(GameObject entity)
    {
        Stockpile component = entity.GetComponent<Stockpile>();
        if (component == null)
            return;
        _toggleableGoodDisallower = component.Inventory.GetComponent<ToggleableGoodDisallower>();
        _goodDesirer = component.Inventory.GetComponent<GoodDesirer>();
    }
}