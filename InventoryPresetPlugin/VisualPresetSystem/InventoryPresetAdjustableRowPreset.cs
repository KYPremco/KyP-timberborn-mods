using TimberbornAPI.Common;
using TimberbornAPI.UIBuilderSystem;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UIElements.Length.Unit;

namespace InventoryPreset.VisualPresetSystem;

public class InventoryPresetAdjustableRowPreset : IVisualPreset
{
    public static readonly string Key = "InventoryPresetAdjustableRow";

    private readonly UIBuilder _uiBuilder;
    
    public string Id => Key;
    
    public InventoryPresetAdjustableRowPreset(UIBuilder uiBuilder)
    {
        _uiBuilder = uiBuilder;
    }
    
    public VisualElement GetElement()
    {
        return _uiBuilder.CreateComponentBuilder().CreateVisualElement()
            .SetFlexDirection(FlexDirection.Row)
            .SetMargin(new Margin(0, new Length(1, Pixel)))
            .AddPreset(factory => factory.Toggles().CheckmarkCrossInverted("Toggle"))
            .AddComponent(builder => builder
                .SetName("Image")
                .SetWidth(new Length(24, Pixel))
                .SetHeight(new Length(24, Pixel))
                .SetMargin(new Margin(new Length(3, Pixel), 0)))
            .AddPreset(factory => factory.Labels().DefaultText(name: "Name", builder: builder => builder.SetWidth(new Length(150, Pixel))))
            .AddPreset(factory => factory.TextFields().InGameTextField(
                new Length(35, Pixel),
                new Length(20, Pixel),
                false,
                new Length(12, Pixel),
                name: "Limit",
                builder: builder => builder.SetStyle(style => style.backgroundColor = new Color(0.1f, 0.19f, 0.17f))))
            .AddPreset(factory => factory.Buttons().MinusInverted("Decrease", new Length(20, Pixel), builder: builder => builder.SetMargin(new Margin(new Length(3, Pixel), 0))))
            .AddPreset(factory => factory.Buttons().PlusInverted("Increase", new Length(20, Pixel), builder: builder => builder.SetMargin(new Margin(new Length(3, Pixel), 0))))
            .Build();
    }
}