using Askalhorn.Common;
using Askalhorn.Render;
using Newtonsoft.Json;

namespace Askalhorn.Characters.Effects
{
    public interface IEffect: IIcon
    {
        void Turn(Character character);
        
        void Subscribe(Character character){}
        
        void Unsubscribe(Character character){}
        
        [JsonIgnore]
        string Description { get; }
    }
}