using System.Collections.Generic;
using Askalhorn.Characters.Control.Moves;
using Askalhorn.Core;
using Askalhorn.Map.Actions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MLEM.Extended.Extensions;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Components
{
    public class ActionsComponent: GameComponent
    {
        private static readonly string PANEL_NAME = "actions";
        private readonly AskalhornGame game;
        private readonly Dictionary<Keys, IAction> _actions= new ();
        private KeyboardState previousState;
        private Panel panel;
        
        public ActionsComponent(AskalhornGame game) : base(game)
        {
            this.game = game;
        }

        public void Clear()
        {
            if (panel is not null)
                game.UiSystem.Remove(PANEL_NAME);
            
            panel = null;
            _actions.Clear();
        }

        public void Add(Keys key, IAction action)
        {
            if (panel is null)
            {
                panel = new Panel(Anchor.Center, new Vector2(0.1f, 0.05f), new Vector2(0, 100));
                game.UiSystem.Add(PANEL_NAME, panel);
            }

            _actions[key] = action;
            
            panel.AddChild(createElement(action));
        }

        private Element createElement(IAction action)
        {
            var image = new Image(Anchor.AutoCenter, new Vector2(0.5f, 0.9f), action.Texture.ToMlem())
            {
                OnPressed = _ => GameProcess.Instance.Player.Make(new UseActionMove(action)),
                CanBePressed = true,
                CanBeMoused = true,
            };
            var tooltip = new Tooltip(100, action.Name.ToString(), image);
            return image;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var state = Keyboard.GetState();

            foreach (var action in _actions)
                if (state[action.Key] == KeyState.Down && previousState[action.Key] == KeyState.Up)
                {
                    GameProcess.Instance.Player.Make(new UseActionMove(action.Value));
                    previousState = state;
                    return;
                }

            previousState = state;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Clear();
        }
    }
}