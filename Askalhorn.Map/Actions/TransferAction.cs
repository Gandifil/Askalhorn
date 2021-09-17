using Askalhorn.Common;
using Askalhorn.Text;

namespace Askalhorn.Map.Actions
{
    public class TransferAction: Action
    {
        public TransferAction(IImpact impact)
        {
            Texture = Storage.Load("guiactions", 2, 0);
            Name = new TextPointer("actions", "Transfer");
            Impact = impact;
        }
        
        public override bool IsOnlyOneCell => true;
    }
}