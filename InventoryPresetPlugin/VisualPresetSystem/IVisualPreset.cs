using UnityEngine.UIElements;

namespace InventoryPreset.VisualPresetSystem;

public interface IVisualPreset
{
    string Id { get; }

    VisualElement GetElement();
}