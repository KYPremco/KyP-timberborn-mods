using System.Collections.Generic;

namespace CategorizedInventory;

public static class Goods
{
    public static IEnumerable<string> Food = new[]
    {
        "Good.Berries.DisplayName", 
        "Good.Carrot.DisplayName", 
        "Good.GrilledPotato.DisplayName", 
        "Good.Bread.DisplayName",
        "Good.CattailCracker.DisplayName",
        "Good.GrilledChestnut.DisplayName",
        "Good.GrilledSpadderdock.DisplayName",
        "Good.MaplePastry.DisplayName"
    };
    
    public static IEnumerable<string> Materials = new[]
    {
        "Good.Plank.DisplayName",
        "Good.Gear.DisplayName",
        "Good.ScrapMetal.DisplayName",
        "Good.MetalBlock.DisplayName",
        "Good.Paper.DisplayName",
        "Good.Explosives.DisplayName",
        "Good.Book.DisplayName",
        "Good.TreatedPlank.DisplayName"
    };
    
    public static IEnumerable<string> Ingredients = new[]
    {
        "Good.Potato.DisplayName",
        "Good.Wheat.DisplayName",
        "Good.WheatFlour.DisplayName",
        "Good.CattailRoot.DisplayName",
        "Good.CattailFlour.DisplayName",
        "Good.Chestnut.DisplayName",
        "Good.Spadderdock.DisplayName",
        "Good.MapleSyrup.DisplayName",
        "Good.PineResin.DisplayName"
    };
    
    public static List<string> Combined()
    {
        List<string> combined = new List<string>();
        combined.AddRange(Food);
        combined.AddRange(Materials);
        combined.AddRange(Ingredients);
        return combined;
    }
}