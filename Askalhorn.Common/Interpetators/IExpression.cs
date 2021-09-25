using System;

namespace Askalhorn.Common.Interpetators
{
    public interface IExpression<out T>
    {
        T Generate(object target, Random random);
    }
}