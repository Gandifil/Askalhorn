using System;
using Newtonsoft.Json;

namespace Askalhorn.Common.Mechanics.Utils
{
    public sealed class LevelParameter : LinearParameter<int>
    {
        public int Energy { get; set; }
        //
        // [JsonConstructor]
        // public LevelParameter(ObservedParameter<int> value, int energy)
        //     :base(value)
        // {
        //     Energy = energy;
        // }

        public void AddEnergy(int bonus)
        {
            Energy += bonus;

            int append = 0;
            do
            {
                var limit = (int)Math.Pow(2, Value + append + 5);
                if (Energy > limit)
                {
                    Energy -= limit;
                    append++;
                }
                else break;
            } while (true);

            Value += append;
        }
    }
}
