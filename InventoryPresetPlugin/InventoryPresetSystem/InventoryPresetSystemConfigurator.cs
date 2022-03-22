using Bindito.Core;
using Timberborn.TemplateSystem;

namespace InventoryPreset.InventoryPresetSystem;

public class InventoryPresetSystemConfigurator : IConfigurator
{
    public void Configure(IContainerDefinition containerDefinition)
    {
        containerDefinition.Bind<InventoryPresetRepository>().AsSingleton();
        containerDefinition.Bind<InventoryPresetObjectSerializer>().AsSingleton();
        containerDefinition.Bind<GoodPresetRowObjectSerializer>().AsSingleton();
        containerDefinition.MultiBind<TemplateModule>().ToProvider<InventoryTemplateModuleProvider>().AsSingleton();
    }
}