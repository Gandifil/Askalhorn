using Askalhorn.Characters;
using Microsoft.Xna.Framework;
using MLEM.Misc;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.Abilities
{
    public class AbilitySkillProgressBar: ProgressBar
    {
        private readonly IAbility _ability;
        
        public AbilitySkillProgressBar(IAbility ability, Anchor anchor, float width, float height): 
            base(anchor, new Vector2(width, height), Direction2.Right, ability.MaxSkill, ability.Skill)
        {
            _ability = ability;
            _ability.OnChanged += SyncSkillValue;
        }

        private void SyncSkillValue()
        {
            CurrentValue = _ability.Skill;
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _ability.OnChanged -= SyncSkillValue;
        }
    }
}