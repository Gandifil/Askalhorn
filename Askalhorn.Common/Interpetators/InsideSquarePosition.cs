using System;
using Askalhorn.Common.Geography;
using Askalhorn.Common.Geography.Local;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace Askalhorn.Common.Interpetators
{
    public class InsideSquarePosition: IExpression<Position>
    {
        public Rectangle? Rect { get; }
        
        [JsonConstructor]
        public InsideSquarePosition(Rectangle? rect)
        {
            Rect = rect;
        }
        
        public Position Generate(ExpressionArgs args)
        {
            if (args is null)
                throw new ArgumentNullException("Args can't be null");
            
            if (args.Random is null)
                throw new ArgumentNullException("Args.Random can't be null");
            
            if (args.Location is null)
                throw new ArgumentNullException("Args.Location can't be null both");
            
            return Calculate(args.Random, args.Location, Rect ?? DefaultRect(args));
        }

        private Position Calculate(Random random, Location location, Rectangle place)
        {
            var x = random.Next(place.Left, place.Right);
            var y = random.Next(place.Top, place.Bottom);

            var point = new Point(x, y);
            if (location.FreeForBuild(point) && World.Instance.Find(new Position(point)) == null)
                return new Position(point);
            else
                return Calculate(random, location, place);
        }

        private Rectangle DefaultRect(ExpressionArgs args)
        {
            return new Rectangle(0, 0, args.Location.TiledMap.Width, args.Location.TiledMap.Height);
        }
    }
}