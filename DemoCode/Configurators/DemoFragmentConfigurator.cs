using Bindito.Core;
using DemoCode.Fragments;
using Timberborn.EntityPanelSystem;
using Timberborn.WaterBuildingsUI;

namespace DemoCode.Configurators
{
    public class DemoFragmentConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.Bind<DemoFragment>().AsSingleton();
            containerDefinition.MultiBind<EntityPanelModule>().ToProvider<EntityPanelModuleProvider>().AsSingleton();
        }
        
        private class EntityPanelModuleProvider : IProvider<EntityPanelModule>
        {
            private readonly DemoFragment _demoFragment;

            public EntityPanelModuleProvider(DemoFragment demoFragment)
            {
                _demoFragment = demoFragment;
            }

            public EntityPanelModule Get()
            {
                EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
                builder.AddTopFragment((IEntityPanelFragment) this._demoFragment);
                return builder.Build();
            }
        }
    }
}