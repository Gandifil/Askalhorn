using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Screens;

namespace Askalhorn.Screens
{
    public class PauseScreen: GameScreen
    {
        public AskalhornGame game;

        private readonly GameScreen backScreen;

        public PauseScreen(Game game, GameScreen backScreen)
            : base(game)
        {
            this.game = (AskalhornGame)game;
            this.backScreen = backScreen;
        }
        
        private void Back()
        {
            ScreenManager.LoadScreen(backScreen);
        }

        private void QuickSave()
        {
            Back();
        }

        public override void LoadContent() 
        { 
            var box = new Panel(Anchor.Center, new Vector2(0.5f, 0.5f), Vector2.Zero, setHeightBasedOnChildren: true);
            
            box.AddChild(new Button(Anchor.AutoCenter, new Vector2(0.5F, 40), "Назад")
            {
                OnPressed = element => Back(),
            });
            box.AddChild(new VerticalSpace(3));
            box.AddChild(new Button(Anchor.AutoCenter, new Vector2(0.5F, 40), "Быстрое сохранение")
            {
                OnPressed = element => QuickSave(),
            });
            game.UiSystem.Add("menu", box);
        }

        public override void UnloadContent()
        {
            game.UiSystem.Remove("menu");
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
        }
        
    }
}