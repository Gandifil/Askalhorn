using Askalhorn.Map.Actions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MLEM.Ui;

namespace Askalhorn.UI.Actions
{
    public class ActionsViewer: FixPanel
    {
        public ActionsViewer(Anchor anchor = Anchor.Center, float width = .1f, float height = .05f): 
            base(anchor, width, height, false, 8)
        {
            IsHidden = true;
            PositionOffset = new Vector2(0, 100);
        }

        public void Add(Keys key, IAction action)
        {
            AddChild(new ActionViwer(action, key, Anchor.Center,-1f, 1f));
            IsHidden = false;
        }
        
        public void Clear()
        {
            RemoveChildren();
            IsHidden = true;
        }
    }
}