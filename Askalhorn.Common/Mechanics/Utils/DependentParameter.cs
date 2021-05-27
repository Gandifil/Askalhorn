using System;

namespace Askalhorn.Common.Mechanics.Utils
{
    abstract public class DependentParameter<T> : ObservedParameter<T> where T: IEquatable<T>
    {
        public DependentParameter(T x) : base(x)
        {
        }
        
        public void Update()
        {
            Value = generate();
        }

        protected abstract T generate();
    }
}
