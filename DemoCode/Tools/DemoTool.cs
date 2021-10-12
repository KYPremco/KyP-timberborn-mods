using Timberborn.ConstructionMode;
using Timberborn.ToolSystem;
using Timberborn.WaterSystemRendering;

namespace DemoCode.Tools
{
    public class DemoTool : Tool, IWaterHider, IConstructionModeEnabler
    {
        public override void Enter()
        {
            Plugin.Log.LogInfo("I GOT PRESSED");
        }

        public override void Exit()
        {
            Plugin.Log.LogInfo("WHY.. WHY.. YOU BULLYING");
        }

        public void Initialize(ToolGroup toolGroup)
        {
            this.ToolGroup = toolGroup;
        }
    }
}