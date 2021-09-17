using AmbrosiaGame.Screens;
using Askalhorn.Core;
using Askalhorn.Screens.Configuration;
using Askalhorn.UI;

namespace Askalhorn.Screens
{
    public class MainMenuScreen : ClearUIScreen
    {
        private readonly Menu _menu;

        public MainMenuScreen(AskalhornGame game)
            : base(game)
        {
            _menu = new Menu();
        }

        public override void Initialize()
        {
            _menu.AddButton("Новая карта", () => ScreenManager.LoadScreen(new WorldGenerationScreen(Game)));
            _menu.AddButton("Играть", () => ScreenManager.LoadScreen(new GameProcessScreen(Game, new GameProcess())));
            _menu.AddButton("Загрузить", () => ScreenManager.LoadScreen(new GameProcessScreen(Game, new GameProcess("quicksave"))));
            _menu.AddButton("Настройки", () => ScreenManager.LoadScreen(new MainSettings(Game, this)));
            _menu.AddButton("Выход", () => Game.Exit());
            Game.UiSystem.Add("menu", _menu);
        }
    }
}
