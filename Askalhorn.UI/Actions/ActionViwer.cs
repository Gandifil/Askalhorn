using Askalhorn.Characters.Control.Moves;
using Askalhorn.Core;
using Askalhorn.Map.Actions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MLEM.Ui;

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
        }

        private void Use()
        {
            GameProcess.Instance.Player.Make(new UseActionMove(_action));
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var state = Keyboard.GetState();
            if (state[_key] == KeyState.Down && _previousState[_key] == KeyState.Up)
                Use();
            _previousState = state;
        }
    }
}