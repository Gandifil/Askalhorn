using Askalhorn.Common.Render;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Plot
{
    public class Speech
    {
        public TextureRenderer Renderer { get; set; }
        
        public Paragraph[] Paragraphs { get; set; }

        public Answer[] Answers { get; set; }

        public bool IsStart { get; set; } = false;
    }
}