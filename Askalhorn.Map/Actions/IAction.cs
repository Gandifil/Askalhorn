using Askalhorn.Common;
using Askalhorn.Render;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Map.Actions
{
    public interface IAction: IIcon
    {
        public IImpact Impact { get; }

        public bool IsOnlyOneCell { get; }
    }
}