namespace Askalhorn.Common.Inventory.Items
{
    internal abstract class Poition: Item
    {
        public override IItem.PurposeType Type { get; } = IItem.PurposeType.Poition;
    }
}