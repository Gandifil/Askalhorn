namespace Askalhorn.Inventory.Items
{
    public abstract class Poition: Item
    {
        public override ItemPurpose Type { get; } = ItemPurpose.Poition;

        public override float Weight => .5f;
    }
}