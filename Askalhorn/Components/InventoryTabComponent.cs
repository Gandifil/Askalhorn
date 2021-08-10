using System;
using System.Collections.Generic;
using System.Linq;
using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Common.Inventory;
using Askalhorn.Elements;
using Askalhorn.Elements.Icons;
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
        private readonly List<ItemIconElement> _icons = new List<ItemIconElement>();

        public InventoryTabComponent(GameProcessScreen screen)
        {
            this.screen = screen;
        }
        
        public void Initialize()
        {
            screen.game.UiSystem.Add(NAME, Create(screen.World.Player));
        }

        public void Dispose()
        {
            screen.game.UiSystem.Remove(NAME);
        }
        
        private  Element Create(IPlayer player)
        {
            var root = new Panel(Anchor.TopCenter, new Vector2(0.9f, 0.9f), Vector2.Zero);
            
            var box = new FixPanel(Anchor.CenterRight, 0.5f, 1);
            player.Bag.OnChanged += () => FillItemPanel(box, player.Bag);
            FillItemPanel(box, player.Bag);
            root.AddChild(box);;
            root.AddChild(new CostumeViewer(player.Costume, Anchor.CenterLeft, 0.5f, 1));
            
            return root;
        }

        private void FillItemPanel(Panel panel, IBag bag)
        {
            panel.RemoveChildren();
            _icons.Clear();
            foreach (var pack in bag.Items)
            {
                _icons.Add(new ItemIconElement(pack, panel, Anchor.AutoCenter, new Vector2(0.9f, 0.05f)));
                _icons.Last().Initialize();
            }
        }
    }
}