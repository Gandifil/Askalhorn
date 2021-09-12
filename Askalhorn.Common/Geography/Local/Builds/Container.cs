using System;
using Askalhorn.Common.Inventory;
using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Mechanics.Impacts;
using Askalhorn.Common.Render;

namespace Askalhorn.Common.Geography.Local.Builds
{
    internal class Container: HasPosition, IBuild
    {
        IPosition IBuild.Position => Position;

        public IBuild.Types Type => IBuild.Types.Chest;
        public IRenderer Renderer { get; set; } = new TextureRenderer("images/grassland_tiles", 0, 256, 64, 64);

        public Bag Bag { get; set; }

        public bool IsOneTime { get; set; }

        public string Name { get; set; }
        
        public IImpact Impact => new OpenBagImpact(Bag);
    }
}