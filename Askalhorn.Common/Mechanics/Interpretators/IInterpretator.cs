namespace Askalhorn.Common.Mechanics.Interpretators
{
    internal interface IInterpretator
    {
        string Description { get; }

        float Calculate(Character character);
    }
}