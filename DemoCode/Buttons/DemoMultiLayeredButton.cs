using DemoCode.Tools;
using Timberborn.BottomBarSystem;
using Timberborn.ToolSystem;
using TimberbornAPI.AssetLoaderSystem.AssetSystem;
using UnityEngine;

namespace DemoCode.Buttons
{
    public class DemoMultiLayeredButton : IBottomBarElementProvider
    {
        private readonly ToolGroupButtonFactory _toolGroupButtonFactory;

        private readonly DemoToolGroup _demoToolGroup;

        private readonly DemoTool _demoTool;
        
        private readonly ToolButtonFactory _toolButtonFactory;

        private readonly IAssetLoader _assetLoader;
        
        public DemoMultiLayeredButton(
            DemoTool demoTool,
            DemoToolGroup demoToolGroup,
            ToolButtonFactory toolButtonFactory,
            ToolGroupButtonFactory toolGroupButtonFactory,
            IAssetLoader assetLoader)
        {
            this._demoTool = demoTool;
            this._demoToolGroup = demoToolGroup;
            this._toolButtonFactory = toolButtonFactory;
            this._toolGroupButtonFactory = toolGroupButtonFactory;
            this._assetLoader = assetLoader;
        }
        
        
        public BottomBarElement GetElement()
        {
            ToolGroupButton blue = this._toolGroupButtonFactory.CreateBlue((ToolGroup) this._demoToolGroup);
            
            this._demoTool.Initialize((ToolGroup) _demoToolGroup);
            this.AddTool((Tool) this._demoTool, _assetLoader.Load<Sprite>("DraggableUtils/UI_Buttons/pause_button"), blue);
            return BottomBarElement.CreateMultiLevel(blue.Root, blue.ToolButtonsElement);
        }
        
        private void AddTool(Tool tool, Sprite sprite, ToolGroupButton toolGroupButton)
        {
            ToolButton button = this._toolButtonFactory.Create(tool, sprite, toolGroupButton.ToolButtonsElement);
            toolGroupButton.AddTool(button);
        }
    }
}