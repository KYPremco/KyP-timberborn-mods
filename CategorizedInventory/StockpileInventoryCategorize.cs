using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Timberborn.CoreUI;
using Timberborn.Goods;
using Timberborn.InventorySystem;
using Timberborn.InventorySystemUI;
using Timberborn.WarehousesUI;
using TimberbornAPI.AssetLoaderSystem.AssetSystem;
using TimberbornAPI.UIBuilderSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace CategorizedInventory;

public class StockpileInventoryCategorize
{
    private static IDictionary<string, IEnumerable<string>> _categories;
    
    private static AdjustableRowsFactory _adjustableRowsFactory;

    private static UIBuilder _uiBuilder;

    public StockpileInventoryCategorize(AdjustableRowsFactory adjustableRowsFactory, UIBuilder uiBuilder, IAssetLoader assetLoader)
    {
        _adjustableRowsFactory = adjustableRowsFactory;
        _uiBuilder = uiBuilder;

        StreamReader sr = File.OpenText(Path.Combine(Paths.Location, "categories.json"));
        string json = sr.ReadToEnd();
        sr.Close();
        ConvertInventoryCategoryListToDictionary(JsonConvert.DeserializeObject<List<InventoryCategory>>(json));


        // var test = assetLoader.Load<GameObject>("timberApi/elec.extendedarchitecture/Elec.4x1Arch");
        var test = assetLoader.Load<GameObject>("timberApi", Array.Empty<string>(), "extendedarchitecture.bundle", "4x1Arch");
        ModLogger.Log.LogFatal(test);
    }

    private static void ConvertInventoryCategoryListToDictionary(List<InventoryCategory> inventoryCategories)
    {
        _categories = new Dictionary<string, IEnumerable<string>>();
        foreach (InventoryCategory inventoryCategory in inventoryCategories)
        {
            _categories.Add(inventoryCategory.CategoryText, inventoryCategory.Items);
        }
    }

    public static void AddRows(
        StockpileInventoryFragment stockpileInventoryFragment, 
        Inventory inventory, 
        List<GoodSpecification> goodSpecifications, 
        List<AdjustableRow> rows, 
        VisualElement root, 
        VisualElement content)
    {
        goodSpecifications.AddRange(inventory.AllowedGoods.Select(good => good.StorableGood.GoodSpecification));
        if (goodSpecifications.Count > 0)
        {
            rows.AddRange(CreateRows(inventory, content));
            stockpileInventoryFragment.UpdateFragment();
            root.ToggleDisplayStyle(true);
        }
        else
            root.ToggleDisplayStyle(false);
    }
    
    private static IEnumerable<AdjustableRow> CreateRows(Inventory inventory, VisualElement parent)
    {
        MethodInfo createRow = typeof(AdjustableRowsFactory).GetMethod("CreateRow", BindingFlags.NonPublic | BindingFlags.Instance);
        MethodInfo createHeader = typeof(AdjustableRowsFactory).GetMethod("CreateHeader", BindingFlags.NonPublic | BindingFlags.Instance);
        if (createRow == null || createHeader == null) yield break;
        createHeader.Invoke(_adjustableRowsFactory, new object[]{ parent });

        



        foreach (StorableGoodAmount storableGoodAmount in inventory.AllowedGoods
                     .Where(good => good.StorableGood.IsGivableAndTakeable))
            // .OrderBy(good => Goods.Combined().IndexOf(good.StorableGood.GoodSpecification.DisplayNameLocKey))
            yield return (AdjustableRow) createRow.Invoke(_adjustableRowsFactory,
                new object[] {storableGoodAmount.StorableGood.GoodSpecification, inventory, parent});
    }
}