using System;
using System.Collections.Generic;

namespace SonarSweep
{
    class Program
    {
        static void Main(string[] args)
        {
            Part2();
        }

        static void Part1()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Day 1\input.txt");
            int prev = Int32.Parse(lines[0]);
            int counter = 0;
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                int number = Int32.Parse(line);
                if (number > prev)
                    counter++;
                prev = number;
            }
            Console.Write(counter);
        }

        static void Part2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Day 1\input.txt");
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(Int32.Parse(lines[0]));
            queue.Enqueue(Int32.Parse(lines[1]));
            queue.Enqueue(Int32.Parse(lines[2]));
            int counter = 0;

            for (int i = 3; i < lines.Length; i++)
            {
                int value = Int32.Parse(lines[i]);
                int queueValue = queue.Dequeue();
                if (value > queueValue)
                    counter++;
                queue.Enqueue(value);
            }
            Console.Write(counter);
        }
    }
}
