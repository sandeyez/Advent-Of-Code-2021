using System;

namespace Dumbo_Octopus
{
    class Program
    {
        // Total number of flashes per round
        static int flashes = 0;

        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Day 11\Dumbo Octopus\input.txt");

            int[,] energy = new int[lines[0].Length, lines.Length];

            for (int i = 0; i < lines.Length; i++)
                for (int j = 0; j < lines[i].Length; j++)
                {
                    energy[j, i] = lines[i][j] - '0';
                }

            bool[,] flashed = new bool[lines[0].Length, lines.Length];

            int step = 0;

            for (; flashes < energy.GetLength(0) * energy.GetLength(1); step++)
            {
                // Reset flashes for new round
                flashes = 0;

                for (int x = 0; x < energy.GetLength(0); x++)
                {
                    for (int y = 0; y < energy.GetLength(1); y++)
                    {
                        if (!flashed[x, y])
                        {
                            // Energy should only be incremented if the octopus hasnt flashed
                            energy[x, y]++;

                            // Octopus should only flash if his energy is greater than 9
                            if (energy[x, y] > 9)
                                Flash(x, y, energy, flashed);
                        }
                    }
                }
                // Reset the flashed array
                flashed = new bool[lines[0].Length, lines.Length];
            }
            Console.WriteLine(step);
        }

        static void Flash(int x, int y, int[,] energy, bool[,] flashed)
        {
            int[] directions = new int[] { -1, 0, 1 };

            // Energy should be set to 0 if the octopus flashed
            energy[x, y] = 0;
            flashed[x, y] = true;
            flashes++;

            for (int i = 0; i < directions.Length; i++)
                for (int j = 0; j < directions.Length; j++)
                {
                    int xDir = directions[i];
                    int yDir = directions[j];

                    int neighbourX = x + xDir;
                    int neighbourY = y + yDir;

                    // If the point is inside the grid
                    if (neighbourX >= 0 && neighbourX < energy.GetLength(0) && neighbourY >= 0 && neighbourY < energy.GetLength(1))
                    {
                        // If you're not pointing to yourself and the neighbour hasn't flashed
                        if (!(xDir == 0 && yDir == 0) && !flashed[neighbourX, neighbourY])
                        {
                            energy[neighbourX, neighbourY]++;

                            int neighbourEnergy = energy[neighbourX, neighbourY];

                            // If the neighbour hasn't flashed this round and should
                            if (neighbourEnergy > 9)
                            {
                                Flash(neighbourX, neighbourY, energy, flashed);
                            }
                        }
                    }
                }
        }

        static void printEnergy(int[,] energy)
        {
            for (int i = 0; i < energy.GetLength(0); i++)
            {
                string line = "";
                for (int j = 0; j < energy.GetLength(1); j++)
                {
                    line += energy[i, j].ToString();
                }
                Console.WriteLine(line);
            }
        }
    }
}
