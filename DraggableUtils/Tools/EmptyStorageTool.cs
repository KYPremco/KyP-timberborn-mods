using System.Collections.Generic;
using System.Linq;
using Timberborn.AreaSelectionSystem;
using Timberborn.BlockSystem;
using Timberborn.Emptying;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.ToolSystem;
using UnityEngine;

namespace DraggableUtils.Tools
{
    public class EmptyStorageTool : DraggableTool, IInputProcessor
    {
        private static readonly string TitleLocKey = "Kyp.EmptyStorageTool.Title";

        private static readonly string DescriptionLocKey = "Kyp.EmptyStorageTool.Description";
        
        private static readonly string PrioritizedLocKey = "Kyp.EmptyStorageTool.Prioritized";

        private readonly ILoc _loc;
        
        private ToolDescription _toolDescription;
        
        public EmptyStorageTool(AreaBlockObjectPickerFactory areaBlockObjectPickerFactory, 
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
            InitializeToolDescription();
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
                Emptiable component = bo.GetComponent<Emptiable>();
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
                Emptiable component = blockObject.GetComponent<Emptiable>();
                if (component == null || !component.enabled)
                    continue;
                
                if (!InputService.IsShiftHeld)
                    component.MarkForEmptyingWithStatus();
                else
                    component.UnmarkForEmptying();
            }
        }
    }
}