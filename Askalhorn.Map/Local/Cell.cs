namespace Askalhorn.Map.Local
{
    public class Cell: ICell
    {
        public bool IsWall { get; set; } = false;
        
        public IBuild Build { get; set; }
    }
}
