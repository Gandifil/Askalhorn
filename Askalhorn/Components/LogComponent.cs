using System;
using System.Resources;
using Askalhorn.Elements;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.Screens;
using Newtonsoft.Json;

namespace Askalhorn.Components
{
    public class LogComponent:  IGameComponent, IDisposable
    {
        private readonly AskalhornGame game;
        
        public LogComponent(AskalhornGame game)
        {
            this.game = game;
            
            box = new FixPanel(Anchor.BottomLeft, 0.3f, 0.1f,true);
        }

        private static Panel box;

        public static void Write(string line)
        {
            if (box is not null)
            {
                box.AddChild(new Paragraph(Anchor.AutoLeft, box.Area.Width - 50, line));
                box.AddChild(new VerticalSpace(10));
                box.ScrollBar.ForceUpdateArea();
                box.ScrollBar.CurrentValue = box.ScrollBar.MaxValue -1;
            }
        }
        
        public void Initialize()
        {
            game.UiSystem.Add("LogComponent", box);
        }

        public void Dispose()
        {
            game.UiSystem.Remove("LogComponent");
        }
    }
}