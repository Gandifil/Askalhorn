using System;
using System.Linq;
using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Plot.Quests;
using Microsoft.Xna.Framework;
using MLEM.Extended.Extensions;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Components
{
    public class JournalTabComponent : IGameComponent, IDisposable
    {
        private static readonly string NAME = "journal";
        private readonly GameProcessScreen screen;

        public JournalTabComponent(GameProcessScreen screen)
        {
            this.screen = screen;
        }

        public void Initialize()
        {
            var box = new Panel(Anchor.Center, new Vector2(0.9f, 0.9f), Vector2.Zero);
            var list = new Panel(Anchor.CenterLeft, new Vector2(0.4f, 0.9f), Vector2.Zero);
            var info = new Panel(Anchor.CenterRight, new Vector2(0.4f, 0.9f), Vector2.Zero);

            foreach (var quest in screen.World.Player.Journal)
            {
                var line = new Panel(Anchor.AutoLeft, new Vector2(0.9f, 0.05f), Vector2.Zero);
                line.AddChild(new Paragraph(Anchor.CenterLeft, 600, quest.Name));
                line.OnPressed += _ =>
                {
                    OpenQuest(info, quest);
                };
                list.AddChild(line);
            }

            if (screen.World.Player.Journal.Any())
                OpenQuest(info, screen.World.Player.Journal.First());

            box.AddChild(list);
            box.AddChild(info);
            screen.game.UiSystem.Add(NAME, box);
        }

        private static void OpenQuest(Panel panel, IQuest quest)
        {
            panel.RemoveChildren();
            panel.AddChild(new Paragraph(Anchor.AutoLeft, 600, quest.Name));
            panel.AddChild(new VerticalSpace(30));
            panel.AddChild(new Paragraph(Anchor.AutoLeft, 600, quest.Description));
            panel.AddChild(new VerticalSpace(30));
        }

        public void Dispose()
        {
            screen.game.UiSystem.Remove(NAME);
        }
    }
}