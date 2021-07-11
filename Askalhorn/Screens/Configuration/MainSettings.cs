using Askalhorn.Elements;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace Askalhorn.Screens.Configuration
{
    public class MainSettings: BackScreenBase
    {
        private readonly AskalhornGame _game;
        private readonly Menu _menu;
        
        public MainSettings(AskalhornGame game, GameScreen backScreen) : base(game, backScreen)
        {
            _game = game;
            _menu = new Menu(game.UiSystem);
        }

        public override void Initialize() 
        { 
            _menu.AddButton("Графика", () => ScreenManager.LoadScreen(new GraphicsSettings(_game, this)));
            _menu.AddButton("Звук", () => ScreenManager.LoadScreen(new MediaSettings(_game, this)));
            _menu.AddButton("Назад", Back);
            _menu.Initialize();
        }

        public override void Dispose()
        {
            Settings.Configuration.Save();
            _menu.Dispose();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
        }
    }
}