using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Askalhorn.Characters.Effects
{
    public interface IEffectPool
    {
        IReadOnlyCollection<EffectBind> Binds { get; }
    }

    public class EffectPool : IEffectPool
    {
        IReadOnlyCollection<EffectBind> IEffectPool.Binds => _binds;
        private readonly  List<EffectBind> _binds = new List<EffectBind>();
        
        private readonly Character _character;

        public EffectPool(Character character, List<EffectBind> binds) 
        {
            _character = character;
            foreach (var bind in binds)
                Add(bind);
        }

        public void Add(EffectBind bind)
        {
            bind.Subscribe(_character);
            bind.EffectRemoved += OnEffectRemoved;
            _binds.Add(bind);
        }

        private void OnEffectRemoved(object? sender, EventArgs e)
        {
            var bind = sender as EffectBind;
            bind.EffectRemoved -= OnEffectRemoved;
            bind.Unsubscribe(_character);
            _binds.Remove(bind);
        }

        public void Turn()
        {
            // on Turn() effects can dispose themself;
            for (int i = _binds.Count - 1; i >= 0; i--)
                _binds[i].Turn(_character);
        }
    }
}