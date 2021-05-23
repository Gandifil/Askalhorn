namespace Askalhorn.Common.Geography.Local
{
    class Cell: ICell
    {
        public bool IsWall { get; set; } = false;
        
        public IBuild Build { get; set; }
    }
}
