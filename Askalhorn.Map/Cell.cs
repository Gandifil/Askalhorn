using Askalhorn.Map.Builds;

namespace Askalhorn.Map
{
    public sealed class Cell: ICell
    {
        public bool IsWall { get; set; } = false;
        
        public IBuild Build { get; private set; }
        
        public IGameObject DynamicObject { get; private set; }

        public void Set(IGameObject obj)
        {
            var build = obj as IBuild;
            if (build is null)
                DynamicObject = obj;
            else
                Build = build;   
        }

        public void Remove(IGameObject obj)
        {
            var build = obj as IBuild;
            if (build is null)
                DynamicObject = null;
            else
                Build = null;   
        }
    }
}
