using System;
using Askalhorn.Common.Geography.Local;

namespace Askalhorn.Common.Geography
{
    internal interface ISpawner
    {
        void Initialize(Location location, Random random, int[] args, uint placeIndex);
    }
}