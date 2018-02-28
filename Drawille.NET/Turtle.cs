using System;

namespace Drawille.NET
{
    public class Turtle
    {
        private readonly Canvas _canvas;

        public bool PenDown { get; private set; }
        public float X { get; private set; }
        public float Y { get; private set; }
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

        public Turtle Move(float x, float y)
        {
            if (x < 0)
                x = 0;
            if (x >= _canvas.Width)
                x = _canvas.Width - 1;
            if (y < 0)
                y = 0;
            if (y >= _canvas.Height)
                y = _canvas.Height - 1;

            if (PenDown)
                _canvas.LineSet((int) X, (int) Y, (int) x, (int) y);
            X = x;
            Y = y;
            return this;
        }

        public Turtle Forward(float step)
        {
            var tx = (float) (X + Math.Cos(Rotation) * step);
            var ty = (float) (Y + Math.Sin(Rotation) * step);
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