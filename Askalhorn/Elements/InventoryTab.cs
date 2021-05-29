using System.Linq;
using System.Numerics;
using Askalhorn.Common;
using Askalhorn.Common.Inventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extended.Extensions;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Askalhorn.Elements
{
    public class InventoryTab
    {
        private static readonly string NAME = "inventory";

        public static void Toggle(UiSystem system, IBag bag)
        {
            if (system.Get(NAME) is null)
                system.Add(NAME, Create(bag));
            else
                system.Remove(NAME);
        }
        
        private static Element Create(IBag bag)
        {
            var texture = new Texture2D(Storage.GraphicsDevice, 1, 1);
            texture.SetData(new[] { Color.Black });
            var box = new Panel(Anchor.CenterRight, new Vector2(0.45f, 0.9f), Vector2.Zero);
            // box.OnDrawn += (element, time, batch, alpha) =>
            // {
            //
            //     var pos = element.Area.Location.ToPoint();
            //     
            //     // vertical lines
            //     var vsize = new Point(1, 320);
            //     for (int i = 0; i < 11; i++)
            //     {
            //         batch.Draw(texture, new Rectangle(pos + new Point(32*i, 0), vsize), Color.White);
            //     }
            //     
            //     // horizontal lines
            //     var hsize = new Point(320, 1);
            //     for (int i = 0; i < 11; i++)
            //     {
            //         batch.Draw(texture, new Rectangle(pos + new Point(0, 32*i), hsize), Color.White);
            //     }
            // };
            foreach (var item in bag.Items)
                box.AddChild(CreateItem(item.item));
            return box;
        }

        private static Element CreateItem(IItem item)
        {
            var box = new Panel(Anchor.AutoCenter, new Vector2(0.9f, 0.05f), Vector2.Zero);
            box.CanBeMoused = true;
            box.CanBeSelected = true;
            box.OnMouseEnter += element => element.Size = new Vector2(0.95f, 0.07f);
            box.OnMouseExit += element => element.Size = new Vector2(0.9f, 0.05f);
            box.OnPressed += element => element.Size = new Vector2(2f, 2f);
            
            box.AddChild(new Image(Anchor.CenterLeft, new Vector2(0.2F, 1F), item.Texture.ToMlem()));
            box.AddChild(new Paragraph(Anchor.Center, 300, item.Name));
            box.AddChild(new Paragraph(Anchor.CenterRight, 300, item.Description));
            return box;
        }
    }
}