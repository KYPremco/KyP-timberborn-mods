using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Timberborn.AssetSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.NeedBehaviorSystem;
using TimberbornAPI.AssetLoaderSystem.AssetSystem;
using TimberbornAPI.UIBuilderSystem;
using TimberbornAPI.UIBuilderSystem.PresetSystem;
using TimberDebug.Helpers;
using TimberDebug.StyleGrabber;
using UnityEngine;
using UnityEngine.UIElements;

namespace TimberDebug.DebugPanels
{
    public class SpriteExportPanel : IDebugPanel
    {
        private static readonly List<string> AllowedStyleProperties = new() { "background-image", "--background-image", "--background-tint", "--background-slice", "--background-slice-top", "--background-slice-right", "--background-slice-bottom", "--background-slice-left", "--background-slice-scale" };
        
        private static readonly List<string> AllowedDirectories = new() { "UI/Images/Core", "UI/Images/Backgrounds", "UI/Images/Buttons" };

        private readonly IFileService _fileService;

        private readonly IResourceAssetLoader _resourceAssetLoader;
        
        private readonly UIBuilder _builder;

        private readonly IAssetLoader _assetLoader;

        private readonly UiPresetFactory _presetFactory;
        
        private readonly VisualElementLoader _visualElementLoader;

        public SpriteExportPanel(UIBuilder builder, IFileService fileService, IResourceAssetLoader resourceAssetLoader, IAssetLoader assetLoader, VisualElementLoader visualElementLoader, UiPresetFactory presetFactory)
        {
            _builder = builder;
            _fileService = fileService;
            _resourceAssetLoader = resourceAssetLoader;
            _assetLoader = assetLoader;
            _visualElementLoader = visualElementLoader;
            _presetFactory = presetFactory;
        }

        public string GetPanelName()
        {
            return "Sprite Export Helper";
        }
        
        // private Action<UIComponentBuilder> CreateButtonWithLabel(string labelText, string buttonText, string buttonName)
        // {
        //     return builder => builder.SetButton().SetText("I'm a button").Build();
        // }

        public VisualElement GetPanel()
        {
            VisualElement child = _builder.CreateComponentBuilder().CreateVisualElement()
                .AddPreset(factory => factory.Sliders().Slider(1,10, text: "Test"))
                .AddPreset(factory => factory.Sliders().Slider(1,10, text: "Test Value", value: 4))
                .AddPreset(factory => factory.Sliders().Slider(1,10, text: "Test Larger"))
                .AddPreset(factory => factory.Sliders().Slider(1,10, text: "Test Omega large"))
                .AddPreset(factory => factory.Sliders().Slider(1,10, text: "Fixed width", width: 350))
                
                .AddPreset(factory => factory.Sliders().SliderInt(1,10, text: "Test Int"))
                .AddPreset(factory => factory.Sliders().SliderInt(1,10, text: "Test Int Value", value: 4))
                .AddPreset(factory => factory.Sliders().SliderInt(1,10, text: "Test Int Larger"))
                .AddPreset(factory => factory.Sliders().SliderInt(1,10, text: "Test Int Omega large"))
                .AddPreset(factory => factory.Sliders().SliderInt(1,10, text: "Fixed width Int", width: 350))
                .AddComponent(builder => builder
                    .AddPreset(factory => factory.Buttons().ArrowLeft())
                    .AddPreset(factory => factory.Buttons().ArrowUp())
                    .AddPreset(factory => factory.Buttons().ArrowRight())
                    .AddPreset(factory => factory.Buttons().ArrowDown())
                    .SetFlexDirection(FlexDirection.Row)
                )
                .AddComponent(builder => builder
                    .AddPreset(factory => factory.Buttons().ArrowLeftInverted())
                    .AddPreset(factory => factory.Buttons().ArrowUpInverted())
                    .AddPreset(factory => factory.Buttons().ArrowRightInverted())
                    .AddPreset(factory => factory.Buttons().ArrowDownInverted())
                    .SetFlexDirection(FlexDirection.Row)
                )
                .AddPreset(factory => factory.Buttons().Button(text: "AAAAAAAA"))
                .AddPreset(factory => factory.Buttons().Close())
                .AddComponent(builder => builder
                    .AddPreset(factory => factory.Buttons().LeftArrow())
                    .AddPreset(factory => factory.Buttons().UpArrow())
                    .AddPreset(factory => factory.Buttons().RightArrow())
                    .AddPreset(factory => factory.Buttons().DownArrow())
                    .SetFlexDirection(FlexDirection.Row)
                )
                .AddComponent(builder => builder
                    .AddPreset(factory => factory.Buttons().LeftArrow())
                    .AddPreset(factory => factory.Buttons().UpArrow())
                    .AddPreset(factory => factory.Buttons().RightArrow())
                    .AddPreset(factory => factory.Buttons().DownArrow())
                    .SetFlexDirection(FlexDirection.Row)
                )
                .AddComponent(builder => builder
                    .AddPreset(factory => factory.Buttons().Minus())
                    .AddPreset(factory => factory.Buttons().MinusInverted())
                    .AddPreset(factory => factory.Buttons().Plus())
                    .AddPreset(factory => factory.Buttons().PlusInverted())
                    .SetFlexDirection(FlexDirection.Row)
                )
                .AddComponent(builder => builder
                    .AddPreset(factory => factory.Buttons().CyclerLeft())
                    .AddPreset(factory => factory.Buttons().CyclerRight())
                    .AddPreset(factory => factory.Buttons().CyclerLeftMain())
                    .AddPreset(factory => factory.Buttons().CyclerRightMain())
                    .SetFlexDirection(FlexDirection.Row)
                )
                .AddPreset(factory => factory.Buttons().CircleEmpty())
                .AddPreset(factory => factory.Buttons().SliderHolder())
                .AddPreset(factory => factory.Buttons().ResetButton())
                .AddComponent(builder => builder
                    .AddPreset(factory => factory.Buttons().SpeedButton0())
                    .AddPreset(factory => factory.Buttons().SpeedButton1())
                    .AddPreset(factory => factory.Buttons().SpeedButton2())
                    .AddPreset(factory => factory.Buttons().SpeedButton3())
                    .SetFlexDirection(FlexDirection.Row)
                )
                .AddPreset(factory => factory.Buttons().BugTracker())
                .AddComponent(builder => builder
                    .AddPreset(factory => factory.Buttons().ClampDown())
                    .AddPreset(factory => factory.Buttons().ClampUp())
                    .SetFlexDirection(FlexDirection.Row)
                )
                .AddPreset(factory => factory.Buttons().LevelVisibilityReset())
                .AddComponent(builder => builder
                    .AddPreset(factory => factory.Toggles().Checkbox())
                    .AddPreset(factory => factory.Toggles().Checkmark())
                    .AddPreset(factory => factory.Toggles().CheckmarkInverted())
                    .AddPreset(factory => factory.Toggles().CheckmarkCross())
                    .AddPreset(factory => factory.Toggles().CheckmarkCrossInverted())
                    .AddPreset(factory => factory.Toggles().CheckmarkAlt())
                    .SetFlexDirection(FlexDirection.Row)
                )
                .AddPreset(factory => factory.Labels().DefaultText(text: "Omega Poggers"))
                .AddPreset(factory => factory.Labels().DefaultBold(text: "Omega Poggers"))
                .AddPreset(factory => factory.Labels().DefaultBig(text: "Omega Poggers"))
                .AddPreset(factory => factory.Labels().DefaultHeader(text: "Omega Poggers"))
                .AddPreset(factory => factory.Labels().GameTextSmall(text: "Omega Poggers"))
                .AddPreset(factory => factory.Labels().GameText(text: "Omega Poggers"))
                .AddPreset(factory => factory.Labels().GameTextBig(text: "Omega Poggers"))
                .AddPreset(factory => factory.Labels().GameTextHeading(text: "Omega Poggers"))
                .AddPreset(factory => factory.Labels().GameTextTitle(text: "Omega Poggers"))

                .Build();

            VisualElement test = _presetFactory.ScrollViews().MainScrollView(builder: builder => builder.AddChild(child));


            return test;
        }
        
