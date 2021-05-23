using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common.Mechanics
{
    public interface IAbility
    {
        string Name { get; }

        string Description{ get; }

        Texture2D Icon { get; }

        internal void Use(Character character, Character target);
    }
}