using System;
using Askalhorn.Common.Inventory;
using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Mechanics.Impacts;
using Askalhorn.Common.Render;

namespace Askalhorn.Common.Geography.Local.Builds
{
    internal class Container: GameObject, IBuild
    {

        public IBuild.Types Type => IBuild.Types.Chest;
        //public IRenderer Renderer { get; set; } = new TextureRenderer("images/grassland_tiles", 0, 256, 64, 64);

        public readonly Bag Bag;

        public readonly bool IsOneTime;

        public readonly string Name;

        public Container(string name, bool isOneTime, Bag bag)
        {
            Name = name;

            IsOneTime = isOneTime;

            Bag = bag;
        }
        
        public IImpact Impact => new OpenBagImpact(Bag);
    }
}