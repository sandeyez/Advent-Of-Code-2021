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
            List<string> zeroes = new List<string>(); 
            List<string> ones = new List<string>();
            int oxygen = 0; 
            int CO2 = 0;
            int index = 0;
            while (lines.Length > 1)
            {
                List<string> lineList = new List<string>();
                lineList.AddRange(lines);
                zeroes = lineList.FindAll(e => e[index] == '0');
                ones = lineList.FindAll(e => e[index] == '1');
                if (zeroes.Count > ones.Count)
                    lines = zeroes.ToArray();
                else
                    lines = ones.ToArray();
                zeroes.Clear();
                ones.Clear();
                index++;
            }
            oxygen = Convert.ToInt32(lines[0], 2);

            lines = initialLines;
            index = 0;
            while (lines.Length > 1)
            {
                List<string> lineList = new List<string>();
                lineList.AddRange(lines);
                zeroes = lineList.FindAll(e => e[index] == '0');
                ones = lineList.FindAll(e => e[index] == '1');
                if (ones.Count >= zeroes.Count)
                    lines = zeroes.ToArray();
                else
                    lines = ones.ToArray();
                zeroes.Clear();
                ones.Clear();
                index++;
            }
            CO2 = Convert.ToInt32(lines[0], 2);

            Console.WriteLine(CO2 * oxygen);
        }
    }
}
