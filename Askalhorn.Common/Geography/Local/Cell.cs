namespace Askalhorn.Common.Geography.Local
{
    class Cell: ICell
    {
        public bool IsWall => false;
        
        public IBuild Build { get; set; }
    }
}
