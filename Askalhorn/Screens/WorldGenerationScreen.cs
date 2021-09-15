using System;
using Askalhorn.World;
using Askalhorn.World.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Font;
using MLEM.Misc;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;
using MonoGame.Extended.Screens;

namespace Askalhorn.Screens
{
    public class WorldGenerationScreen : GameScreen
    {
        public AskalhornGame game;
        private Texture2D texture;
        private SpriteBatch spriteBatch;
        private IMapProvider mapProvider = new PerlinNoise(8, 1024, 1024);
        private IMapRenderer mapRenderer;
        private World.Map map;

        public WorldGenerationScreen(Game game)
            : base(game)
        {
            this.game = (AskalhornGame)game; 
        }

        public override void Initialize()
        {
            base.Initialize();

            spriteBatch = new SpriteBatch(GraphicsDevice);
            mapRenderer = new LevelMapRenderer(GraphicsDevice);
        }

        public override void LoadContent() {
            RegenerateMap();
            
            var box = new Panel(Anchor.Center, new Vector2(0.75f, 0.9f), Vector2.Zero);
            box.Padding = new Padding(new Padding(), -12.5f);
            
            var image = new Image(Anchor.Center, new Vector2(0.6F, 0.75F), new MLEM.Textures.TextureRegion(texture));
            var restartButton = new Button(Anchor.BottomCenter, new Vector2(0.4f, 0.1f), "Перестроить")
            {
                OnPressed = _ => RegenerateMap()
            };
            var exitButton = new Button(Anchor.BottomLeft, new Vector2(0.2f, 0.1f), "Назад")
            {
                OnPressed = _ => ScreenManager.LoadScreen(new MainMenuScreen(game)),
            };
            box.AddChild(image);
            box.AddChild(restartButton);
            box.AddChild(exitButton);
            
            var radiobox = new Panel(Anchor.CenterLeft, new Vector2(0.1f, 0.9f), Vector2.Zero);
            radiobox.Padding = new Padding(new Padding(), -12.5f);
            radiobox.AddChild(new RadioButton(Anchor.AutoLeft, new Vector2(0.05f, 0.05f), "Высота", true, "render")
            {
                OnSelected = element => SetMapRenderer(new LevelMapRenderer(GraphicsDevice))
            });
            radiobox.AddChild(new RadioButton(Anchor.AutoLeft, new Vector2(0.05f, 0.05f), "Влажность", false, "render")
            {
                OnSelected = element => SetMapRenderer(new HydroMapRenderer(GraphicsDevice))
            });
            radiobox.AddChild(new RadioButton(Anchor.AutoLeft, new Vector2(0.05f, 0.05f), "Температура", false, "render")
            {
                OnSelected = element => SetMapRenderer(new TemperatureMapRenderer(GraphicsDevice))
            });
            radiobox.AddChild(new RadioButton(Anchor.AutoLeft, new Vector2(0.05f, 0.05f), "Биомы", false, "render")
            {
                OnSelected = element => SetMapRenderer(new BiomeMapRenderer(GraphicsDevice))
            });

            
            game.UiSystem.Add("box", box);
            game.UiSystem.Add("radiobox", radiobox);
        }

        public override void UnloadContent()
        {
            game.UiSystem.Remove("box");
            game.UiSystem.Remove("radiobox");
        }

        private void RegenerateMap()
        {
            map = mapProvider.Get();
            RerenderMap();
        }

        private void RerenderMap()
        {
            mapRenderer.Draw(map, ref texture);
        }

        private void SetMapRenderer(IMapRenderer renderer)
        {
            mapRenderer = renderer;
            RerenderMap();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
        }
    }
}
