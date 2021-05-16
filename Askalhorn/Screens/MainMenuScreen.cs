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

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent() { 
            var style = new UntexturedStyle(game.SpriteBatch) {
                Font = new GenericSpriteFont(game.Content.Load<SpriteFont>("fonts/GameLogsFont")),

            };
            game.UiSystem.Style = style;
            
            var box = new Panel(Anchor.Center, new Vector2(0.5f, 0.5f), Vector2.Zero, setHeightBasedOnChildren: true);
            box.AddChild(new Button(Anchor.AutoCenter, new Vector2(0.5F, 40), "Новая карта", "Okay", 200)
            {
                OnPressed = element => ScreenManager.LoadScreen(new WorldGenerationScreen(game)),
            });
            box.AddChild(new Button(Anchor.AutoCenter, new Vector2(0.5F, 40), "Играть", "Okay", 200)
            {
                OnPressed = element => ScreenManager.LoadScreen(new GameProcessScreen(game)),
            });
            box.AddChild(new Button(Anchor.AutoCenter, new Vector2(0.5F, 40), "Выход", "Okay", 200)
            {
                OnPressed = element => game.Exit(),
            });
            game.UiSystem.Add("MainMenuBox", box);
        }

        public override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            game.UiSystem.Remove("MainMenuBox");
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            //game.SpriteBatch.Begin();
            //game.SpriteBatch.DrawString(font, "Score", new Vector2(100, 100), Color.Black);
            //game.SpriteBatch.End();
            //UiSystem.Draw(gameTime, spriteBatch);
        }
    }
}
