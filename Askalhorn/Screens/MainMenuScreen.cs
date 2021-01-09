using Askalhorn;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Screens;

namespace AmbrosiaGame.Screens
{
    public class MainMenuScreen : GameScreen
    {
        public AskalhornGame game;
        private SpriteBatch spriteBatch;
        private SpriteFont font; 

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
            font = game.Content.Load<SpriteFont>("fonts/GameLogsFont");
            var box = new Panel(Anchor.AutoCenter, new Vector2(0.5f, 0.5f), Vector2.Zero, setHeightBasedOnChildren: true);
            var startButton = new Button(Anchor.Center, new Vector2(0.5F, 40), "Okay", "Okay", 200)
            {
                //OnPressed = element => game.UiSystem.Remove("InfoBox"),
                PositionOffset = new Vector2(0, 1)
            };
            var exitButton = new Button(Anchor.Center, new Vector2(0.5F, 40), "Exit")
            {
                //OnPressed = element => game.UiSystem.Remove("InfoBox"),
                PositionOffset = new Vector2(0, 100)
            };
            box.AddChild(startButton);
            box.AddChild(exitButton);
            game.UiSystem.Add("MainMenuBox", box);
        }

        public override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            // UiSystem.Dispose();
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
