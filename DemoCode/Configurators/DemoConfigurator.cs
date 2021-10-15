using Bindito.Core;
using DemoCode.Buttons;
using DemoCode.Tools;
using Timberborn.BottomBarSystem;

namespace DemoCode.Configurators
{
    public class DemoConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.Bind<DemoToolGroup>().AsSingleton();
            containerDefinition.Bind<DemoTool>().AsSingleton();

            containerDefinition.Bind<DemoMultiLayeredButton>().AsSingleton();
            containerDefinition.MultiBind<BottomBarModule>().ToProvider<BottomBarModuleProvider>().AsSingleton();
        }
        
        private class BottomBarModuleProvider : IProvider<BottomBarModule>
        {
            private readonly DemoMultiLayeredButton _showMultilayeredButton;

            public BottomBarModuleProvider(DemoMultiLayeredButton showMultilayeredButton) => this._showMultilayeredButton = showMultilayeredButton;

            public BottomBarModule Get()
            {
                BottomBarModule.Builder builder = new BottomBarModule.Builder();
                builder.AddRightSectionElement((IBottomBarElementProvider) this._showMultilayeredButton);
                return builder.Build();
            }
        }
    }
}