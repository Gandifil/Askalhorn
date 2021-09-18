using Askalhorn.Common;
using Askalhorn.Render;
using Askalhorn.Text;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Map.Actions
{
    public abstract class Action: IAction
    {
        public TextureRegion2D Texture { get; set; }

        public TextPointer Name { get; set; }

        string IIcon.TooltipText => Name.ToString();
        
        public IImpact Impact { get; set; }
        
        public abstract bool IsOnlyOneCell { get; }
    }
}