using Askalhorn.Characters.Impacts;
using Askalhorn.Common;
using Askalhorn.Dialogs;
using Askalhorn.Map.Actions;
using Askalhorn.Text;

namespace Askalhorn.Characters.Actions
{
    public class SayAction: Action
    {
        public SayAction(Dialog dialog)
        {
            Texture = Storage.Load("guiactions", 0, 0);
            Name = new TextPointer("actions", "Say");
            Impact = new DialogImpact(dialog);
        }
        
        public override bool IsOnlyOneCell => false;
    }
}