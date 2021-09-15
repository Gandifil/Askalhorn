using System;

namespace Askalhorn.Map.Spawners
{
    internal interface ISpawner
    {
        void Initialize(Location location, Random random, int[] args, uint placeIndex);
    }
}