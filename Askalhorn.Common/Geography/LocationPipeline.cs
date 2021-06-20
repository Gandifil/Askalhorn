using System;
using System.Collections.Generic;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Common.Geography.Local.Builds;
using Askalhorn.Common.Geography.Local.Designers;
using Askalhorn.Common.Geography.Local.Generators;
using Askalhorn.Common.Geography.Local.Spawners;
using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Geography
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

        private static LocationPipeline FromFile(string name)
        {
            return new LocationPipeline()
            {
                Designer = new FileDesigner
                {
                    Name = name,
                },
                Spawners = new List<ISpawner>
                {
                    new TiledMapSpawner(),
                    new WitchSpawner(),
                }
            };
        }

        public static IDictionary<string, LocationPipeline> Templates = new Dictionary<string, LocationPipeline>
        {
            {"start", FromFile("temple")},
            {"castle", FromFile("castle")},
            {"dungeon", new LocationPipeline
            {
                Generator = new OneRoomGenerator(15, 15),
                Designer = new WhiteCastleDesigner(),
                Spawners = new List<ISpawner>
                {
                    new CustomBuildSpawner((point, _, _, _) =>
                        new GlobalTeleport()
                        {
                            Position = new Position(point),
                            Location = new LocationInfo()
                            {
                                PipelineName = "dungeons",
                                Args = new []{1},
                            }
                        })
                },
            }},
            {"dungeons", new LocationPipeline
            {
                Generator = new OneRoomGenerator(25, 25),
                Designer = new WhiteCastleDesigner(),
                Spawners = new List<ISpawner>
                {
                    new ChestSpawner(),
                    new TestEnemySpawner(),
                    new CustomBuildSpawner((point, _, args, _) =>
                        new GlobalTeleport()
                        {
                            Position = new Position(point),
                            Location = new LocationInfo()
                            {
                                PipelineName = args[0] == 1 ? "dungeon" : "dungeons",
                                Args = new []{args[0] - 1},
                            }
                        }),
                    new CustomBuildSpawner((point, _, args, _) =>
                        new GlobalTeleport()
                        {
                            Position = new Position(point),
                            Location = new LocationInfo()
                            {
                                PipelineName = "dungeons",
                                Args = new []{args[0] + 1},
                            }
                        })
                },
            }},
        };

    }
}