using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbrosiaGame.Utils
{
    public class BitMatrix
    {
        private BitArray bits;
        private int width;
        private int height;

        public BitMatrix(int w, int h)
        {
            if (w <= 0)
                throw new ArgumentException("w must be positive");

            if (h <= 0)
                throw new ArgumentException("h must be positive");

            this.width = w;
            this.height = h;
            this.bits = new BitArray(w * h);
        }

        public bool this[int x, int y] {
            get => bits[x + width * y];
            set => bits[x + width * y] = value;
        }
    }
}
