using InventoryPreset.InventoryPresetSystem;
using Timberborn.BottomBarSystem;
using Timberborn.ToolSystem;

namespace InventoryPreset.InventoryPresetSystemUI;

public class InventoryPresetButton : IBottomBarElementProvider
{
    private readonly InventoryPresetTool _inventoryPresetTool;
    private readonly ToolButtonFactory _toolButtonFactory;

    public InventoryPresetButton(
        InventoryPresetTool inventoryPresetTool,
        ToolButtonFactory toolButtonFactory)
    {
        _inventoryPresetTool = inventoryPresetTool;
        _toolButtonFactory = toolButtonFactory;
    }

    public BottomBarElement GetElement()
    {
        return BottomBarElement
            .CreateSingleLevel(
                _toolButtonFactory
                .CreateGrouplessBlue(_inventoryPresetTool, "Cursor")
                .Root);
    }
}