using Askalhorn.Plot;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.Journal
{
    public class QuestViewer: FixPanel
    {
        public QuestViewer(IQuest quest, Anchor anchor, float width, float height) : base(anchor, width, height)
        {
            AddChild(new CustomText(Anchor.AutoLeft, .4f, quest.Name));
            AddChild(new VerticalSpace(30));
            AddChild(new CustomText(Anchor.AutoLeft, .4f, quest.Description));
        }
    }
}