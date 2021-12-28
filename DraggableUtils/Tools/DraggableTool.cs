using System;
using System.Collections.Generic;
using Timberborn.AreaSelectionSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.InputSystem;
using Timberborn.ToolSystem;
using UnityEngine;

namespace DraggableUtils.Tools
{
    public abstract class DraggableTool : Tool, IInputProcessor
    {
        protected readonly InputService InputService;

        private readonly AreaBlockObjectPickerFactory _areaBlockObjectPickerFactory;

        private readonly BlockObjectSelectionDrawerFactory _blockObjectSelectionDrawerFactory;

        private readonly CursorService _cursorService;
        
        private BlockObjectSelectionDrawer _actionSelectionDrawer;

        private BlockObjectSelectionDrawer _highlightSelectionDrawer;
        
        private AreaBlockObjectPicker _areaBlockObjectPicker;

        public DraggableTool(AreaBlockObjectPickerFactory areaBlockObjectPickerFactory,
            InputService inputService,
            BlockObjectSelectionDrawerFactory blockObjectSelectionDrawerFactory,
            CursorService cursorService)
        {
            this._areaBlockObjectPickerFactory = areaBlockObjectPickerFactory;
            this.InputService = inputService;
            this._blockObjectSelectionDrawerFactory = blockObjectSelectionDrawerFactory;
            this._cursorService = cursorService;
        }

        public abstract IEnumerable<BlockObject> AreaSelectionExpression(IEnumerable<BlockObject> blockObjects);

        public bool ProcessInput() => this._areaBlockObjectPicker.PickBlockObjects<PausableBuilding>(
            new AreaBlockObjectPicker.Callback(this.PreviewCallback),
            new AreaBlockObjectPicker.Callback(this.ActionCallback), new Action(this.ShowNoneCallback));

        public override void Enter()
        {
            this.InputService.AddInputProcessor((IInputProcessor) this);
            this._areaBlockObjectPicker = this._areaBlockObjectPickerFactory.Create();
        }

        public override void Exit()
        {
            this._cursorService.ResetCursor();
            this._highlightSelectionDrawer.StopDrawing();
            this._actionSelectionDrawer.StopDrawing();
            this.InputService.RemoveInputProcessor((IInputProcessor) this);
        }
        
        protected void InitializeTool(ToolGroup toolGroup,
            Color highlightColor,
            Color actionColor,
            Color areaTileColor,
            Color areaSideColor)
        {
            this._highlightSelectionDrawer =
                this._blockObjectSelectionDrawerFactory.Create(highlightColor, areaTileColor, areaSideColor);
            this._actionSelectionDrawer =
                this._blockObjectSelectionDrawerFactory.Create(actionColor, areaTileColor, areaSideColor);
            this.ToolGroup = toolGroup;
        }
        
        private void PreviewCallback(
            IEnumerable<BlockObject> blockObjects,
            Vector3Int start,
            Vector3Int end,
            bool selectionStarted,
            bool selectingArea)
        {
            IEnumerable<BlockObject>blockObjects1 = AreaSelectionExpression(blockObjects);
            if (selectionStarted && !selectingArea)
                this._actionSelectionDrawer.Draw(blockObjects1, start, end, false);
            else if (selectingArea)
                this._actionSelectionDrawer.Draw(blockObjects1, start, end, true);
            else
                this._highlightSelectionDrawer.Draw(blockObjects1, start, end, false);
        }
        
        protected abstract void ActionCallback(
            IEnumerable<BlockObject> blockObjects,
            Vector3Int start,
            Vector3Int end,
            bool selectionStarted,
            bool selectingArea);
        

        private void ShowNoneCallback()
        {
            this._highlightSelectionDrawer.StopDrawing();
            this._actionSelectionDrawer.StopDrawing();
        }
    }
}