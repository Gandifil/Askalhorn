using System;
using System.Collections.Generic;
using Askalhorn.Common;
using Askalhorn.Common.Interpetators;
using Askalhorn.Map.Designers;
using Askalhorn.Map.Generators;
using Askalhorn.Map.Local;
using Askalhorn.Map.Spawners;
using Microsoft.Xna.Framework;

namespace Askalhorn.Map
{
    internal class LocationPipeline
    {
        public ILocationGenerator Generator { get; set; } = new MockGenerator();
        
        public ILocationDesigner Designer { get; set; }

        public IEnumerable<ISpawner> Spawners { get; set; } = new List<ISpawner>();
        
        public Location Run(int seed, int[] args, uint placeIndex)
        {
            var random = new Random(seed);
            Point[] places;
            var cells = Generator.Create(random, out places);

            var location = Designer.FormLocation(random, ref cells);
            foreach (var place in places)
                location.Places.Add(new Position(place));
            
            foreach (var spawner in Spawners)
                spawner.Initialize(location, random, args, placeIndex);
            return location;
        }
        //
        // private static LocationPipeline FromFile(string name)
        // {
        //     return new LocationPipeline()
        //     {
        //         Designer = new FileDesigner
        //         {
        //             Name = name,
        //         },
        //         Spawners = new List<ISpawner>
        //         {
        //             new TiledMapSpawner(),
        //             new Spawner(new StaticPosition(34, 32), "roxy_greyrat"),
        //             new MultipleSpawner
        //             {
        //                 Count = 2,
        //                 Spawner = new Spawner(
        //                     new InsideSquarePosition(new Rectangle(40, 40, 10, 10)), 
        //                     "skelet01")
        //             },
        //             new MultipleSpawner
        //             {
        //                 Count = 12,
        //                 Spawner = new ChestSpawner(),
        //             },
        //         }
        //     };
        // }
        //
        // public static IDictionary<string, LocationPipeline> Templates = new Dictionary<string, LocationPipeline>
        // {
        //     {"start", FromFile("temple")},
        //     {"castle", FromFile("castle")},
        //     {"dungeon", new LocationPipeline
        //     {
        //         Generator = new OneRoomGenerator(15, 15),
        //         Designer = new WhiteCastleDesigner(),
        //         Spawners = new List<ISpawner>
        //         {
        //             new CustomBuildSpawner((point, _, _, _) =>
        //                 new GlobalTeleport()
        //                 {
        //                     Position = new Position(point),
        //                     Location = new LocationInfo()
        //                     {
        //                         PipelineName = "dungeons",
        //                         Args = new []{1},
        //                     }
        //                 })
        //         },
        //     }},
        //     {"dungeons", new LocationPipeline
        //     {
        //         Generator = new OneRoomGenerator(25, 25),
        //         Designer = new WhiteCastleDesigner(),
        //         Spawners = new List<ISpawner>
        //         {
        //             new ChestSpawner(),
        //             new CustomBuildSpawner((point, _, args, _) =>
        //                 new GlobalTeleport()
        //                 {
        //                     Position = new Position(point),
        //                     Location = new LocationInfo()
        //                     {
        //                         PipelineName = args[0] == 1 ? "dungeon" : "dungeons",
        //                         Args = new []{args[0] - 1},
        //                     }
        //                 }),
        //             new CustomBuildSpawner((point, _, args, _) =>
        //                 new GlobalTeleport()
        //                 {
        //                     Position = new Position(point),
        //                     Location = new LocationInfo()
        //                     {
        //                         PipelineName = "dungeons",
        //                         Args = new []{args[0] + 1},
        //                     }
        //                 })
        //         },
        //     }},
        // };

    }
    internal class LocationPipelineReader : PolymorphJsonReader<LocationPipeline>
    {
        
    }
}