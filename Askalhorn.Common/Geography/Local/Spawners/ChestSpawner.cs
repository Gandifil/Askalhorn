using System;
using Askalhorn.Common.Geography.Local.Builds;
using Askalhorn.Common.Inventory;
using Askalhorn.Common.Inventory.BagFillers;
using Askalhorn.Common.Inventory.Items;
using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Geography.Local.Spawners
{
    internal class ChestSpawner: BuildSpawner
    {
        public override void Initialize(Location location, Random random, IPosition playerPosition)
        {        
            var start = RandomPoint(location, random);
            
            var bag = new Bag();
            bag.Put(new EnergyPoition(10));
            bag.Put(new EnergyPoition(10));
            bag.Put(new EnergyPoition(10));
            bag.Put(new PoisonPoition(10, 3));
            
            location.AddBuild(new Chest(
                new StaticBagFiller
                {
                    Item = new LifePoition(10),
                    Minimum = 10,
                    Maximum = 20,
                })
            {
                Position = new Position(start),
            });
        }
    }
}