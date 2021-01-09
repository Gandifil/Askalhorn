using System;

namespace Askalhorn.Mechanics.Parameters
{
    public class LevelParameter : Parameter
    {
        int level;
        int energy;

        public LevelParameter(int level = 1)
        {
            if (level < 1)
                throw new ArgumentOutOfRangeException();

            this.level = level;
            Value = level;
            this.energy = 0;
        }

        private void Up()
        {
            level++;
            Value = level;
        }

        public void AddEnergy(int value)
        {
            if (value < 1)
                throw new ArgumentOutOfRangeException();

            energy += value;

            var limit = (int)Math.Pow(2, level + 5);
            if ( energy > limit)
            {
                energy -= limit;
                Up();
            }
        }
    }
}
