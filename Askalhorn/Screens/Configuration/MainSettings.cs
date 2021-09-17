using Askalhorn.Elements;
using Askalhorn.UI;
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
            _menu = new Menu();
        }

        public override void Initialize() 
        { 
            _menu.AddButton("Графика", () => ScreenManager.LoadScreen(new GraphicsSettings(_game, this)));
            _menu.AddButton("Звук", () => ScreenManager.LoadScreen(new MediaSettings(_game, this)));
            _menu.AddButton("Управление", () => ScreenManager.LoadScreen(new ControlSettings(_game, this)));
            _menu.AddButton("По умолчанию", Askalhorn.Settings.Configuration.SetDefaultOptions);
            _menu.AddButton("Назад", Back);
            _game.UiSystem.Add("menu", _menu);
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