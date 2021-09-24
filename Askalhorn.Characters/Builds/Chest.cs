using Askalhorn.Inventory;
using Askalhorn.Map.Builds;
using Askalhorn.Map.Local;
using Askalhorn.Render;
using Microsoft.Xna.Framework;

namespace Askalhorn.Characters.Builds
{
    internal class Chest: Container, IBuild
    {
        public Chest(Bag bag)
            :base("сундук", false, bag)
        {
            Renderer = new TextureRenderer("grassland_tiles", new Point(0, 4));
        }

        public override bool IsStatic => false;
    }
}