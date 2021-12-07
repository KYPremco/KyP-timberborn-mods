using Bindito.Core;
using DraggableUtils.Buttons;
using DraggableUtils.Factorys;
using DraggableUtils.Tools;
using Timberborn.BottomBarSystem;

namespace DraggableUtils.Configurators
{
    public class DraggableUtilsConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.Bind<DraggableToolFactory>().AsSingleton();
            
            containerDefinition.Bind<DraggableUtilsGroup>().AsSingleton();
            containerDefinition.Bind<PauseTool>().AsSingleton();
            containerDefinition.Bind<HaulPrioritizeTool>().AsSingleton();
            containerDefinition.Bind<EmptyStorageTool>().AsSingleton();

            containerDefinition.Bind<DraggableToolButtons>().AsSingleton();
            containerDefinition.MultiBind<BottomBarModule>().ToProvider<BottomBarModuleProvider>().AsSingleton();
            
        }
        
        private class BottomBarModuleProvider : IProvider<BottomBarModule>
        {
            private readonly DraggableToolButtons _draggableToolButtons;

            public BottomBarModuleProvider(DraggableToolButtons draggableToolButtons) => this._draggableToolButtons = draggableToolButtons;

            public BottomBarModule Get()
            {
                BottomBarModule.Builder builder = new BottomBarModule.Builder();
                builder.AddLeftSectionElement((IBottomBarElementProvider) this._draggableToolButtons, 8);
                return builder.Build();
            }
        }
    }
}