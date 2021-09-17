using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.Core;
using Askalhorn.Elements;
using Askalhorn.Screens.Configuration;
using Askalhorn.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Screens;

namespace Askalhorn.Screens
{
    public class MainMenuScreen : GameScreen
    {
        public AskalhornGame game;

        private Texture2D _texture;

        private readonly Menu _menu;

        public MainMenuScreen(AskalhornGame game)
            : base(game)
        {
            this.game = game;
            _menu = new Menu(game.UiSystem);
        }

        public override void Initialize()
        {
            _menu.AddButton("Новая карта", () => ScreenManager.LoadScreen(new WorldGenerationScreen(game)));
            _menu.AddButton("Играть", () => ScreenManager.LoadScreen(new GameProcessScreen(game, new GameProcess())));
            _menu.AddButton("Загрузить", () => ScreenManager.LoadScreen(new GameProcessScreen(game, new GameProcess("quicksave"))));
            _menu.AddButton("Настройки", () => ScreenManager.LoadScreen(new MainSettings(game, this)));
            _menu.AddButton("Выход", () => game.Exit());
            _menu.Initialize();
        }

        public override void Dispose()
        {
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
