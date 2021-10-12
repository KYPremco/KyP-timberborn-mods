using Bindito.Core;
using PauseConfigurator.Buttons;
using PauseConfigurator.Factorys;
using PauseConfigurator.Tools;
using Timberborn.BottomBarSystem;
using Timberborn.Effects;

namespace PauseConfigurator.Configurators
{
    public class DemoConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.Bind<PauseToolFactory>().AsSingleton();
            
            containerDefinition.Bind<PauseConfiguratorGroup>().AsSingleton();
            containerDefinition.Bind<PauseTool>().AsSingleton();

            containerDefinition.Bind<PauseToolButton>().AsSingleton();
            containerDefinition.MultiBind<BottomBarModule>().ToProvider<BottomBarModuleProvider>().AsSingleton();
            
        }
        
        private class BottomBarModuleProvider : IProvider<BottomBarModule>
        {
            private readonly PauseToolButton _showMultilayeredButton;

            public BottomBarModuleProvider(PauseToolButton showMultilayeredButton) => this._showMultilayeredButton = showMultilayeredButton;

            public BottomBarModule Get()
            {
                BottomBarModule.Builder builder = new BottomBarModule.Builder();
                builder.AddLeftSectionElement((IBottomBarElementProvider) this._showMultilayeredButton, 8);
                return builder.Build();
            }
        }
    }
}