using Askalhorn.Mechanics.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using static Askalhorn.Mechanics.ICharacter;

namespace Askalhorn.Mechanics
{
    class Character: ICharacter
    {
        public string Name { get; protected set; }

        public LevelParameter Level { get; private set; } = new LevelParameter();

        public LimitedParameter HP { get; private set; } = new LimitedParameter(100);

        ILimitedParameter ICharacter.HP => throw new System.NotImplementedException();

        int ICharacter.Energy => throw new System.NotImplementedException();

        public bool IsAlive => true;

        public Attributes<BaseAttributeTypes> Primary { get; private set; } = new Attributes<BaseAttributeTypes>();

        IAttributes<BaseAttributeTypes> ICharacter.Primary => Primary;

        public Attributes<AdditionalAttributeTypes> Secondary { get; private set; } = new Attributes<AdditionalAttributeTypes>();

        IAttributes<AdditionalAttributeTypes> ICharacter.Secondary => Secondary;

        public Character(string name)
        {
            Name = name;

            foreach (BaseAttributeTypes item in Enum.GetValues(typeof(BaseAttributeTypes)))
            {
                Primary[item] = new LinearParameter(() => Level * 5);
                Level.Changed += Primary[item].Update;
            }

            foreach (BaseAttributeTypes item in Enum.GetValues(typeof(BaseAttributeTypes)))
            {
                Primary[item] = new LinearParameter(() => Level * 5);
                Level.Changed += Primary[item].Update;
            }


            var hpParameter = new LinearParameter(() =>
                Primary[BaseAttributeTypes.Endurance] * 10 +
                Primary[BaseAttributeTypes.Strength] * 5 +
                Primary[BaseAttributeTypes.Will] * 5 +
                Primary[BaseAttributeTypes.Luck] * 2
            );
            Primary[BaseAttributeTypes.Endurance].Changed += hpParameter.Update;
            Primary[BaseAttributeTypes.Strength].Changed += hpParameter.Update;
            Primary[BaseAttributeTypes.Will].Changed += hpParameter.Update;
            Primary[BaseAttributeTypes.Luck].Changed += hpParameter.Update;
            Secondary[AdditionalAttributeTypes.MaxHealthPoint] = hpParameter;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine($"Name: {Name}");
            result.AppendLine($"Level: {Level}");
            result.AppendLine($"HP: {HP}");
            result.AppendLine(Primary.ToString());
            result.AppendLine(Secondary.ToString());
            return result.ToString();
        }
    }
}
