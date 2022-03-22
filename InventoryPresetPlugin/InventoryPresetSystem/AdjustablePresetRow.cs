using Timberborn.Goods;

namespace InventoryPreset.InventoryPresetSystem;

public class AdjustablePresetRow
{
    public readonly GoodSpecification GoodSpecification;

    public int DesiredAmount;

    public bool Active;

    public AdjustablePresetRow(int desiredAmount, GoodSpecification goodSpecification)
    {
        DesiredAmount = desiredAmount;
        GoodSpecification = goodSpecification;
        Active = true;
    }
}