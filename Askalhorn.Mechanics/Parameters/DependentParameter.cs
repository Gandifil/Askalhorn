using BaseParameterFunction = System.Func<int>;

namespace Askalhorn.Mechanics.Parameters
{
    public class DependentParameter : Parameter
    {
        public BaseParameterFunction F { get; private set; }

        public DependentParameter(BaseParameterFunction F)
        {
            this.F = F;
        }

        public virtual void Update()
        {
            int? value = F?.Invoke();
            Value = value ?? 0;
        }
    }
}
