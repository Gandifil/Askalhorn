using System;
using Askalhorn.Plot;
using Askalhorn.Text;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.UI.JournalW
{
    public class QuestStateFilter: FixPanel
    {
        public QuestState State { get; set; } = QuestState.None;
        
        public QuestStateFilter(Anchor anchor, float width, float height): 
            base(anchor, width, height)
        {
            foreach (var type in (QuestState[]) Enum.GetValues(typeof(QuestState)))
            {
                var text = new TextPointer("questState", type.ToString());
                AddChild(new Button(Anchor.AutoInlineIgnoreOverflow, new Vector2(.25f, 1f), text.ToString())
                {
                    OnPressed = _ => OnPressFilter?.Invoke(type),
                });
            }
        }

        public event Action<QuestState> OnPressFilter;
    }
}