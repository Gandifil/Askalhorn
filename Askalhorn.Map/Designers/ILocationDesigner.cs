using System;
using Askalhorn.Map.Generators;

namespace Askalhorn.Map.Designers
{
    internal interface ILocationDesigner
    {
        Location FormLocation(Random random, ref ILocationGenerator.CellType[,] map);
    }
}