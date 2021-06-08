using System;
using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Common.Inventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extended.Extensions;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Components
{
    public class InventoryTabComponent: IGameComponent, IDisposable
    {
        private static readonly string NAME = "inventory";
        private readonly GameProcessScreen screen;

        public InventoryTabComponent(GameProcessScreen screen)
        {
            this.screen = screen;
        }
        
        public void Initialize()
        {
            screen.game.UiSystem.Add(NAME, Create(screen.World.Player.Bag));
        }

        public void Dispose()
        {
            screen.game.UiSystem.Remove(NAME);
        }
        
        private  Element Create(IBag bag)
        {
            var texture = new Texture2D(Storage.GraphicsDevice, 1, 1);
            texture.SetData(new[] { Color.Black });
            var box = new Panel(Anchor.CenterRight, new Vector2(0.45f, 0.9f), Vector2.Zero);

            foreach (var (item, count) in bag.Items)
                box.AddChild(CreateItem(item));
            return box;
        }

        private Element CreateItem(IItem item)
        {
            var box = new Panel(Anchor.AutoCenter, new Vector2(0.9f, 0.05f), Vector2.Zero);
            box.CanBeMoused = true;
            box.CanBeSelected = true;
            box.OnMouseEnter += element => element.Size = new Vector2(0.95f, 0.07f);
            box.OnMouseExit += element => element.Size = new Vector2(0.9f, 0.05f);
            
            box.AddChild(new Image(Anchor.CenterLeft, new Vector2(0.2F, 1F), item.Texture.ToMlem()));
            box.AddChild(new Paragraph(Anchor.Center, 300, item.Name));
            box.AddChild(new Paragraph(Anchor.CenterRight, 300, item.Description));
            box.OnPressed += _ => screen.World.playerController.AddMove(new UseItemMove(item));
            return box;
        }

    }
}