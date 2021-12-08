using System;
using System.Collections.Generic;
using System.Linq;

namespace Seven_Segment_Search
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Day 8\Seven Segment Search\input.txt");
            List<string[]> input = new List<string[]>();
            List<string[]> digits = new List<string[]>();

            foreach (string line in lines)
            {
                string[] splitLine = line.Split(" | ");

                input.Add(string.Join(" ", splitLine).Split());
                digits.Add(splitLine[1].Split());
            }

            char[] one = new char[2];
            char[] seven = new char[3];
            char[] four = new char[4];

            int sum = 0;
            for (int i = 0; i < input.Count; i++)
            {
                string[] digitInput = input[i];
                string[] puzzleDigits = digits[i];

                foreach (string digit in digitInput)
                    switch (digit.Length)
                    {
                        case 2: // 1
                            {
                                one = digit.ToCharArray();
                                break;
                            }
                        case 3: // 7
                            {
                                seven = digit.ToCharArray();
                                break;
                            }
                        case 4: // 4
                            {
                                four = digit.ToCharArray();
                                break;
                            }
                    }
                int solution = 0;
                foreach(string puzzleDigit in puzzleDigits)
                {
                    if (puzzleDigit.Length == 2)
                        solution = solution * 10 + 1;
                    else if (puzzleDigit.Length == 3)
                        solution = solution * 10 + 7;
                    else if (puzzleDigit.Length == 4)
                        solution = solution * 10 + 4;
                    else if (puzzleDigit.Length == 7)
                        solution = solution * 10 + 8;
                    else // Digit is 2, 3, 5, 6, 9, 0
                    {
                        if (puzzleDigit.Length == 5) // 2, 3 or 5
                            if (containsAllChars(puzzleDigit, one)) // 3 is the only 5-long digit that contains 1.
                                solution = solution * 10 + 3;
                            else if (containsHowManyChars(puzzleDigit, four) == 2) // 3 contains 2 digits of 4, whereas 5 contains 3.
                                solution = solution * 10 + 2;
                            else
                                solution = solution * 10 + 5;
                        if (puzzleDigit.Length == 6) // 0, 6, 9
                            if (containsAllChars(puzzleDigit, seven)) // 0 or 9
                                if (containsAllChars(puzzleDigit, four))
                                    solution = solution * 10 + 9;
                                else
                                    solution = solution * 10 + 0;
                            else
                                solution = solution * 10 + 6;
                    }

                }
                sum += solution;
            }
            Console.WriteLine(sum);
        }

        static bool containsAllChars(string str, char[] chars)
        {
            bool result = true;

            foreach (char c in chars)
                result &= str.Contains(c);

            return result;
        }

        static int containsHowManyChars(string str, char[] chars)
        {
            int counter = 0;

            foreach (char c in chars)
                if (str.Contains(c)) counter++;

            return counter;
        }
    }
}