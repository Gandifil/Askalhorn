namespace Askalhorn.Characters.Interpretators
{
    internal class SkillRelativeInterpretator: IInterpretator
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

        public float Calculate(Character character)
        {
            return _value;
        }
    }
}