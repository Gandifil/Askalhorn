using System;
using System.Collections.Generic;
using System.Linq;
using Askalhorn.Characters.Abilities;
using Askalhorn.Characters.Actions;
using Askalhorn.Characters.Builds;
using Askalhorn.Characters.Control;
using Askalhorn.Characters.Control.Moves;
using Askalhorn.Characters.Effects;
using Askalhorn.Combat;
using Askalhorn.Dialogs;
using Askalhorn.Inventory;
using Askalhorn.Map;
using Askalhorn.Map.Actions;
using Askalhorn.Utils;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Collections;
using Newtonsoft.Json;

namespace Askalhorn.Characters
{
    public class Character: GameObject, ICharacter
    {
        public string Name { get; set; }
        public IFraction Fraction { get; protected set; }

        IAttributes<PrimaryType> ICharacter.Primary => Primary;
        [JsonIgnore]
        public Attributes<PrimaryType> Primary { get; private set; }

        IReadOnlyDictionary<PrimaryType, int> ICharacter.PrimaryBase => PrimaryBase;
        public Dictionary<PrimaryType, int> PrimaryBase { get; set; } = new()
        {
            {PrimaryType.Strength, 0},
            {PrimaryType.Agility, 0},
            {PrimaryType.Endurance, 0},
            {PrimaryType.Intelligence, 0},
            {PrimaryType.Willpower, 0},
            {PrimaryType.Luck, 0},
        };
        
        
        IAttributes<SecondaryType> ICharacter.Secondary => Secondary;
        [JsonIgnore]
        public Attributes<SecondaryType> Secondary { get; private set; }

        IReadOnlyDictionary<SecondaryType, int> ICharacter.SecondaryBase => SecondaryBase;
        public Dictionary<SecondaryType, int> SecondaryBase { get; set; } = new()
        {
            {SecondaryType.MaxHP, 0},
            {SecondaryType.RegenHP, 0},
            {SecondaryType.MaxMagic, 0},
            {SecondaryType.RegenMagic, 0},
            {SecondaryType.Accuracy, 0},
            {SecondaryType.Dodge, 0},
            {SecondaryType.PhysicalPower, 0},
            {SecondaryType.MagicPower, 0},
        };
        
        IAttributes<DamageType> ICharacter.Protection => Protection;
        [JsonIgnore]
        public Attributes<DamageType> Protection { get; private set; }

        IReadOnlyDictionary<DamageType, int> ICharacter.ProtectionBase => ProtectionBase;
        public Dictionary<DamageType, int> ProtectionBase { get; set; } = new()
        {
            {DamageType.Clear, 0},
            {DamageType.Blade, 0},
            {DamageType.Blunt, 0},
            {DamageType.Piercing, 0},
            {DamageType.Fire, 0},
            {DamageType.Poison, 0},
            {DamageType.Magic, 0},
        };
        
        ILinearParameter<int> ICharacter.Level => Level;
        public LevelParameter Level { get; } = new()
        {
            Base = new ObservedParameter<int>(1),
        };

        ILimitedValue<IObservedParameter<int>> ICharacter.HP => HP;
        
        public ObservedLimitedValue<int> HP { get; } = new(int.MaxValue);

        ILimitedValue<IObservedParameter<int>> ICharacter.MP => MP;
        
        public ObservedLimitedValue<int> MP { get; } = new(int.MaxValue);
        
        public IController Controller { get; set; }

        //IReadOnlyCollection<IEffect> ICharacter.Effects => Effects;
        IEffectPool ICharacter.EffectPool => EffectPool;

        public EffectPool EffectPool { get; }
        public Dialog Dialog { get; set; }
        public Costume Costume { get; } = new Costume();
        //
        // [JsonIgnore]
        // public readonly EffectPool Effects;

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
            

        IReadOnlyCollection<IAbility> ICharacter.Abilities => Abilities;

        [JsonIgnore]
        public List<Ability> Abilities { get; set; } = new List<Ability>()
        {
            new Strike(),
        };
        
        public Character()
        {
            SetupPrimaryRules();
            SetupSecondaryRules();
            SetupProtectionRules();
            HP.Max = Secondary[SecondaryType.MaxHP];
            //HP.Current.Value = HP.Max.Value;
            MP.Max = Secondary[SecondaryType.MaxMagic];

            EffectPool = new EffectPool(this, new List<EffectBind>());
        }

        private void SetupProtectionRules()
        {
            Protection = new Attributes<DamageType>(ProtectionBase.ToDictionary(
                x => x.Key, 
                x=> new ObservedParameter<int>(x.Value)));
        }

        private void SetupPrimaryRules()
        {
            var attrs = new Dictionary<PrimaryType, ObservedParameter<int>>();
            foreach (var type in (PrimaryType[]) Enum.GetValues(typeof(PrimaryType)))
            {
                var parameter = new FunctionParameter<int>(() => 10 + Level.Value + PrimaryBase[type]);
                Level.Changed += parameter.Update;
                attrs[type] = parameter;
            }
            
            Primary = new Attributes<PrimaryType>(attrs);
        }

