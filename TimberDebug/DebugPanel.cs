using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.CoreUI;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using TimberbornAPI.Common;
using TimberbornAPI.UIBuilderSystem;
using UnityEngine.UIElements;

namespace TimberDebug
{
    public class DebugPanel : IPanelController
    {
        public static Action OpenPreviewBoxDelegate;
        
        private static ImmutableArray<IDebugPanel> _debugPanels;
        
        private readonly PanelStack _panelStack;
        
        private readonly UIBuilder _uiBuilder;
        
        public DebugPanel(UIBuilder uiBuilder, PanelStack panelStack, IEnumerable<IDebugPanel> debugPanels)
        {
            _uiBuilder = uiBuilder;
            _panelStack = panelStack;
            _debugPanels = debugPanels.ToImmutableArray();
            OpenPreviewBoxDelegate = OpenPreviewBox;
        }

        private void OpenPreviewBox()
        {
            _panelStack.HideAndPush(this);
            
            ILoadableSingleton
        }

        public VisualElement GetPanel()
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            UIBoxBuilder debugBox = _uiBuilder.CreateBoxBuilder().AddCloseButton().AddHeader("UI box").SetHeight(500).SetWidth(700);

            UIBoxBuilder secondBox = _uiBuilder.CreateBoxBuilder().AddCloseButton().SetHeight(400).SetWidth(400).ModifyBox(builder =>
            {
                builder.SetMargin(new Margin(0,0,0,700));
                builder.SetStyle(style => style.position = Position.Absolute);
            });
            debugBox.ModifyWrapper(builder => builder.AddComponent(secondBox.Build()));
            
            UIBoxBuilder thirdBox = _uiBuilder.CreateBoxBuilder().AddHeader().AddCloseButton().SetHeight(400).SetWidth(400).ModifyBox(builder =>
            {
                builder.SetMargin(new Margin(0,0,0,-400));
                builder.SetStyle(style => style.position = Position.Absolute);
            });
            
            debugBox.ModifyWrapper(builder => builder.AddComponent(thirdBox.Build()));
            

            foreach (IDebugPanel debugPanel in _debugPanels)
            {
                debugBox.AddComponent(debugPanel.GetPanel());
                thirdBox.AddComponent(debugPanel.GetPanel());
                secondBox.AddComponent(debugPanel.GetPanel());
            }

            var test = debugBox.SetBoxInCenter().BuildAndInitialize();
            
            watch.Stop();

            Console.WriteLine("Time elapsed (ms): {0}", watch.ElapsedMilliseconds);
            Console.WriteLine("Time elapsed (ns): {0}", watch.Elapsed.TotalMilliseconds * 1000000);
            
            return test;


            // root.Q<Button>("CloseButton").clicked += OnUICancelled;

            
        }

        public bool OnUIConfirmed()
        {
            return false;
        }

        public void OnUICancelled()
        {
            _panelStack.Pop(this);
        }
    }
}