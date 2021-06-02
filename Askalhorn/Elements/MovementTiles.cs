﻿using System.Collections.Generic;
using Askalhorn.Common;
using Askalhorn.Common.Control;
using Askalhorn.Common.Control.Moves;
using Askalhorn.Common.Geography.Local;
using Askalhorn.Common.Maths;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MLEM.Extensions;
using Serilog;

namespace Askalhorn.Elements
{
    public class MovementTiles
    {
        private readonly ICharacter character;
        public IEnumerable<MovementMove> AvailableMovements { get; set; } = new List<MovementMove>();
        private readonly Texture2D texture;
        
        public MovementTiles(ICharacter character)
        {
            this.character = character;
            this.texture = Storage.Content.Load<Texture2D>("images/selection");
        }

        public IMove CheckClick(Point point, Matrix matrix)
        {
            var f = Vector2.Transform(point.ToVector2(), Matrix.Invert(matrix));
            var position = Vectors.Detransform(f);
            
            Log.Information("{position}",  position);
            
            foreach (var movement in AvailableMovements)
                if (character.Position.Shift(movement.Offset).Point == position )
                    return movement;

            return null;
        }

        public void Draw(SpriteBatch batch, Matrix matrix)
        {
            var f = Vector2.Transform(Mouse.GetState().Position.ToVector2(), Matrix.Invert(matrix));
            var point = Vectors.Detransform(f);
            
            batch.Draw(texture, Vectors.Transform(point) - Vectors.SmallOrigin, Color.Black); 
            
            foreach (var movement in AvailableMovements)
                batch.Draw(texture, 
                    character.Position.Shift(movement.Offset).RenderVector - Vectors.SmallOrigin, 
                    Color.LightGreen);
        }
    }
}