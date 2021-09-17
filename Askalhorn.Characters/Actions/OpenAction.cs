using Askalhorn.Characters.Impacts;
using Askalhorn.Common;
using Askalhorn.Inventory;
using Askalhorn.Map.Actions;
using Askalhorn.Text;

namespace Askalhorn.Characters.Actions
{
    public class OpenAction: Action
    {
        public OpenAction(Bag bag)
        {
            Texture = Storage.Load("guiactions", 1, 0);
            Name = new TextPointer("actions", "Open");
            Impact = new OpenBagImpact(bag);
        }
        
        public override bool IsOnlyOneCell => true;
    }
}