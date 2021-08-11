using System;
using System.Collections.Generic;
using System.Linq;
using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Common.Inventory;
using Askalhorn.Elements;
using Askalhorn.Elements.Icons;
using Askalhorn.Elements.Inventory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            screen.game.UiSystem.Add(NAME, Create(screen.World.Player));
        }

        public void Dispose()
        {
            screen.game.UiSystem.Remove(NAME);
        }
        
        private Element Create(IPlayer player)
        {
            var root = new FixPanel(Anchor.TopCenter, 0.9f, 0.9f);
            root.AddChild(new BagViewer(player.Bag, Anchor.CenterRight, 0.5f, 1));
            root.AddChild(new CostumeViewer(player.Costume, Anchor.CenterLeft, 0.5f, 1));
            return root;
        }
    }
}