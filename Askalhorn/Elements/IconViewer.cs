using Askalhorn.Common;
using Askalhorn.Render;
using Microsoft.Xna.Framework;
using MLEM.Extended.Extensions;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Elements
{
    public class IconViewer: Image
    {
        private readonly IIcon _icon;
        private readonly Tooltip _tooltip;
        
        public IconViewer(IIcon icon, Anchor anchor, float width, float height): 
            base(anchor, new Vector2(width, height), icon.Texture.ToMlem(), false)
        {
            CanBeMoused = true;
            _icon = icon;
            _tooltip = new Tooltip(500, _icon.TooltipText, this);
            _tooltip.MouseOffset = new Vector2(32, -64);
        }
    }
}