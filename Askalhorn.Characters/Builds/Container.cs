using Askalhorn.Characters.Impacts;
using Askalhorn.Common;
using Askalhorn.Inventory;
using Askalhorn.Map;
using Askalhorn.Map.Local;

namespace Askalhorn.Characters.Builds
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