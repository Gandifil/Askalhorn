using System;
using Askalhorn.Inventory.Items;

namespace Askalhorn.Inventory
{
    public sealed class Slot
    {
        public readonly ItemPurpose Type;
        
        public IItem Item { get; set; }

        public Slot(ItemPurpose a)
        {
            Type = a;
        }

        public event Action<Slot> OnPutOn;

        public event Action<Slot> OnTakeOff;

        public bool IsValid(IItem item)
        {
            return item is null || item.Type != Type;
        }

        public IItem PutOn(IItem item)
        {
            if (IsValid(item))
                throw new ArgumentException($"Item must be {Type}");
            
            var result = TakeOff();

            Item = item;
            OnPutOn?.Invoke(this);
                
            return result;
        }

        public IItem TakeOff()
        {
            if (Item is null)
                return null;
            else
            {
                var result = Item;
                
                Item = null;
                OnTakeOff?.Invoke(this);

                return result;
            }
        }
    }
}