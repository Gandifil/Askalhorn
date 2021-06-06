using System.IO;
using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.Common.Geography.Local;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Screens;
using Newtonsoft.Json;

namespace Askalhorn.Screens
{
    public class PauseScreen: BackScreenBase
    {
        public AskalhornGame game;
        public readonly World world;

        public PauseScreen(Game game, GameScreen backScreen, World world)
            : base(game, backScreen)
        {
            this.game = (AskalhornGame)game;
            this.world = world;
        }

        private void QuickSave()
        {
            world.Save("quicksave");
            
            Back();
        }

        public override void LoadContent() 
        { 
            var box = new Panel(Anchor.Center, new Vector2(0.5f, 0.5f), Vector2.Zero, setHeightBasedOnChildren: true);
            
            box.AddChild(new Button(Anchor.AutoCenter, new Vector2(0.5F, 40), "Назад")
            {
                OnPressed = _ => Back(),
            });
            box.AddChild(new VerticalSpace(3));
            box.AddChild(new Button(Anchor.AutoCenter, new Vector2(0.5F, 40), "Быстрое сохранение")
            {
                OnPressed = _ => QuickSave(),
            });
            box.AddChild(new VerticalSpace(3));
            box.AddChild(new Button(Anchor.AutoCenter, new Vector2(0.5F, 40), "В главное меню")
            {
                OnPressed = _ => ScreenManager.LoadScreen(new MainMenuScreen(game)),
            });
            box.AddChild(new VerticalSpace(3));
            box.AddChild(new Button(Anchor.AutoCenter, new Vector2(0.5F, 40), "Выход")
            {
                OnPressed = _ => Game.Exit(),
            });
            game.UiSystem.Add("menu", box);
        }

        public override void UnloadContent()
        {
            game.UiSystem.Remove("menu");
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
        }
        
    }
}