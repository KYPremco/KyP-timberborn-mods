using TimberbornAPI.Common;
using TimberbornAPI.UIBuilderSystem;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UIElements.Length.Unit;

namespace InventoryPreset.VisualPresetSystem;

public class InventoryPresetPanelPreset : IVisualPreset
{
    public static readonly string Key = "InventoryPresetPanelPreset";

    private readonly UIBuilder _uiBuilder;
    
    public string Id => Key;
    
    public InventoryPresetPanelPreset(UIBuilder uiBuilder)
    {
        _uiBuilder = uiBuilder;
    }
    
    public VisualElement GetElement()
    {
        return _uiBuilder.CreateFragmentBuilder().SetBackground(TimberApiStyle.Backgrounds.BgSquare1)
            .ModifyWrapper(builder => builder.SetWidth(new Length(350, Pixel)))
            .AddComponent(builder => builder.SetFlexDirection(FlexDirection.Row).SetJustifyContent(Justify.SpaceBetween).SetMargin(new Margin(0,0,new Length(10, Pixel),0))
                .AddPreset(factory => factory.TextFields().InGameTextField(
                    new Length(235, Pixel), 
                    name: "NameInput", 
                    builder: fieldBuilder => fieldBuilder.SetMargin(new Margin(0, new Length(10, Pixel),0,0))
                        .SetStyle(style => style.backgroundColor = new Color(0.1f, 0.19f, 0.17f))))
                .AddPreset(factory => factory.Buttons().ButtonGame(name: "Add", locKey: "inventory.preset.add", width: new Length(75, Pixel), height: new Length(24, Pixel))))
            .AddComponent(builder => builder.SetFlexDirection(FlexDirection.Row).SetJustifyContent(Justify.SpaceBetween).SetMargin(new Margin(0,0,new Length(20, Pixel),0))
                .AddPreset(factory => factory.Buttons().ButtonGame("inventory.preset.load", new Length(155, Pixel), name: "Load"))
                .AddPreset(factory => factory.Buttons().ButtonGame("inventory.preset.remove", new Length(155, Pixel), name: "Remove")))
            .AddPreset(factory => factory.ListViews().MainMenuListView(name: "Presets", height: new Length(125, Pixel), builder: builder => builder.SetMargin(new Margin(0,0,new Length(20, Pixel),0))))
            .AddPreset(factory => factory.ScrollViews().MainScrollView("Rows", new Length(250, Pixel))).Build();
    }
}