using MLEM.Ui.Elements;

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
    }
}