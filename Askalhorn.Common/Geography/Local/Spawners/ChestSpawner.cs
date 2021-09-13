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
        private static int index = 0;
        private static Location loc;
        public override void Initialize(Location location, Random random, int[] args, uint placeIndex)
        {       
            //
            if (location != loc)
            {
                index = 0;
                loc = location;
            }
            
            //
            var start = RandomPoint(location, random);
            
            var bag = new Bag();
            var filler =
                new StaticLootChooser
                {
                    Item = new LifePoition
                    {
                        Value = 20,
                    },
                    Minimum = 10,
                    Maximum = 20,
                };
            filler.Fill(random, bag);

            if (Askalhorn.Common.World.Instance.info.Bags.Count > index)
                bag = Askalhorn.Common.World.Instance.info.Bags[index];
            else
                Askalhorn.Common.World.Instance.info.Bags.Add(bag);
            
            location.AddBuild(new Chest(bag)
            {
                Position = new Position(start),
            });

            index++;
        }
    }
}