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
        public PauseScreen(AskalhornGame game, GameScreen backScreen)
            : base(game, backScreen)
        {
        }

        private void QuickSave()
        {
            GameProcess.Instance.Save("quicksave");
            Back();
        }

        public override void Initialize() 
        { 
            var menu = new Menu();
            menu.AddButton("Назад", Back);
            menu.AddButton("Быстрое сохранение", QuickSave);
            menu.AddButton("Настройки", () => ScreenManager.LoadScreen(new MainSettings(Game, this)));
            menu.AddButton("В главное меню", () => ScreenManager.LoadScreen(new MainMenuScreen(Game)));
            menu.AddButton("Выход", Game.Exit);
            Game.UiSystem.Add("menu", menu);
        }
    }
}