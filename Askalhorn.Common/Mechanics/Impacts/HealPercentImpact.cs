using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using Serilog;

namespace Askalhorn.Common.Mechanics.Impacts
{
    internal class HealPercentImpact: IImpact
    {
        public int Value { get; set; }
        
        public string Description => $"Восстанавливает {Value}% здоровья";
        
        public TextureRegion2D TextureRegion => Storage.Load("effects", 0, 0);
        
        public void On(Character character)
        {
            var increment = character.HP.Current.Value * Value / 100;
            character.HP.Current.Value += increment;
            Log.Information("{Name} восстановил {increment} единиц здоровья", character.Name, increment);
        }
    }
}