using Askalhorn.Utils;

namespace Askalhorn.Combat
{
    public interface ICultivation
    {
        ILinearParameter<int> Level { get; }
        
        ILimitedValue<IObservedParameter<int>> Energy { get; } 
    }
}