using AmbrosiaGame.Screens;
using AmbrosiaGame.Utils;
using Askalhorn.Common;
using Askalhorn.Logging;
using Askalhorn.Screens;
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
        private ScreenManager ScreenManager { get; set; }

        public GraphicsDeviceManager Graphics { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }

        public UiSystem UiSystem { get; private set; }

        public AskalhornGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            ScreenManager = new ScreenManager();
            Components.Add(ScreenManager);
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

            Storage.Initialize(Content);
            LineRenderer.Initialize(GraphicsDevice);

            SpriteBatch = new SpriteBatch(GraphicsDevice);
            // Initialize the Ui system
            var style = new UntexturedStyle(SpriteBatch)
            {
                Font = new GenericSpriteFont(Content.Load<SpriteFont>("fonts/MenuGame")),
            };
            UiSystem = new UiSystem(Window, GraphicsDevice, style);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            ScreenManager.LoadScreen(new MainMenuScreen(this), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));    
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

            base.Draw(gameTime);

            // TODO: Add your drawing code here
            // Call Draw at the end to draw the Ui on top of your game
            UiSystem.Draw(gameTime, SpriteBatch);
            //GameLog.Draw(SpriteBatch);
            //LineRenderer.Draw(SpriteBatch, );
        }
    }
}
