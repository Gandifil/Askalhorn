using System.Collections.Generic;
using Askalhorn.Characters;
using Askalhorn.Characters.Control;
using Askalhorn.Characters.Control.Moves;
using Askalhorn.Common;
using Askalhorn.Core;
using Askalhorn.Math;
using Autofac.Core.Activators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Serilog;

namespace Askalhorn.UI
{
    public class MovementTiles
    {
        public static MovementTiles Instance = new MovementTiles();
        public IEnumerable<UseAbilityMove> AvailableAbilities { get; set; } = new List<UseAbilityMove>();

        public IEnumerable<MovementMove> AvailableMovements { get; set; } = new List<MovementMove>();
        private readonly Texture2D texture;
        
        public MovementTiles()
        {
            this.texture = Storage.Content.Load<Texture2D>("images/selection");
        }

        public IMove CheckClick(Point point, Matrix matrix)
        {
            var character = GameProcess.Instance.Player;
            var f = Vector2.Transform(point.ToVector2(), Matrix.Invert(matrix));
            var position = Vectors.Detransform(f);
            
            Log.Information("{position}",  position);
            
            foreach (var move in AvailableMovements)
                if (character.Position.Shift(move.Offset).Point == position )
                    return move;
            
            foreach (var move in AvailableAbilities)
                if (move.Target.Position.Point == position)
                    return move;

            return null;
        }

        public void Draw(SpriteBatch batch, Matrix matrix)
        {
            var character = GameProcess.Instance.Player;
            var f = Vector2.Transform(Mouse.GetState().Position.ToVector2(), Matrix.Invert(matrix));
            var point = Vectors.Detransform(f);
            
            batch.Draw(texture, Vectors.Transform(point) - Vectors.SmallOrigin, Color.Black); 
            
            foreach (var move in AvailableMovements)
                batch.Draw(texture, 
                    character.Position.Shift(move.Offset).RenderVector - Vectors.SmallOrigin, 
                    Color.LightGreen);
            
            foreach (var move in AvailableAbilities)
                batch.Draw(texture, 
                    move.Target.Position.RenderVector - Vectors.SmallOrigin, 
                    Color.Red);
        }
    }
}