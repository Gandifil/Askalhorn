using System;

namespace Askalhorn.Common.Mechanics.Utils
{
    public class LevelParameter : ObservedParameter<int>
    {
        int energy;

        public LevelParameter(int level = 0)
            :base(level)
        {
            this.energy = 0;
        }

        public void AddEnergy(int bonus)
        {
            energy += bonus;

            int append = 0;
            do
            {
                var limit = (int)Math.Pow(2, Value + append + 5);
                if (energy > limit)
                {
                    energy -= limit;
                    append++;
                }
                else break;
            } while (true);

            Value += append;
        }
    }
}
