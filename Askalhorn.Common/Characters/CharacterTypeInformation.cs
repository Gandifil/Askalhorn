using Askalhorn.Common.Render;
using MonoGame.Extended.Serialization;

namespace Askalhorn.Common.Characters
{
    public class CharacterTypeInformation
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public TextureRenderer Renderer { get; set; }
    }
    
    public class CharacterTypeInformationReader : JsonContentTypeReader<CharacterTypeInformation>
    {
    }
}