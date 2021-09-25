using System;
using Askalhorn.Render;

namespace Askalhorn.Dialogs
{
    public class  Speech
    {
        public TextureRenderer Renderer { get; set; }
        
        public Paragraph[] Paragraphs { get; set; } = Array.Empty<Paragraph>();

        public Answer[] Answers { get; set; } = Array.Empty<Answer>();

        public bool IsStart { get; set; } = false;
    }
}