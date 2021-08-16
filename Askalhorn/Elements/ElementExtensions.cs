using Microsoft.Xna.Framework;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;

namespace Askalhorn.Elements
{
    public static class ElementExtensions
    {
        public static void RecursiveDispose(this Element element)
        {
            foreach (var child in element.GetChildren(x => true))
                child.RecursiveDispose();
            
            element.Dispose();
        }
        
        public static void ToggleToSelectedState(this Image element)
        {
            element.Color = new StyleProp<Color>(Color.Gray);
            
        }

        public static void ToggleToUnselectedState(this Image element)
        {
            element.Color = new StyleProp<Color>();
        }
    }
}