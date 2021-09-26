using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Askalhorn.Characters.Items;
using Askalhorn.Inventory;
using Askalhorn.Inventory.Items;

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
            _character.Costume.PutOnItem += OnPutOnItem;
            foreach (var bind in binds)
                Add(bind);
        }

        private void OnPutOnItem(Slot obj)
        {
            if ((obj.Item.InnerItem as IPuttable)?.Effect is not null)
                Add(new ItemEffectBind(obj));
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