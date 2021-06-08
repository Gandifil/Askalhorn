using System;
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
        private static readonly string EXCHANGE_NAME = "inventoryE";
        
        public static void CreateExchangeTab(UiSystem system, IBag chest, IBag playerBag)
        {
            if (system.Get(EXCHANGE_NAME) is null)
                system.Add(EXCHANGE_NAME, CreateExchange(chest, playerBag));
        }

        private static Element CreateItem(IItem item)
        {
            var box = new Panel(Anchor.AutoCenter, new Vector2(0.9f, 0.05f), Vector2.Zero);
            box.CanBeMoused = true;
            box.CanBeSelected = true;
            box.OnMouseEnter += element => element.Size = new Vector2(0.95f, 0.07f);
            box.OnMouseExit += element => element.Size = new Vector2(0.9f, 0.05f);
            
            box.AddChild(new Image(Anchor.CenterLeft, new Vector2(0.2F, 1F), item.Texture.ToMlem()));
            box.AddChild(new Paragraph(Anchor.Center, 300, item.Name));
            box.AddChild(new Paragraph(Anchor.CenterRight, 300, item.Description));
            return box;
        }
        
        private static void CreateExchangeElement(IItem item, IBag bag1, Panel box1, IBag bag2, Panel box2)
        {
            var element = CreateItem(item);
            element.OnPressed += element =>
            {
                bag2.Put(bag1.Pull(item));
                box1.RemoveChild(element);
                CreateExchangeElement(item, bag2, box2, bag1, box1);
            };
            box1.AddChild(element);
        }
        
        private static Element CreateExchange(IBag chest, IBag playerBag)
        {
            var texture = new Texture2D(Storage.GraphicsDevice, 1, 1);
            texture.SetData(new[] { Color.Black });
            var box = new Panel(Anchor.Center, new Vector2(0.9f, 0.9f), Vector2.Zero);
            var box1 = new Panel(Anchor.CenterLeft, new Vector2(0.45f, 0.95f), Vector2.Zero);
            var box2 = new Panel(Anchor.CenterRight, new Vector2(0.45f, 0.95f), Vector2.Zero);
            box.AddChild(box1);
            box1.AddChild(new Paragraph(Anchor.AutoCenter, 500f, "Сундук"));
            box.AddChild(box2);
            box2.AddChild(new Paragraph(Anchor.AutoCenter, 500f, "Игрок"));
            box.AddChild(new Button(Anchor.BottomRight, new Vector2(0.45f, 0.04f), "Выйти")
            {
                OnPressed = element => element.System.Remove(EXCHANGE_NAME),
            });
            foreach (var item in chest.Items)
                CreateExchangeElement(item.item, chest, box1, playerBag, box2);
            foreach (var item in playerBag.Items)
                CreateExchangeElement(item.item, playerBag, box2, chest, box1);
            return box;
        }
        
    }
}