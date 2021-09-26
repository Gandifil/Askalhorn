using Askalhorn.Elements;
using Askalhorn.Settings;
using Askalhorn.UI;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Screens;

namespace Askalhorn.Screens.Configuration
{
    public class GraphicsSettings: BackScreenBase
    {
        private readonly AskalhornGame _game;
        private readonly Menu _menu;
        private readonly Options _options;
        
        public GraphicsSettings(AskalhornGame game, GameScreen backScreen) : base(game, backScreen)
        {
            _game = game;
            _menu = new Menu();
            _options = Settings.Configuration.Options;
        }

        public override void Initialize() 
        { 
            _menu.Add(new Checkbox(Anchor.AutoCenter, 
                new Vector2(0.5f, 40), 
                "Полноэкранный режим", 
                _options.IsFullScreen)
            {
                OnCheckStateChange = (box, checced) => SetFullScreen(checced), 
            });
            _menu.AddButton("Назад", Back);
            _game.UiSystem.Add("menu", _menu);
        }

        private void SetFullScreen(bool checced)
        {
            _options.IsFullScreen = checced;
            Settings.Configuration.Change();
        }
    }
}