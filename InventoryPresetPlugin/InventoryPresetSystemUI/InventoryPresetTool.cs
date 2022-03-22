using System;
using Timberborn.ToolSystem;

namespace InventoryPreset.InventoryPresetSystemUI;

public class InventoryPresetTool : Tool
{
    public override void Enter()
    {
        Console.WriteLine("Selected");
    }

    public override void Exit()
    {
        Console.WriteLine("Deselected");
    }
}