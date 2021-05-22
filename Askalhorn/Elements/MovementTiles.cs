using System.Collections.Generic;
using Askalhorn.Common;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Common.Maths;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MLEM.Extensions;

namespace Askalhorn.Elements
{
    public class MovementTiles
    {
        private readonly ICharacter character;
        private IEnumerable<IPosition> movements;
        private readonly Texture2D texture;
        
        public MovementTiles(ICharacter character)
        {
            this.character = character;
            this.texture = Storage.Content.Load<Texture2D>("images/movement");
        }

        public IPosition CheckClick(Point point, Matrix matrix)
        {
            var shape = new Rhombus()
            {
                Width = (int)(64 * matrix.Scale().X),
                Height = (int)(32 * matrix.Scale().Y),
            };
            
            foreach (var movement in movements)
            {
                shape.Center = Vector2.Transform(movement.RenderVector + new Vector2(0, 16), matrix);
                if (shape.Contains(Mouse.GetState().Position.ToVector2()))
                    return movement;
            }

            return null;
        }

        public void Draw(SpriteBatch batch, Matrix matrix)
        {
            this.movements = character.CanMoveTo;
            
            var shape = new Rhombus()
            {
                Width = (int)(64 * matrix.Scale().X),
                Height = (int)(32 * matrix.Scale().Y),
            };
            
            foreach (var movement in movements)
            {
                shape.Center = Vector2.Transform(movement.RenderVector + new Vector2(0, 16), matrix);
                if (shape.Contains(Mouse.GetState().Position.ToVector2()))
                    batch.Draw(texture, movement.RenderTileVector, Color.Green);
                else
                    batch.Draw(texture, movement.RenderTileVector, Color.White);
            }
        }
    }
}