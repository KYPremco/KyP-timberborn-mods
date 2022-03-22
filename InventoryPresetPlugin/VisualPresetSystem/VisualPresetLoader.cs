using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using TimberbornAPI.UIBuilderSystem;
using UnityEngine.UIElements;

namespace InventoryPreset.VisualPresetSystem;

public class VisualPresetLoader
{
    private readonly ImmutableArray<IVisualPreset> _presets;

    private readonly UIBuilder _uiBuilder;

    public VisualPresetLoader(IEnumerable<IVisualPreset> visualPresets, UIBuilder uiBuilder)
    {
        _uiBuilder = uiBuilder;
        _presets = visualPresets.ToImmutableArray();
        ValidatePresets(_presets);
    }
    
    private void ValidatePresets(ImmutableArray<IVisualPreset> presets)
    {
        List<string> duplicates = presets.GroupBy(x => x.Id).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
        foreach (string duplicate in duplicates)
        {
            ModLogger.Log.LogFatal($"Found a duplicated visual preset: {duplicate}");
        }
    }
    
    public VisualElement LoadVisualElement(string elementName)
    {
        VisualElement element = _presets.Single(preset => preset.Id.Equals(elementName)).GetElement();
        _uiBuilder.InitializeVisualElement(element);
        return element;
    }
}