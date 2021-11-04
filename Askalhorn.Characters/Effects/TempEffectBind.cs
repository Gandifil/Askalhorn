using Newtonsoft.Json;

namespace Askalhorn.Characters.Effects
{
    public class TempEffectBind: EffectBind
    {
        public uint TurnCount { get; private set; }
        
        [JsonConstructor]
        public TempEffectBind(IEffect effect, uint turnCount) : base(effect)
        {
            TurnCount = turnCount;
        }

        public override void Turn(Character character)
        {
            base.Turn(character);
            
            --TurnCount;

            if (TurnCount <= 0)
                Remove();
        }
    }
}