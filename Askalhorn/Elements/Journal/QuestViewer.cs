using Askalhorn.Plot;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Elements.Journal
{
    public class QuestViewer: FixPanel
    {
        public QuestViewer(IQuest quest, Anchor anchor, float width, float height) : base(anchor, width, height)
        {
            AddChild(new Paragraph(Anchor.AutoLeft, 600, quest.Name));
            AddChild(new VerticalSpace(30));
            AddChild(new Paragraph(Anchor.AutoLeft, 600, quest.Description));
        }
    }
}