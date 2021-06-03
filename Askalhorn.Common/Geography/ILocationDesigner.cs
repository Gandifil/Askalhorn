using System;

namespace Askalhorn.Common.Geography
{
    internal interface ILocationDesigner
    {
        Location FormLocation(Random random, ref ILocationGenerator.CellType[,] map);
    }
}