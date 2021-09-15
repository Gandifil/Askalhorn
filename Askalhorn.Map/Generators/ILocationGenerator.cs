using System;
using Microsoft.Xna.Framework;

namespace Askalhorn.Map.Generators
{
    internal interface ILocationGenerator
    {
        public enum CellType
        {
            Floor,
            Wall,
            Door,
        }

        CellType[,] Create(Random random, out Point[] places);
    }
}