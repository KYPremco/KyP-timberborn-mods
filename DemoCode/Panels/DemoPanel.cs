using CustomAssetLoader.AssetSystem;
using Timberborn.CoreUI;
using Timberborn.GameSaveRuntimeSystem;
using Timberborn.GameUI;
using Timberborn.Options;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace DemoCode.Panels
{
    public class DemoPanel : ILoadableSingleton
    {
        private readonly GameLayout _gameLayout;
        private readonly IAssetLoader _assetLoader;
        private readonly VisualElementInitializer _visualElementInitializer;
        private VisualElement _test;
        private Autosaver _autosaver;


        public DemoPanel(
            GameLayout gameLayout,
            IAssetLoader assetLoader, 
            VisualElementInitializer visualElementInitializer, 
            Autosaver autosaver)
        {
            this._gameLayout = gameLayout;
            this._assetLoader = assetLoader;
            _visualElementInitializer = visualElementInitializer;
            _autosaver = autosaver;
        }

        public void Load()
        {
            // this._test = LoadVisualElement(_assetLoader.Load<VisualTreeAsset>("Demo/demo/TestLayout"));
            // _gameLayout.AddTopRight(this._test, -1);
            // this._test.Q<Button>("testButton").clicked += ButtonPressed;
        }
        
        private void ButtonPressed()
        {
            Plugin.Log.LogFatal("AAAAAAAAAAAAAAAA");
        }
        
        private VisualElement LoadVisualElement(VisualTreeAsset visualTreeAsset)
        {
            VisualElement visualElement = visualTreeAsset.CloneTree().ElementAt(0);
            this._visualElementInitializer.InitializeVisualElement(visualElement);
            return visualElement;
        }
    }
}