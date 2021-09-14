using Askalhorn.Common.Localization;
using Askalhorn.Common.Mechanics;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Inventory.Items
{
    internal abstract class Item : IItem
    {
        public abstract TextureRegion2D Texture { get; }

        private static string[] RarityColors = {"White", "Green", "Orange"};

        public string TooltipText =>
            @$"{Name}
Тип: {Type}
Редкость: {new TextPointer("rarity", Rarity.ToString()).ToString().WithColor(RarityColors[(int) Rarity])}
Вес: {Weight} кг
{Description}
";


        public virtual bool Equals(IItem? other)

        {
            return other?.GetType() == GetType();
        }

        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract IItem.PurposeType Type { get; }
        public abstract IItem.RarityLevel Rarity { get; }
        public float Weight => 0;

        protected internal abstract IImpact Impact { get; }

        IImpact IItem.Impact => Impact;
    }
}