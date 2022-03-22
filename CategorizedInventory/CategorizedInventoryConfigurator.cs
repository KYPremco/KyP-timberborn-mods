using Bindito.Core;

namespace CategorizedInventory;

public class CategorizedInventoryConfigurator : IConfigurator
{
    public void Configure(IContainerDefinition containerDefinition)
    {
        containerDefinition.Bind<StockpileInventoryCategorize>().AsSingleton();
    }
}