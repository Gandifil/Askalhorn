using Askalhorn.Common.Inventory;
using Microsoft.Xna.Framework;
using MLEM.Extended.Extensions;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Components
{
    public abstract class InventoryBase
    {
        protected Element CreateItem(IBag.Pack pack)
        {
            var box = new Panel(Anchor.AutoCenter, new Vector2(0.9f, 0.05f), Vector2.Zero);
            box.CanBeMoused = true;
            box.CanBeSelected = true;
            box.OnMouseEnter += element => element.Size = new Vector2(0.95f, 0.07f);
            box.OnMouseExit += element => element.Size = new Vector2(0.9f, 0.05f);
            
            box.AddChild(new Paragraph(Anchor.TopLeft, 100, pack.Count.ToString()));
            box.AddChild(new Image(Anchor.CenterLeft, new Vector2(0.2F, 1F), pack.Item.Texture.ToMlem()));
            box.AddChild(new Paragraph(Anchor.Center, 300, pack.Item.Name));
            box.AddChild(new Paragraph(Anchor.CenterRight, 300, pack.Item.Description));
            return box;
        }
    }
}