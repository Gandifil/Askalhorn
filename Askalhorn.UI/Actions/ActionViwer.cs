using Askalhorn.Characters.Control.Moves;
using Askalhorn.Core;
using Askalhorn.Map.Actions;
using Askalhorn.UI.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MLEM.Ui;
using MonoGame.Extended.Input.InputListeners;

namespace Askalhorn.UI.Actions
{
    public class ActionViwer: IconViewer
    {
        private readonly IAction _action;
        private readonly Keys _key;
        
        private KeyboardState _previousState;
        
        public ActionViwer(IAction action, Keys key, Anchor anchor, float width, float height): 
            base(action, anchor, width, height)
        {
            _action = action;

            _key = key;

            OnPressed += _ => Use();
            
            _previousState = Keyboard.GetState();
            
            InputListeners.Keyboard.KeyReleased += OnKeyReleased;
        }

        public override void Dispose()
        {
            InputListeners.Keyboard.KeyReleased -= OnKeyReleased;
            
            base.Dispose();
        }

        private void OnKeyReleased(object? sender, KeyboardEventArgs e)
        {
            if (e.Key == _key)
                Use();
        }

        private void Use()
        {
            GameProcess.Instance.Player.Make(new UseActionMove(_action));
        }
    }
}