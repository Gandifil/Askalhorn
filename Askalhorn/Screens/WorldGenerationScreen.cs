using System;
using Askalhorn.Common.Geography.World;
using Askalhorn.Common.Geography.World.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Font;
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
        private Map map;

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
            var style = new UntexturedStyle(spriteBatch) {
                Font = new GenericSpriteFont(game.Content.Load<SpriteFont>("fonts/GameLogsFont")),

            };
            game.UiSystem.Style = style;
            
            var box = new Panel(Anchor.Center, new Vector2(0.75f, 0.9f), Vector2.Zero);
            
            var image = new Image(Anchor.Center, new Vector2(0.6F, 0.75F), new MLEM.Textures.TextureRegion(texture));
            var restartButton = new Button(Anchor.BottomCenter, new Vector2(0.4f, 0.1f), "Перестроить")
            {
                OnPressed = element => RegenerateMap()
            };
            box.AddChild(image);
            box.AddChild(restartButton);
            
            var radiobox = new Panel(Anchor.CenterLeft, new Vector2(0.1f, 0.9f), Vector2.Zero);
            radiobox.AddChild(new RadioButton(Anchor.AutoLeft, new Vector2(0.05f, 0.05f), "Высота", true, "render")
            {
                OnSelected = element => SetMapRenderer<LevelMapRenderer>(),
            });
            radiobox.AddChild(new RadioButton(Anchor.AutoLeft, new Vector2(0.05f, 0.05f), "Влажность", false, "render")
            {
                OnSelected = element => SetMapRenderer<HydroMapRenderer>(),
            });
            radiobox.AddChild(new RadioButton(Anchor.AutoLeft, new Vector2(0.05f, 0.05f), "Температура", false, "render")
            {
                OnSelected = element => SetMapRenderer<TemperatureMapRenderer>(),
            });
            radiobox.AddChild(new RadioButton(Anchor.AutoLeft, new Vector2(0.05f, 0.05f), "Биомы", false, "render")
            {
                OnSelected = element => SetMapRenderer<BiomeMapRenderer>(),
            });

            
            game.UiSystem.Add("box", box);
            game.UiSystem.Add("radiobox", radiobox);
        }

        public override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            game.UiSystem.Remove("box");
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

        private void SetMapRenderer<T>() where T: IMapRenderer
        {
            mapRenderer = (IMapRenderer)Activator.CreateInstance(typeof(T), GraphicsDevice);
            RerenderMap();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.Black);

            //spriteBatch.Begin();
            //if (texture != null)
            //    spriteBatch.Draw(texture, new Rectangle(0, 0, 1024, 1024), Color.White);

            //spriteBatch.End();
        }
    }
}
