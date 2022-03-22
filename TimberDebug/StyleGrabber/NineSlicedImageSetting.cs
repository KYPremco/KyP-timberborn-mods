using UnityEngine;

namespace TimberDebug.StyleGrabber
{
    public class NineSlicedImageSetting
    {
        public string Name { get; set; }
        
        public NineSlicedImage Normal { get; set; }

        public NineSlicedImage Hover { get; set; }

        public NineSlicedImage Active { get; set; }
        
        public string ToUssClass()
        {
            string uss = Normal.NineSlicedImageToClass(Name);
            
            if(Hover != null)
                uss += Hover.NineSlicedImageToClass(Name + ":hover");
            
            if(Active != null)
             uss += Active.NineSlicedImageToClass(Name + ":active");

            return uss;
        }
        
    }

    public class NineSlicedImage
    {
        public string ImagePath { get; set; }
        
        public Color32 SliceTint { get; set; }
        
        public int SliceBottom { get; set; }
        
        public int SliceLeft { get; set; }
        
        public int SliceRight { get; set; }
        
        public float SliceScale { get; set; }
        
        public int SliceTop { get; set; }
        
        public string NineSlicedImageToClass(string className)
        {
            string uss = $".{className} {{\r\n";
            
            uss += $"    --background-image: resource('{ImagePath}');\r\n";
            
            if(SliceScale.Equals(1f))
                uss += $"    --background-slice-scale: {ImagePath};\r\n";

            if (SliceBottom == SliceLeft && SliceLeft == SliceRight &&
                SliceRight == SliceTop)
            {
                uss += $"    --background-slice: {SliceBottom};\r\n";
                uss += "}\r\n";
                return uss;
            }
            
            // uss += $"    --background-slice-top: {SliceTop};\r\n";
            // uss += $"    --background-slice-right: {SliceRight};\r\n";
            // uss += $"    --background-slice-bottom: {SliceBottom};\r\n";
            // uss += $"    --background-slice-left: {SliceLeft};\r\n";
            uss += "}\r\n";

            return uss;
        }
    }
}