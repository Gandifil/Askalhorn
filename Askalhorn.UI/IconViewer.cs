using System;
using Askalhorn.Render;
using Microsoft.Xna.Framework;
using MLEM.Extended.Extensions;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI
{
    public class IconViewer: Image
    {
        private readonly IIcon _icon;
        private readonly Tooltip _tooltip;
        
        public IconViewer(IIcon icon, Anchor anchor, float width, float height): 
            base(anchor, new Vector2(width, height), icon.Renderer.Region.ToMlem(), false)
        {
            if (icon is null)
                throw new ArgumentNullException(nameof(icon));
            
            CanBeMoused = true;
            CanBePressed= true;
            
            _icon = icon;
            _icon.OnChanged += SyncState;
            
            _tooltip = new Tooltip(500, _icon.TooltipText, this);
            _tooltip.MouseOffset = new Vector2(32, -64);
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _icon.OnChanged -= SyncState;
        }

        protected virtual void SyncState()
        {
            _tooltip.Paragraph.Text = _icon.TooltipText;
        }
    }
}