using System;
using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.Common.Inventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extended.Extensions;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Components
{
    public class ExchangeTabComponent: InventoryBase, IGameComponent, IDisposable
    {
        
        private static readonly string EXCHANGE_NAME = "inventoryE";
        private readonly GameProcessScreen screen;
        private readonly IBag left;
        private readonly IBag right;

        public ExchangeTabComponent(GameProcessScreen screen, IBag left, IBag right)
        {
            this.screen = screen;
            this.left = left;
            this.right = right;
        }
        
        private Element CreateExchange()
        {
            var box = new Panel(Anchor.Center, new Vector2(0.9f, 0.9f), Vector2.Zero);
            
            var leftBox = new Panel(Anchor.CenterLeft, new Vector2(0.45f, 0.9f), Vector2.Zero);
            box.AddChild(new Paragraph(Anchor.TopLeft, 500f, "Сундук"));
            left.OnChanged += () => FillBagPanel(leftBox, left, right);
            FillBagPanel(leftBox, left, right);
            box.AddChild(leftBox);
            
            
            var rightBox = new Panel(Anchor.CenterRight, new Vector2(0.45f, 0.9f), Vector2.Zero);
            box.AddChild(new Paragraph(Anchor.TopRight, 500f, "Игрок"));
            right.OnChanged += () => FillBagPanel(rightBox, right, left);
            FillBagPanel(rightBox, right, left);
            box.AddChild(rightBox);
            
            box.AddChild(new Button(Anchor.BottomRight, new Vector2(0.45f, 0.04f), "Выйти")
            {
                OnPressed = _ => Dispose(),
            });
            return box;
        }

        private void FillBagPanel(Panel panel, IBag source, IBag dist)
        {
            panel.RemoveChildren();
            foreach (var pack in source.Items)
            {
                var element = CreateItem(pack);
                element.OnPressed += _ => dist.Put(source.Pull(pack.Item));
                panel.AddChild(element);
            }
        }

        public void Initialize()
        {
            screen.game.UiSystem.Add(EXCHANGE_NAME, CreateExchange());
        }

        public void Dispose()
        {
            screen.game.UiSystem.Remove(EXCHANGE_NAME);
        }
    }
}