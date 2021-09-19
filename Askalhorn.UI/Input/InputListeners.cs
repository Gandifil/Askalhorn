using Microsoft.Xna.Framework;
using MonoGame.Extended.Input.InputListeners;

namespace Askalhorn.UI.Input
{
    public class InputListeners: InputListenerComponent
    {
        public StackInputListener<KeyboardListener> KeyboardListener { get; }
        
        public StackInputListener<MouseListener> MouseListener { get; }
        
        public InputListeners(Game game) : base(game)
        {
            KeyboardListener = new StackInputListener<KeyboardListener>(new KeyboardListener());
            Listeners.Add(KeyboardListener);
                
            MouseListener = new StackInputListener<MouseListener>(new MouseListener());
            Listeners.Add(MouseListener);
        }

        public static InputListeners Input;
    }
}