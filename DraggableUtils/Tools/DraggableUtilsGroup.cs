using Timberborn.ConstructionMode;
using Timberborn.ToolSystem;
using Timberborn.WaterSystemRendering;

namespace DraggableUtils.Tools
{
    public class DraggableUtilsGroup : ToolGroup, IWaterHider, IConstructionModeEnabler
    {
        public override string IconName => "BeaverGeneratorTool";

        public override string DisplayNameLocKey => "Draggable Utilities";
    }
}