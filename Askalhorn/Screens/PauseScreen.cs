using System.IO;
using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.Core;
using Askalhorn.Elements;
using Askalhorn.Screens.Configuration;
using Askalhorn.UI;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Screens;
using Newtonsoft.Json;

namespace Askalhorn.Screens
{
    public class PauseScreen: BackScreenBase
    {
        private readonly Menu _menu;

        public PauseScreen(AskalhornGame game, GameScreen backScreen)
            : base(game, backScreen)
        {
            _menu = new Menu();
        }

        private void QuickSave()
        {
            GameProcess.Instance.Save("quicksave");
            Back();
        }

        public override void Initialize() 
        { 
            _menu.AddButton("Назад", Back);
            _menu.AddButton("Быстрое сохранение", QuickSave);
            _menu.AddButton("Настройки", () => ScreenManager.LoadScreen(new MainSettings(Game, this)));
            _menu.AddButton("В главное меню", () => ScreenManager.LoadScreen(new MainMenuScreen(Game)));
            _menu.AddButton("Выход", Game.Exit);
            Game.UiSystem.Add("menu", _menu);
        }
    }
}