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

            // Numbers 0 to 9 in the mixed up notation.
            List<string[]> input = new List<string[]>();

            // Numbers that need to be figured out
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
                foreach (string puzzleDigit in puzzleDigits)
                {
                    if (puzzleDigit.Length == 2)
                        solution = solution * 10 + 1;

                    else if (puzzleDigit.Length == 3) // 7
                        solution = setSolution(solution, 7);

                    else if (puzzleDigit.Length == 4) // 4
                        solution = setSolution(solution, 4);

                    else if (puzzleDigit.Length == 7) // 8
                        solution = setSolution(solution, 8)

                    else // Digit is 2, 3, 5, 6, 9 or 0
                    {
                        if (puzzleDigit.Length == 5) // 2, 3 or 5

                            // 3 is the only 5-long digit that fully contains 1.
                            if (containsAllChars(puzzleDigit, one))
                                solution = setSolution(solution, 3);

                            // 2 contains 2 digits of 4, whereas 5 contains 3.
                            else if (containsHowManyChars(puzzleDigit, four) == 2)
                                solution = setSolution(solution, 2);

                            // If the digit has length 5 and it isn't 2 or 3, it has to be 5.
                            else
                                solution = setSolution(solution, 5);

                        if (puzzleDigit.Length == 6) // 0, 6, 9

                            // Only 0 and 9 fully contain 7.
                            if (containsAllChars(puzzleDigit, seven)) // 0 or 9

                                // Out of 9 and 0, 9 is the only one that fully contains 4.
                                if (containsAllChars(puzzleDigit, four))
                                    solution = setSolution(solution, 9);

                                else
                                    solution = setSolution(solution, 0);

                            // If the digit didn't contain 7 and was 6 long, it has to be 6.
                            else
                                solution = setSolution(solution, 6);
                    }
                }
                sum += solution;
            }
            Console.WriteLine(sum);
        }

        // Add an extra digit to the solution
        static int setSolution(int solution, int n)
        {
            return (solution * 10 + n);
        }

        // Returns true if a string contains all chars from the char[], false otherwise.
        static bool containsAllChars(string str, char[] chars)
        {
            bool result = true;

            foreach (char c in chars)
                result &= str.Contains(c);

            return result;
        }

        // Returns how many chars out of the char[] are contained in the string.
        static int containsHowManyChars(string str, char[] chars)
        {
            int counter = 0;

            foreach (char c in chars)
                if (str.Contains(c)) counter++;

            return counter;
        }
    }
}