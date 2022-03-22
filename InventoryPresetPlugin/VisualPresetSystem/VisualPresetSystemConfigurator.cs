using Bindito.Core;

namespace InventoryPreset.VisualPresetSystem;

public class VisualPresetSystemConfigurator : IConfigurator
{
    public void Configure(IContainerDefinition containerDefinition)
    {
        containerDefinition.MultiBind<IVisualPreset>().To<InventoryPresetAdjustableRowPreset>().AsSingleton();
        containerDefinition.MultiBind<IVisualPreset>().To<InventoryPresetPanelPreset>().AsSingleton();
        containerDefinition.MultiBind<IVisualPreset>().To<InventoryPresetFragmentPreset>().AsSingleton();
        containerDefinition.Bind<VisualPresetLoader>().AsSingleton();
    }
}