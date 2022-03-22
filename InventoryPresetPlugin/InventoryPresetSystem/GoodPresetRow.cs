using Timberborn.Goods;

namespace InventoryPreset.InventoryPresetSystem;

public class GoodPresetRow
{
    public readonly GoodSpecification GoodSpecification;

    public readonly int DesiredAmount;

    public readonly bool IsDesired;
    
    public GoodPresetRow(int desiredAmount, GoodSpecification goodSpecification, bool isDesired)
    {
        DesiredAmount = desiredAmount;
        GoodSpecification = goodSpecification;
        IsDesired = isDesired;
    }
}