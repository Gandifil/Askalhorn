using System;
using Askalhorn.Common.Mechanics.Utils;
using BaseParameterFunction = System.Func<int>;

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
