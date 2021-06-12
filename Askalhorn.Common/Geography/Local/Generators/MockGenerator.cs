using System;
using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Geography.Local.Generators
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