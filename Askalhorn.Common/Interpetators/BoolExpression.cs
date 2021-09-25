using System;

namespace Askalhorn.Common.Interpetators
{
    public abstract class BoolExpression: IExpression<bool>
    {
        public bool IsInversed { get; set; }
        
        public bool Generate(object target, Random random)
        {
            if (IsInversed)
                return !PrivateCalculate(target, random);
            else 
                return PrivateCalculate(target, random);
        }

        protected abstract bool PrivateCalculate(object target, Random random);
    }
}