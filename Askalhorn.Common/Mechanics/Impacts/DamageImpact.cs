using System;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using Serilog;

namespace Askalhorn.Common.Mechanics.Impacts
{
    public class DamageImpact: IImpact
    {
        public readonly int Value;

        public DamageImpact(int value)
        {
            this.Value = value;
        }
        public string Description => $"Нанесение {Value} единиц урона";
        public TextureRegion2D TextureRegion => Storage.Load("effects", 1, 0);
        
        public void On(Character character)
        {
            character.HP.Current.Value -= Value;
            Log.Information("{Name} получил {Value} единиц урона", character.Name, Value);
        }
    }
}