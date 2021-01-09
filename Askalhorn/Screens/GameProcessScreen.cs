using AmbrosiaGame.GameWorld;
using AmbrosiaGame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;

namespace AmbrosiaGame.Screens
{
    public class GameProcessScreen: GameScreen
    {
        private SpriteBatch spriteBatch;
        private TiledMapRenderer mapRenderer;
        private OrthographicCamera camera;
        private KeyboardListener keyboardListener;

        private IWorld world;


        public GameProcessScreen(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            spriteBatch = new SpriteBatch(GraphicsDevice);
            var viewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, 800, 600);
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
                shift = new Point(1, 0);

            if (e.Key == Keys.D)
                shift = new Point(-1, 0);

            if (shift.HasValue)
                if (CheckCell(world.GetPlayer().Position + shift.Value))
                    world.GetPlayer().Move(shift.Value);
        }

        private bool CheckCell(Point point)
        {
            return world.IsFreeCell(point.X, point.Y);
        }
        
        public override void LoadContent()
        {
            world = new LocalWorld(Content.Load<TiledMap>("maps/start"));
            mapRenderer.LoadMap(world.GetTiledMap());
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

            spriteBatch.Begin(transformMatrix: camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            mapRenderer.Draw(camera.GetViewMatrix());

            foreach (var obj in world.GetGameObjects())
                obj.Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();

            GameLog.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
