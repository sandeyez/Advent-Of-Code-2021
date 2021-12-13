using System;
using System.Collections.Generic;

namespace Binary_Diagnostic
{
    class Program
    {
        static void Main(string[] args)
        {
            Part2();
        }

        static void Part1() { 
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Day 3\Binary Diagnostic\input.txt");

            // Count the number of times 1 occurs in each position. The number of 0's can be derived from this.
            int bitLength = lines[0].Length;

            int[] counters = new int[bitLength];

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                for (int j = 0; j < bitLength; j++)
                {
                    char n = line[j];
                    if (n == '1')
                        counters[j]++;
                }
            }

            // If the number of occurences of 1 is greater than half the length of the input, it has to occur more than the other occurences of 0.
            int halfLength = lines.Length / 2 + 1;

            string gamma = "";
            string epsilon = ""; 

            for (int i = 0; i < counters.Length; i++)
            {
                if (counters[i] >= halfLength)
                {
                    gamma += "1";
                    epsilon += "0";
                }
                else
                {
                    gamma += "0";
                    epsilon += "1";
                }
            }

            int result = Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
            Console.Write(result);
        }

        static void Part2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Day 3\Binary Diagnostic\input.txt");

            string[] initialLines = lines;

            int oxygen = 0; 
            int CO2 = 0;

            // Calculate oxygen value
            for (int i = 0; lines.Length > 1; i++)
            {
                List<string> lineList = new List<string>();

                List<string> zeroes = new List<string>();
                List<string> ones = new List<string>();

                lineList.AddRange(lines);

                // Find all bits with 0 at index i
                zeroes = lineList.FindAll(e => e[i] == '0');
                // Find all bits with 1 at index i
                ones = lineList.FindAll(e => e[i] == '1');

                // If zero occured more on this position, continue with all bits that had 0 at index i.
                if (zeroes.Count > ones.Count) 
                    lines = zeroes.ToArray();
                // Else, continue with all bits that had 1 at index i.
                else
                    lines = ones.ToArray(); 
            }
            oxygen = Convert.ToInt32(lines[0], 2);

            // Start with the input lines again.
            lines = initialLines; 

            // Calculate CO2 value
            for (int i = 0; lines.Length > 1; i++)
            {
                List<string> lineList = new List<string>();

                List<string> zeroes = new List<string>();
                List<string> ones = new List<string>();

                lineList.AddRange(lines);

                zeroes = lineList.FindAll(e => e[i] == '0'); 
                ones = lineList.FindAll(e => e[i] == '1');

                if (ones.Count >= zeroes.Count)
                    lines = zeroes.ToArray();
                else
                    lines = ones.ToArray();
            }
            CO2 = Convert.ToInt32(lines[0], 2);

            Console.WriteLine(CO2 * oxygen);
        }
    }
}
