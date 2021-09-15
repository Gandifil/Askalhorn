using Askalhorn.Utils;

namespace Askalhorn.Characters
{
    public interface IAttributes<T>
        where T:System.Enum
    {
        ILinearParameter<int> this[T index] { get; }        
    }
}