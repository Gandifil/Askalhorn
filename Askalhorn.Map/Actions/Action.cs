using Askalhorn.Common;
using Askalhorn.Render;
using Askalhorn.Text;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Map.Actions
{
    public abstract class Action: IAction
    {
        public TextureRenderer Renderer { get; set; }

        public TextPointer Name { get; set; }

        string IIcon.TooltipText => Name.ToString();
        
        public event System.Action OnChanged;

        public IImpact Impact { get; set; }
        
        public abstract bool IsOnlyOneCell { get; }
    }
}