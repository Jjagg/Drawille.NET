using System;

namespace Drawille.NET
{
    public class Turtle
    {
        private readonly Canvas _canvas;

        public bool PenDown { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public float Rotation { get; private set; }

        public Turtle(Canvas canvas)
        {
            _canvas = canvas;
        }

        public Turtle Up()
        {
            PenDown = false;
            return this;
        }

        public Turtle Down()
        {
            PenDown = true;
            return this;
        }

        public Turtle Toggle()
        {
            PenDown = !PenDown;
            return this;
        }

        public Turtle Move(int x, int y)
        {
            if (PenDown)
                _canvas.LineSet(X, Y, x, y);
            X = x;
            Y = y;
            return this;
        }

        public Turtle Forward(float step)
        {
            var tx = (int) (X + Math.Cos(Rotation) * step);
            var ty = (int) (Y + Math.Sin(Rotation) * step);
            Move(tx, ty);
            return this;
        }

        public Turtle Back(float step)
        {
            return Forward(-step);
        }

        public Turtle Right(float angle)
        {
            Rotation += angle;
            return this;
        }

        public Turtle Left(float angle)
        {
            Rotation -= angle;
            return this;
        }

    }
}