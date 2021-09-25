using System;
using Askalhorn.Inventory;
using Askalhorn.Inventory.Items;
using Askalhorn.Render;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.UI.Inventory.Search
{
    public class Filter: IIcon
    {
        public TextureRenderer Renderer { get; set; }
        public string TooltipText { get; set; }
        public event Action OnChanged;

        public Func<IItem, bool> Predicate { get; set; }
    }
}