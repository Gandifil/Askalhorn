using System;
using Askalhorn.Characters.Builds;
using Askalhorn.Inventory;
using Askalhorn.Map;
using Askalhorn.Map.Local;
using Newtonsoft.Json;

namespace Askalhorn.Characters.Builders
{
    public class ChestBuilder: IGameObjectBuilder
    {
        public ILootChooser Loot { get; set; }

        [JsonConstructor]
        public ChestBuilder(ILootChooser loot)
        {
            Loot = loot;
        }
        
        public IGameObject Build(Position position)
        {
            var bag = new Bag();
            Loot.Fill(new Random(), bag);
            return new Chest(bag)
            {
                Position = position,
            };
        }
    }
}