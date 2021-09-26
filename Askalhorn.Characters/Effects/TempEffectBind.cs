namespace Askalhorn.Characters.Effects
{
    public class TempEffectBind: EffectBind
    {
        public uint TurnCount { get; private set; }
        
        public TempEffectBind(IEffect effect, uint count) : base(effect)
        {
            TurnCount = count;
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