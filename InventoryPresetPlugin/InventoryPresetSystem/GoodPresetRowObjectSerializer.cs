using Timberborn.Goods;
using Timberborn.Persistence;

namespace InventoryPreset.InventoryPresetSystem;

public class GoodPresetRowObjectSerializer : IObjectSerializer<GoodPresetRow>
{
    private static readonly PropertyKey<GoodSpecification> GoodSpecificationKey = new PropertyKey<GoodSpecification>("Good");

    private static readonly PropertyKey<int> DesiredAmountKey = new PropertyKey<int>("DesiredAmount");

    private static readonly PropertyKey<bool> IsDesiredKey = new PropertyKey<bool>("IsDesired");

    public void Serialize(GoodPresetRow goodPresetRow, IObjectSaver objectSaver)
    {
        objectSaver.Set(GoodSpecificationKey, goodPresetRow.GoodSpecification);
        objectSaver.Set(DesiredAmountKey, goodPresetRow.DesiredAmount);
        objectSaver.Set(IsDesiredKey, goodPresetRow.IsDesired);
        
    }

    public Obsoletable<GoodPresetRow> Deserialize(IObjectLoader objectLoader)
    {
        int desiredAmount = objectLoader.Get<int>(DesiredAmountKey);
        bool isDesired = objectLoader.Get<bool>(IsDesiredKey);
        GoodSpecification goodSpecification;
        return !objectLoader.GetObsoletable<GoodSpecification>(GoodSpecificationKey, out goodSpecification) ? 
            new Obsoletable<GoodPresetRow>() : 
            (Obsoletable<GoodPresetRow>) new GoodPresetRow(desiredAmount, goodSpecification, isDesired);
    }
}