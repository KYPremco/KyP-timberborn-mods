using Timberborn.CoreUI;
using Timberborn.InputSystem;
using TimberbornAPI.Common;
using TimberbornAPI.UIBuilderSystem;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UIElements.Length.Unit;

namespace InventoryPreset.VisualPresetSystem;

public class InventoryPresetFragmentPreset : IVisualPreset, IInputProcessor
{
    public static readonly string Key = "InventoryPresetFragment";
    
    private readonly UIBuilder _uiBuilder;
    
    public string Id => Key;
    
    public InventoryPresetFragmentPreset(UIBuilder uiBuilder)
    {
        _uiBuilder = uiBuilder;
    }
    
    public VisualElement GetElement()
    {
        return _uiBuilder.CreateFragmentBuilder()
            .AddPreset(factory => factory.Labels().GameText("inventory.preset.fragment.text"))
            .AddPreset(factory => factory.ListViews().ColorListView(
                makeItem: ListViewItem, 
                scrollbarColor: new Color(0,0,0,0.001f), 
                dragButtonColor: new Color(0.16f, 0.29f, 0.24f), 
                name: "Presets", 
                height: new Length(140, Pixel),
                builder: builder => builder.ModifyVerticalScroller(scroller =>
                {
                    scroller.lowButton.ToggleDisplayStyle(false);
                    scroller.highButton.ToggleDisplayStyle(false);
                })))
            .Build();
    }
    
    private VisualElement ListViewItem()
    {
        return _uiBuilder.CreateComponentBuilder().CreateVisualElement()
            .AddClass(TimberApiStyle.ListViews.Hover.BgPixel3Hover)
            .AddClass(TimberApiStyle.ListViews.Selected.BgPixel3Selected)
            .SetJustifyContent(Justify.Center)
            .SetAlignItems(Align.Center)
            .SetMargin(new Margin(10, 0))
            .AddPreset(factory => factory.Labels().DefaultText(name: "ItemLabel"))
            .Build();
    }

    public bool ProcessInput()
    {
        return true;
    }
}