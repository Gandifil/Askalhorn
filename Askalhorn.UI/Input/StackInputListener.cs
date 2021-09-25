using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Input.InputListeners;

namespace Askalhorn.UI.Input
{
    public class StackInputListener<T>: InputListener where T:InputListener
    {
        private Stack<T> _stack = new Stack<T>();

        public StackInputListener(T baseElement)
        {
            Push(baseElement);
        }

        public void Push(T next)
        {
            _stack.Push(next);
        }

        public void Pop()
        {
            var listener = _stack.Pop();
            ListenerRemoved?.Invoke(listener);
        }

        public T Current => _stack.Peek();

        public event Action<T> ListenerRemoved;
        
        public override void Update(GameTime gameTime)
        {
            Current.Update(gameTime);
        }
    }
}