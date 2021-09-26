using System;

namespace Askalhorn.Characters.Effects
{
    public abstract class EffectBind
    {
        public IEffect Effect { get; }
        
        public EffectBind(IEffect effect)
        {
            if (effect is null)
                throw new ArgumentException(nameof(effect));
            
            Effect = effect;
        }

        public virtual void Turn(Character character)
        {
            Effect.Turn(character);
        }

        public void Subscribe(Character character)
        {
            Effect.Subscribe(character);
        }

        public void Unsubscribe(Character character)
        {
            Effect.Unsubscribe(character);
        }

        protected void Remove()
        {
            EffectRemoved?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler EffectRemoved;
    }
}