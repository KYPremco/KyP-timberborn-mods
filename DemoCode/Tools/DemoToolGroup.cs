using Timberborn.ConstructionMode;
using Timberborn.ToolSystem;
using Timberborn.WaterSystemRendering;

namespace DemoCode.Tools
{
    public class DemoToolGroup : ToolGroup, IWaterHider, IConstructionModeEnabler
    {
        public override string IconName => "PriorityToolGroupIcon";

        public override string DisplayNameLocKey => "ToolGroups.Priority";
    }
}