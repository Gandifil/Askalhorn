﻿using System.Collections.Generic;
using System.Linq;
using Askalhorn;
using Askalhorn.Common;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Elements;
using Askalhorn.Logging;
using Askalhorn.Render;
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
        private ParticleRenderer particles;

        private GameLog log;
        private World world;

        public GameProcessScreen(AskalhornGame game)
            : base(game)
        {
            this.game = game;
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
            
            var keyboardListener = new KeyboardListener();
            var mouseListener = new MouseListener();
            Game.Components.Add(new InputListenerComponent(Game, keyboardListener, mouseListener));
            
            keyboardListener.KeyReleased += KeyRelease;
            mouseListener.MouseClicked += MouseClick;
        }

        private void MovePlayerOn(Point shift)
        {
            world.playerController.AddMove(new MovementMove(shift));
            world.Turn();
        }


        private void MouseClick(object sender, MouseEventArgs args)
        {
            var movement = movements.CheckClick(args.Position, camera.GetViewMatrix());
            if (movement is not null)
            {
                MovePlayerOn(movement.Point - world.Characters.First().Position.Point);
            }
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
                MovePlayerOn(shift.Value);
        }
        
        public override void LoadContent()
        {
            particles = new ParticleRenderer(GraphicsDevice);
            
            world = new World();
            mapRenderer.LoadMap(world.Location.TiledMap);
            movements = new MovementTiles(world.Characters.First());
            
            
            var box = new Panel(Anchor.BottomCenter, new Vector2(0.75f, 0.1f), Vector2.Zero);

            foreach (var item in world.Characters.First().Abilities)
            {
                var image = new Image(Anchor.Center, new Vector2(0.6F, 0.75F), new MLEM.Textures.TextureRegion(item.Icon));
                image.CanBeMoused = true;
                var tooltip = new Tooltip(200, item.Name + "\n" + item.Description, image);
                tooltip.MouseOffset = new Vector2(32, -64);
                tooltip.AddChild(new Image(Anchor.Center, new Vector2(0.6F, 0.75F), new MLEM.Textures.TextureRegion(item.Icon)));
                tooltip.AddChild(new Button(Anchor.BottomCenter, Vector2.Zero, "test"));
                box.AddChild(image);
            }
            game.UiSystem.Add("box", box);
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

            mapRenderer.Update(gameTime);
            particles.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var matrix = camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: matrix, samplerState: SamplerState.PointClamp);
            mapRenderer.Draw(matrix);
            movements.Draw(spriteBatch, matrix);

            particles.Draw(spriteBatch);
            
            foreach (var item in world.Characters)
                characterRenderer.Draw(spriteBatch, item);
            log.Draw();
            spriteBatch.End();
        }
    }
}
