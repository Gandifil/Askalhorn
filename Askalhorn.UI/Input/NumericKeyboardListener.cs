using System;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;

namespace Askalhorn.UI.Input
{
    public class NumericKeyboardListener: KeyboardListener
    {
        private static readonly Range<int> NUMERIC_RANGE = new Range<int>(0, 9);
        
        public NumericKeyboardListener()
        {
            KeyReleased += OnKeyReleased;
        }

        private void OnKeyReleased(object? sender, KeyboardEventArgs e)
        {
            var topLine = e.Key - Keys.D0;
            var numPad = e.Key - Keys.NumPad0;
            
            if (NUMERIC_RANGE.IsInBetween(topLine))
                NumericKeyReleased?.Invoke(this, topLine);
            
            if (NUMERIC_RANGE.IsInBetween(numPad))
                NumericKeyReleased?.Invoke(this, numPad);
        }

        public event EventHandler<int> NumericKeyReleased;
    }
}