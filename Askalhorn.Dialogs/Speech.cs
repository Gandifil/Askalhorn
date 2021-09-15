using Askalhorn.Render;

namespace Askalhorn.Dialogs
{
    public class Speech
    {
        public TextureRenderer Renderer { get; set; }
        
        public Paragraph[] Paragraphs { get; set; }

        public Answer[] Answers { get; set; }

        public bool IsStart { get; set; } = false;
    }
}