using AmbrosiaGame.Resources;
using AmbrosiaGame.Screens;
using AmbrosiaGame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MLEM.Extended.Font;
using MLEM.Font;
using MLEM.Ui;
using MLEM.Ui.Style;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

namespace Askalhorn
{
    public class AskalhornGame : Game
    {
        private ScreenManager screenManager;

        public GraphicsDeviceManager Graphics { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }

        public UiSystem UiSystem { get; private set; }

        public AskalhornGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            screenManager = new ScreenManager();
            Components.Add(screenManager);
        }

        protected override void Initialize()
        {
            base.Initialize();

            Graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            Graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            Graphics.ApplyChanges();

            Content.RootDirectory = "Content";

            Mouse.SetCursor(MouseCursor.FromTexture2D(Content.Load<Texture2D>("images/cursor"), 0, 0));
            IsMouseVisible = true;

            InfoManager.Initialize(Content);
            GameLog.Initialize(Content);
            LineRenderer.Initialize(GraphicsDevice);

            SpriteBatch = new SpriteBatch(GraphicsDevice);
            // Initialize the Ui system
            var style = new UntexturedStyle(SpriteBatch)
            {
                Font = new GenericSpriteFont(Content.Load<SpriteFont>("fonts/MenuGame")),
                //ButtonTexture = new NinePatch(Content.Load<Texture2D>("Textures/ExampleTexture"), padding: 1)
            };
            UiSystem = new UiSystem(Window, GraphicsDevice, style);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            screenManager.LoadScreen(new MainMenuScreen(this), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));    
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            UiSystem.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // DrawEarly needs to be called before clearing your graphics context
            UiSystem.DrawEarly(gameTime, SpriteBatch);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // Call Draw at the end to draw the Ui on top of your game
            UiSystem.Draw(gameTime, SpriteBatch);

            base.Draw(gameTime);
        }
    }
}
