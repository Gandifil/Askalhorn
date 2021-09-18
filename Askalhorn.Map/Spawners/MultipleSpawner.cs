using System;

namespace Askalhorn.Map.Spawners
{
    internal class MultipleSpawner: ISpawner
    {
        public int Count { get; set; } = 1;

        public ISpawner Spawner { get; set; }
        
        public void Initialize(Location location, Random random, int[] args, bool isLoading)
        {
            for (int i = 0; i < Count; i++)
                Spawner?.Initialize(location, random, args, isLoading);
        }
    }
}