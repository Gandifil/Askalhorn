﻿using System;
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
        public override void Initialize(Location location, Random random, IPosition playerPosition)
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
                new StaticBagFiller
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
            
            location.AddBuild(new Chest()
            {
                Position = new Position(start),
                Bag = bag,
            });

            index++;
        }
    }
}