using System.Reflection.Metadata;

namespace Askalhorn.Common.Inventory
{
    public interface ISlot
    {
        IItem Item { get; }

        void PutOn(IItem item);

        void TakeOff();

        void Change(IItem item);
    }
}