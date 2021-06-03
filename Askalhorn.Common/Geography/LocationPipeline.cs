using System;
using System.Collections.Generic;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Common.Geography.Local.Builds;
using Askalhorn.Common.Geography.Local.Designers;
using Askalhorn.Common.Geography.Local.Generators;
using Askalhorn.Common.Geography.Local.Spawners;

namespace Askalhorn.Common.Geography
{
    internal class LocationPipeline
    {
        public ILocationGenerator Generator { get; set; } = new MockGenerator();
        
        public ILocationDesigner Designer { get; set; }

        public IEnumerable<ISpawner> Spawners { get; set; } = new List<ISpawner>();

        public readonly int Seed;

        public LocationPipeline()
        {
            Seed = (int) DateTime.Now.Ticks & 0x0000FFFF;
        }

        public LocationPipeline(int seed)
        {
            Seed = seed;
        }
        
        public Location Run(IPosition position)
        {
            var random = new Random(Seed);
            var cells = Generator.Create(random);
            var location = Designer.FormLocation(random, ref cells);
            foreach (var spawner in Spawners)
                spawner.Initialize(location, random, position);
            return location;
        }

        public static List<LocationPipeline> Templates = new List<LocationPipeline>
        {
            new LocationPipeline(12)
            {
                Generator = new OneRoomGenerator(25, 25),
                Designer = new WhiteCastleDesigner(),
                Spawners = new List<ISpawner>
                {
                    new ChestSpawner(),
                    new ChestSpawner(),
                    new ChestSpawner(),
                    new TestEnemySpawner(),
                    new CustomBuildSpawner(point =>
                        new GlobalTeleport()
                        {
                            Position = new Position(point),
                            TargetPosition = new Position(1, 1),
                            LocationPipeline = Templates[2],
                        })
                },
            },
            new LocationPipeline(10)
            {
                Generator = new OneRoomGenerator(15, 15),
                Designer = new WhiteCastleDesigner(),
                Spawners = new List<ISpawner>
                {
                    new ChestSpawner(),
                    new CustomBuildSpawner(point =>
                        new GlobalTeleport()
                        {
                            Position = new Position(point),
                            TargetPosition = new Position(1, 1),
                            LocationPipeline = Templates[0],
                        })
                }
            },
            new LocationPipeline(10)
            {
                Generator = new RoomsAndCorridorsGenerator(50, 50),
                Designer = new WhiteCastleDesigner(),
            },
            new LocationPipeline()
            {
                Designer = new FileDesigner(),
            }
        };

    }
}