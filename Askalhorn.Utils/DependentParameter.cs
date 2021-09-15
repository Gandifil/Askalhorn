using System;

namespace Askalhorn.Utils
{
    abstract public class DependentParameter<T> : ObservedParameter<T> where T: IEquatable<T>
    {
        public void Update()
        {
            Value = generate();
        }

        protected abstract T generate();
    }
}
