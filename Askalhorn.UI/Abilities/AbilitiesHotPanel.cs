using System;
using System.Linq;
using Askalhorn.Characters;
using Askalhorn.Core;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.Abilities
{
    public class AbilitiesHotPanel: InvisiblePanel
    {
        private const int ABILITIES_COUNT = 10;

        private AbilitySlotViewer[] slots = new AbilitySlotViewer[ABILITIES_COUNT];

        public AbilitiesHotPanel(Anchor anchor, float x = .6f, float y = .1f) : base(anchor, x, y)
        {
            ICharacter character = GameProcess.Instance.Player;
            SetHeightBasedOnChildren = true;
            
            for (uint i = 0; i < ABILITIES_COUNT; i++)
                slots[i] = new AbilitySlotViewer(character, i, Anchor.AutoInlineIgnoreOverflow, .1f, -1f);
            //slots[0] = new AbilitySlotViewer(character, 0, Anchor.AutoInlineIgnoreOverflow, .1f, -1f);
            
            for (int i = 0; i < 3; i++)
                slots[i+1].Ability = character.Abilities.ElementAt(i);

            foreach (var slot in slots)
                AddChild(slot);
        }
        
        public void Run(int index)
        {
            if (index < 0)
                throw new ArgumentException("Index must be greater than or equal zero");
            
            if (index >= ABILITIES_COUNT)
                throw new ArgumentException($"Index must be lower than count of ability's block ({ABILITIES_COUNT})");
            
            slots[index].TryUse();
        }
    }
}