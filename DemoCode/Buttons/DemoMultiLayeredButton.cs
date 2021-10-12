using DemoCode.Tools;
using Timberborn.BottomBarSystem;
using Timberborn.ToolSystem;

namespace DemoCode.Buttons
{
    public class DemoMultiLayeredButton : IBottomBarElementProvider
    {
        private readonly ToolGroupButtonFactory _toolGroupButtonFactory;

        private readonly DemoToolGroup _demoToolGroup;

        private readonly DemoTool _demoTool;
        
        private readonly ToolButtonFactory _toolButtonFactory;
        
        public DemoMultiLayeredButton(
            DemoTool demoTool,
            DemoToolGroup demoToolGroup,
            ToolButtonFactory toolButtonFactory,
            ToolGroupButtonFactory toolGroupButtonFactory)
        {
            this._demoTool = demoTool;
            this._demoToolGroup = demoToolGroup;
            this._toolButtonFactory = toolButtonFactory;
            this._toolGroupButtonFactory = toolGroupButtonFactory;
        }
        
        
        public BottomBarElement GetElement()
        {
            ToolGroupButton blue = this._toolGroupButtonFactory.CreateBlue((ToolGroup) this._demoToolGroup);
            
            this._demoTool.Initialize((ToolGroup) _demoToolGroup);
            this.AddTool((Tool) this._demoTool, "BeaverGeneratorTool", blue);
            return BottomBarElement.CreateMultiLevel(blue.Root, blue.ToolButtonsElement);
        }
        
        private void AddTool(Tool tool, string imageName, ToolGroupButton toolGroupButton)
        {
            ToolButton button = this._toolButtonFactory.Create(tool, imageName, toolGroupButton.ToolButtonsElement);
            toolGroupButton.AddTool(button);
        }
    }
}