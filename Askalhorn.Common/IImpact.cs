using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;

namespace Askalhorn.Common
{
    public interface IImpact
    {
        [JsonIgnore]
        string Description { get; }
        
        [JsonIgnore]
        TextureRegion2D  TextureRegion { get; }
        
        void On(object target);
    }
}