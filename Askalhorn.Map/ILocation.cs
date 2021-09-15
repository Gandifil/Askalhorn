using System.Collections.Generic;
using Askalhorn.Map.Local;
using MonoGame.Extended.Tiled;

namespace Askalhorn.Map
{
    public interface ILocation
    {
        TiledMap TiledMap { get; }
        
        ICell this[IPosition position] { get; }
        
        IReadOnlyCollection<IGameObject> GameObjects { get; }

        bool Contain(IPosition position);
    }
}
