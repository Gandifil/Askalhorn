using System;
using System.Collections.Generic;
using System.Linq;
using Askalhorn.Common.Control;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Common.Inventory;
using Askalhorn.Common.Inventory.Items;
using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Mechanics.Abilities;
using Askalhorn.Common.Mechanics.Effects;
using Askalhorn.Common.Mechanics.Utils;
using Askalhorn.Common.Plot;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace Askalhorn.Common
{
    internal class Character: ICharacter
    {
        public string Name { get; set; }
        
        IAttributes<PrimaryTypes> ICharacter.Primary => Primary;
        [JsonIgnore]
        public Attributes<PrimaryTypes> Primary { get; private set; }

        IReadOnlyDictionary<PrimaryTypes, int> ICharacter.PrimaryBase => PrimaryBase;
        public Dictionary<PrimaryTypes, int> PrimaryBase { get; set; } = new()
        {
            {PrimaryTypes.Strength, 10},
            {PrimaryTypes.Agility, 5},
            {PrimaryTypes.Endurance, 10},
            {PrimaryTypes.Intelligence, 10},
            {PrimaryTypes.Willpower, 10},
            {PrimaryTypes.Luck, 1},
        };
        
        
        IAttributes<SecondaryTypes> ICharacter.Secondary => Secondary;
        [JsonIgnore]
        public Attributes<SecondaryTypes> Secondary { get; private set; }

        IReadOnlyDictionary<SecondaryTypes, int> ICharacter.SecondaryBase => SecondaryBase;
        public Dictionary<SecondaryTypes, int> SecondaryBase { get; set; } = new()
        {
            {SecondaryTypes.MaxHP, 0},
            {SecondaryTypes.RegenHP, 0},
            {SecondaryTypes.MaxMagic, 0},
            {SecondaryTypes.RegenMagic, 0},
            {SecondaryTypes.Accuracy, 0},
            {SecondaryTypes.Dodge, 0},
            {SecondaryTypes.PhysicalPower, 0},
            {SecondaryTypes.MagicPower, 0},
        };
        
        ILinearParameter<int> ICharacter.Level => Level;
        public LevelParameter Level { get; } = new()
        {
            Base = new ObservedParameter<int>(1),
        };

        ILimitedValue<IObservedParameter<int>> ICharacter.HP => HP;
        
        public ObservedLimitedValue<int> HP { get; } = new()
        {
            Current = new ObservedParameter<int>(int.MaxValue),
        };

        ILimitedValue<IObservedParameter<int>> ICharacter.MP => MP;
        
        public ObservedLimitedValue<int> MP { get; } = new()
        {
            Current = new ObservedParameter<int>(int.MaxValue),
        };

        [JsonIgnore]
        public IController Controller { get; set; }
        
        [JsonIgnore]
        public Texture2D Texture { get; set; }

        IPosition ICharacter.Position => Position;
        public Position Position { get; set; }

        IReadOnlyCollection<IEffect> ICharacter.Effects => Effects;
        public Dialog Dialog { get; set; }
        IBag ICharacter.Bag => Bag;
        
        public readonly Bag Bag = new Bag();

        [JsonIgnore]
        public readonly Pool Effects;

        private static readonly List<Point> Variants = new List<Point>
        {
            new Point(0, -1),
            new Point(0, 1),
            new Point(1, 0),
            new Point(-1, 0),
        };
        
        [JsonIgnore]
        public IEnumerable<MovementMove> AvailableMovements =>
            Variants
                .Select(x => new MovementMove(x))
                .Where(x => x.IsValid(this));
            

        IEnumerable<IAbility> ICharacter.Abilities => Abilities;

        [JsonIgnore]
        public List<IAbility> Abilities { get; set; } = new List<IAbility>()
        {
            new Strike(),
            new FireBall(),
        };
        
        public Character()
        {
            Controller = new AgressiveController
            {
                Parent = this,
            };
            SetupPrimaryRules();
            SetupSecondaryRules();
            HP.Max = Secondary[SecondaryTypes.MaxHP];
            MP.Max = Secondary[SecondaryTypes.MaxMagic];

            Effects = new Pool(this);
        }

        private void SetupPrimaryRules()
        {
            var attrs = new Dictionary<PrimaryTypes, ObservedParameter<int>>();
            foreach (var type in (PrimaryTypes[]) Enum.GetValues(typeof(PrimaryTypes)))
            {
                var parameter = new FunctionParameter<int>(() => Level.Value * 2 + PrimaryBase[type]);
                Level.Changed += parameter.Update;
                attrs[type] = parameter;
            }
            
            Primary = new Attributes<PrimaryTypes>(attrs);
        }

        private void SetupSecondaryRules()
        {
            var attrs = new Dictionary<SecondaryTypes, ObservedParameter<int>>();
            foreach (var type in (SecondaryTypes[]) Enum.GetValues(typeof(SecondaryTypes)))
            {
                FunctionParameter<int> parameter;
                switch (type)
                {
                    case SecondaryTypes.MaxHP:
                        parameter = new FunctionParameter<int>(() => 
                            Primary[PrimaryTypes.Endurance] * 20 + 
                            Primary[PrimaryTypes.Strength] * 5 +
                            SecondaryBase[type]);
                        Primary[PrimaryTypes.Endurance].Changed += parameter.Update;
                        Primary[PrimaryTypes.Strength].Changed += parameter.Update;
                        break;
                    
                    case SecondaryTypes.RegenHP:
                        parameter = new FunctionParameter<int>(() => 
                            Convert.ToInt32(Primary[PrimaryTypes.Endurance] + 
                                            Primary[PrimaryTypes.Willpower] * 0.2) +
                            SecondaryBase[type]);
                        Primary[PrimaryTypes.Endurance].Changed += parameter.Update;
                        Primary[PrimaryTypes.Willpower].Changed += parameter.Update;
                        break;
                    
                    case SecondaryTypes.MaxMagic:
                        parameter = new FunctionParameter<int>(() => 
                            Primary[PrimaryTypes.Willpower] * 10 + 
                            Primary[PrimaryTypes.Intelligence] * 2 +
                            SecondaryBase[type]);
                        Primary[PrimaryTypes.Willpower].Changed += parameter.Update;
                        Primary[PrimaryTypes.Intelligence].Changed += parameter.Update;
                        break;
                    
                    case SecondaryTypes.RegenMagic:
                        parameter = new FunctionParameter<int>(() => 
                            Convert.ToInt32(Primary[PrimaryTypes.Willpower] * 2 + 
                                            Primary[PrimaryTypes.Endurance] * 0.4) +
                            SecondaryBase[type]);
                        Primary[PrimaryTypes.Willpower].Changed += parameter.Update;
                        Primary[PrimaryTypes.Endurance].Changed += parameter.Update;
                        break;
                    
                    case SecondaryTypes.Accuracy:
                        parameter = new FunctionParameter<int>(() => 
                            Primary[PrimaryTypes.Agility] * 20 + 
                            Primary[PrimaryTypes.Strength] * 5 + 
                            Primary[PrimaryTypes.Luck] * 5 +
                            SecondaryBase[type]);
                        Primary[PrimaryTypes.Agility].Changed += parameter.Update;
                        Primary[PrimaryTypes.Strength].Changed += parameter.Update;
                        Primary[PrimaryTypes.Luck].Changed += parameter.Update;
                        break;
                    
                    case SecondaryTypes.Dodge:
                        parameter = new FunctionParameter<int>(() => 
                            Primary[PrimaryTypes.Agility] * 20 + 
                            Primary[PrimaryTypes.Endurance] * 5 + 
                            Primary[PrimaryTypes.Luck] * 5 +
                            SecondaryBase[type]);
                        Primary[PrimaryTypes.Agility].Changed += parameter.Update;
                        Primary[PrimaryTypes.Strength].Changed += parameter.Update;
                        Primary[PrimaryTypes.Luck].Changed += parameter.Update;
                        break;
                    
                    case SecondaryTypes.PhysicalPower:
                        parameter = new FunctionParameter<int>(() => 
                            Primary[PrimaryTypes.Strength] * 4 + 
                            Primary[PrimaryTypes.Endurance] * 1 +
                            SecondaryBase[type]);
                        Primary[PrimaryTypes.Agility].Changed += parameter.Update;
                        Primary[PrimaryTypes.Endurance].Changed += parameter.Update;
                        break;
                    
                    case SecondaryTypes.MagicPower:
                        parameter = new FunctionParameter<int>(() => 
                            Primary[PrimaryTypes.Intelligence] * 4 + 
                            Primary[PrimaryTypes.Willpower] * 1 +
                            SecondaryBase[type]);
                        Primary[PrimaryTypes.Intelligence].Changed += parameter.Update;
                        Primary[PrimaryTypes.Willpower].Changed += parameter.Update;
                        break;
                    
                    
                    default:
                        continue;
                }
                parameter.Changed += Level.Update;
                attrs[type] = parameter;
            }
            
            Secondary = new Attributes<SecondaryTypes>(attrs);
            
        }

        public void Turn()
        {
            Effects.Turn();
        }
    }
}