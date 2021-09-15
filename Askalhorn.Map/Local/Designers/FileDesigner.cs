using System;

namespace Askalhorn.Map.Local.Designers
{
    internal class FileDesigner: ILocationDesigner
    {
        public string Name { get; set; } = "start";
        
        public Location FormLocation(Random random, ref ILocationGenerator.CellType[,] map)
        {
            return new Location(Name);
        }
    }
}