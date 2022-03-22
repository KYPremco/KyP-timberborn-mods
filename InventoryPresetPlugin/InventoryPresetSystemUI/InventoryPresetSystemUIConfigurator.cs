using Bindito.Core;
using InventoryPreset.InventoryPresetSystem;
using Timberborn.BottomBarSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.ToolPanelSystem;

namespace InventoryPreset.InventoryPresetSystemUI;

public class InventoryPresetSystemUIConfigurator : IConfigurator
{
    public void Configure(IContainerDefinition containerDefinition)
    {
        containerDefinition.Bind<InventoryPresetPanel>().AsSingleton();
        containerDefinition.Bind<InventoryPresetButton>().AsSingleton();
        containerDefinition.Bind<InventoryPresetTool>().AsSingleton();
        containerDefinition.Bind<InventoryPresetFragment>().AsSingleton();
        containerDefinition.Bind<InventoryAdditionFragment>().AsSingleton();

        containerDefinition.MultiBind<BottomBarModule>().ToProvider<BottomBarModuleProvider>().AsSingleton();
        containerDefinition.MultiBind<ToolPanelModule>().ToProvider<ToolPanelModuleProvider>().AsSingleton();
        containerDefinition.MultiBind<EntityPanelModule>().ToProvider<EntityPanelModuleProvider>().AsSingleton();
    }
    
    private class EntityPanelModuleProvider : IProvider<EntityPanelModule>
    {
        private readonly InventoryPresetFragment _presetFragment;
        
        public EntityPanelModuleProvider(InventoryPresetFragment presetFragment)
        {
            _presetFragment = presetFragment;
        }
        
        public EntityPanelModule Get()
        {
            EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
            builder.AddBottomFragment(_presetFragment);
            return builder.Build();
        }
    }
    
    private class ToolPanelModuleProvider : IProvider<ToolPanelModule>
    {
        private readonly InventoryPresetPanel _inventoryPresetPanel;
        public ToolPanelModuleProvider(InventoryPresetPanel inventoryPresetPanel)
        {
            _inventoryPresetPanel = inventoryPresetPanel;
        }
        
        public ToolPanelModule Get()
        {
            ToolPanelModule.Builder builder = new ToolPanelModule.Builder();
            builder.AddFragment(_inventoryPresetPanel, 10);
            return builder.Build();
        }
    }
    
    private class BottomBarModuleProvider : IProvider<BottomBarModule>
    {
        private readonly InventoryPresetButton _inventoryInventoryPresetButton;
    
        public BottomBarModuleProvider(InventoryPresetButton inventoryInventoryPresetButton) => _inventoryInventoryPresetButton = inventoryInventoryPresetButton;
    
        public BottomBarModule Get()
        {
            BottomBarModule.Builder builder = new BottomBarModule.Builder();
            builder.AddRightSectionElement(_inventoryInventoryPresetButton);
            return builder.Build();
        }
    }
}