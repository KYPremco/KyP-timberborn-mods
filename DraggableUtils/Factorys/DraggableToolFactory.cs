using System;
using DraggableUtils.Tools;
using Timberborn.AreaSelectionSystem;
using Timberborn.CoreUI;
using Timberborn.Emptying;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.ToolSystem;
using Timberborn.UISound;

namespace DraggableUtils.Factorys
{
    public class DraggableToolFactory
    {
        private readonly InputService _inputService;
        
        private readonly AreaBlockObjectPickerFactory _areaBlockObjectPickerFactory;

        private readonly BlockObjectSelectionDrawerFactory _blockObjectSelectionDrawerFactory;

        private readonly CursorService _cursorService;
        
        private readonly UISoundController _uiSoundController;

        private readonly Colors _colors;

        private readonly DraggableUtilsGroup _pauseAbleToolGroup;

        private readonly ILoc _loc;
        
        public DraggableToolFactory(AreaBlockObjectPickerFactory areaBlockObjectPickerFactory,
            InputService inputService,
            BlockObjectSelectionDrawerFactory blockObjectSelectionDrawerFactory,
            CursorService cursorService,
            UISoundController uiSoundController,
            DraggableUtilsGroup pauseAbleToolGroup,
            Colors colors, 
            ILoc loc,
            EmptyInventoriesService emptyInventoriesService)
        {
            this._areaBlockObjectPickerFactory = areaBlockObjectPickerFactory;
            this._inputService = inputService;
            this._blockObjectSelectionDrawerFactory = blockObjectSelectionDrawerFactory;
            this._cursorService = cursorService;
            this._uiSoundController = uiSoundController;
            this._pauseAbleToolGroup = pauseAbleToolGroup;
            this._colors = colors;
            this._loc = loc;
        }
        
        public PauseTool CreatePauseTool(bool pauseBuildings)
        {
            PauseTool pauseTool = new PauseTool(_areaBlockObjectPickerFactory, _inputService, _blockObjectSelectionDrawerFactory, _cursorService, _loc);
            pauseTool.Initialize(pauseBuildings, (ToolGroup) _pauseAbleToolGroup, this._colors.PriorityHiglightColor, this._colors.PriorityActionColor, this._colors.PriorityTileColor, this._colors.PrioritySideColor);
            return pauseTool;
        }
        
        public HaulPrioritizeTool CreateHaulPrioritizeTool(bool prioritize)
        {
            HaulPrioritizeTool haulPrioritizeTool = new HaulPrioritizeTool(_areaBlockObjectPickerFactory, _inputService, _blockObjectSelectionDrawerFactory, _cursorService, _loc);
            haulPrioritizeTool.Initialize(prioritize, (ToolGroup) _pauseAbleToolGroup, this._colors.PriorityHiglightColor, this._colors.PriorityActionColor, this._colors.PriorityTileColor, this._colors.PrioritySideColor);
            return haulPrioritizeTool;
        }
        
        public EmptyStorageTool CreateEmptyStorageTool(bool emptyStorage)
        {
            EmptyStorageTool emptyStorageTool = new EmptyStorageTool(_areaBlockObjectPickerFactory, _inputService, _blockObjectSelectionDrawerFactory, _cursorService, _loc);
            emptyStorageTool.Initialize(emptyStorage, (ToolGroup) _pauseAbleToolGroup, this._colors.PriorityHiglightColor, this._colors.PriorityActionColor, this._colors.PriorityTileColor, this._colors.PrioritySideColor);
            return emptyStorageTool;
        }
    }
}