using System;
using AmbrosiaGame.Screens;
using Askalhorn.Common;
using Askalhorn.Common.Mechanics;
using Microsoft.Xna.Framework;
using MLEM.Extended.Extensions;
using MLEM.Misc;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Components
{
    public class AbilitiesTabComponent: IGameComponent, IDisposable
    {
        private static readonly string NAME = "abilitiesTab";
        private readonly GameProcessScreen screen;

        public AbilitiesTabComponent(GameProcessScreen screen)
        {
            this.screen = screen;
        }
        
        public void Initialize()
        {
            screen.game.UiSystem.Add(NAME, Create(screen.World.Player));
        }
        
        private static Element Create(ICharacter character)
        {
            var box = new Panel(Anchor.CenterLeft, new Vector2(0.45f, 0.9f), Vector2.Zero);
            foreach (var ability in character.Abilities)
                box.AddChild(CreateItem(ability));
            return box;
        }

        private static Element CreateItem(IAbility ability)
        {
            var box = new Panel(Anchor.AutoCenter, new Vector2(0.9f, 0.1f), Vector2.Zero);
            box.AddChild(new Image(Anchor.CenterLeft, new Vector2(-1, 1F), ability.Icon.ToMlem()));
            box.AddChild(new Paragraph(Anchor.TopRight, 500, ability.Name, true));
            box.AddChild(new ProgressBar(Anchor.BottomRight, new Vector2(0.8f, 0.5f), Direction2.Right, ability.MaxSkill, ability.Skill));
            return box;
        }

        public void Dispose()
        {
            screen.game.UiSystem.Remove(NAME);
        }
        
    }
}