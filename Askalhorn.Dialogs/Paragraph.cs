using Askalhorn.Common;
using Askalhorn.Render;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Dialogs
{
    public class Paragraph
    {
        public string Text { get; set; }

        public uint ShowMilliseconds { get; set; } = 5000;

        public TextureRegion2D Texture { get; set; }

        public IImpact Impact { get; set; }
        
        public TextureRenderer Renderer { get; set; }
    }
}