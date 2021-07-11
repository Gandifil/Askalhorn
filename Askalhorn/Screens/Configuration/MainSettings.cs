using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Screens;

namespace Askalhorn.Screens.Configuration
{
    public class MainSettings: BackScreenBase
    {
        private readonly AskalhornGame _game;
        
        public MainSettings(AskalhornGame game, GameScreen backScreen) : base(game, backScreen)
        {
            _game = game;
        }

        public override void LoadContent() 
        { 
            var box = new Panel(Anchor.Center, new Vector2(0.5f, 0.5f), Vector2.Zero, setHeightBasedOnChildren: true);
            
            box.AddChild(new Button(Anchor.AutoCenter, new Vector2(0.5F, 40), "Назад")
            {
                OnPressed = _ => Back(),
            });
            box.AddChild(new VerticalSpace(3));
            box.AddChild(new Button(Anchor.AutoCenter, new Vector2(0.5F, 40), "Графика")
            {
                OnPressed = _ => ScreenManager.LoadScreen(new GraphicsSettings(_game, this)),
            });
            box.AddChild(new VerticalSpace(3));
            box.AddChild(new Button(Anchor.AutoCenter, new Vector2(0.5F, 40), "Выход")
            {
                OnPressed = _ => _game.Exit(),
            });
            _game.UiSystem.Add("menu3", box);
        }

        public override void UnloadContent()
        {
            _game.UiSystem.Remove("menu3");
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
        }
    }
}