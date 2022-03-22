using System.Collections.Generic;
using HarmonyLib;
using Timberborn.CoreUI;
using TimberbornAPI.Internal;
using UnityEngine.UIElements;

namespace TimberDebug.StyleGrabber
{
    public class NineSlicedBackgroundPatch
    {
        public static IDictionary<string, NineSlicedImageSetting> NineSlicedImageSettings = new Dictionary<string, NineSlicedImageSetting>();

        [HarmonyPatch(typeof(NineSliceBackground), "GetDataFromStyle", typeof(ICustomStyle))]
        public static class ResourceAssetLoaderPatch
        {
            public static void Postfix(ICustomStyle customStyle, NineSliceBackground __instance, int ____sliceBottom, int ____sliceLeft, int ____sliceRight, int ____sliceTop, float ____sliceScale)
            {
                if(!__instance.IsNineSlice)
                    return;

                string imagePath;
                customStyle.TryGetValue(new CustomStyleProperty<string>("--background-image"), out imagePath);
                
                string name = $"{imagePath.Replace("-hover", "").Replace("-active", "")}";
                if(!NineSlicedImageSettings.ContainsKey(name))
                    NineSlicedImageSettings.Add(name, new NineSlicedImageSetting() { Name = name });

                NineSlicedImage image = new NineSlicedImage();
                image.ImagePath = imagePath;
                image.SliceBottom = ____sliceBottom;
                image.SliceLeft = ____sliceLeft;
                image.SliceRight = ____sliceRight;
                image.SliceTop = ____sliceTop;
                image.SliceScale = ____sliceScale;
                
                if (imagePath.EndsWith("-hover"))
                {
                    NineSlicedImageSettings[name].Hover = image;
                } 
                else if (imagePath.EndsWith("-active"))
                {
                    NineSlicedImageSettings[name].Active = image;
                }
                else
                {
                    NineSlicedImageSettings[name].Normal = image;
                }
            }
        }
    }
}