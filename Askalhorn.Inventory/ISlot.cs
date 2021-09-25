using Askalhorn.Inventory.Items;

namespace Askalhorn.Inventory
{
    public interface ISlot
    {
        IItem Item { get; }

        void PutOn(IItem item);

        void TakeOff();

        void Change(IItem item);
    }
}