using System;
using Microsoft.Xna.Framework;

namespace Askalhorn.Map.Local.Generators
{
    internal class MockGenerator: ILocationGenerator
    {
        public ILocationGenerator.CellType[,] Create(Random random, out Point[] places)
        {
            places = new Point[0];
            return null;
        }
    }
}