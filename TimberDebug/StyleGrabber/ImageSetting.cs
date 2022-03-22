using System;
using UnityEngine.UIElements;

namespace TimberDebug.StyleGrabber
{
    public class ImageSetting : IComparable<ImageSetting>
    {
        public StyleSheet StyleSheet { get; set; }
        
        public string Name { get; set; }

        public StyleComplexSelector Normal { get; set; }

        public StyleComplexSelector Hover { get; set; }

        public StyleComplexSelector Active { get; set; }

        public int CompareTo(ImageSetting other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }
    }
}