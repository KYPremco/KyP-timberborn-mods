using System.IO;
using CustomAssetLoader.AssetSystem;
using DraggableUtils.Factorys;
using DraggableUtils.Tools;
using Timberborn.AssetSystem;
using Timberborn.BottomBarSystem;
using Timberborn.CoreUI;
using Timberborn.PrioritySystem;
using Timberborn.PrioritySystemUI;
using Timberborn.ToolSystem;
using UnityEngine;

namespace DraggableUtils.Buttons
{
    public class PauseToolButton : IBottomBarElementProvider
    {
        private readonly ToolGroupButtonFactory _toolGroupButtonFactory;

        private readonly DraggableUtilsGroup _draggableUtilsGroup;
        
        private readonly ToolButtonFactory _toolButtonFactory;

        private readonly PauseToolFactory _pauseToolFactory;
        
        private readonly PrioritySpriteLoader _prioritySpriteLoader;

        private readonly IAssetLoader _assetLoader;

        public PauseToolButton(
            DraggableUtilsGroup draggableUtilsGroup,
            ToolButtonFactory toolButtonFactory,
            ToolGroupButtonFactory toolGroupButtonFactory,
            PauseToolFactory pauseToolFactory, 
            PrioritySpriteLoader prioritySpriteLoader,
            IAssetLoader assetLoader)
        {
            this._draggableUtilsGroup = draggableUtilsGroup;
            this._toolButtonFactory = toolButtonFactory;
            this._toolGroupButtonFactory = toolGroupButtonFactory;
            this._pauseToolFactory = pauseToolFactory;
            this._prioritySpriteLoader = prioritySpriteLoader;
            this._assetLoader = assetLoader;
        }
        
        
        public BottomBarElement GetElement()
        {
            ToolGroupButton blue = this._toolGroupButtonFactory.CreateBlue((ToolGroup) this._draggableUtilsGroup);
            
            //Pause buildings
            this.AddTool((Tool) this._pauseToolFactory.Create(true), _assetLoader.Load<Sprite>("DraggableUtils/UI_Buttons/pause_button"), blue);
            
            //Resume buildings
            this.AddTool((Tool) this._pauseToolFactory.Create(false), _assetLoader.Load<Sprite>("DraggableUtils/UI_Buttons/play_button"), blue);
            return BottomBarElement.CreateMultiLevel(blue.Root, blue.ToolButtonsElement);
        }
        
        private void AddTool(Tool tool, Sprite sprite, ToolGroupButton toolGroupButton)
        {
            ToolButton button = this._toolButtonFactory.Create(tool, sprite, toolGroupButton.ToolButtonsElement);
            toolGroupButton.AddTool(button);
        }
    }
}