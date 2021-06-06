using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Askalhorn;
using Askalhorn.Common;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Components;
using Askalhorn.Elements;
using Askalhorn.Logging;
using Askalhorn.Render;
using Askalhorn.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MLEM.Ui;
using MLEM.Ui.Elements;
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
        
        private TiledMapRenderer mapRenderer;
        private CharacterRenderer characterRenderer;
        private MovementTiles movements;
        private readonly World world;
        private InputListenerComponent listeners;

        public GameProcessScreen(AskalhornGame game, World world)
            : base(game)
        {
            this.game = game;
            this.world = world;
            world.OnTurn += UpdateMovements;
            world.OnOpenBag += bag =>
            {
                InventoryTab.CreateExchangeTab(game.UiSystem, bag, world.Player.Bag);
            };
            world.OnChangeLocation += UpdateMap;
        }

        private void UpdateMap()
        {
            mapRenderer.LoadMap(world.Location.TiledMap);
        }

        public override void Initialize()
        {
            base.Initialize();

            spriteBatch = new SpriteBatch(GraphicsDevice);
            var viewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, 1280, 1024);
            camera = new OrthographicCamera(viewportAdapter);
            
            mapRenderer = new TiledMapRenderer(GraphicsDevice);
            UpdateMap();
            
            characterRenderer = new CharacterRenderer();
            
            var keyboardListener = new KeyboardListener();
            keyboardListener.KeyReleased += KeyRelease;
            
            var mouseListener = new MouseListener();
            mouseListener.MouseClicked += MouseClick;

            listeners = new InputListenerComponent(Game, keyboardListener, mouseListener);
            Game.Components.Add(listeners);
            
        }

        private void MovePlayer(Point shift)
        {
            var move = new MovementMove(shift);
            if (move.IsValid(world.Player))
                world.playerController.AddMove(new MovementMove(shift));
        }


        private void MouseClick(object sender, MouseEventArgs args)
        {
            var move = movements.CheckClick(args.Position, camera.GetViewMatrix());
            if (move is not null && move.IsValid(world.Player))
                world.playerController.AddMove(move);
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
                world.Location[world.Player.Position].Build?.Action();

            if (e.Key == Keys.C)
                CharacterTab.Toggle(game.UiSystem, world.Player);

            if (e.Key == Keys.I)
                InventoryTab.Toggle(game.UiSystem, world.Player.Bag, (_, item) => 
                    world.playerController.AddMove(new UseItemMove(item)));

            if (e.Key == Keys.E)
                world.playerController.AddMove(new AttackMove(world.Characters.ElementAt(1)));
        }

        private void UpdateMovements()
        {
            movements.AvailableAbilities = new List<UseAbilityMove>();
            movements.AvailableMovements = world.Player.AvailableMovements;
            
            
            game.UiSystem.Remove("abilities");
            var box = new Panel(Anchor.BottomRight, new Vector2(0.7f, 0.1f), Vector2.Zero);
            foreach (var item in world.Player.Abilities)
            {
                var image = new Image(Anchor.Center, new Vector2(0.6F, 0.75F), new MLEM.Textures.TextureRegion(item.Icon));
                image.CanBeMoused = true;
                image.OnPressed += element =>
                {
                    movements.AvailableAbilities = world.Characters.Select(x =>
                        new UseAbilityMove(item)
                        {
                            Target = x,
                        });
                };
                var tooltip = new Tooltip(200, item.Name + "\n" + item.Description, image);
                tooltip.MouseOffset = new Vector2(32, -64);
                box.AddChild(image);
            }
            game.UiSystem.Add("abilities", box);
        }

        public override void LoadContent()
        {
            movements = new MovementTiles(world.Player);
            UpdateMovements();

            //game.UiSystem.Add("log", GameLogSink.Create());
            Game.Components.Add(new LogComponent(game));
        }

        public override void UnloadContent()
        {
            Game.Components.ClearWithDispose();
            
            game.UiSystem.Remove("abilities");
            // TODO: Unload any non ContentManager content here
        }
        
        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                ScreenManager.LoadScreen(new PauseScreen(game, this));
        
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                camera.Move(new Vector2(-10, 0));

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                camera.Move(new Vector2(10, 0));

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                camera.Move(new Vector2(0, 10));

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                camera.Move(new Vector2(0, -10));

            mapRenderer.Update(gameTime);
            foreach (var build in world.Location.Builds)
                build.Renderer.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var matrix = camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: matrix, samplerState: SamplerState.PointClamp);
            mapRenderer.Draw(matrix);
            movements.Draw(spriteBatch, matrix);

            foreach (var build in world.Location.Builds)
                build.Renderer.Draw(spriteBatch, build.Position);
            
            foreach (var item in world.Characters)
                characterRenderer.Draw(spriteBatch, item);
            spriteBatch.End();
        }
    }
}
