using System;
using System.Linq;
using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.UI.Journal;
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
            screen.Game.UiSystem.Add(NAME, new JournalViewer(screen.GameProcess.Player.Journal, Anchor.Center, 0.9f, 0.9f));
        }

        public void Dispose()
        {
            screen.Game.UiSystem.Remove(NAME);
        }
    }
}