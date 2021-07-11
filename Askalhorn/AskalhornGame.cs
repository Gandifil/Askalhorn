using System;
using System.Collections.Generic;
using System.Reflection;
using AmbrosiaGame.Screens;
using AmbrosiaGame.Utils;
using Askalhorn.Common;
using Askalhorn.Logging;
using Askalhorn.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MLEM.Extended.Font;
using MLEM.Font;
using MLEM.Textures;
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

        private UntexturedStyle style;

        private DevelopConsole log;

        public AskalhornGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            //Graphics.IsFullScreen = true;
            ScreenManager = new ScreenManager();
            Components.Add(ScreenManager);
        }

        protected override void Initialize()
        {
            base.Initialize();

            Graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            Graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            Graphics.ApplyChanges();


            Mouse.SetCursor(MouseCursor.FromTexture2D(Content.Load<Texture2D>("images/cursor"), 0, 0));
            IsMouseVisible = true;

            Storage.Initialize(Content, GraphicsDevice);
            log = new DevelopConsole(Content.Load<SpriteFont>("fonts/GameLogsFont"), Color.Red);
            LineRenderer.Initialize(GraphicsDevice);
            
            UiSystem = new UiSystem(this, style);
            ScreenManager.LoadScreen(new MainMenuScreen(this), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));    
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
            Content.RootDirectory = "Content";

            var testTexture = Content.Load<Texture2D>("images/Test");
            var testPatch = new NinePatch(new TextureRegion(testTexture, 40, 0, 40, 40), 16);

            style = new UntexturedStyle(this.SpriteBatch) {
                Font = new GenericSpriteFont(
                    Content.Load<SpriteFont>("fonts/gui"), 
                    Content.Load<SpriteFont>("fonts/Text"), 
                    Content.Load<SpriteFont>("fonts/Text")),
                PanelTexture = testPatch,
                ButtonTexture = new NinePatch(new TextureRegion(testTexture, 24, 8, 16, 16), 4),
                TextFieldTexture = new NinePatch(new TextureRegion(testTexture, 24, 8, 16, 16), 4),
                ScrollBarBackground = new NinePatch(new TextureRegion(testTexture, 12, 0, 4, 8), 1, 1, 2, 2),
                ScrollBarScrollerTexture = new NinePatch(new TextureRegion(testTexture, 8, 0, 4, 8), 1, 1, 2, 2),
                CheckboxTexture = new NinePatch(new TextureRegion(testTexture, 24, 8, 16, 16), 4),
                CheckboxCheckmark = new TextureRegion(testTexture, 24, 0, 8, 8),
                RadioTexture = new NinePatch(new TextureRegion(testTexture, 16, 0, 8, 8), 3),
                RadioCheckmark = new TextureRegion(testTexture, 32, 0, 8, 8),
            };

            var music1 = Content.Load<Song>("music/Ivan Happy - Ode");
            var music2 = Content.Load<Song>("music/ivan happy - futility");
            var music3 = Content.Load<Song>("music/KOLARD BEATS - WAKE FROM ETERNAL SLEEP");
            var musics = CreateInstance<SongCollection>();
            musics.Add(music1);
            musics.Add(music2);
            musics.Add(music3);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(musics);
        }
        
        public static T CreateInstance<T>(params object[] args)
        {
            var type = typeof (T);
            var instance = type.Assembly.CreateInstance(
                type.FullName, false,
                BindingFlags.Instance | BindingFlags.NonPublic,
                null, args, null, null);
            return (T) instance;
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            UiSystem.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            UiSystem.DrawEarly(gameTime, SpriteBatch);

            //GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);

            UiSystem.Draw(gameTime, SpriteBatch);
            log.Draw(SpriteBatch);
        }
    }
}
