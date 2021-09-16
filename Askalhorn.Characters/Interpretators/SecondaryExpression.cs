using System;
using Askalhorn.Combat;
using Askalhorn.Common.Interpetators;

namespace Askalhorn.Characters.Interpretators
{
    internal class SecondaryExpression: IExpression<float>
    {
        public SecondaryTypes Type { get; set; }

        public override string ToString()
        {
            return Type.ToString();
        }
        
        public float Generate(ExpressionArgs args)
        {
            var characterArgs = args as CharacterExpressionArgs;
            if (characterArgs is null)
                throw new ArgumentNullException(nameof(args), "Args must be CharacterExpressionArgs!");
                    
            return characterArgs.Character.Secondary[Type].Value;
        }

    }
}