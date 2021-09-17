using System;
using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.Inventory;
using Askalhorn.UI.Inventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extended.Extensions;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Components
{
    public class ExchangeTabComponent: IGameComponent, IDisposable
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
            box.AddChild(new Paragraph(Anchor.TopLeft, 500f, "Сундук"));
            box.AddChild(new Paragraph(Anchor.TopRight, 500f, "Игрок"));
            box.AddChild(new ExchangeBagViewer(right, left, Anchor.CenterLeft, 0.45f, 0.9f));
            box.AddChild(new ExchangeBagViewer(left, right, Anchor.CenterRight, 0.45f, 0.9f));
            box.AddChild(new Button(Anchor.BottomRight, new Vector2(0.45f, 0.04f), "Выйти")
            {
                OnPressed = _ => Dispose(),
            });
            return box;
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