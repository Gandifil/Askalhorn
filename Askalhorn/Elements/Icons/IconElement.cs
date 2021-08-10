using Askalhorn.Common;
using Microsoft.Xna.Framework;
using MLEM.Extended.Extensions;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Elements.Icons
{
    public class IconElement
    {
        protected readonly Panel _parent;
        protected readonly IIcon _icon;

        private Element _root;
        protected Anchor _anchor;
        protected Vector2 _position;
        
        protected Image _image;
        protected Tooltip _tooltip;
        
        public IconElement(IIcon icon, Panel parent, Anchor anchor, Vector2 position)
        {
            _icon = icon;
            _parent = parent;
            _anchor = anchor;
            _position = position;
        }

        protected void AddTooltipTo(Element element)
        {
            _tooltip = new Tooltip(500, _icon.TooltipText, element);
            _tooltip.MouseOffset = new Vector2(32, -64);
        }

        protected Image CreateTextureImage(Anchor anchor, Vector2 position)
        {
            _image = new Image(anchor, position, _icon.Texture.ToMlem());
            _image.CanBeMoused = true;
            return _image;
        }
        
        protected virtual Element BuildRootElement()
        {
            var result = CreateTextureImage(_anchor, _position);
            AddTooltipTo(result);
            return result;
        }
        
        public void Initialize()
        {
            _parent.AddChild(BuildRootElement());
        }
    }
}