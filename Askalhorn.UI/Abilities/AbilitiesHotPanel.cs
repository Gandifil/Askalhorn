using System;
using System.Linq;
using Askalhorn.Characters;
using Askalhorn.Core;
using Askalhorn.UI.Input;
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
            
            for (int i = 0; i < 3; i++)
                slots[i+1].Ability = character.Abilities.ElementAt(i);

            foreach (var slot in slots)
                AddChild(slot);
            
            InputListeners.Keyboard.NumericKeyReleased += OnNumericKeyReleased;
        }

        public override void Dispose()
        {
            InputListeners.Keyboard.NumericKeyReleased -= OnNumericKeyReleased;
            
            base.Dispose();
        }

        private void OnNumericKeyReleased(object? sender, int e)
        {
            if (0 <= e && e < ABILITIES_COUNT)
                slots[e].TryUse();
        }
    }
}