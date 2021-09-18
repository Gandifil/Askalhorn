using System;
using Askalhorn.Map.Local;
using Askalhorn.Render;

namespace Askalhorn.Map
{
    public abstract class GameObject : IGameObject
    {
        private Position _position;
        public Position Position
        {
            get => _position;
            set
            {
                OnMoved?.Invoke(this, _position, value);
                _position = value;
            }
        }
        
        public IRenderer Renderer { get; set; }
        public abstract bool IsStatic { get; }
        public event Action<IGameObject> OnDisposed;
        public event Action<IGameObject, IPosition, IPosition> OnMoved;

        public virtual void Dispose()
        {
            OnDisposed?.Invoke(this);
        }


        public virtual void Turn()
        {
            
        }
    }
}