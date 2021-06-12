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
        
        public Location Run(int seed, uint placeIndex)
        {
            var random = new Random(seed);
            var cells = Generator.Create(random);
            var location = Designer.FormLocation(random, ref cells);
            foreach (var spawner in Spawners)
                spawner.Initialize(location, random, placeIndex);
            return location;
        }

        public static IDictionary<string, LocationPipeline> Templates = new Dictionary<string, LocationPipeline>
        {
            {"start", 
                new LocationPipeline()
                {
                    Designer = new FileDesigner
                    {
                        Name = "temple"
                    },
                    Spawners = new List<ISpawner>
                    {
                        new TiledMapSpawner(),
                    }
                }},
            {"dungeon", new LocationPipeline
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
                            Place = Convert.ToUInt32(0),
                        })
                },
            }},
            {"file",
                new LocationPipeline
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
                                Place = Convert.ToUInt32(0),
                                Location = new LocationInfo
                                {
                                    PipelineName = "file",
                                    Seed = 10,
                                }
                            })
                    },
                }}
        };

    }
}