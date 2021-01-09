namespace Askalhorn.Mechanics
{
    public interface ILimitedParameter
    {
        int Value { get; }

        int Limit { get; }

        float Percent { get; }
    }
}
