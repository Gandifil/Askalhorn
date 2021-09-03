using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Askalhorn;
using Askalhorn.Common;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Components;
using Askalhorn.Elements;
using Askalhorn.Render;
using Askalhorn.Screens;
using Askalhorn.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MLEM.Textures;
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
        public MovementTiles movements;
        private InputListenerComponent listeners;
        private SwitchComponent switcher;
        private ActionsComponent actions;
        private EffectsComponent effects;
        private AbilitiesComponent abilities;
        private Options _options;

        public GameProcessScreen(AskalhornGame game, World world)
            : base(game)
        {
            this.game = game;
            this.World = world;
            world.OnTurn += UpdateMovements;
            world.OnTurn += UpdateActions;
            world.OnTurn += LookAtPlayer;
            world.OnOpenBag += bag =>
            {
                switcher.SwitchTo(new ExchangeTabComponent(this, bag, world.Player.Bag));
            };
            world.OnChangeLocation += UpdateMap;
            world.OnChangeLocation += UpdateActions;
            world.OnChangeLocation += LookAtPlayer;
            
            _options = Configuration.Options;
        }

        private void UpdateActions()
        {
            actions.Clear();

            var build = World.Location[World.Player.Position].Build;
            if (build is not null)
            {
                switch (build.Type)
                {
                    case IBuild.Types.Chest:
                        actions.Add(new ActionBlock
                        {
                            Region = Storage.Load("guiactions", 1, 0),
                            Key = _options.Keys[Options.KeyActions.Use],
                            Action = () => World.Location[World.Player.Position].Build.Action.Invoke()
                        });
                        break;
                    case IBuild.Types.Teleport:
                        actions.Add(new ActionBlock
                        {
                            Region = Storage.Load("guiactions", 2, 0),
                            Key = _options.Keys[Options.KeyActions.Use],
                            Action = () => World.Location[World.Player.Position].Build.Action.Invoke()
                        });
                        break;
                    default:
                        break;
                }     
            }

            var characterNear = World.FindNear(World.Player.Position);
            if (characterNear is not null)
            {
                if (characterNear.Dialog is not null)
                    actions.Add(new ActionBlock
                    {
                        Region = Storage.Load("guiactions", 0, 0),
                        Key = _options.Keys[Options.KeyActions.Use],
                        Action = () => switcher.SwitchTo(new DialogTabComponent(characterNear.Dialog))
                    });
            }
            
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
            
            var keyboardListener = new KeyboardListener();
            keyboardListener.KeyReleased += KeyRelease;
            
            var mouseListener = new MouseListener();
            mouseListener.MouseClicked += MouseClick;

            listeners = new InputListenerComponent(Game, keyboardListener, mouseListener);
            
            Game.Components.Add(listeners);
            Game.Components.Add(new LogComponent(game));
            switcher = new SwitchComponent(this);
            Game.Components.Add(switcher);
            actions = new ActionsComponent(game);
            Game.Components.Add(actions);
            effects = new EffectsComponent(this, World.Player);
            World.OnTurn += effects.Update;
            Game.Components.Add(effects);
            abilities = new AbilitiesComponent(this, World.Player);
            Game.Components.Add(abilities);
            movements = new MovementTiles(World.Player);
            UpdateMovements();
        }

        private void MovePlayer(Point shift)
        {
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
            if (e.Key == _options.Keys[Options.KeyActions.Pause])
                ScreenManager.LoadScreen(new PauseScreen(game, this, World));
            
            if (e.Key == _options.Keys[Options.KeyActions.TopRight])
                MovePlayer(new Point(0, -1));

            if (e.Key == _options.Keys[Options.KeyActions.BottomLeft])
                MovePlayer(new Point(0, 1));

            if (e.Key == _options.Keys[Options.KeyActions.TopLeft])
                MovePlayer(new Point(-1, 0));

            if (e.Key == _options.Keys[Options.KeyActions.BottomRight])
                MovePlayer(new Point(1, 0));

            //if (e.Key == Keys.F)
            //    World.Location[World.Player.Position].Build?.Action();

            if (e.Key == _options.Keys[Options.KeyActions.Character])
                switcher.SwitchTo<CharacterTabComponent>();

            if (e.Key == _options.Keys[Options.KeyActions.Inventory])
                switcher.SwitchTo<InventoryTabComponent>();

            if (e.Key == _options.Keys[Options.KeyActions.Journal])
                switcher.SwitchTo<JournalTabComponent>();

            if (e.Key == _options.Keys[Options.KeyActions.Abilities])
                switcher.SwitchTo<AbilitiesTabComponent>();

            
           // if (e.Key == Keys.E)
           //     World.playerController.AddMove(new AttackMove(World.Characters.ElementAt(1)));

            if (e.Key >= Keys.D0 && e.Key <= Keys.D9)
                abilities.Run(e.Key - Keys.D0);
        }

        private void UpdateMovements()
        {
            movements.AvailableAbilities = new List<UseAbilityMove>();
            movements.AvailableMovements = World.Player.AvailableMovements;
        }

        public override void LoadContent()
        {
            characterRenderer = new CharacterRenderer(spriteBatch);
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
