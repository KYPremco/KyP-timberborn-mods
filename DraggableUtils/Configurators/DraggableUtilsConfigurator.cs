using Bindito.Core;
using DraggableUtils.Buttons;
using DraggableUtils.Factorys;
using DraggableUtils.Tools;
using Timberborn.BottomBarSystem;
using Timberborn.Effects;

namespace DraggableUtils.Configurators
{
    public class DraggableUtilsConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.Bind<PauseToolFactory>().AsSingleton();
            
            containerDefinition.Bind<DraggableUtilsGroup>().AsSingleton();
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