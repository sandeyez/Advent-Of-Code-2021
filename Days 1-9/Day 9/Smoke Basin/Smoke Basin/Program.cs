using System;
using System.Collections.Generic;
using System.Linq;

namespace Smoke_Basin
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Day 9\Smoke Basin\input.txt");

            int[,] map = new int[lines[0].Length, lines.Length];

            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];
                for (int x = 0; x < lines[y].Length; x++)
                {
                    // Convert char to int
                    map[x, y] = line[x] - '0';
                }
            }

            List<int> basins= new List<int>();

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (checkIfLowest(map, i, j))
                        basins.Add(getBasinSize(map, i, j));
                }
            }
            Console.WriteLine(multiplyThreeGreatest(basins));
        }

        static bool checkIfLowest(int[,] map, int x, int y)
        {
            int[] directions = new int[] { 1, -1 };
            int value = map[x, y];

            bool lower = true;

            foreach (int direction in directions)
            {
                try
                {
                    lower &= map[x + direction, y] > value;
                }

                // If the point looked at is an edge case, it may go out of range of the array.
                catch (IndexOutOfRangeException)
                {
                    continue;
                }
            }

            foreach (int direction in directions)
            {
                try
                {
                    lower &= map[x, y + direction] > value;
                }

                // If the point looked at is an edge case, it may go out of range of the array.
                catch (IndexOutOfRangeException)
                {
                    continue;
                }
            }

            return lower;
        }

        // Saves each point that has been looked at.
        static Dictionary<(int, int), bool> inBasin = new Dictionary<(int, int), bool>();
        static int getBasinSize(int[,] map, int x, int y)
        {
            // All points we're currently looking at are at the frontier.
            List<(int, int)> frontier = new List<(int, int)> { (x, y) };
            int size = 1;

            while (frontier.Count > 0)
            {
                // Frontier for the next iteration.
                List<(int, int)> newFrontier = new List<(int, int)>();

                foreach ((int i, int j) in frontier)
                {
                    int[] directions = new int[] { 1, -1 };
                    foreach (int direction in directions)
                    {
                        try
                        {
                            int newI = i + direction;

                            // If the point hasn't been looked at, is greater than our previous point and isn't 9.
                            if (!inBasin.ContainsKey((newI, j)) && (map[newI, j] > map[i, j]) && map[newI, j] != 9)
                            {
                                size++;
                                inBasin[(newI, j)] = true;
                                newFrontier.Add((newI, j));
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                        }
                        try
                        {
                            int newJ = j + direction;

                            // If the point hasn't been looked at, is greater than our previous point and isn't 9.
                            if (!inBasin.ContainsKey((i, newJ)) && (map[i, newJ] > map[i, j]) && map[i, newJ] != 9)
                            {
                                size++;
                                inBasin[(i, newJ)] = true;
                                newFrontier.Add((i, newJ));
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                        }
                    }
                }
                frontier = newFrontier;
            }
            return size;
        }
        static int multiplyThreeGreatest(List<int> list)
        {
            List<int> descending = list.OrderByDescending(x=> x).ToList();

            return descending[0] * descending[1] * descending[2];
        }
    }
}
