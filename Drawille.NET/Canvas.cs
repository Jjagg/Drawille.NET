using System;
using System.Collections.Generic;
using System.Text;

namespace Drawille.NET
{
    public class Canvas
    {
        private const int CharOffset = 0x2800;
        private static readonly int[,] PixelMap =
        {
            {0x01, 0x08},
            {0x02, 0x10},
            {0x04, 0x20},
            {0x40, 0x80}
        };

        public int Width { get; }
        public int Height { get; }

        private readonly int[] _chars;

        public Canvas(int width, int height)
        {
            if (width % 2 != 0)
                throw new ArgumentException("Width must be multiple of 2");
            if (height % 4 != 0)
                throw new ArgumentException("Height must be multiple of 4");

            Width = width;
            Height = height;
            _chars = new int[width * height / 8];
        }

        public void Clear()
        {
            for (var i = 0; i < _chars.Length; i++)
                _chars[i] = 0;
        }

        public bool Get(int x, int y)
        {
            return (_chars[Coord(x, y)] & Mask(x, y)) > 0;
        }

        public void Set(int x, int y)
        {
            _chars[Coord(x, y)] |= Mask(x, y);
        }

        public void Unset(int x, int y)
        {
            _chars[Coord(x, y)] &= ~Mask(x, y);
        }

        public void Toggle(int x, int y)
        {
            _chars[Coord(x, y)] ^= Mask(x, y);
        }

        private int Coord(int x, int y)
        {
            var nx = x / 2;
            var ny = y / 4;
            return nx + Width / 2 * ny;
        }

        private int Mask(int x, int y)
        {
            return PixelMap[y % 4, x % 2];
        }

        private static readonly StringBuilder Sb = new StringBuilder();

        public IEnumerable<string> Rows()
        {
            lock (Sb)
            {
                Sb.Clear();
                for (int i = 0, j = 0; i < _chars.Length; i++, j++)
                {
                    if (j == Width / 2)
                    {
                        yield return Sb.ToString();
                        Sb.Clear();
                        j = 0;
                    }
                    Sb.Append(_chars[i] == 0 ? ' ' : Convert.ToChar(CharOffset + _chars[i]));
                }
                if (Sb.Length > 0)
                    yield return Sb.ToString();
            }
        }
    }
}