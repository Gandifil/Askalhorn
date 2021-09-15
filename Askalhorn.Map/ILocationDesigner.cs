using System;

namespace Askalhorn.Map
{
    internal interface ILocationDesigner
    {
        Location FormLocation(Random random, ref ILocationGenerator.CellType[,] map);
    }
}