using System;
using Timberborn.BlockSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.WaterBuildings;
using UnityEngine;
using UnityEngine.UIElements;

namespace DemoCode.Fragments
{
    public class DemoFragment : IEntityPanelFragment
    {
        private readonly VisualElementLoader _visualElementLoader;
        
        private readonly ILoc _loc;
        
        private VisualElement _root;
        
        private DepthMarker _depthMarker;

        private bool _toggleFragment;

        public static DemoFragment Instance;

        public DemoFragment(VisualElementLoader visualElementLoader, ILoc loc)
        {
            Plugin.Log.LogWarning("INITIALIZED BROOO");
            this._visualElementLoader = visualElementLoader;
            this._loc = loc;
            Instance = this;
        }

        public VisualElement InitializeFragment()
        {
            this._root = new Label("I AM A BUTIFULL BEEVER");
            // this._root.
            // this._root.Q<Button>("ResetHighestWaterLevelButton", (string) null).clicked += (Action) (() => this.ClickMePlease());
            this._root.ToggleDisplayStyle(false);
            return this._root;
        }
        
        public void ShowFragment(GameObject entity) => this._depthMarker = entity.GetComponent<DepthMarker>();
        
        public void ClearFragment()
        {
            this._depthMarker = (DepthMarker) null;
            this._root.ToggleDisplayStyle(false);
        }

        public void UpdateFragment()
        {
            this._root.ToggleDisplayStyle(true);
        }
        
        private void ClickMePlease()
        {
            Plugin.Log.LogWarning("I AM CLICKED");
        }
    }
}