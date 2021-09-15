using System;

namespace Askalhorn.Map.Local.Spawners
{
    internal interface IPositionGenerator
    {
        Position Generate(Location location, Random random);
    }
}