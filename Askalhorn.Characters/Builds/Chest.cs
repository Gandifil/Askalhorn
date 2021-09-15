using Askalhorn.Inventory;
using Askalhorn.Map.Builds;
using Askalhorn.Map.Local;
using Askalhorn.Render;

namespace Askalhorn.Characters.Builds
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