using Askalhorn.Elements;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Screens;

namespace Askalhorn.Screens.Configuration
{
    public class GraphicsSettings: BackScreenBase
    {
        private readonly AskalhornGame _game;
        private readonly Menu _menu;
        
        public GraphicsSettings(AskalhornGame game, GameScreen backScreen) : base(game, backScreen)
        {
            _game = game;
            _menu = new Menu(game.UiSystem);
        }

        public override void Initialize() 
        { 
            _menu.Add(new Checkbox(Anchor.AutoCenter, new Vector2(0.5f, 40), "Полноэкранный режим", true));
            _menu.AddButton("Назад", Back);
            _menu.Initialize();
        }

        public override void Dispose()
        {
            _menu.Dispose();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
        }
    }
}