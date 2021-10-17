using System.Collections.Generic;

namespace DraggableUtils.Localization
{
    internal static class LocalizationList
    {
        public static readonly Dictionary<string, string> English = new Dictionary<string, string>()
        {
            { "Kyp.ToolGroups.DraggableUtils", "Draggable utilities" },
            { "Kyp.PauseTool.Play.Title", "Resume buildings" },
            { "Kyp.PauseTool.Pause.Title", "Pause buildings" },
            { "Kyp.PauseTool.Description", "Use this tool to set the status of buildings." },
            { "Kyp.PauseTool.Prioritized", "Click an item to set it's status or hold to select a bigger area." },
            
            { "Kyp.HaulPrioritizeTool.Start.Title", "Prioritize haulers" },
            { "Kyp.HaulPrioritizeTool.Stop.Title", "Deprioritize haulers" },
            { "Kyp.HaulPrioritizeTool.Description", "Use this tool to set the priority by haulers." },
            { "Kyp.HaulPrioritizeTool.Prioritized", "Click an item to set it's status or hold to select a bigger area." },
            
            { "Kyp.EmptyStorageTool.Start.Title", "Empty storage" },
            { "Kyp.EmptyStorageTool.Stop.Title", "Unempty storage" },
            { "Kyp.EmptyStorageTool.Description", "Use this tool to set the status of empty storage." },
            { "Kyp.EmptyStorageTool.Prioritized", "Click an item to set it's status or hold to select a bigger area." },
        };
    }
}