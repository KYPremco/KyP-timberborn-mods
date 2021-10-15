using CustomAssetLoader.AssetSystem;
using Timberborn.ConstructionMode;
using Timberborn.ToolSystem;
using Timberborn.WaterSystemRendering;
using UnityEngine;

namespace DemoCode.Tools
{
    public class DemoToolGroup : ToolGroup, IWaterHider, IConstructionModeEnabler
    {
        public DemoToolGroup(IAssetLoader assetLoader)
        {
            this.Icon = assetLoader.Load<Sprite>("Demo/SubFolder/Demo2/smiles");
        }

        public override string IconName => "PriorityToolGroupIcon";

        public override string DisplayNameLocKey => "ToolGroups.Priority";
    }
}