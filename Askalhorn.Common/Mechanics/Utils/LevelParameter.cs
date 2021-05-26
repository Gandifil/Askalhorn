using System;

namespace Askalhorn.Common.Mechanics.Utils
{
    public class LevelParameter : ObservedParameter<uint>
    {
        int energy;

        public LevelParameter(uint level = 0)
            :base(level)
        {
            this.energy = 0;
        }

        public void AddEnergy(int bonus)
        {
            energy += bonus;

            uint append = 0;
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
