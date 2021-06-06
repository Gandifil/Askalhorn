using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace Askalhorn.Screens
{
    public abstract class BackScreenBase : GameScreen
    {
        private readonly GameScreen backScreen;

        protected BackScreenBase(Game game, GameScreen backScreen) : base(game)
        {
            this.backScreen = backScreen;
        }

        protected void Back()
        {
            ScreenManager.LoadScreen(backScreen);
        }
    }
}