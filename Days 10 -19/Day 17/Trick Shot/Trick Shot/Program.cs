using System;
using System.Linq;

namespace Trick_Shot
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Days 10 -19\Day 17\Trick Shot\input.txt")[0];

            string[] coords = line.Split(": ")[1].Split(", ");

            int[] xRange = coords[0].Substring(2).Split("..").Select(s => int.Parse(s)).ToArray();
            int[] yRange = coords[1].Substring(2).Split("..").Select(s => int.Parse(s)).ToArray();

            int hits = 0;

            int maxY = Math.Abs(yRange[1] * 2);
            for (int x = 1; x <= xRange[1]; x++)
            {
                for (int y = yRange[0]; y <= maxY; y++)
                {
                    (int high, bool insideArea) = simulateArrow(x, y, xRange, yRange);

                    if (insideArea)
                    {
                        hits++;
                    }
                }
            }

            Console.WriteLine(hits);
        }

        static (int, bool) simulateArrow(int x, int y, int[] xRange, int[] yRange)
        {
            (int, int) pos = (0, 0);

            int highest = 0;

            while (pos.Item1 <= xRange[1] && pos.Item2 >= yRange[0])
            {
                (int prevX, int prevY) = pos;

                pos = (prevX + x, prevY + y);
                if (pos.Item2 > highest)
                    highest = pos.Item2;

                x -= Math.Sign(x); // Drag
                y -= 1; // Gravity

                // If the arrow made it inside of the goal
                if ((xRange[0] <= pos.Item1 && pos.Item1 <= xRange[1]) && (yRange[0] <= pos.Item2 && pos.Item2 <= yRange[1]))
                    return (highest, true);

            }

            return (highest, false);
        }
    }
}
