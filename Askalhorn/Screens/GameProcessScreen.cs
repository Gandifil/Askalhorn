using AmbrosiaGame.Utils;
using Askalhorn.Common;
using Askalhorn.Common.Characters;
using Askalhorn.Common.Geography;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;
using Serilog;

namespace AmbrosiaGame.Screens
{
    public class GameProcessScreen: GameScreen
    {
        private SpriteBatch spriteBatch;
        private TiledMapRenderer mapRenderer;
        private OrthographicCamera camera;
        private KeyboardListener keyboardListener;

        private ILocation location;
        private ICharacter player;
        private GameLog log;

        public GameProcessScreen(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            spriteBatch = new SpriteBatch(GraphicsDevice);
            var viewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, 1280, 1024);
            camera = new OrthographicCamera(viewportAdapter);
            mapRenderer = new TiledMapRenderer(GraphicsDevice);
            keyboardListener = new KeyboardListener();

            keyboardListener.KeyReleased += KeyRelease;
        }

        private void KeyRelease(object sender, KeyboardEventArgs e)
        {
            Point? shift = null;

            if (e.Key == Keys.W)
                shift = new Point(0, -1);

            if (e.Key == Keys.S)
                shift = new Point(0, 1);

            if (e.Key == Keys.A)
                shift = new Point(-1, 0);

            if (e.Key == Keys.D)
                shift = new Point(1, 0);

            if (shift.HasValue)
            {
                player.Position.Move(shift.Value);
                Log.Information("Shift player to {Point}", player.Position.Point);
            }
        }
        
        public override void LoadContent()
        {
            log = new GameLog(GraphicsDevice);
            location = new TiledMapLocation("start");
            player = new StaticCharacter()
            {
                Texture = Storage.Content.Load<Texture2D>("images/mage"),
                Position = new Position(0, 0),
            };
            mapRenderer.LoadMap(location.TiledMap);
        }

        public override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        
        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game.Exit();
        
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                camera.Move(new Vector2(-10, 0));

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                camera.Move(new Vector2(10, 0));

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                camera.Move(new Vector2(0, 10));

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                camera.Move(new Vector2(0, -10));

            keyboardListener.Update(gameTime);
            mapRenderer.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var matrix = camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: matrix, samplerState: SamplerState.PointClamp);
            mapRenderer.Draw(matrix);
            player.Draw(spriteBatch, matrix);
            log.Draw();
            spriteBatch.End();
        }
    }
}
