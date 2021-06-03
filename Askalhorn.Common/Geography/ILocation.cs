using System.Collections.Generic;
using Askalhorn.Common.Geography.Local;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;

namespace Askalhorn.Common.Geography
{
    public interface ILocation
    {
        TiledMap TiledMap { get; }
        
        ICell this[IPosition position] { get; }
        
        IReadOnlyCollection<IBuild> Builds { get; }

        bool Contain(IPosition position);
    }
}
