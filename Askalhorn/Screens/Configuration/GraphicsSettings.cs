using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Screens;

namespace Askalhorn.Screens.Configuration
{
    public class GraphicsSettings: BackScreenBase
    {
        private readonly AskalhornGame _game;
        
        public GraphicsSettings(AskalhornGame game, GameScreen backScreen) : base(game, backScreen)
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
            box.AddChild(new Checkbox(Anchor.AutoCenter, new Vector2(0.5f, 40), "Полноэкранный режим", true));
            _game.UiSystem.Add("menu12", box);
        }

        public override void UnloadContent()
        {
            _game.UiSystem.Remove("menu12");
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
        }
    }
}