using System;
using System.Collections.Generic;
using System.Linq;
using Askalhorn.Inventory.Items;
using Newtonsoft.Json;

namespace Askalhorn.Inventory
{
    public class Bag
    {
        [JsonIgnore]
        private readonly List<Pack> _packs;

        public IReadOnlyCollection<Pack> Packs => _packs;

        [JsonConstructor]
        public Bag(List<Pack> packs = null)
        {
            _packs = packs ?? new List<Pack>();
            foreach (var pack in _packs)
                pack.OnCountSetNull += Remove;
        }

        public void Put(IItem item, uint count = 1)
        {
            var founded = Find(item);
            if (founded is null) 
                Add(new Pack(item, count));
            else
                founded.Add(count);
        }

        public void Put(Pack pack)
        {
            Put(pack.Item, pack.Count);
        }

        public IItem Pull(IItem item, uint count = 1)
        {
            var founded = Find(item);
            founded.Remove(count);
            return item;
        }

        public IItem Pull(Pack pack)
        {
            return Pull(pack.Item, pack.Count);
        }

        private void Add(Pack pack)
        {
            pack.OnCountSetNull += Remove;
            _packs.Add(pack);
            OnChanged?.Invoke();
        }

        private void Remove(Pack pack)
        {
            _packs.Remove(pack);
            OnChanged?.Invoke();
        }

        public bool Contains(IItem item, uint count = 1)
        {
            var founded = Find(item);
            return founded is null ? false : founded.Count >= count;
        }

        public void MergeTo(Bag bag)
        {
            foreach (var pack in _packs)
                bag.Put(pack.Item, pack.Count);
            _packs.Clear();
            OnChanged?.Invoke();
        }

        private Pack Find(IItem item)
        {
            return _packs.Find(x => item.Equals(x.Item));
        }

        [Newtonsoft.Json.JsonIgnore]
        public float Weight => _packs.Select(x => x.Count * x.Item.Weight).Sum();
        public event Action OnChanged;
    }
}