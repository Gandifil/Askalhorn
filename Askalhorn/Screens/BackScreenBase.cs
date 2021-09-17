using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace Askalhorn.Screens
{
    public abstract class BackScreenBase : ClearUIScreen
    {
        private readonly GameScreen backScreen;

        protected BackScreenBase(AskalhornGame game, GameScreen backScreen) : base(game)
        {
            this.backScreen = backScreen;
        }

        protected void Back()
        {
            ScreenManager.LoadScreen(backScreen);
        }
    }
}