using System.Collections.Generic;
using System.Linq;
using Timberborn.AreaSelectionSystem;
using Timberborn.BlockSystem;
using Timberborn.Hauling;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.ToolSystem;
using UnityEngine;

namespace DraggableUtils.Tools
{
    public class HaulPrioritizeTool : DraggableTool, IInputProcessor
    {
        private static readonly string TitleLocKey = "Kyp.HaulPrioritizeTool.Title";

        private static readonly string DescriptionLocKey = "Kyp.HaulPrioritizeTool.Description";
        
        private static readonly string PrioritizedLocKey = "Kyp.HaulPrioritizeTool.Prioritized";

        private readonly ILoc _loc;
        
        private ToolDescription _toolDescription;
        
        public HaulPrioritizeTool(AreaBlockObjectPickerFactory areaBlockObjectPickerFactory, 
            InputService inputService, 
            BlockObjectSelectionDrawerFactory blockObjectSelectionDrawerFactory, 
            CursorService cursorService,
            ILoc loc) 
            : base(areaBlockObjectPickerFactory, inputService, blockObjectSelectionDrawerFactory, cursorService)
        {
            this._loc = loc;
        }
        
        public void Initialize(ToolGroup toolGroup,
            Color highlightColor,
            Color actionColor,
            Color areaTileColor,
            Color areaSideColor)
        {
            base.InitializeTool(toolGroup, highlightColor, actionColor, areaTileColor, areaSideColor);
            this.InitializeToolDescription();
        }
        
        private void InitializeToolDescription()
        {
            this._toolDescription = new ToolDescription.Builder(_loc.T(TitleLocKey))
                .AddSection(_loc.T(DescriptionLocKey))
                .AddPrioritizedSection(_loc.T(PrioritizedLocKey))
                .Build();
        }
        
        public override ToolDescription Description() => this._toolDescription;

        public override IEnumerable<BlockObject> AreaSelectionExpression(IEnumerable<BlockObject> blockObjects)
        {
            return blockObjects.Where((bo =>
            {
                HaulPrioritizable component = bo.GetComponent<HaulPrioritizable>();
                return component != null && component.enabled;
            }));
        }

        protected override void ActionCallback(
            IEnumerable<BlockObject> blockObjects,
            Vector3Int start,
            Vector3Int end,
            bool selectionStarted,
            bool selectingArea)
        {
            foreach (BlockObject blockObject in blockObjects)
            {
                HaulPrioritizable component = blockObject.GetComponent<HaulPrioritizable>();
                if (component == null || !component.enabled)
                    continue;
                
                component.Prioritized = !InputService.IsShiftHeld;
            }
        }
    }
}