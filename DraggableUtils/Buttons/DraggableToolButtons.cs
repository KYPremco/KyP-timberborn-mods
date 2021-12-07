using DraggableUtils.Factorys;
using DraggableUtils.Tools;
using Timberborn.BottomBarSystem;
using Timberborn.ToolSystem;
using TimberbornAPI.AssetLoader.AssetSystem;
using UnityEngine;

namespace DraggableUtils.Buttons
{
    public class DraggableToolButtons : IBottomBarElementProvider
    {
        private readonly ToolGroupButtonFactory _toolGroupButtonFactory;

        private readonly DraggableUtilsGroup _draggableUtilsGroup;
        
        private readonly ToolButtonFactory _toolButtonFactory;

        private readonly DraggableToolFactory _draggableToolFactory;

        private readonly HaulPrioritizeTool _haulPrioritizeTool;

        private readonly IAssetLoader _assetLoader;

        public DraggableToolButtons(
            DraggableUtilsGroup draggableUtilsGroup,
            ToolButtonFactory toolButtonFactory,
            ToolGroupButtonFactory toolGroupButtonFactory,
            DraggableToolFactory draggableToolFactory,
            IAssetLoader assetLoader, HaulPrioritizeTool haulPrioritizeTool)
        {
            this._draggableUtilsGroup = draggableUtilsGroup;
            this._toolButtonFactory = toolButtonFactory;
            this._toolGroupButtonFactory = toolGroupButtonFactory;
            this._draggableToolFactory = draggableToolFactory;
            this._assetLoader = assetLoader;
            this._haulPrioritizeTool = haulPrioritizeTool;
        }
        
        
        public BottomBarElement GetElement()
        {
            ToolGroupButton blue = this._toolGroupButtonFactory.CreateBlue((ToolGroup) this._draggableUtilsGroup);
            
            //Pause buildings
            this.AddTool((Tool) this._draggableToolFactory.CreatePauseTool(), _assetLoader.Load<Sprite>("DraggableUtils/UI_Buttons/pause_button"), blue);

            //Prioritize haulers
            this.AddTool(this._draggableToolFactory.CreateHaulPrioritizeTool(), _assetLoader.Load<Sprite>("DraggableUtils/UI_Buttons/hauler_button"), blue);

            //Empty storage
            this.AddTool(this._draggableToolFactory.CreateEmptyStorageTool(), _assetLoader.Load<Sprite>("DraggableUtils/UI_Buttons/storage_empty_button"), blue);

            return BottomBarElement.CreateMultiLevel(blue.Root, blue.ToolButtonsElement);
        }
        
        private void AddTool(Tool tool, Sprite sprite, ToolGroupButton toolGroupButton)
        {
            ToolButton button = this._toolButtonFactory.Create(tool, sprite, toolGroupButton.ToolButtonsElement);
            toolGroupButton.AddTool(button);
        }
    }
}