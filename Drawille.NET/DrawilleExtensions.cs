using System;
using System.Collections.Generic;

namespace Drawille.NET
{
    public static class DrawilleExtensions
    {
        #region Console
        
#if NETSTANDARD1_3
        public static void DrawToConsole(this Canvas c) {
            DrawToConsole(c, Console.CursorLeft, Console.CursorTop);
        }

        public static void DrawToConsole(this Canvas c, int x, int y)
        {
            foreach (var l in c.Rows())
            {
                Console.SetCursorPosition(x, y);
                Console.Write(l);
                y++;
            }
        }
#endif
        #endregion

        #region Line

        public static void LineSet(this Canvas c, int x1, int y1, int x2, int y2)
        {
            foreach (var p in Line(x1, y1, x2, y2))
                c.Set(p.X, p.Y);
        }
        
        public static void LineUnset(this Canvas c, int x1, int y1, int x2, int y2)
        {
            foreach (var p in Line(x1, y1, x2, y2))
                c.Unset(p.X, p.Y);
        }

        public static void LineToggle(this Canvas c, int x1, int y1, int x2, int y2)
        {
            foreach (var p in Line(x1, y1, x2, y2))
                c.Toggle(p.X, p.Y);
        }
        
        private static IEnumerable<Point> Line(int x1, int y1, int x2, int y2)
        {
            var dx = Math.Abs(x1 - x2);
            var dy = -Math.Abs(y1 - y2);
            var xdir = x1 < x2 ? 1 : -1;
            var ydir = y1 < y2 ? 1 : -1;
            var err = dx + dy;

            while (x1 != x2 || y1 != y2)
            {
                yield return new Point(x1, y1);
                var err2 = 2 * err;
                if (err2 >= dy)
                {
                    err += dy;
                    x1 += xdir;
                }
                if (err2 <= dx)
                {
                    err += dx;
                    y1 += ydir;
                }
            }
            yield return new Point(x2, y2);
        }
        
        #endregion

        #region Polygon

        private static void SetPolygon(this Canvas c, int x, int y, int sides, int radius)
        {
            foreach (var p in Polygon(x, y, sides, radius))
                c.Set(p.X, p.Y);
        }

        private static void UnsetPolygon(this Canvas c, int x, int y, int sides, int radius)
        {
            foreach (var p in Polygon(x, y, sides, radius))
                c.Unset(p.X, p.Y);
        }

        private static void TogglePolygon(this Canvas c, int x, int y, int sides, int radius)
        {
            foreach (var p in Polygon(x, y, sides, radius))
                c.Toggle(p.X, p.Y);
        }

        private static IEnumerable<Point> Polygon(int x, int y, int sides, int radius)
        {
            var step = (float) (Math.PI * 2 / sides);
            var theta = 0f;
            var p1X = x + radius;
            var p1Y = y;
            for (var i = 0; i < sides; i++)
            {
                theta += step;
                var p2X = (int) (x + radius * Math.Cos(theta));
                var p2Y = (int) (y + radius * Math.Sin(theta));
                foreach (var p in Line(p1X, p1Y, p2X, p2Y))
                    yield return p;
                p1X = p2X;
                p1Y = p2Y;
            }
            foreach (var p in Line(p1X, p1Y, x + radius, y))
                yield return p;
        }

        #endregion
    }
}