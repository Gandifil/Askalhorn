using System;
using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Common.Inventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Components
{
    public class InventoryTabComponent: InventoryBase, IGameComponent, IDisposable
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
            bag.OnChanged += () => FillItemPanel(box, bag);
            FillItemPanel(box, bag);
            return box;
        }

        private void FillItemPanel(Panel panel, IBag bag)
        {
            panel.RemoveChildren();
            foreach (var pack in bag.Items)
                panel.AddChild(CreateCharacterItem(pack));
        }

        private Element CreateCharacterItem(IBag.Pack pack)
        {
            var result = CreateItem(pack);
            result.OnPressed += _ => screen.World.playerController.AddMove(new UseItemMove(pack.Item));
            return result;
        }
    }
}