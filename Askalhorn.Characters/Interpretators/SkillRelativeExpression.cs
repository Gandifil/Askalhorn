using System;
using Askalhorn.Common.Interpetators;

namespace Askalhorn.Characters.Interpretators
{
    internal class SkillRelativeExpression: IExpression<float>
    {
        public IAbility Ability { get; set; }

        public float Min { get; set; }

        public float Max { get; set; }


        private float _difference => Max - Min;

        private float _value => Min + _difference * Ability.Skill / Ability.MaxSkill;
        
        public override string ToString()
        {
            return _value.ToString();
        }
        
        public float Generate(object target, Random random)
        {
            return _value;
        }
    }
}