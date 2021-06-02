using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Askalhorn.Common.Control;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Common.Inventory;
using Askalhorn.Common.Inventory.Items;
using Askalhorn.Common.Maths;
using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Mechanics.Abilities;
using Askalhorn.Common.Mechanics.Effects;
using Askalhorn.Common.Mechanics.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Effect = Askalhorn.Common.Mechanics.Effect;

namespace Askalhorn.Common
{
    internal class Character: ICharacter
    {
        public string Name => "Test";

        public LinearParameter<int> Strength { get; private set; } = new LinearParameter<int>(new ObservedParameter<int>(100), -5, 25);
        IAttributes<PrimaryTypes> ICharacter.Primary => Primary;
        public Attributes<PrimaryTypes> Primary { get; private set; }

        IObservedParameter<int> ICharacter.Level => Level;
        public LevelParameter Level { get; private set; } = new LevelParameter();

        ILimitedValue<IObservedParameter<int>> ICharacter.HP => HP;
        
        public ObservedLimitedValue<int> HP { get; private set; } = new ObservedLimitedValue<int>(100, 100);

        public IController Controller { get; set; }
        public Texture2D Texture { get; set; }

        IPosition ICharacter.Position => Position;
        public Position Position { get; set; }

        IReadOnlyCollection<IEffect> ICharacter.Effects => Effects;
        IBag ICharacter.Bag => Bag;
        
        public readonly Bag Bag = new Bag();

        public readonly Pool Effects;

        private static readonly List<Point> Variants = new List<Point>
        {
            new Point(0, -1),
            new Point(0, 1),
            new Point(1, 0),
            new Point(-1, 0),
        };

        public IEnumerable<IPosition> CanMoveTo => Variants
            .Where(x => new MovementMove(x).IsValid(this))
            .Select(x => (IPosition)new Position(Position.Shift(x)));

        IEnumerable<IAbility> ICharacter.Abilities => Abilities;

        public List<IAbility> Abilities { get; set; } = new List<IAbility>()
        {
            new FireBall()
                { }
        };
        
        public Character()
        {
            var attrs = new Dictionary<PrimaryTypes, LinearParameter<int>>();
            foreach (var type in (PrimaryTypes[]) Enum.GetValues(typeof(PrimaryTypes)))
            {
                var parameter = new FunctionParameter<int>(() => Level.Value * 5);
                Level.Changed += parameter.Update;
                attrs[type] = new LinearParameter<int>(parameter);
            }

            Primary = new Attributes<PrimaryTypes>(attrs);
            
            Effects = new Pool(this);
            Bag.Put(new PoisonPoition(10, 9));
        }

        public void Turn()
        {
            Effects.Turn();
        }
    }
}