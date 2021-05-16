using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Maths
{
    class PerlinNoise2D
    {
        private Random _random = new Random();
        private int[] _permutation;
        private float frequency = 1f;
        private float amplitude = 1f;

        private Vector2[] _gradients;

        public PerlinNoise2D(float frequency, float amplitude)
        {
            CalculatePermutation(out _permutation);
            CalculateGradients(out _gradients);
            this.frequency = frequency;
            this.amplitude = amplitude;
        }

        private void CalculatePermutation(out int[] p)
        {
            p = Enumerable.Range(0, 256).ToArray();

            /// shuffle the array
            for (var i = 0; i < p.Length; i++)
            {
                var source = _random.Next(p.Length);

                var t = p[i];
                p[i] = p[source];
                p[source] = t;
            }
        }

        private void CalculateGradients(out Vector2[] grad)
        {
            grad = new Vector2[256];

            for (var i = 0; i < grad.Length; i++)
            {
                Vector2 gradient;

                do
                {
                    gradient = new Vector2((float)(_random.NextDouble() * 2 - 1), (float)(_random.NextDouble() * 2 - 1));
                }
                while (gradient.LengthSquared() >= 1);

                gradient.Normalize();

                grad[i] = gradient;
            }

        }

        private float Drop(float t)
        {
            t = Math.Abs(t);
            return 1f - t * t * t * (t * (t * 6 - 15) + 10);
        }

        private float Q(float u, float v)
        {
            return Drop(u) * Drop(v);
        }

        private float Noise(float x, float y)
        {
            x *= frequency;
            y *= frequency;

            var cell = new Vector2((float)Math.Floor(x), (float)Math.Floor(y));

            var total = 0f;

            var corners = new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) };

            foreach (var n in corners)
            {
                var ij = cell + n;
                var uv = new Vector2(x - ij.X, y - ij.Y);

                var index = _permutation[(int)ij.X % _permutation.Length];
                index = _permutation[(index + (int)ij.Y) % _permutation.Length];

                var grad = _gradients[index % _gradients.Length];

                total += Q(uv.X, uv.Y) * Vector2.Dot(grad, uv);
            }

            return Math.Max(Math.Min(total, 1f), -1f) * amplitude;
        }

        public float this[float x, float y] => Noise(x, y);
    }
}
