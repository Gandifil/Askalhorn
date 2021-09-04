namespace Askalhorn.Common.Interpetators
{
    public interface IExpression<out T>
    {
        T Generate(ExpressionArgs args);
    }
}