using System;
using Askalhorn.Common;
using Askalhorn.Inventory;
using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Characters.Impacts
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

        public static event Action<Bag> OnBagOpened;
        
        public void On(object target)
        {
            OnBagOpened?.Invoke(Bag);
        }
    }
}