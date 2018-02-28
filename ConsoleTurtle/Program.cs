using System;
using Drawille.NET;

namespace ConsoleTurtle
{
    internal static class Program
    {
        private static char LowerLeft = '\u2596';
        private static char LowerRight = '\u2597';
        private static char UpperLeft = '\u2598';
        private static char UpperRight = '\u259D';

        private static void Main(string[] args)
        {
            var width = args.Length > 0 ? int.Parse(args[0]) : Console.WindowWidth;
            var height = args.Length > 1 ? int.Parse(args[1]) : Console.WindowHeight - 1;
            var x = args.Length > 2 ? int.Parse(args[2]) : 0;
            var y = args.Length > 3 ? int.Parse(args[3]) : 1;
            if (width > Console.WindowWidth) width = Console.WindowWidth;
            if (height > Console.WindowHeight) height = Console.WindowHeight;

            Console.Clear();
            Console.CursorVisible = false;

            var cv = new Canvas(width * 2, height * 4);
            var turtle = new Turtle(cv);

            turtle.Move(width, height * 2);
            turtle.Down();
            /*Mandela(turtle);
            cv.DrawToConsole();
            Console.CursorVisible = true;
            return;*/

            while (true)
            {
                Console.SetCursorPosition((int) (turtle.X / 2), (int) (turtle.Y / 4));
                var c = turtle.X % 2 < 0
                    ? ((turtle.Y / 2) % 2 < 0 ? UpperLeft : LowerLeft)
                    : ((turtle.Y / 2) % 2 < 0 ? UpperRight : LowerRight);
                Console.Write(c);
                var k = Console.ReadKey(true);
                if (k.Key == ConsoleKey.Q)
                    break;
                if (k.Key == ConsoleKey.UpArrow)
                    turtle.Forward(10f);
                if (k.Key == ConsoleKey.DownArrow)
                    turtle.Back(10f);
                if (k.Key == ConsoleKey.RightArrow)
                    turtle.Right((float) (Math.PI / 4));
                if (k.Key == ConsoleKey.LeftArrow)
                    turtle.Left((float) (Math.PI / 4));
                if (k.Key == ConsoleKey.D)
                    turtle.Toggle();
                cv.DrawToConsole(x, y);
            }

            Console.CursorVisible = true;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

        private static void Mandela(Turtle t)
        {

            for (var i = 0; i < 36; i++)
            {
                t.Right(10 * 0.017453292519943295769236907684886f);
                for (var j = 0; j < 36; j++)
                {
                    t.Right(10 * 0.017453292519943295769236907684886f);
                    t.Forward(8);
                }
            }
        }
    }
}
