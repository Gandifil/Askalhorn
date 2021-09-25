using System;
using Askalhorn.Common;
using Askalhorn.Common.Interpetators;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace Askalhorn.Map.Local
{
    public class InsideSquarePosition: IExpression<Position>
    {
        public Rectangle? Rect { get; }
        
        [JsonConstructor]
        public InsideSquarePosition(Rectangle? rect)
        {
            Rect = rect;
        }
        
        public Position Generate(object target, Random random)
        {
            var location = target as Location;
            if (location is null)
                throw new ArgumentNullException("Target must be " + nameof(Location));
            
            return Calculate(random, location, Rect ?? DefaultRect(location));
        }

        private Position Calculate(Random random, Location location, Rectangle place)
        {
            var x = random.Next(place.Left, place.Right);
            var y = random.Next(place.Top, place.Bottom);

            var point = new Point(x, y);
            if (location.FreeForBuild(point))
                return new Position(point);
            else
                return Calculate(random, location, place); 
        }

        private Rectangle DefaultRect(Location location)
        {
            return new Rectangle(0, 0, location.TiledMap.Width, location.TiledMap.Height);
        }
    }
}