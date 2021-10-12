using Timberborn.ConstructionMode;
using Timberborn.ToolSystem;
using Timberborn.WaterSystemRendering;

namespace PauseConfigurator.Tools
{
    public class PauseConfiguratorGroup : ToolGroup, IWaterHider, IConstructionModeEnabler
    {
        public override string IconName => "BeaverGeneratorTool";

        public override string DisplayNameLocKey => "Pause Configurator";
    }
}