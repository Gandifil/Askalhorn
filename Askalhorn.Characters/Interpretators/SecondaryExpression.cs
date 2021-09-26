using System;
using Askalhorn.Combat;
using Askalhorn.Common.Interpetators;

namespace Askalhorn.Characters.Interpretators
{
    internal class SecondaryExpression: IExpression<float>
    {
        public SecondaryType Type { get; set; }

        public override string ToString()
        {
            return Type.ToString();
        }
        
        public float Generate(object target, Random random)
        {
            var character= target as ICharacter;
            if (character is null)
                throw new ArgumentNullException(nameof(target), "Target must be " + nameof(ICharacter));
                    
            return character.Secondary[Type].Value;
        }

    }
}