﻿using System.Linq;
using Askalhorn.Characters;
using Askalhorn.Characters.Control.Moves;
using Askalhorn.Core;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;
using MonoGame.Extended;

namespace Askalhorn.UI.Abilities
{
    public class AbilityViewer: IconViewer
    {
        private readonly ICharacter _owner;

        private readonly IAbility _ability;
        private readonly UseAbilityMove _move;
        
        public AbilityViewer(ICharacter owner, IAbility ability, Anchor anchor, float width, float height): 
            base(ability, anchor, width, height)
        {
            _ability = ability;

            _move = new UseAbilityMove
            {
                Ability = _ability,
            };
            
            _owner = owner;
            _owner.MP.Current.Changed += UpdateMagicRequirement;
            
            OnDrawn += (element, time, batch, alpha) =>
            {
                if (!ability.IsReady)
                    batch.DrawCircle(element.DisplayArea.Center, 
                        64.0f * ((float)ability.CoolDownTimer / ability.CoolDown), 50, Microsoft.Xna.Framework.Color.Red);
            };

            OnPressed += TryUse;
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _owner.MP.Current.Changed -= UpdateMagicRequirement;
        }

        private void UpdateMagicRequirement()
        {
            Color = new StyleProp<Color>(!_move.IsEnoughMagic(_owner) ? 
                Microsoft.Xna.Framework.Color.Red : 
                Microsoft.Xna.Framework.Color.Transparent);
        }

        public void TryUse(Element element = null)
        {
            if (_ability.Type == IAbility.TargetType.Self)
            {
                GameProcess.Instance.Player.Make(_move);
                //screen.movements.AvailableAbilities = new List<UseAbilityMove>();
            }

            if (_ability.Type == IAbility.TargetType.Character)
            {
                MovementTiles.Instance.AvailableAbilities = GameProcess.Instance.Characters
                    .Where(x => x != _owner && x.Position.IsInside(_owner.Position, _ability.Radius))
                    .Select(x =>
                        new UseAbilityMove()
                        {
                            Ability = _ability,
                            Target = x,
                        });
            }
        }
    }
}