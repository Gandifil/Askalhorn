using Askalhorn.Common.Mechanics.Utils;

namespace Askalhorn.Common.Mechanics
{
    public interface IAttributes<T>
        where T:System.Enum
    {
        IModifiedParameter<int> this[T index] { get; }        
    }
}