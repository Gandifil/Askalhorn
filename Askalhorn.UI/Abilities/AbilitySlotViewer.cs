using System;
using Askalhorn.Characters;
using Askalhorn.Characters.Control.Moves;
using Askalhorn.Core;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;

namespace Askalhorn.UI.Abilities
{
    public class AbilitySlotViewer: FixPanel
    {
        private readonly ICharacter _owner;
        
        private IAbility _ability;
        private AbilityViewer _abilityViewer;
        
        public IAbility Ability
        {
            get => _ability;
            set
            {
                if (_ability is not null)
                    RemoveChildren();

                _ability = value;
                _abilityViewer = new AbilityViewer(_owner, value, Anchor.Center, 1f, 1f);
                AddChild(_abilityViewer, 0);
            }
        }
        
        public AbilitySlotViewer(ICharacter owner, uint number, Anchor anchor, float width, float height): 
            base(anchor, width, height, false, 10)
        {
            _owner = owner;
            
            AddChild(new Paragraph(Anchor.BottomRight, 20, number.ToString())
            {
                TextScaleMultiplier = 0.7f,
            });
            
            DragAndDrop.OnDrop += OnDrop;
        }

        public override void Dispose()
        {
            DragAndDrop.OnDrop -= OnDrop;

            base.Dispose();
        }

        private void OnDrop(DragAndDrop obj)
        {
            if (DisplayArea.Contains(obj.PositionOffset) && obj.Icon is IAbility)
            {
                try 
                {
                    Ability = obj.Icon as IAbility;
                    obj.SuccesfullyDrop();
                }
                catch (ArgumentException e)
                {
                }
            }
        }

        public void TryUse()
        {
            if (_ability is not null)
                _abilityViewer.TryUse();
        }
    }
}