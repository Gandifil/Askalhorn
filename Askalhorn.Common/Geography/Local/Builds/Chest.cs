using Askalhorn.Common.Inventory;
using Askalhorn.Common.Render;

namespace Askalhorn.Common.Geography.Local.Builds
{
    internal class Chest: Container, IBuild
    {
        public Chest(Bag bag)
            :base("сундук", false, bag)
        {
            Renderer = new TextureRenderer("images/grassland_tiles", 0, 256, 64, 64);
        }
    }
}