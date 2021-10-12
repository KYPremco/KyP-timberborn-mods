using PauseConfigurator.Tools;
using Timberborn.AreaSelectionSystem;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.ToolSystem;
using Timberborn.UISound;

namespace PauseConfigurator.Factorys
{
    public class PauseToolFactory
    {
        private readonly InputService _inputService;
        
        private readonly AreaBlockObjectPickerFactory _areaBlockObjectPickerFactory;

        private readonly BlockObjectSelectionDrawerFactory _blockObjectSelectionDrawerFactory;

        private readonly CursorService _cursorService;
        
        private readonly UISoundController _uiSoundController;

        private readonly Colors _colors;

        private readonly PauseConfiguratorGroup _pauseAbleToolGroup;

        public PauseToolFactory(AreaBlockObjectPickerFactory areaBlockObjectPickerFactory,
            InputService inputService,
            BlockObjectSelectionDrawerFactory blockObjectSelectionDrawerFactory,
            CursorService cursorService,
            UISoundController uiSoundController,
            PauseConfiguratorGroup pauseAbleToolGroup,
            Colors colors)
        {
            this._areaBlockObjectPickerFactory = areaBlockObjectPickerFactory;
            this._inputService = inputService;
            this._blockObjectSelectionDrawerFactory = blockObjectSelectionDrawerFactory;
            this._cursorService = cursorService;
            this._uiSoundController = uiSoundController;
            this._pauseAbleToolGroup = pauseAbleToolGroup;
            this._colors = colors;
        }
        
        public PauseTool Create(bool pauseBuildings)
        {
            PauseTool pauseTool = new PauseTool(_areaBlockObjectPickerFactory, _inputService, _blockObjectSelectionDrawerFactory, _cursorService, _uiSoundController);
            pauseTool.Initialize((ToolGroup) _pauseAbleToolGroup, pauseBuildings, this._colors.PriorityHiglightColor, this._colors.PriorityActionColor, this._colors.PriorityTileColor, this._colors.PrioritySideColor);
            return pauseTool;
        }
    }
}