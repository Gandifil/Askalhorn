using System;
using Askalhorn.Common;
using Askalhorn.Render;
using Askalhorn.Text;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Inventory.Items
{
    public class Item : IItem
    {
        private static string[] RarityColors = {"White", "Green", "Orange"};
        
        protected virtual string PreDescription => @$"";

        public string TooltipText =>
@$"{Name}
Тип: {EnumTextPointer<ItemPurpose>.Get(Type)}
Редкость: {new TextPointer("rarity", ItemRarity.ToString()).ToString().WithColor(RarityColors[(int) ItemRarity])}
Вес: {Weight} кг 
{PreDescription}
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
        
        public virtual ItemPurpose Type { get; set; }
        public virtual ItemRarity ItemRarity { get; set; }
        public virtual float Weight { get; set; }

        protected virtual IImpact Impact { get; }

        public IItem InnerItem => this;


        IImpact IItem.Impact => Impact;
    }
}