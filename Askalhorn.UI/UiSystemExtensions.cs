using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Ui;

namespace Askalhorn.UI
{
    public static class UiSystemExtensions
    {
        public static void Clear(this UiSystem system)
        {
            foreach (var root in system.GetRootElements())
                system.Remove(root.Name);
        }
        
    }
}