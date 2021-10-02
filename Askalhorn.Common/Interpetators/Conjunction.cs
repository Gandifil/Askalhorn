using System;
using System.Collections.Generic;

namespace Askalhorn.Common.Interpetators
{
    public class Conjunction: BoolExpression
    {
        public List<IExpression<bool>> List { get; set; }
        
        protected override bool PrivateCalculate(object target, Random random)
        {
            foreach (var expression in List)
                if (!expression.Generate(target, random))
                    return false;
            
            return true;
        }
    }
}