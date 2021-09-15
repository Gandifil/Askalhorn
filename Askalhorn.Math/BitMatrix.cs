using System.Collections;

namespace Askalhorn.Math
{
    public class BitMatrix
    {
        private readonly BitArray bits;
        
        public uint Width { get; private set; }
        
        public uint Height { get; private set; }
        
        public BitMatrix(uint width, uint height)
        {
            this.Width = width;
            this.Height = height;
            this.bits = new BitArray((int)(Width * Height));
        }

        private int Index(uint x, uint y)
        {
            return (int)(x + Width * y);
        }

        public bool this[uint x, uint y]{
            get => bits[Index(x, y)];
            set => bits[Index(x, y)] = value;
        }
    }
}
