using System;
using Microsoft.Xna.Framework;

namespace Askalhorn.Map
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