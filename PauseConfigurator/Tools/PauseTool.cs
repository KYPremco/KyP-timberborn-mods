using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.AreaSelectionSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.InputSystem;
using Timberborn.PrioritySystemUI;
using Timberborn.ToolSystem;
using Timberborn.UISound;
using UnityEngine;

namespace PauseConfigurator.Tools
{
    public class PauseTool : Tool, IInputProcessor
    {
        private readonly InputService _inputService;

        private readonly AreaBlockObjectPickerFactory _areaBlockObjectPickerFactory;

        private readonly BlockObjectSelectionDrawerFactory _blockObjectSelectionDrawerFactory;

        private readonly CursorService _cursorService;

        private readonly UISoundController _uiSoundController;
        
        private BlockObjectSelectionDrawer _actionSelectionDrawer;

        private BlockObjectSelectionDrawer _highlightSelectionDrawer;
        
        private AreaBlockObjectPicker _areaBlockObjectPicker;

        private bool _pauseBuilding;
        
        private ToolDescription _toolDescription;

        public PauseTool(AreaBlockObjectPickerFactory areaBlockObjectPickerFactory,
            InputService inputService,
            BlockObjectSelectionDrawerFactory blockObjectSelectionDrawerFactory,
            CursorService cursorService,
            UISoundController uiSoundController)
        {
            this._areaBlockObjectPickerFactory = areaBlockObjectPickerFactory;
            this._inputService = inputService;
            this._blockObjectSelectionDrawerFactory = blockObjectSelectionDrawerFactory;
            this._cursorService = cursorService;
            this._uiSoundController = uiSoundController;
        }

        public bool ProcessInput() => this._areaBlockObjectPicker.PickBlockObjects<PausableBuilding>(
            new AreaBlockObjectPicker.Callback(this.PreviewCallback),
            new AreaBlockObjectPicker.Callback(this.ActionCallback), new Action(this.ShowNoneCallback));

        public override void Enter()
        {
            this._inputService.AddInputProcessor((IInputProcessor) this);
            this._areaBlockObjectPicker = this._areaBlockObjectPickerFactory.Create();
        }

        public override void Exit()
        {
            this._cursorService.ResetCursor();
            this._highlightSelectionDrawer.StopDrawing();
            this._actionSelectionDrawer.StopDrawing();
            this._inputService.RemoveInputProcessor((IInputProcessor) this);
        }
        
        public override ToolDescription Description() => this._toolDescription;

        public void Initialize(ToolGroup toolGroup,
            bool pauseBuilding,
            Color highlightColor,
            Color actionColor,
            Color areaTileColor,
            Color areaSideColor)
        {
            this._pauseBuilding = pauseBuilding;
            this._highlightSelectionDrawer =
                this._blockObjectSelectionDrawerFactory.Create(highlightColor, areaTileColor, areaSideColor);
            this._actionSelectionDrawer =
                this._blockObjectSelectionDrawerFactory.Create(actionColor, areaTileColor, areaSideColor);
            this.ToolGroup = toolGroup;

            this.InitializeToolDescription();
        }

        private void PreviewCallback(
            IEnumerable<BlockObject> blockObjects,
            Vector3Int start,
            Vector3Int end,
            bool selectionStarted,
            bool selectingArea)
        {
            IEnumerable<BlockObject> blockObjects1 = blockObjects.Where((bo =>
            {
                PausableBuilding component = bo.GetComponent<PausableBuilding>();
                return component != null && component.enabled && component.IsPausable();
            }));
            if (selectionStarted && !selectingArea)
                this._actionSelectionDrawer.Draw(blockObjects1, start, end, false);
            else if (selectingArea)
                this._actionSelectionDrawer.Draw(blockObjects1, start, end, true);
            else
                this._highlightSelectionDrawer.Draw(blockObjects1, start, end, false);
        }

        private void ActionCallback(
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

        private void ShowNoneCallback()
        {
            this._highlightSelectionDrawer.StopDrawing();
            this._actionSelectionDrawer.StopDrawing();
        }

        private void InitializeToolDescription()
        {
            this._toolDescription = new ToolDescription.Builder(
                _pauseBuilding ? "Pause buildings" : "Resume buildings")
                .AddSection("Use this tool to set the status of buildings.")
                .AddPrioritizedSection("Click an item to set it's status or hold to select a bigger area.")
                .Build();
        }
    }
}