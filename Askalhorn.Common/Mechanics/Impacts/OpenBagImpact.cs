using Askalhorn.Common.Inventory;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Common.Mechanics.Impacts
{
    public class OpenBagImpact: IImpact
    {
        public string Description { get; }
        public TextureRegion2D TextureRegion { get; }

        public readonly Bag Bag;

        [JsonConstructor]
        public OpenBagImpact(Bag bag)
        {
            Bag = bag;
        }
        
        public void On(Character character)
        {
            World.Instance.OpenBag(Bag);
        }
    }
}