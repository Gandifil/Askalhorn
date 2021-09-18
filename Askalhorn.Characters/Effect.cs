using System;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Characters
{
    public abstract class Effect: IEffect
    {
        public uint TurnCount { get; private set; }

        public Effect(uint time)
        {
            TurnCount = time;
        }

        public void Turn(Character character)
        {
            Tick(character);
            --TurnCount;
        }

        public bool IsAlive => TurnCount > 0;
        
        public virtual void Subscribe(Character character){}
        
        public virtual void Unsubscribe(Character character){}
        
        public virtual void Tick(Character character){}
        public abstract string TooltipText { get; }
        public event Action OnChanged;
        public abstract TextureRegion2D Texture { get; }
    }
}