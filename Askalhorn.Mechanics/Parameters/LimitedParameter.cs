namespace Askalhorn.Mechanics.Parameters
{
    class LimitedParameter : Parameter, ILimitedParameter
    {
        public LimitedParameter(int limit)
        {
            Value = limit;
            this.limit = limit;
        }

        private int limit = 0;

        public int Limit 
        {
            get
            {
                return limit;
            }
            set
            {
                Value = (Value * value) / limit;
                limit = value;
            }
        }

        public float Percent => (float)Value / Limit;

        public override string ToString()
        {
            return $"{Value}/{Limit} ({Percent})";
        }
    }
}
