using System;

namespace Askalhorn.Common.Mechanics.Utils
{
    public class LinearParameter<T> : DependentParameter<T>, IModifiedParameter<T> where T:IEquatable<T>,IConvertible
    {
        // public LinearParameter(System.Func<int> F):
        //     base(F)
        // {
        //     Factor.Changed += Update;
        //     Shift.Changed += Update;
        //     Update();
        // }
        //
        // public Parameter Factor { get; set; } = new Parameter();
        //
        // public Parameter Shift { get; set; } = new Parameter();
        //
        // public int BaseValue { get; private set; } = 0;
        //
        // public bool IsModified => BaseValue != Value;
        //
        // public override void Update()
        // {
        //     BaseValue = F.Invoke();
        //     Value = calculate(BaseValue);
        // }
        //
        // private int calculate(int pureValue)
        // {
        //     return (100 + Factor) / 100 * pureValue + Shift;
        // }
        //
        // public override string ToString()
        // {
        //     return IsModified ? $"{Value}({BaseValue})" : Value.ToString();
        // }
        //
        // public IObservedParameter<T> Addition { get; }
        //
        //
        // public IObservedParameter<int> Multiplication { get; }
        
        private static readonly T zero = (T) (object) 0;
        public LinearParameter(T start, T addition, int mulltiplication = 100)
            :base(zero)
        {
            Base = new ObservedParameter<T>(start);
            
            Addition = new ObservedParameter<T>(addition);
            
            Multiplication = new ObservedParameter<int>(mulltiplication);
            
            Update();

            Base.Changed += Update;
            Addition.Changed += Update;
            Multiplication.Changed += Update;
        }
        
        protected override T generate()
        {
            double k = (100 + Multiplication.Value) / 100d;
            var value = Addition.Value.ToDouble(null) + k * Base.Value.ToDouble(null);
            return (T)(object)((int)Math.Floor(value));
        }

        IObservedParameter<T> IModifiedParameter<T>.Base => Base;

        public ObservedParameter<T> Base { get; set; }

        IObservedParameter<T> IModifiedParameter<T>.Addition => Addition;
            
        public ObservedParameter<T> Addition { get; set; }
        
            
        IObservedParameter<int> IModifiedParameter<T>.Multiplication => Multiplication;
        public ObservedParameter<int> Multiplication { get; set; }
    }
}
