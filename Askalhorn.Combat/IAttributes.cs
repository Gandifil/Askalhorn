using Askalhorn.Utils;

namespace Askalhorn.Combat
{
    public interface IAttributes<T> where T:System.Enum
    {
        ILinearParameter<int> this[T index] { get; }        
    }
}