using System;
using Askalhorn.Common;
using Askalhorn.Render;
using Askalhorn.Text;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Inventory.Items
{
    public abstract class Item : IItem
    {
        private static string[] RarityColors = {"White", "Green", "Orange"};

        public string TooltipText =>
            @$"{Name}
Тип: {Type}
Редкость: {new TextPointer("rarity", ItemRarity.ToString()).ToString().WithColor(RarityColors[(int) ItemRarity])}
Вес: {Weight} кг
{Description}
";

        public event Action OnChanged;

        public virtual bool Equals(IItem? other)
        {
            return other?.GetType() == GetType();
        }

        public TextureRenderer Renderer { get; set; }

        string IItem.Name => Name.ToString();
        public TextPointer Name { get; set; }
        
        public TextPointer Description { get; set; }
        
        public abstract ItemPurpose Type { get; }
        public abstract ItemRarity ItemRarity { get; }
        public abstract float Weight { get; }

        protected abstract IImpact Impact { get; }

        IImpact IItem.Impact => Impact;
    }
}