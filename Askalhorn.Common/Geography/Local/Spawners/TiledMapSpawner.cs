using System;
using System.Linq;
using Askalhorn.Common.Geography.Local.Builds;
using Askalhorn.Common.Maths;

namespace Askalhorn.Common.Geography.Local.Spawners
{
    internal class TiledMapSpawner: ISpawner
    {
        public void Initialize(Location location, Random random, uint placeIndex)
        {
            foreach (var layer in location.TiledMap.ObjectLayers)
            {
                if (layer.Name == "teleports")
                    foreach (var obj in layer.Objects)
                    {
                        location.AddBuild(new GlobalTeleport
                        {
                            Location = new LocationInfo
                            {
                                PipelineName = obj.Type,
                                Seed = 0,
                            },
                            Position = new Position((obj.Position / 32).ToPoint()),
                            Place = Convert.ToUInt32(obj.Name),
                        });
                    }
                if (layer.Name == "places")
                    foreach (var obj in layer.Objects)
                        location.Places.Add(new Position((obj.Position / 32).ToPoint()));
            }
        }
    }
}