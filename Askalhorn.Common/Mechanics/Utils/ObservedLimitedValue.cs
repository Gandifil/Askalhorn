using System;

namespace Askalhorn.Common.Mechanics.Utils
{
    class ObservedLimitedValue<T> : ILimitedValue<IObservedParameter<T>> 
        where T:IComparable<T>, IConvertible
    {
        public IObservedParameter<T> Current { get; private set; }
        
        public IObservedParameter<T> Max { get; private set; }

        public ObservedLimitedValue(T value, T max)
        {
            Current = new ObservedParameter<T>(max);
            Current.Changed += ResetValue;
            
            Max = new ObservedParameter<T>(max);
            Max.Changed += ResetValue;
        }

        private readonly T zero = (T) (object) 0;

        private void ResetValue()
        {
            if (Current.Value.CompareTo(zero) < 0)
                Current.Value = zero;
            
            if (Current.Value.CompareTo(Max.Value) > 0)
                Current.Value = Max.Value;
        }

        public float Percent => (float)(Current.Value.ToDouble(null) / Max.Value.ToDouble(null));
    }
    //
    // class LimitedValue<T>: ILimitedValue<T> where T:IObservedParameter<D>
    // {
    //     public LimitedValue(T value, T max)
    //     {
    //         Value = value;
    //         this.max = max;
    //     }
    //     
    //     public T Value { get; }
    //
    //     private T max;
    //
    //     
    //     public T Max
    //     {
    //         get => max;
    //         set
    //         {
    //             max = value;
    //             Value.Value = (Value.Value * value) / max;
    //         }
    //     }
    //     
    //     public float Percent => (float)(Value.Value) / Max.Value;
    // }
}
