using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Askalhorn;
using Askalhorn.Characters;
using Askalhorn.Characters.Control.Moves;
using Askalhorn.Characters.Impacts;
using Askalhorn.Common;
using Askalhorn.Components;
using Askalhorn.Core;
using Askalhorn.Elements;
using Askalhorn.Map;
using Askalhorn.Map.Builds;
using Askalhorn.Map.Local;
using Askalhorn.Render;
using Askalhorn.Screens;
using Askalhorn.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MLEM.Textures;
using MLEM.Ui;
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
        
        public readonly GameProcess GameProcess;
        private TiledMapRenderer mapRenderer;
        private CharacterRenderer characterRenderer;
        public MovementTiles movements;
        private InputListenerComponent listeners;
        private SwitchComponent switcher;
        private ActionsComponent actions;
        private EffectsComponent effects;
        private AbilitiesComponent abilities;
        private Options _options;

        public GameProcessScreen(AskalhornGame game, GameProcess gameProcess)
            : base(game)
        {
            this.game = game;
            
            this.GameProcess = gameProcess;
            gameProcess.OnTurned += UpdateMovements;
            gameProcess.OnTurned += UpdateActions;
            gameProcess.OnTurned += LookAtPlayer;
            
            OpenBagImpact.OnBagOpened += bag =>
            {
                switcher.SwitchTo(new ExchangeTabComponent(this, bag, gameProcess.Player.Bag));
            };
            Location.Current.OnChange += UpdateMap;
            Location.Current.OnChange += UpdateActions;
            Location.Current.OnChange += LookAtPlayer;
            
            _options = Configuration.Options;
        }

        private void UpdateActions()
        {
            actions.Clear();

            var build = Location.Current.Location[GameProcess.Player.Position].Build;
            if (build is not null)
            {
                actions.Add(new ActionBlock
                {
                    Region = Storage.Load("guiactions", (uint)(build.Type == IBuild.Types.Chest ? 1 : 2), 0),
                    Key = _options.Keys[Options.KeyActions.Use],
                    Action = () => Location.Current.Location[GameProcess.Player.Position].Build.Impact.On((Character)GameProcess.Player)
                });
            }
            
            var characterNear = Location.Current.Location
                .FindNear(GameProcess.Player.Position, x => x is ICharacter) as ICharacter;
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
            camera.LookAt(GameProcess.Player.Position.RenderVector);
        }


        private void UpdateMap()
        {
            mapRenderer.LoadMap(Location.Current.Location.TiledMap);
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
            effects = new EffectsComponent(this, GameProcess.Player);
            GameProcess.OnTurned += effects.Update;
            Game.Components.Add(effects);
            abilities = new AbilitiesComponent(this, GameProcess.Player);
            Game.Components.Add(abilities);
            movements = new MovementTiles(GameProcess.Player);
            UpdateMovements();
        }

        private void MovePlayer(Point shift)
        {
            GameProcess.Instance.Player.Make(new MovementMove(shift));
        }


        private void MouseClick(object sender, MouseEventArgs args)
        {
            var move = movements.CheckClick(args.Position, camera.GetViewMatrix());
            GameProcess.Instance.Player.Make(move);
        }

        private void KeyRelease(object sender, KeyboardEventArgs e)
        {
            if (e.Key == _options.Keys[Options.KeyActions.Pause])
                ScreenManager.LoadScreen(new PauseScreen(game, this));
#if DEBUG
            if (e.Key == Keys.OemTilde)
                DebugConsole.Toggle();
            
            if (DebugConsole.IsExist)
                return;
#endif

            if (e.Key == _options.Keys[Options.KeyActions.TopRight])
                MovePlayer(new Point(0, -1));

            if (e.Key == _options.Keys[Options.KeyActions.BottomLeft])
                MovePlayer(new Point(0, 1));

            if (e.Key == _options.Keys[Options.KeyActions.TopLeft])
                MovePlayer(new Point(-1, 0));

            if (e.Key == _options.Keys[Options.KeyActions.BottomRight])
                MovePlayer(new Point(1, 0));

            if (e.Key == _options.Keys[Options.KeyActions.Character])
                switcher.SwitchTo<CharacterTabComponent>();

            if (e.Key == _options.Keys[Options.KeyActions.Inventory])
                switcher.SwitchTo<InventoryTabComponent>();

            if (e.Key == _options.Keys[Options.KeyActions.Journal])
                switcher.SwitchTo<JournalTabComponent>();

            if (e.Key == _options.Keys[Options.KeyActions.Abilities])
                switcher.SwitchTo<AbilitiesTabComponent>();

            if (e.Key >= Keys.D0 && e.Key <= Keys.D9)
                abilities.Run(e.Key - Keys.D0);
        }

        private void UpdateMovements()
        {
            movements.AvailableAbilities = new List<UseAbilityMove>();
            movements.AvailableMovements = GameProcess.Player.AvailableMovements;
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
            foreach (var gameObject in Location.Current.Location.GameObjects)
                gameObject.Renderer.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var matrix = camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: matrix, samplerState: SamplerState.PointClamp);
            mapRenderer.Draw(matrix);
            movements.Draw(spriteBatch, matrix);

            foreach (var build in Location.Current.Location.Builds)
                build.Renderer.Draw(spriteBatch, build.Position.RenderVector);
            
            foreach (var item in GameProcess.Characters)
                characterRenderer.Draw(spriteBatch, item);
            spriteBatch.End();
        }
    }
}
