using System.Collections.Generic;

namespace Askalhorn.Characters.Effects
{
    public interface IEffectPool
    {
        IReadOnlyCollection<EffectBind> Binds { get; }
    }
}