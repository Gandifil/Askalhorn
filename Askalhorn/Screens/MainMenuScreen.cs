using System.Reflection.Metadata;
using AmbrosiaGame.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Font;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;
using MonoGame.Extended.Screens;

namespace Askalhorn.Screens
{
    public class MainMenuScreen : GameScreen
    {
        public AskalhornGame game;

        public MainMenuScreen(Game game)
            : base(game)
        {
            this.game = (AskalhornGame)game;
        }

        public override void LoadContent() { 
            var box = new Panel(Anchor.Center, new Vector2(0.5f, 0.5f), Vector2.Zero, setHeightBasedOnChildren: true);
            
            box.AddChild(new VerticalSpace(3));
            box.AddChild(new Button(Anchor.AutoCenter, new Vector2(0.5F, 40), "Новая карта")
            {
                OnPressed = element => ScreenManager.LoadScreen(new WorldGenerationScreen(game)),
            });
            box.AddChild(new VerticalSpace(3));
            box.AddChild(new Button(Anchor.AutoCenter, new Vector2(0.5F, 40), "Играть")
            {
                OnPressed = element => ScreenManager.LoadScreen(new GameProcessScreen(game)),
            });
            box.AddChild(new VerticalSpace(3));
            box.AddChild(new Button(Anchor.AutoCenter, new Vector2(0.5F, 40), "Загрузить")
            {
                OnPressed = element => ScreenManager.LoadScreen(new GameProcessScreen(game)),
            });
            box.AddChild(new VerticalSpace(3));
            box.AddChild(new Button(Anchor.AutoCenter, new Vector2(0.5F, 40), "Настройки")
            {
                OnPressed = element => ScreenManager.LoadScreen(new GameProcessScreen(game)),
            });
            box.AddChild(new VerticalSpace(3));
            box.AddChild(new Button(Anchor.AutoCenter, new Vector2(0.5F, 40), "Выход")
            {
                OnPressed = element => game.Exit(),
            });
            box.AddChild(new VerticalSpace(3));
            game.UiSystem.Add("MainMenuBox", box);
        }

        public override void UnloadContent()
        {
            game.UiSystem.Remove("MainMenuBox");
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
        }
    }
}
