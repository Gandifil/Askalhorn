using System;
using Askalhorn.Common;
using Askalhorn.Map.Local;
using Askalhorn.Render;

namespace Askalhorn.Map
{
    public interface IGameObject: ITurnBased, IDisposable
    {
        Position Position { get; set; }
        IRenderer Renderer { get; }
        bool IsStatic { get; }

        event Action<IGameObject> OnDisposed;
        event Action<IGameObject, IPosition, IPosition> OnMoved;
    }
}