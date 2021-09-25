using Askalhorn.Characters.Impacts;
using Askalhorn.Common;
using Askalhorn.Inventory;
using Askalhorn.Map.Actions;
using Askalhorn.Render;
using Askalhorn.Text;

namespace Askalhorn.Characters.Actions
{
    public class OpenAction: Action
    {
        public OpenAction(Bag bag)
        {
            Renderer = new TextureRenderer("guiactions", new (1, 0));
            Name = new TextPointer("actions", "Open");
            Impact = new OpenBagImpact(bag);
        }
        
        public override bool IsOnlyOneCell => true;
    }
}