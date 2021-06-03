using System;

namespace Askalhorn.Common.Geography
{
    internal interface ILocationGenerator
    {
        public enum CellType
        {
            Floor,
            Wall,
            Door,
        }

        CellType[,] Create(Random random);
    }
}