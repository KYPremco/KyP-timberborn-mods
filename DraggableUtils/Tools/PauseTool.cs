using System.Collections.Generic;
using System.Linq;
using Timberborn.AreaSelectionSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.InputSystem;
using Timberborn.InventorySystem;
using Timberborn.Localization;
using Timberborn.ToolSystem;
using UnityEngine;

namespace DraggableUtils.Tools
{
    public class PauseTool : DraggableTool, IInputProcessor
    {
        private static readonly string TitlePlayLocKey = "Kyp.PauseTool.Play.Title";
        
        private static readonly string TitlePauseLocKey = "Kyp.PauseTool.Pause.Title";
        
        private static readonly string DescriptionLocKey = "Kyp.PauseTool.Description";
        
        private static readonly string PrioritizedLocKey = "Kyp.PauseTool.Prioritized";

        private readonly ILoc _loc;
        
        private ToolDescription _toolDescription;

        private bool _pauseBuilding;

        public void Initialize(bool pauseBuilding, ToolGroup toolGroup, Color highlightColor, Color actionColor, Color areaTileColor,
            Color areaSideColor)
        {
            base.Initialize(toolGroup, highlightColor, actionColor, areaTileColor, areaSideColor);
            this._pauseBuilding = pauseBuilding;
            this.InitializeToolDescription();
        }
        
        private void InitializeToolDescription()
        {
            this._toolDescription = new ToolDescription.Builder(
                _pauseBuilding ? _loc.T(TitlePauseLocKey) : _loc.T(TitlePlayLocKey))
                .AddSection(_loc.T(DescriptionLocKey))
                .AddPrioritizedSection(_loc.T(PrioritizedLocKey))
                .Build();
        }
        
        public override ToolDescription Description() => this._toolDescription;

        public PauseTool(AreaBlockObjectPickerFactory areaBlockObjectPickerFactory, 
            InputService inputService, 
            BlockObjectSelectionDrawerFactory blockObjectSelectionDrawerFactory, 
            CursorService cursorService,
            ILoc loc) 
            : base(areaBlockObjectPickerFactory, inputService, blockObjectSelectionDrawerFactory, cursorService)
        {
            this._loc = loc;
        }

        public override IEnumerable<BlockObject> AreaSelectionExpression(IEnumerable<BlockObject> blockObjects)
        {
            return blockObjects.Where((bo =>
            {
                PausableBuilding component = bo.GetComponent<PausableBuilding>();
                return component != null && component.enabled && component.IsPausable();
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
                PausableBuilding component = blockObject.GetComponent<PausableBuilding>();
                if (component == null || !component.IsPausable())
                    continue;

                if (_pauseBuilding)
                    component.Pause();
                else
                    component.Resume();
            }
        }
    }
}