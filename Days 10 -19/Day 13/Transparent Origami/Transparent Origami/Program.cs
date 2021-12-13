using System;
using System.Collections.Generic;

namespace Transparent_Origami
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Day 13\Transparent Origami\input.txt");

            // List all folds
            List<string> folds = new List<string>();

            // Store all points, used a hashset to easily handle duplicate items later.
            HashSet<(int, int)> points = new HashSet<(int, int)>();

            foreach (string line in lines)
            {
                string[] split;

                // If it's a point
                if (line.Contains(','))
                {
                    split = line.Split(',');
                    points.Add((int.Parse(split[0]), int.Parse(split[1])));
                }

                // If it's not the white space between the points and folds
                else if (!string.IsNullOrEmpty(line))
                {
                    split = line.Split();
                    folds.Add(split[2]);
                }
            }

            foreach (string fold in folds)
            {
                string[] split = fold.Split('=');

                switch (split[0])
                {
                    case "x":
                        {
                            points = verticalFold(points, int.Parse(split[1]));
                            break;
                        }
                    case "y":
                        {
                            points = horizontalFold(points, int.Parse(split[1]));
                            break;
                        }
                }
            }
            printPoints(points);
            Console.WriteLine(points.Count);
        }

        static HashSet<(int, int)> horizontalFold(HashSet<(int, int)> points, int fold)
        {
            HashSet<(int, int)> result = new HashSet<(int, int)>();

            foreach ((int x, int y) in points)
            {
                int diff = Math.Abs(y - fold);

                result.Add((x, fold - diff));
            }

            return result;
        }

        static HashSet<(int, int)> verticalFold(HashSet<(int, int)> points, int fold)
        {
            HashSet<(int, int)> result = new HashSet<(int, int)>();

            foreach ((int x, int y) in points)
            {
                int diff = Math.Abs(x - fold);

                result.Add((fold - diff, y));
            }

            return result;
        }

        static void printPoints(HashSet<(int, int)> points)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    if (points.Contains((j, i)))
                        Console.Write("#");
                    else
                        Console.Write(".");
                }
                Console.Write("\n");
            }
        }
    }
}