        private void SetupSecondaryRules()
        {
            var attrs = new Dictionary<SecondaryType, ObservedParameter<int>>();
            foreach (var type in (SecondaryType[]) Enum.GetValues(typeof(SecondaryType)))
            {
                FunctionParameter<int> parameter;
                switch (type)
                {
                    case SecondaryType.MaxHP:
                        parameter = new FunctionParameter<int>(() => 
                            Primary[PrimaryType.Endurance] * 20 + 
                            Primary[PrimaryType.Strength] * 5 +
                            SecondaryBase[type]);
                        Primary[PrimaryType.Endurance].Changed += parameter.Update;
                        Primary[PrimaryType.Strength].Changed += parameter.Update;
                        break;
                    
                    case SecondaryType.RegenHP:
                        parameter = new FunctionParameter<int>(() => 
                            Convert.ToInt32(Primary[PrimaryType.Endurance] + 
                                            Primary[PrimaryType.Willpower] * 0.2) +
                            SecondaryBase[type]);
                        Primary[PrimaryType.Endurance].Changed += parameter.Update;
                        Primary[PrimaryType.Willpower].Changed += parameter.Update;
                        break;
                    
                    case SecondaryType.MaxMagic:
                        parameter = new FunctionParameter<int>(() => 
                            Primary[PrimaryType.Willpower] * 10 + 
                            Primary[PrimaryType.Intelligence] * 2 +
                            SecondaryBase[type]);
                        Primary[PrimaryType.Willpower].Changed += parameter.Update;
                        Primary[PrimaryType.Intelligence].Changed += parameter.Update;
                        break;
                    
                    case SecondaryType.RegenMagic:
                        parameter = new FunctionParameter<int>(() => 
                            Convert.ToInt32(Primary[PrimaryType.Willpower] * 2 + 
                                            Primary[PrimaryType.Endurance] * 0.4) +
                            SecondaryBase[type]);
                        Primary[PrimaryType.Willpower].Changed += parameter.Update;
                        Primary[PrimaryType.Endurance].Changed += parameter.Update;
                        break;
                    
                    case SecondaryType.Accuracy:
                        parameter = new FunctionParameter<int>(() => 
                            Primary[PrimaryType.Agility] * 20 + 
                            Primary[PrimaryType.Strength] * 5 + 
                            Primary[PrimaryType.Luck] * 5 +
                            SecondaryBase[type]);
                        Primary[PrimaryType.Agility].Changed += parameter.Update;
                        Primary[PrimaryType.Strength].Changed += parameter.Update;
                        Primary[PrimaryType.Luck].Changed += parameter.Update;
                        break;
                    
                    case SecondaryType.Dodge:
                        parameter = new FunctionParameter<int>(() => 
                            Primary[PrimaryType.Agility] * 20 + 
                            Primary[PrimaryType.Endurance] * 5 + 
                            Primary[PrimaryType.Luck] * 5 +
                            SecondaryBase[type]);
                        Primary[PrimaryType.Agility].Changed += parameter.Update;
                        Primary[PrimaryType.Strength].Changed += parameter.Update;
                        Primary[PrimaryType.Luck].Changed += parameter.Update;
                        break;
                    
                    case SecondaryType.PhysicalPower:
                        parameter = new FunctionParameter<int>(() => 
                            Primary[PrimaryType.Strength] * 4 + 
                            Primary[PrimaryType.Endurance] * 1 +
                            SecondaryBase[type]);
                        Primary[PrimaryType.Agility].Changed += parameter.Update;
                        Primary[PrimaryType.Endurance].Changed += parameter.Update;
                        break;
                    
                    case SecondaryType.MagicPower:
                        parameter = new FunctionParameter<int>(() => 
                            Primary[PrimaryType.Intelligence] * 4 + 
                            Primary[PrimaryType.Willpower] * 1 +
                            SecondaryBase[type]);
                        Primary[PrimaryType.Intelligence].Changed += parameter.Update;
                        Primary[PrimaryType.Willpower].Changed += parameter.Update;
                        break;
                    
                    
                    default:
                        continue;
                }
                parameter.Changed += Level.Update;
                attrs[type] = parameter;
            }
            
            Secondary = new Attributes<SecondaryType>(attrs);

            OnDisposed += o =>
            {
                Location.Current.Location.Add(new LootContainer(o as Character));
            };
        }

        public override bool IsStatic => false;

        public override void Turn()
        {
            if (HP.Current.Value <= 0)
                Dispose(); // Die!
            else
            {
                HP.Current.Value += Secondary[SecondaryType.RegenHP].Value;
                MP.Current.Value += Secondary[SecondaryType.RegenMagic].Value;
                
                EffectPool.Turn();
                foreach (var ability in Abilities)
                    ability.Turn();

                foreach (var move in Controller.Decide(this))
                    move.Make(this);
            }
        }

        private IAction _action => new SayAction(Dialog);

        public IAction Action => Dialog is null ? null : _action;
        public Bag Bag { get; } = new Bag();
    }
}