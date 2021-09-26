using Askalhorn.Elements;
using Askalhorn.Settings;
using Askalhorn.UI;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Screens;

namespace Askalhorn.Screens.Configuration
{
    public class MediaSettings: BackScreenBase
    {
        private readonly AskalhornGame _game;
        private readonly Menu _menu;
        private readonly Options _options;
        
        public MediaSettings(AskalhornGame game, GameScreen backScreen) : base(game, backScreen)
        {
            _game = game;
            _menu = new Menu();
            _options = Settings.Configuration.Options;
        }

        public override void Initialize()
        {
            var cScrollBar = _menu.AddScroll("Общая громкость");
            cScrollBar.CurrentValue = _options.CommonVolume;
            cScrollBar.OnValueChanged += (element, value) =>
            {
                _options.CommonVolume = value;
                Settings.Configuration.Change();
            };
              
            var mScrollBar = _menu.AddScroll("Громкость музыки");
            mScrollBar.CurrentValue = _options.SongVolume;
            mScrollBar.OnValueChanged += (element, value) =>
            {
                _options.SongVolume = value;
                Settings.Configuration.Change();
            };
            
            var sScrollBar = _menu.AddScroll("Громкость эффектов");
            sScrollBar.CurrentValue = _options.SoundVolume;
            sScrollBar.OnValueChanged += (element, value) =>
            {
                _options.SoundVolume = value;
                Settings.Configuration.Change();
            };
            
            
            _menu.AddButton("Назад", Back);
            _game.UiSystem.Add("menu", _menu);
        }
    }
}