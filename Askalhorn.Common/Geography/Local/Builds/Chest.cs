using System;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Common.Inventory;
using Askalhorn.Common.Inventory.Items;
using Askalhorn.Common.Render;
using Microsoft.Xna.Framework;
using Serilog;

namespace Askalhorn.Common.Geography.Local.Builds
{
    internal class Chest: HasPosition, IBuild
    {
        IPosition IBuild.Position => Position;
        public IRenderer Renderer { get; set; } = new TextureRenderer("images/grassland_tiles", 0, 256, 64, 64);

        private readonly Bag Bag = new Bag();

        public Chest()
        {
            Bag.Put(new EnergyPoition(10));
            Bag.Put(new EnergyPoition(10));
            Bag.Put(new EnergyPoition(10));
            Bag.Put(new PoisonPoition(10, 3));
        }
        
        public Action Action => () =>
        {
            Common.World.Instance.OpenBag(Bag);
        };
    }
}