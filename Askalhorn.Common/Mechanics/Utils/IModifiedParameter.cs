namespace Askalhorn.Common.Mechanics.Utils
{
    public interface IModifiedParameter<T>: IObservedParameter<T>
    {
        IObservedParameter<T> Base { get; }
        
        IObservedParameter<T> Addition { get; }
        
        IObservedParameter<int> Multiplication { get; }
    }
}