        private void GenerateUssSpriteImagesFile()
        {
            string uss = "";
            List<NineSlicedImageSetting> settings = NineSlicedBackgroundPatch.NineSlicedImageSettings.Select(pair => pair.Value).ToList();
            settings.Sort((setting, imageSetting) => String.CompareOrdinal(setting.Name, imageSetting.Name));
            foreach (NineSlicedImageSetting imageSetting in settings)
            {
                uss += imageSetting.ToUssClass();
            }
            _fileService.WriteTextToFile(GetUnUsedFile(DebugPlugin.FilePath, "style", "txt"), uss);
        }

        #region ImageSetting

        private string GenerateUssClasses(ImageSetting imageSetting)
        {
            string uss = "";
            string className = imageSetting.Name.Split("/").Last();
            uss += GenerateClass(className, imageSetting.Normal, imageSetting.StyleSheet);
            uss += GenerateClass(className + ":hover", imageSetting.Hover, imageSetting.StyleSheet);
            uss += GenerateClass(className + ":active", imageSetting.Active, imageSetting.StyleSheet);
            return uss;
        }
        
        private List<ImageSetting> FilterImageSettings()
        {
            List<ImageSetting> imageSettings = new List<ImageSetting>();
            
            foreach (KeyValuePair<string, ImageSetting> imageSettingDic in VisualElementLoaderPatch.ImageSettings)
            {
                string normalizedName = imageSettingDic.Value.Name.Split(':').Last();
                
                if(!AllowedDirectories.Any(directory => normalizedName.Contains(directory)))
                    continue;
                
                if(imageSettings.Any(setting => setting.Name.Split(':').Last().Equals(normalizedName)))
                    continue;

                imageSettings.Add(imageSettingDic.Value);
            }

            imageSettings.Sort((x, y) => String.Compare(x.Name.Split(':').Last(), y.Name.Split(':').Last(), StringComparison.Ordinal));
            return imageSettings;
        }
        
        private string GenerateClass(string className, StyleComplexSelector style, StyleSheet styleSheet)
        {
            if(style == null)
                return "";

            string uss = $".{className} {{\r\n";

            foreach (StyleProperty styleProperty in style.rule.properties)
            {
                if (styleProperty.values == null || !AllowedStyleProperties.Contains(styleProperty.name))
                    continue;
                uss += $"    {styleProperty.name}: {styleSheet.ReadStyleValue(styleProperty.values.First())};\r\n";
            }

            uss += $"}}\r\n";
            
            return uss;
        }

        #endregion

        private string GetUnUsedFile(string path, string filename, string extension)
        {
            int number = 0;
            while (true)
            {
                string file = number == 0 ? Path.Combine(path, $"{filename}.{extension}") : Path.Combine(path, $"{filename}({number}).{extension}");
                if (!_fileService.FileExists(file))
                    return file;
                number++;
            }
        }
    }
}