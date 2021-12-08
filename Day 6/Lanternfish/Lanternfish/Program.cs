using System;
using System.Collections.Generic;
using System.Linq;

namespace Lanternfish
{
    class Program
    {
        static void Main(string[] args)
        { 
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Day 6\Lanternfish\input.txt");

            int[] input = Array.ConvertAll(lines[0].Split(','), int.Parse);

            // Store the amount of fish with a timer of x, where x is the index in the array.
            long[] timers = new long[9];

            foreach (int timer in input)
            {
                timers[timer]++;
            }

            for (int i = 0; i < 256; i++)
            {
                long[] newRound = new long[9];

                // Fish whose timer is at 0 at the beginning of the round, give birth to a baby fish with timer 8.
                newRound[8] = timers[0];

                for(int j = 0; j < 9; j++ )
                {
                    // Timers of fish that have given birth this round will return to 6.
                    if (j == 0)
                        newRound[6] = timers[0];
                    // If the fish hasn't given birth, its' timer decreases by 1.
                    else
                        newRound[j - 1] += timers[j];
                }
                timers = newRound;
            }

            // Sum all timers to get the total number of fish.
            Console.WriteLine(timers.Sum());
        }
    }
}
