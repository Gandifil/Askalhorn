using System.Linq;
using Askalhorn.Utils.Containers;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI
{
    public class GameLogViewer: FixPanel
    {
        public GameLogViewer(Anchor anchor = Anchor.BottomLeft, float width = .3f, float height = .1f): 
            base(anchor, width, height, true)
        {
            Storage.OnLineWrote += Write;
            Storage.OnLineRemoved += RemoveFirst;
        }

        public void Write(string line)
        {
            AddChild(new CustomText(Anchor.AutoLeft, (uint)(.8f*Area.Width), line));
            AddChild(new VerticalSpace(10));
            ScrollBar.ForceUpdateArea();
            ScrollBar.CurrentValue = ScrollBar.MaxValue -1;
        }

        private void RemoveFirst()
        {
            // TODO: fix trouble with removing old records
            //RemoveChild(Children.Last());
            //RemoveChild(Children.Last());
        }

        public override void Dispose()
        {
            base.Dispose();
            
            Storage.OnLineWrote -= Write;
            Storage.OnLineRemoved -= RemoveFirst;
        }

        public static IRollingList Storage = new RAMRollingList(10);
    }
}