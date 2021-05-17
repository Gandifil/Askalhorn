using System.Collections.Generic;
using System.Linq;
using AmbrosiaGame.Utils;
using Askalhorn.Common;
using Askalhorn.Common.Control;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Logging;
using Askalhorn.Render;
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
        private CharacterRenderer characterRenderer;
        private OrthographicCamera camera;
        private KeyboardListener keyboardListener;

        private GameLog log;
        private World world;

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
            characterRenderer = new CharacterRenderer();
            log = new GameLog(GraphicsDevice);
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
                world.playerController.AddMove(new MovementMove(shift.Value));
                world.Turn();
            }
        }
        
        public override void LoadContent()
        {
            world = new World();
            mapRenderer.LoadMap(world.Location.TiledMap);
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
        
        private static readonly List<Point> Variants = new List<Point>
        {
            new Point(0, -1),
            new Point(0, 1),
            new Point(1, 0),
            new Point(-1, 0),
        };
        
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            var texture = Content.Load<Texture2D>("images/movement");
            var texture2 = Content.Load<Texture2D>("images/movement_selected");

            var matrix = camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: matrix, samplerState: SamplerState.PointClamp);
            mapRenderer.Draw(matrix);
            var player = world.Characters.First();
            foreach (var item in Variants)
            {
                
                var pos = new Position()
                {
                    Point = player.Position.Point + item,
                };
                var r = Vector2.Transform(pos.RenderVectorOrigin, matrix);
                if ((r - Mouse.GetState().Position.ToVector2()).Length() < 40)
                    spriteBatch.Draw(texture2, pos.RenderVector, Color.White);
                else
                    spriteBatch.Draw(texture, pos.RenderVector, Color.White);
            }
            
            foreach (var item in world.Characters)
                characterRenderer.Draw(spriteBatch, item);
            log.Draw();
            spriteBatch.End();
        }
    }
}
