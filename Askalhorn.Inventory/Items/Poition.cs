namespace Askalhorn.Inventory.Items
{
    public abstract class Poition: Item
    {
        public override IItem.PurposeType Type { get; } = IItem.PurposeType.Poition;
    }
}