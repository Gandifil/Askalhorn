using System;
using Askalhorn.Map.Local;

namespace Askalhorn.Map.Spawners
{
    public class LabelSpawner: ISpawner
    {
        public string Name { get; }

        public uint X { get; set; }

        public uint Y { get; set; }

        public LabelSpawner(string name, uint x, uint y)
        {
            Name = name;
            X = x;
            Y = y;
        }
        
        public void Initialize(Location location, Random random, int[] args, bool isLoading)
        {
            location.Labels.Add(Name, new Position(X, Y));
        }
    }
}