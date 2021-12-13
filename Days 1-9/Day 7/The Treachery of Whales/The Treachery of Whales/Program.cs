using System;
using System.Linq;

namespace The_Treachery_of_Whales
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\OneDrive\Desktop\Projects\AoC2021\Day 7\The Treachery of Whales\input.txt");

            int[] input = Array.ConvertAll(lines[0].Split(','), int.Parse);

            // The initial fuel cost is infinite.
            int result = int.MaxValue;
            for (int i = input.Min(); i <= input.Max(); i++)
            {
                int diff = input.Select(num => sumDiff(num, i)).Sum();

                // If the fuel cost of this center position is less than we though the lowest one was, set it as new lowest one.
                if (diff < result)
                {
                    result = diff;
                }
            }
            Console.WriteLine(result);
        }

        static int sumDiff(int start, int end)
        {
            // return Math.Abs(start - end);
            int sum = 0;
            for (int i = Math.Min(start, end); i <= Math.Max(start, end); i++)
                sum += i - start;
            return (Math.Abs(sum));
        }
    }
}
