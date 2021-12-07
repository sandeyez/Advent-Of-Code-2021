using System;
using System.Collections.Generic;
using System.Linq;

namespace Lanternfish
{
    class Program
    {
        static void Main(string[] args)
        {
            Part2();
        }


        static void Part1()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Day 6\Lanternfish\input.txt");
            int[] input = Array.ConvertAll(lines[0].Split(','), int.Parse);

            List<Lanternfish> lanternfishes = new List<Lanternfish>();

            foreach (int timer in input)
            {
                lanternfishes.Add(new Lanternfish(timer));
            }

            for (int i = 0; i < 128; i++)
            {
                List<Lanternfish> newLanternfishes = new List<Lanternfish>();

                foreach (Lanternfish lanternfish in lanternfishes)
                {
                    newLanternfishes.AddRange(lanternfish.iterate());
                }
                lanternfishes = newLanternfishes;
            }
            Console.WriteLine(lanternfishes.Count);
        }
        class Lanternfish
        {
            int timer;
            public Lanternfish(int days)
            {
                this.timer = days;
            }

            public List<Lanternfish> iterate()
            {
                if (this.timer == 0)
                {
                    this.timer = 6;
                    return (new List<Lanternfish> { this, new Lanternfish(8) });
                }
                timer--;
                return (new List<Lanternfish> { this });
            }
        }

        static void Part2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Day 6\Lanternfish\input.txt");
            int[] input = Array.ConvertAll(lines[0].Split(','), int.Parse);

            long[] timers = new long[9];
            foreach (int timer in input)
            {
                timers[timer]++;
            }

            for (int i = 0; i < 256; i++)
            {
                long[] newRound = new long[9];
                newRound[8] = timers[0];

                for(int j = 0; j < 9; j++ )
                {
                    if (j == 0)
                        newRound[6] = timers[0];
                    else
                        newRound[j - 1] += timers[j];
                }
                timers = newRound;
            }

            Console.WriteLine(timers.Sum());
        }
    }
}
