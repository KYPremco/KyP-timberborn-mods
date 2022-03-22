using System.Text.RegularExpressions;

namespace InventoryPreset.Helpers;

public static class TrimmedStringHelper
{
    public static string TrimBetween(this string text)
    {
        return Regex.Replace(text.Trim(), " +", " ");
    }
}