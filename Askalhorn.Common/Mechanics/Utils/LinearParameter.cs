using System;
using Newtonsoft.Json;

namespace Askalhorn.Common.Mechanics.Utils
{
    public class LinearParameter<T> : DependentParameter<T>, ILinearParameter<T> where T:IEquatable<T>, IConvertible
    {
        private static readonly T zero = (T) (object) 0;
        
        public LinearParameter()
        {
            Addition.Changed += Update;
            Multiplication.Changed += Update;
        }
        
        protected override T generate()
        {
            double k = (100 + Multiplication.Value) / 100d;
            var value = Addition.Value.ToDouble(null) + k * Base.Value.ToDouble(null);
            return (T)(object)((int)Math.Floor(value));
        }

        IObservedParameter<T> ILinearParameter<T>.Base => Base;

        private ObservedParameter<T> _base;

        public ObservedParameter<T> Base
        {
            get => _base;
            set
            {
                _base = value;
                _base.Changed += Update;
                Update();
            }
        }

        [JsonIgnore]
        IObservedParameter<T> ILinearParameter<T>.Addition => Addition;
            
        [JsonIgnore]
        public ObservedParameter<T> Addition { get; } = new ObservedParameter<T>(zero);
        
            
        [JsonIgnore]
        IObservedParameter<int> ILinearParameter<T>.Multiplication => Multiplication;
        
        [JsonIgnore]
        public ObservedParameter<int> Multiplication { get; } = new ObservedParameter<int>(0);
    }
}
