using Askalhorn.UI;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace Askalhorn.Screens
{
    public class ClearUIScreen: GameScreen
    {
        public AskalhornGame Game { get; } 
        
        public ClearUIScreen(AskalhornGame game) : base(game)
        {
            Game = game;
        }

        public override void Dispose()
        {
            base.Dispose();
            
            Game.UiSystem.Clear();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
        }
    }
}