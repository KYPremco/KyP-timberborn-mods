using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Timberborn.CoreUI;
using TimberbornAPI.Internal;
using TimberDebug.Helpers;
using UnityEngine.UIElements;

namespace TimberDebug.StyleGrabber
{
    public static class VisualElementLoaderPatch
    {
        public static IDictionary<string, ImageSetting> ImageSettings = new Dictionary<string, ImageSetting>();

        [HarmonyPatch(typeof(VisualElementLoader), "LoadVisualElement", typeof(string))]
        public static class ResourceAssetLoaderPatch
        {
            private static void Postfix(VisualElement __result)
            {
                RecursiveImageSearch(__result);
            }

            private static void RecursiveImageSearch(VisualElement element)
            {
                foreach (VisualElement child in element.Children())
                {
                    RecursiveImageSearch(child);
                }

                if (element.styleSheetList == null)
                    return;

                foreach (StyleSheet styleSheet in element.styleSheetList)
                {
                    foreach (KeyValuePair<string, StyleComplexSelector> styleSheetOrderedClassSelector in styleSheet.orderedClassSelectors)
                    {
                        if (styleSheetOrderedClassSelector.Value == null) 
                            continue;
                        
                        //
                        
                        
                        foreach (StyleProperty ruleProperty in styleSheetOrderedClassSelector.Value.rule.properties)
                        {
                            if (ruleProperty.values == null)
                                continue;

                            if (!ruleProperty.name.StartsWith("background-image"))
                                continue;
                            
                            StyleValueHandle styleValueHandle = ruleProperty.values.First();
                            string result = styleSheet.ReadStyleValue(styleValueHandle);
                            
                            if(!result.StartsWith("UI"))
                                continue;
                            
                            string name = $"{styleSheet.name}::{result.Replace("-hover", "").Replace("-active", "")}";
                            if(!ImageSettings.ContainsKey(name))
                                ImageSettings.Add(name, new ImageSetting() { Name = name, StyleSheet = styleSheet });

                            if (result.EndsWith("-hover"))
                            {
                                ImageSettings[name].Hover = styleSheetOrderedClassSelector.Value;
                            } 
                            else if (result.EndsWith("-active"))
                            {
                                ImageSettings[name].Active = styleSheetOrderedClassSelector.Value;
                            }
                            else
                            {
                                ImageSettings[name].Normal = styleSheetOrderedClassSelector.Value;
                            }
                            
                            break;
                        }
                    }
                }
            }
        }
    }
}