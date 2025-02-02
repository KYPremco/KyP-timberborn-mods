using Bindito.Core;

namespace PumpExtender;

[Context("MainMenu")]
public class PumpExtenderConfigurator : IConfigurator
{
    public void Configure(IContainerDefinition containerDefinition)
    {
        containerDefinition.Bind<GlobalPumpSettings>().AsSingleton();
        containerDefinition.Bind<ExtendablePumpSettings>().AsSingleton();
    }
}