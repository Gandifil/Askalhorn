namespace Askalhorn.Mechanics.Parameters
{
    public interface IModifiedParameter
    {
        int BaseValue { get; }

        int Value { get; }

        bool IsModified { get; }
    }
}
