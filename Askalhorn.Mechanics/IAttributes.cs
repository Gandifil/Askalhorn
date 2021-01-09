using Askalhorn.Mechanics.Parameters;

namespace Askalhorn.Mechanics
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Enum of attribute name</typeparam>
    public interface IAttributes<T>
    {
        IModifiedParameter this[T index] { get; }
    }
}
