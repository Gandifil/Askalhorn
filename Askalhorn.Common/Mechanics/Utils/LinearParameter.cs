using System;
using Newtonsoft.Json;

namespace Askalhorn.Common.Mechanics.Utils
{
    public class LinearParameter<T> : DependentParameter<T>, IModifiedParameter<T> where T:IEquatable<T>,IConvertible
    {
        private static readonly T zero = (T) (object) 0;
        
        public LinearParameter(ObservedParameter<T> start)
            :base(zero)
        {
            Base = start;
            
            Addition = new ObservedParameter<T>(zero);
            
            Multiplication = new ObservedParameter<int>(100);
            
            Update();

            Base.Changed += Update;
            Addition.Changed += Update;
            Multiplication.Changed += Update;
        }
        //
        // public LinearParameter(ObservedParameter<T> start, T addition, int mulltiplication = 100)
        //     :base(zero)
        // {
        //     Base = start;
        //     
        //     Addition = new ObservedParameter<T>(addition);
        //     
        //     Multiplication = new ObservedParameter<int>(mulltiplication);
        //     
        //     Update();
        //
        //     Base.Changed += Update;
        //     Addition.Changed += Update;
        //     Multiplication.Changed += Update;
        // }
        
        protected override T generate()
        {
            double k = (100 + Multiplication.Value) / 100d;
            var value = Addition.Value.ToDouble(null) + k * Base.Value.ToDouble(null);
            return (T)(object)((int)Math.Floor(value));
        }

        IObservedParameter<T> IModifiedParameter<T>.Base => Base;

        public ObservedParameter<T> Base { get; private set; }

        [JsonIgnore]
        IObservedParameter<T> IModifiedParameter<T>.Addition => Addition;
            
        [JsonIgnore]
        public ObservedParameter<T> Addition { get; private set; }
        
            
        [JsonIgnore]
        IObservedParameter<int> IModifiedParameter<T>.Multiplication => Multiplication;
        
        [JsonIgnore]
        public ObservedParameter<int> Multiplication { get; private set; }
    }
}
