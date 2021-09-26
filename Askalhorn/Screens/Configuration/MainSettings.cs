using Askalhorn.Elements;
using Askalhorn.UI;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace Askalhorn.Screens.Configuration
{
    public class MainSettings: BackScreenBase
    {
        private readonly AskalhornGame _game;
        
        public MainSettings(AskalhornGame game, GameScreen backScreen) : base(game, backScreen)
        {
            _game = game;
        }

        public override void Initialize() 
        { 
            var menu = new Menu();
            menu.AddButton("Графика", () => ScreenManager.LoadScreen(new GraphicsSettings(_game, this)));
            menu.AddButton("Звук", () => ScreenManager.LoadScreen(new MediaSettings(_game, this)));
            menu.AddButton("Управление", () => ScreenManager.LoadScreen(new ControlSettings(_game, this)));
            menu.AddButton("По умолчанию", Askalhorn.Settings.Configuration.SetDefaultOptions);
            menu.AddButton("Назад", Back);
            _game.UiSystem.Add("menu", menu);
        }

        public override void Dispose()
        {
            Settings.Configuration.Save();
            
            base.Dispose();
        }
    }
}