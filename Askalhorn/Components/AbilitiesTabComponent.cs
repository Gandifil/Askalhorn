using System;
using AmbrosiaGame.Screens;
using Askalhorn.UI.Abilities;
using Microsoft.Xna.Framework;
using MLEM.Ui;

namespace Askalhorn.Components
{
    public class AbilitiesTabComponent: IGameComponent, IDisposable
    {
        private const string NAME = "abilitiesTab";
        private readonly GameProcessScreen screen;

        public AbilitiesTabComponent(GameProcessScreen screen)
        {
            this.screen = screen;
        }
        
        public void Initialize()
        {
            screen.Game.UiSystem.Add(NAME, new AbilitiesWindow(Anchor.CenterLeft, 0.45f, 0.9f));
        }

        public void Dispose()
        {
            screen.Game.UiSystem.Remove(NAME);
        }
        
    }
}