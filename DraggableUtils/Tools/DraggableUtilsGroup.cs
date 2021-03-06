using Timberborn.ConstructionMode;
using Timberborn.ToolSystem;
using Timberborn.WaterSystemRendering;
using TimberbornAPI.AssetLoaderSystem.AssetSystem;
using UnityEngine;

namespace DraggableUtils.Tools
{
    public class DraggableUtilsGroup : ToolGroup, IWaterHider, IConstructionModeEnabler
    {
        public DraggableUtilsGroup(IAssetLoader assetLoader)
        {
            this.Icon = assetLoader.Load<Sprite>("DraggableUtils/UI_Buttons/group_button");
        }
        
        public override string IconName => "BeaverGeneratorTool";

        public override string DisplayNameLocKey => "Kyp.ToolGroups.DraggableUtils";
    }
}