using System;

namespace Askalhorn.Map
{
    internal interface ISpawner
    {
        void Initialize(Location location, Random random, int[] args, uint placeIndex);
    }
}