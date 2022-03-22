using Bindito.Core;
using Timberborn.InventorySystem;
using Timberborn.TemplateSystem;
using Timberborn.Warehouses;

namespace InventoryPreset.InventoryPresetSystem;

public class InventoryTemplateModuleProvider : IProvider<TemplateModule>
{
    public TemplateModule Get()
    {
        TemplateModule.Builder builder = new TemplateModule.Builder();
        builder.AddDecorator<Stockpile, InventoryPresetAssigner>();
        return builder.Build();
    }
}