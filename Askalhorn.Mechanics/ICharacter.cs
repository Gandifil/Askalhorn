using System.Collections.Generic;

namespace Askalhorn.Mechanics
{
    public interface ICharacter
    {
        public enum BaseAttributeTypes
        {
            Strength,
            Endurance,
            Agility,
            Perception,
            Will,
            Intelligence,
            Luck,
        }

        public enum AdditionalAttributeTypes
        {
            MaxHealthPoint,
            MaxStamina,
            StrikePower,
            SpellPower,
            Defence,
            Accuracy,
            Avoidance,
        }

        string Name { get; }

        ILimitedParameter HP { get; }

        int Energy { get; }

        bool IsAlive { get; }

        IAttributes<BaseAttributeTypes> Primary { get; }
        IAttributes<AdditionalAttributeTypes> Secondary { get; }

    }
}
