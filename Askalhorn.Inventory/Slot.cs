using System;
using Askalhorn.Inventory.Items;

namespace Askalhorn.Inventory
{
    public sealed class Slot
    {
        public readonly ItemPurpose Type;
        
        public IItem Item { get; private set; }

        public Slot(ItemPurpose a)
        {
            Type = a;
        }

        public event Action<IItem> OnPutOn;

        public event Action<IItem> OnTakeOff;

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
            OnPutOn?.Invoke(item);
                
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
                OnTakeOff?.Invoke(result);

                return result;
            }
        }
    }
}