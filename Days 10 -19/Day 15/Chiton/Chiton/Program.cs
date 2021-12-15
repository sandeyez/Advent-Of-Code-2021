using System;
using System.Collections.Generic;

namespace Chiton
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Days 10 -19\Day 15\Chiton\input.txt");

            int horLength = lines[0].Length;
            int verLength = lines.Length;

            // Store all risks
            int[,] map = new int[horLength * 5, verLength * 5];

            // Fill the initial array
            for (int j = 0; j < verLength; j++)
                for (int i = 0; i < horLength; i++)
                {
                    int value = lines[j][i] - '0';
                    map[i, j] = value;
                }

            // Duplicate the array 5 times horizontally and vertically
            for (int m = 0; m < 5; m++)
                for (int n = 0; n < 5; n++)
                    for (int i = 0; i < horLength; i++)
                        for (int j = 0; j < verLength; j++)
                        {
                            if (m != 0 || n != 0)
                            {
                                int value = map[i, j] + n + m;

                                if (value > 9 && value <= 18)
                                    value -= 9;
                                else if (value > 18)
                                    value -= 18;

                                map[i + m * horLength, j + n * verLength] = value;
                                    }
                        }

            // This array will hold the distance from the starting point to i,j
            int[,] distances = new int[map.GetLength(0), map.GetLength(1)];

            // This array will say if i,j is in the Shortest Path Tree set (when the shortest distance from i is finalized)
            bool[,] sptSet = new bool[map.GetLength(0), map.GetLength(1)];

            // Initialize both arrays with default values
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    distances[i, j] = int.MaxValue;
                    sptSet[i, j] = false;
                }
            }

            // Perform Dijkstra's algorithm
            distances[0, 0] = 0;

            int count = map.GetLength(0) * map.GetLength(1);

            for (int i = 0; i < count; i++)
            {
                (int x, int y) = getMinUnprocessed(distances, sptSet);

                // Set this node to visited
                sptSet[x, y] = true;

                int[] directions = new int[] { -1, 1 };

                for (int j = 0; j < directions.Length; j++)
                {
                    int direction = directions[j];

                    // If this point is in range
                    if (x + direction > 0 && x + direction < map.GetLength(0))
                        if (!sptSet[x + direction, y] && map[x + direction, y] != 0 && map[x, y] + distances[x + direction, y] < distances[x, y])
                            distances[x + direction, y] = distances[x, y] + map[x + direction, y];

                    // If this point is in range
                    if (y + direction > 0 && y + direction < map.GetLength(1))
                        if (!sptSet[x, y + direction] && map[x, y + direction] != 0 && map[x, y] + distances[x, y + direction] < distances[x, y])
                            distances[x, y + direction] = distances[x, y] + map[x, y + direction];
                }
            }
            Console.WriteLine(distances[map.GetLength(0) - 1, map.GetLength(1) - 1]);
        }

        // Gets the unprocessed node with least distance
        static (int, int) getMinUnprocessed(int[,] distances, bool[,] sptSet)
        {
            int minValue = int.MaxValue;
            (int, int) result = (0, 0);

            for (int i = 0; i < distances.GetLength(0); i++)
            {
                for (int j = 0; j < distances.GetLength(1); j++)
                {
                    if (distances[i, j] < minValue && !sptSet[i, j])
                    {
                        minValue = distances[i, j];
                        result = (i, j);
                    }
                }
            }
            return result;
        }
    }
}
