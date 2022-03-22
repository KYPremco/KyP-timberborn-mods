using System.Globalization;
using UnityEngine.UIElements;

namespace TimberDebug.Helpers
{
    public static class StylePropertyExtender
    {
        public static string ReadStyleValue(this StyleSheet styleSheet, StyleValueHandle handle)
        {
            switch (handle.valueType)
            {
                case StyleValueType.Invalid:
                    return "Invalid";
                case StyleValueType.Keyword:
                    return styleSheet.ReadKeyword(handle).ToString();
                case StyleValueType.Float:
                    return styleSheet.ReadFloat(handle).ToString(CultureInfo.InvariantCulture);
                case StyleValueType.Dimension:
                    return styleSheet.ReadDimension(handle).ToString();
                case StyleValueType.Color:
                    return styleSheet.ReadColor(handle).ToString();
                case StyleValueType.ResourcePath:
                    return styleSheet.ReadResourcePath(handle);
                case StyleValueType.AssetReference:
                    return styleSheet.ReadAssetReference(handle).ToString();
                case StyleValueType.Enum:
                    return styleSheet.ReadEnum(handle).ToString();
                case StyleValueType.Variable:
                    return styleSheet.ReadVariable(handle).ToString();
                case StyleValueType.String:
                    return styleSheet.ReadString(handle).ToString();
                case StyleValueType.Function:
                    return styleSheet.ReadFunction(handle).ToString();
                case StyleValueType.CommaSeparator:
                    return "Comma";
                case StyleValueType.ScalableImage:
                    return styleSheet.ReadScalableImage(handle).ToString();
                case StyleValueType.MissingAssetReference:
                    return styleSheet.ReadMissingAssetReferenceUrl(handle).ToString();
                default:
                    return "None";
            }
        }
    }
}