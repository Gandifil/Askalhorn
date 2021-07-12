using System.IO;
using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Elements;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Screens;
using Newtonsoft.Json;

namespace Askalhorn.Screens
{
    public class PauseScreen: BackScreenBase
    {
        public AskalhornGame _game;
        public readonly World _world;
        private readonly Menu _menu;

        public PauseScreen(AskalhornGame game, GameScreen backScreen, World world)
            : base(game, backScreen)
        {
            _game = game;
            _world = world;
            _menu = new Menu(game.UiSystem);
        }

        private void QuickSave()
        {
            _world.Save("quicksave");
            Back();
        }

        public override void Initialize() 
        { 
            _menu.AddButton("Назад", Back);
            _menu.AddButton("Быстрое сохранение", QuickSave);
            _menu.AddButton("В главное меню", () => ScreenManager.LoadScreen(new MainMenuScreen(_game)));
            _menu.AddButton("Выход", Game.Exit);
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