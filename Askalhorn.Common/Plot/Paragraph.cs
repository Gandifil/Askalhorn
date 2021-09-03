using System.Net.Mime;
using Askalhorn.Common.Mechanics;
using Askalhorn.Common.Render;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Common.Plot
{
    public class Paragraph
    {
        public string Text { get; set; }

        public uint ShowMilliseconds { get; set; } = 1000;

        public TextureRegion2D Texture { get; set; }

        public IImpact Impact { get; set; }
        
        public TextureRenderer Renderer { get; set; }
    }
}