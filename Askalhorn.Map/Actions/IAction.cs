using Askalhorn.Common;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Map.Actions
{
    public interface IAction
    {
        public TextureRegion2D Texture { get; }

        public string Name { get; }

        public IImpact Impact { get; }

        public bool IsOnlyOneCell { get; }
    }
}