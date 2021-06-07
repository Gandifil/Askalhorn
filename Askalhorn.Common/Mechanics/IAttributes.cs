using Askalhorn.Common.Mechanics.Utils;

namespace Askalhorn.Common.Mechanics
{
    public interface IAttributes<T>
        where T:System.Enum
    {
        ILinearParameter<int> this[T index] { get; }        
    }
}