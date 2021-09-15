using System;
using Askalhorn.Map.Builds;
using Askalhorn.Map.Generators;
using Askalhorn.Map.Local;
using Newtonsoft.Json;

namespace Askalhorn.Map.Designers
{
    internal class TiledMapDesigner: ILocationDesigner
    {
        public string Name { get; }

        [JsonConstructor]
        public TiledMapDesigner(string name)
        {
            Name = name;
        }
        
        public Location FormLocation(Random random, ref ILocationGenerator.CellType[,] map)
        {
            var location = new Location(Name);
            Initialize(location);
            return location;
        }
        
        private void Initialize(Location location)
        {
            foreach (var layer in location.TiledMap.ObjectLayers)
            {
                if (layer.Name == "teleports")
                    foreach (var obj in layer.Objects)
                    {
                        location.Add(new GlobalTeleport
                        {
                            Location = new LocationInfo
                            {
                                PipelineName = obj.Type,
                                Seed = 0,
                            },
                            Position = new Position((obj.Position / 32).ToPoint()),
                            Place = Convert.ToUInt32((string?) obj.Name),
                        });
                    }
                if (layer.Name == "places")
                    foreach (var obj in layer.Objects)
                        location.Places.Add(new Position((obj.Position / 32).ToPoint()));
            }
        }
    }
}