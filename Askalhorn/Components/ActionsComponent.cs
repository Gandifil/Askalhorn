using System.Collections.Generic;
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
        private readonly List<IActionBlock> Actions = new List<IActionBlock>();
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
            Actions.Clear();
        }

        public void Add(IActionBlock action)
        {
            if (panel is null)
            {
                panel = new Panel(Anchor.Center, new Vector2(0.1f, 0.05f), new Vector2(0, 100));
                game.UiSystem.Add(PANEL_NAME, panel);
            }
            
            Actions.Add(action);
            panel.AddChild(createElement(action));
        }

        private Element createElement(IActionBlock action)
        {
            var image = new Image(Anchor.AutoCenter, new Vector2(0.5f, 0.9f), action.Region.ToMlem())
            {
                OnPressed = _ => action.Action.Invoke(),
                CanBePressed = true,
                CanBeMoused = true,
            };
            var tooltip = new Tooltip(100, action.Key.ToString(), image);
            return image;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var state = Keyboard.GetState();

            foreach (var action in Actions)
                if (state[action.Key] == KeyState.Down && previousState[action.Key] == KeyState.Up)
                    action.Action?.Invoke();

            previousState = state;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Clear();
        }
    }
}