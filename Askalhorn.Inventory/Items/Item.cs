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
        
        protected virtual string PreDescription =>
@$"{Name}
Тип: {Type}
Редкость: {new TextPointer("rarity", ItemRarity.ToString()).ToString().WithColor(RarityColors[(int) ItemRarity])}
Вес: {Weight} кг";

        public string TooltipText =>
@$"{PreDescription}
{Description}";

        public event Action OnChanged;

        public virtual bool Equals(IItem? other)
        {
            return other?.GetType() == GetType();
        }

        public TextureRenderer Renderer { get; set; }

        string IItem.Name => Name.ToString();
        public TextPointer Name { get; set; }
        
        public TextPointer Description { get; set; }
        
        public virtual ItemPurpose Type { get; }
        public virtual ItemRarity ItemRarity { get; }
        public virtual float Weight { get; set; }

        protected virtual IImpact Impact { get; }

        IImpact IItem.Impact => Impact;
    }
}