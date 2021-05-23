using System;
using Askalhorn.Common.Mechanics.Utils;
using BaseParameterFunction = System.Func<int>;

namespace Askalhorn.Common.Maths
{
    public class DependentParameter<T> : ObservedParameter<T>
    {
        public Func<T> F { get; private set; }

        public DependentParameter(Func<T> F) : base(F())
        {
            this.F = F;
        }

        public void Update()
        {
            Value = F();
        }
    }
}
