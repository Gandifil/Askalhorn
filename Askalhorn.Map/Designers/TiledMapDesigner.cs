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
                if (layer.Name == "labels")
                    foreach (var obj in layer.Objects)
                    {
                        var pos = new Position((obj.Position / 32).ToPoint());
                        location.Labels.Add(obj.Name, pos);
                    }
        }
    }
}