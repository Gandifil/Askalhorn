using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Askalhorn;
using Askalhorn.Common;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Components;
using Askalhorn.Elements;
using Askalhorn.Render;
using Askalhorn.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;

namespace AmbrosiaGame.Screens
{
    public class GameProcessScreen: GameScreen
    {
        public AskalhornGame game;
        private SpriteBatch spriteBatch;
        private OrthographicCamera camera;
        
        public readonly World World;
        private TiledMapRenderer mapRenderer;
        private CharacterRenderer characterRenderer;
        private MovementTiles movements;
        private InputListenerComponent listeners;
        private SwitchComponent switcher;

        public GameProcessScreen(AskalhornGame game, World world)
            : base(game)
        {
            this.game = game;
            this.World = world;
            world.OnTurn += UpdateMovements;
            world.OnTurn += LookAtPlayer;
            world.OnOpenBag += bag =>
            {
                switcher.SwitchTo(new ExchangeTabComponent(this, bag, world.Player.Bag));
            };
            world.OnChangeLocation += UpdateMap;
            world.OnChangeLocation += LookAtPlayer;
        }
        private void LookAtPlayer()
        {
            camera.LookAt(World.Player.Position.RenderVector);
        }


        private void UpdateMap()
        {
            mapRenderer.LoadMap(World.Location.TiledMap);
        }

        public override void Initialize()
        {
            base.Initialize();

            spriteBatch = new SpriteBatch(GraphicsDevice);
            var viewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, 1280, 1024);
            camera = new OrthographicCamera(viewportAdapter);
            LookAtPlayer();
            
            mapRenderer = new TiledMapRenderer(GraphicsDevice);
            UpdateMap();
            
            characterRenderer = new CharacterRenderer();
            
            var keyboardListener = new KeyboardListener();
            keyboardListener.KeyReleased += KeyRelease;
            
            var mouseListener = new MouseListener();
            mouseListener.MouseClicked += MouseClick;

            listeners = new InputListenerComponent(Game, keyboardListener, mouseListener);
            
            Game.Components.Add(listeners);
            Game.Components.Add(new LogComponent(game));
            switcher = new SwitchComponent(this);
            Game.Components.Add(switcher);
        }

        private void MovePlayer(Point shift)
        {
            var move = new MovementMove(shift);
            if (move.IsValid(World.Player))
                World.playerController.AddMove(new MovementMove(shift));
        }


        private void MouseClick(object sender, MouseEventArgs args)
        {
            var move = movements.CheckClick(args.Position, camera.GetViewMatrix());
            if (move is not null && move.IsValid(World.Player))
                World.playerController.AddMove(move);
        }

        private void KeyRelease(object sender, KeyboardEventArgs e)
        {
            if (e.Key == Keys.W)
                MovePlayer(new Point(0, -1));

            if (e.Key == Keys.S)
                MovePlayer(new Point(0, 1));

            if (e.Key == Keys.A)
                MovePlayer(new Point(-1, 0));

            if (e.Key == Keys.D)
                MovePlayer(new Point(1, 0));

            if (e.Key == Keys.F)
                World.Location[World.Player.Position].Build?.Action();

            if (e.Key == Keys.C)
                switcher.SwitchTo<CharacterTabComponent>();

            if (e.Key == Keys.I)
                switcher.SwitchTo<InventoryTabComponent>();

            if (e.Key == Keys.J)
                switcher.SwitchTo<JournalTabComponent>();

            
            if (e.Key == Keys.E)
                World.playerController.AddMove(new AttackMove(World.Characters.ElementAt(1)));
        }

        private void UpdateMovements()
        {
            movements.AvailableAbilities = new List<UseAbilityMove>();
            movements.AvailableMovements = World.Player.AvailableMovements;
        }

        public override void LoadContent()
        {
            movements = new MovementTiles(World.Player);
            UpdateMovements();
            Game.Components.Add(new AbilitiesComponent(game, World.Player));
        }

        public override void UnloadContent()
        {
        }

        public override void Dispose()
        {
            base.Dispose();
            
            Game.Components.ClearWithDispose();
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                ScreenManager.LoadScreen(new PauseScreen(game, this, World));
            //
            // if (Keyboard.GetState().IsKeyDown(Keys.Left))
            //     camera.Move(new Vector2(-10, 0));
            //
            // if (Keyboard.GetState().IsKeyDown(Keys.Right))
            //     camera.Move(new Vector2(10, 0));
            //
            // if (Keyboard.GetState().IsKeyDown(Keys.Down))
            //     camera.Move(new Vector2(0, 10));
            //
            // if (Keyboard.GetState().IsKeyDown(Keys.Up))
            //     camera.Move(new Vector2(0, -10));

            mapRenderer.Update(gameTime);
            foreach (var build in World.Location.Builds)
                build.Renderer.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var matrix = camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: matrix, samplerState: SamplerState.PointClamp);
            mapRenderer.Draw(matrix);
            movements.Draw(spriteBatch, matrix);

            foreach (var build in World.Location.Builds)
                build.Renderer.Draw(spriteBatch, build.Position);
            
            foreach (var item in World.Characters)
                characterRenderer.Draw(spriteBatch, item);
            spriteBatch.End();
        }
    }
}
