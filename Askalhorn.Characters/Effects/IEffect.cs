using Askalhorn.Common;
using Askalhorn.Render;

namespace Askalhorn.Characters.Effects
{
    public interface IEffect: IIcon
    {
        void Turn(Character character);
        
        void Subscribe(Character character){}
        
        void Unsubscribe(Character character){}
    }
}