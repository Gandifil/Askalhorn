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
        
        public Location Run(int seed, int[] args, bool IsLoading)
        {
            var random = new Random(seed);
            Point[] places;
            var cells = Generator.Create(random, out places);

            var location = Designer.FormLocation(random, ref cells);
            foreach (var place in places)
                location.Places.Add(new Position(place));
            
            foreach (var spawner in Spawners)
                spawner.Initialize(location, random, args, IsLoading);
            return location;
        }
    }
    internal class LocationPipelineReader : PolymorphJsonReader<LocationPipeline>
    {
        
    }
}