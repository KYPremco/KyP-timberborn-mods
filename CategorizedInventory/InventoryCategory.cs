using System.Collections.Generic;

namespace CategorizedInventory;

public class InventoryCategory
{
    public string CategoryText { get; set; }

    public IEnumerable<string> Items { get; set; }
}