using Askalhorn.Common.Geography.Local;
using MonoGame.Extended.Tiled;

namespace Askalhorn.Common.Geography
{
    public class TiledMapLocation: ILocation
    {
        public TiledMap TiledMap { get; private set;  }

        public ICell this[uint x, uint y] => throw new System.NotImplementedException();

        public TiledMapLocation(string name)
        {
            TiledMap = Storage.Content.Load<TiledMap>("maps/" + name);
        }
    }
}