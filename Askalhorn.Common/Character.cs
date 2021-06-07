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

        ILinearParameter<int> ICharacter.Level => Level;
        public LevelParameter Level { get; } = new()
        {
            Base = new ObservedParameter<int>(1),
        };

        ILimitedValue<IObservedParameter<int>> ICharacter.HP => HP;
        
        public ObservedLimitedValue<int> HP { get; private set; } = new ObservedLimitedValue<int>(100, 100);

        [JsonIgnore]
        public IController Controller { get; set; }
        
        [JsonIgnore]
        public Texture2D Texture { get; set; }

        IPosition ICharacter.Position => Position;
        public Position Position { get; set; }

        IReadOnlyCollection<IEffect> ICharacter.Effects => Effects;
        IBag ICharacter.Bag => Bag;
        
        [JsonIgnore]
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
            new FireBall()
                { }
        };
        
        public Character()
        {
            Controller = new RandomMovementController(this);
            SetupRules();

            Effects = new Pool(this);
            Bag.Put(new PoisonPoition(10, 9));
        }

        private void SetupRules()
        {
            var attrs = new Dictionary<PrimaryTypes, ObservedParameter<int>>();
            foreach (var type in (PrimaryTypes[]) Enum.GetValues(typeof(PrimaryTypes)))
            {
                var parameter = new FunctionParameter<int>(() => Level.Value * 5);
                Level.Changed += parameter.Update;
                attrs[type] = parameter;
            }
            
            Primary = new Attributes<PrimaryTypes>(attrs);
        }

        public void Turn()
        {
            Effects.Turn();
        }
    }
}