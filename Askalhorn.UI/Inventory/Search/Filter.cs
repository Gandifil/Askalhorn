using System;
using Askalhorn.Inventory;
using Askalhorn.Render;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.UI.Inventory.Search
{
    public class Filter: IIcon
    {
        public TextureRegion2D Texture { get; set; }
        public string TooltipText { get; set; }
        
        public Func<IItem, bool> Predicate { get; set; }
    }
}