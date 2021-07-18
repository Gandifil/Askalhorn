using System;

namespace Askalhorn.Common.Geography.Local.Spawners
{
    internal interface IPositionGenerator
    {
        Position Generate(Location location, Random random);
    }
}