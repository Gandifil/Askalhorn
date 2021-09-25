using Askalhorn.Common;
using Askalhorn.Render;
using Askalhorn.Text;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Inventory.Items
{
    public class Dagger: Item
    {
        public Dagger()
        {
            Name = new TextPointer("items", "Dagger_Name");
            Description = new TextPointer("items", "Dagger_Description");
            Renderer = new TextureRenderer("effects", new(1, 1));
        }
        
        public override ItemPurpose Type { get; } = ItemPurpose.Weapon;
        public override ItemRarity ItemRarity { get; } = ItemRarity.Rare;
        public override float Weight => .5f;
        protected override IImpact Impact { get; } = null;
    }
}