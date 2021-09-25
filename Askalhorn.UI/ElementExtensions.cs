using Microsoft.Xna.Framework;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;

namespace Askalhorn.UI
{
    public static class ElementExtensions
    {
        public static void RecursiveDisposeChildren(this Element element)
        {
            foreach (var child in element.GetChildren(x => true))
                child.RecursiveDispose();
        }
        
        public static void RecursiveDispose(this Element element)
        {
            element.RecursiveDisposeChildren();
            
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

        public static void Close(this Element element)
        {
            if (element.Root.Element == element)
                element.System.Remove(element.Root.Name);
            else
                element.Parent.RemoveChild(element);
        }
    }
}