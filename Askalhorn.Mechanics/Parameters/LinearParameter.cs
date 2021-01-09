namespace Askalhorn.Mechanics.Parameters
{
    public class LinearParameter : DependentParameter, IModifiedParameter
    {
        public LinearParameter(System.Func<int> F):
            base(F)
        {
            Factor.Changed += Update;
            Shift.Changed += Update;
            Update();
        }

        public Parameter Factor { get; set; } = new Parameter();

        public Parameter Shift { get; set; } = new Parameter();

        public int BaseValue { get; private set; } = 0;

        public bool IsModified => BaseValue != Value;

        public override void Update()
        {
            BaseValue = F.Invoke();
            Value = calculate(BaseValue);
        }

        private int calculate(int pureValue)
        {
            return (100 + Factor) / 100 * pureValue + Shift;
        }

        public override string ToString()
        {
            return IsModified ? $"{Value}({BaseValue})" : Value.ToString();
        }
    }
}
