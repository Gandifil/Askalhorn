using System;
using BaseParameterFunction = System.Func<int>;

namespace Askalhorn.Utils
{
    public class FunctionParameter<T>: DependentParameter<T> where T: IEquatable<T>
    {
        private readonly System.Func<T> f;
        
        public FunctionParameter(System.Func<T> f)
      //      :base((T)(object)0)
        {
            this.f = f;
            Update();
        }
        
        protected override T generate()
        {
            return f();
        }

    }
}