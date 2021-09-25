using Askalhorn.Common;
using Askalhorn.Render;
using Askalhorn.Text;

namespace Askalhorn.Map.Actions
{
    public class TransferAction: Action
    {
        public TransferAction(IImpact impact)
        {
            Renderer = new TextureRenderer("guiactions", new (2, 0));
            Name = new TextPointer("actions", "Transfer");
            Impact = impact;
        }
        
        public override bool IsOnlyOneCell => true;
    }
}