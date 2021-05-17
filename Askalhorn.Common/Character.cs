using System.Collections.Generic;
using System.Linq;
using Askalhorn.Common.Control;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Common.Maths;
using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Mechanics.Abilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common
{
    internal class Character: ICharacter
    {
        public string Name => "Test";
        public IObservedParameter<uint> HP { get;  set;}
        public IObservedParameter<uint> MaxHP { get;  set; }

        public IController Controller { get; set; }
        public Texture2D Texture { get; set; }

        IPosition ICharacter.Position => Position;
        public Position Position { get; set; }
        
        private static readonly List<Point> Variants = new List<Point>
        {
            new Point(0, -1),
            new Point(0, 1),
            new Point(1, 0),
            new Point(-1, 0),
        };

        public IEnumerable<IPosition> CanMoveTo => Variants.Select(x =>
            new Position()
            {
                Point = Position.Point + x,
            });

        IEnumerable<IAbility> ICharacter.Abilities => Abilities;

        public List<IAbility> Abilities { get; set; } = new List<IAbility>()
        {
            new FireBall()
                { }
        };
    }
}