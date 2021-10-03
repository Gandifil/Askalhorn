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
            var player = GameProcess.Instance.Player;
            SetHeightBasedOnChildren = true;

            player.HotKeys.BindingsChanged += HotKeysOnBindingsChanged;
            var abilities = player.HotKeys.GetAbilities(player);
            for (int i = 0; i < ABILITIES_COUNT; i++)
            {
                slots[i] = new AbilitySlotViewer(player, (uint)i, Anchor.AutoInlineIgnoreOverflow, .1f, -1f);
                if (abilities[i] is not null)
                    slots[i].Ability = abilities[i];
            }
            
            foreach (var slot in slots)
                AddChild(slot);
            
            InputListeners.Keyboard.NumericKeyReleased += OnNumericKeyReleased;
        }

        private void HotKeysOnBindingsChanged()
        {
            var player = GameProcess.Instance.Player;
            var abilities = player.HotKeys.GetAbilities(player);
            for (int i = 0; i < ABILITIES_COUNT; i++)
            {
                if (abilities[i] is not null)
                    slots[i].Ability = abilities[i];
            }
        }

        public override void Dispose()
        {
            var player = GameProcess.Instance.Player;
            if (player is not null)
                player.HotKeys.BindingsChanged -= HotKeysOnBindingsChanged;
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