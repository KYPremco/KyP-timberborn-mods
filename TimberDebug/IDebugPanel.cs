using UnityEngine.UIElements;

namespace TimberDebug
{
    public interface IDebugPanel
    {
        string GetPanelName();
        
        VisualElement GetPanel();
    }
}