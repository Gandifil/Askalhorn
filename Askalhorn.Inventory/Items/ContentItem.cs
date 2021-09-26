using System;
using Askalhorn.Common;
using Askalhorn.Render;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Inventory.Items
{
    public class ContentItem: IItem
    {
        public string ContentName { get; }

        private readonly IItem _item;

        [JsonConstructor]
        public ContentItem(string contentName)
        {
            ContentName = contentName;
            _item = Storage.Content.Load<IItem>($"items/{contentName}");
        }
        
        [JsonIgnore]
        public TextureRenderer Renderer => _item.Renderer;

        [JsonIgnore]
        public string TooltipText => _item.TooltipText;

        public event Action OnChanged
        {
            add => _item.OnChanged += value;
            remove => _item.OnChanged -= value;
        }

        [JsonIgnore]
        public string Name => _item.Name;

        [JsonIgnore]
        public ItemPurpose Type => _item.Type;

        [JsonIgnore]
        public ItemRarity ItemRarity => _item.ItemRarity;

        [JsonIgnore]
        public float Weight => _item.Weight;

        [JsonIgnore]
        public IImpact Impact => _item.Impact;

        [JsonIgnore]
        public IItem InnerItem => _item;

        public bool Equals(IItem? other)
        {
            if (other is null)
                return false;

            var otherContentItem = other as ContentItem;

            if (otherContentItem is null)
                return _item.Equals(other);
            else
                return ContentName == otherContentItem.ContentName;
        }
    }
}