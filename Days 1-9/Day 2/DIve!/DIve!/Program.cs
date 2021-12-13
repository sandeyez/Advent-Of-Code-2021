using System;

namespace DIve_
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\OneDrive\Desktop\Projects\AoC2021\Day 2\DIve!\input.txt");

            int depth = 0;
            int hor = 0;
            int aim = 0;

            foreach (string line in lines)
            {
                string[] values = line.Split(); // E.g. Forward 8 becomes "Forward" and "8".

                string direction = values[0];
                int value = Int32.Parse(values[1]);

                switch (direction)
                {
                    case "forward":
                        {
                            hor += value;
                            depth += aim * value;
                            break;
                        }
                    case "down":
                        {
                            //depth += value;
                            aim += value;
                            break;
                        }
                    case "up":
                        {
                            //depth -= value;
                            aim -= value;
                            break;
                        }
                }
            }
            Console.WriteLine(depth * hor);
        }
    }
}
