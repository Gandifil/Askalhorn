// using System;
// using Askalhorn.Characters.Builds;
// using Askalhorn.Characters.Items;
// using Askalhorn.Inventory;
// using Askalhorn.Inventory.BagFillers;
// using Askalhorn.Map;
// using Askalhorn.Map.Local;
// using Askalhorn.Map.Spawners;
//
// namespace Askalhorn.Characters.Spawners
// {
//     public class ChestSpawner: BuildSpawner
//     {
//         private static int index = 0;
//         private static Location loc;
//         public override void Initialize(Location location, Random random, int[] args, uint placeIndex)
//         {       
//             //
//             if (location != loc)
//             {
//                 index = 0;
//                 loc = location;
//             }
//             
//             //
//             var start = RandomPoint(location, random);
//             
//             var bag = new Bag();
//             var filler =
//                 new StaticLootChooser
//                 {
//                     Item = new LifePoition
//                     {
//                         Value = 20,
//                     },
//                     Minimum = 10,
//                     Maximum = 20,
//                 };
//             filler.Fill(random, bag);
//
//             // if (Askalhorn.Common.World.Instance.info.Bags.Count > index)
//             //     bag = Askalhorn.Common.World.Instance.info.Bags[index];
//             // else
//             //     Askalhorn.Common.World.Instance.info.Bags.Add(bag);
//             
//             location.Add(new Chest(bag)
//             {
//                 Position = new Position(start),
//             });
//
//             index++;
//         }
//     }
// }