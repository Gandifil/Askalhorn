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
using Askalhorn.Dialogs;
using Askalhorn.Elements;
using Askalhorn.Map;
using Askalhorn.Map.Actions;
using Askalhorn.Map.Builds;
using Askalhorn.Map.Local;
using Askalhorn.Render;
using Askalhorn.Screens;
using Askalhorn.Settings;
using Askalhorn.UI;
using Askalhorn.UI.Abilities;
using Askalhorn.UI.Actions;
using Askalhorn.UI.Dialogs;
using Askalhorn.UI.Effects;
using Askalhorn.UI.Input;
using Askalhorn.UI.Inventory;
using Askalhorn.UI.Journal;
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
using Serilog;

namespace AmbrosiaGame.Screens
{
    public class GameProcessScreen: ClearUIScreen
    {
        private SpriteBatch spriteBatch;
        private OrthographicCamera camera;
        
        public readonly GameProcess GameProcess;
        private TiledMapRenderer mapRenderer;
        private CharacterRenderer characterRenderer;
        public MovementTiles movements;
        private SwitchComponent switcher;
        private ActionsViewer actions;
        private EffectsViewer effects;
        private AbilitiesHotPanel abilities;
        private Options _options;

        public GameProcessScreen(AskalhornGame game, GameProcess gameProcess)
            : base(game)
        {
            this.GameProcess = gameProcess;
            GameProcess.OnTurned += UpdateActions;
            GameProcess.OnTurned += LookAtPlayer;
            
            OpenBagImpact.OnBagOpened += bag =>
            {
                Game.UiSystem.Add("sda1", new ExchangeWindow(bag, GameProcess.Player.Bag));
            };
            
            DialogImpact.OnDialogOpened += d =>
            {
                Game.UiSystem.Add("sda2", new DialogViewer(d, Anchor.Center, .9f, .5f));
            };
            Location.Current.OnChange += UpdateMap;
            Location.Current.OnChange += UpdateActions;
            Location.Current.OnChange += LookAtPlayer;
            
            _options = Configuration.Options;
        }

        public override void Dispose()
        {
            movements.Dispose();
            GameProcess.OnTurned -= UpdateActions;
            GameProcess.OnTurned -= LookAtPlayer;
            Location.Current.OnChange -= UpdateMap;
            Location.Current.OnChange -= UpdateActions;
            Location.Current.OnChange -= LookAtPlayer;
            
            base.Dispose();
        }

        private void UpdateActions()
        {
            actions.Clear();

            var build = Location.Current.Location[GameProcess.Player.Position].Build as IActionable;
            if (build is not null)
                if (build.Action is not null)
                    actions.Add(_options.Keys[Options.KeyActions.Use], build.Action);
            
            var characterNear = Location.Current.Location
                .FindNear(GameProcess.Player.Position, x => x is ICharacter) as IActionable;
            if (characterNear is not null)
                if (characterNear.Action is not null)
                    actions.Add(_options.Keys[Options.KeyActions.Use], characterNear.Action);
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
            
            InputListeners.Keyboard.KeyReleased += KeyRelease;
            
            Game.UiSystem.Add("GameLog", new GameLogViewer());

            movements = new MovementTiles(camera);
            
            abilities = new AbilitiesHotPanel(Anchor.BottomRight);
            Game.UiSystem.Add("Abilities", abilities);
                
            effects = new EffectsViewer(GameProcess.Player, Anchor.TopLeft, 1f, .1f);
            GameProcess.OnTurned += effects.Update;
            Game.UiSystem.Add("Effects", effects);
            
            actions = new ActionsViewer();
            Game.UiSystem.Add("Actions", actions);
            UpdateActions();
        }

        private void MovePlayer(Point shift)
        {
            GameProcess.Instance.Player.Make(new MovementMove(shift));
        }

        private void KeyRelease(object sender, KeyboardEventArgs e)
        {
            if (e.Key == _options.Keys[Options.KeyActions.Pause])
                ScreenManager.LoadScreen(new PauseScreen(Game, this));
#if DEBUG
            if (e.Key == Keys.OemTilde)
                DebugConsole.Open();
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
                Game.ElementSlot.SwitchTo(() => new CharacterWindow(GameProcess.Player));

            if (e.Key == _options.Keys[Options.KeyActions.Inventory])
                Game.ElementSlot.SwitchTo(() => new CharacterInventoryViewer(GameProcess.Player));

            if (e.Key == _options.Keys[Options.KeyActions.Journal])
                Game.ElementSlot.SwitchTo(() => new JournalViewer(GameProcess.Player.Journal, Anchor.Center, 0.9f, 0.9f));

            if (e.Key == _options.Keys[Options.KeyActions.Abilities])
                Game.ElementSlot.SwitchTo(() => new AbilitiesWindow(Anchor.CenterLeft, 0.45f, 0.9f));
        }


        public override void LoadContent()
        {
            characterRenderer = new CharacterRenderer(spriteBatch);
        }

        public override void UnloadContent()
        {
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
