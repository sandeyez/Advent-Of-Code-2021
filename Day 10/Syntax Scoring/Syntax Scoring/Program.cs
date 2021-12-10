using System;
using System.Collections.Generic;
using System.Linq;

namespace Syntax_Scoring
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Day 10\Syntax Scoring\input.txt");

            char[] opening = new char[] { '(', '[', '<', '{' };

            Dictionary<char, char> pairs = new Dictionary<char, char>()
            {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' },
                { '<', '>' },
            };

            Dictionary<char, int> scores = new Dictionary<char, int>()
            {
                { ')', 1 },
                { ']', 2 },
                { '}', 3 },
                { '>', 4 },
            };

            List<long> points = new List<long>();
            foreach (string line in lines)
            {
                bool corrupted = false;
                Stack<char> characters = new Stack<char>();
                foreach (char c in line)
                {
                    // If c is an opening character
                    if (opening.Contains(c))
                        characters.Push(c);

                    // If c is a closing character
                    else
                    {
                        char open = characters.Pop();

                        // If the closing character doesn't match, it's a corrupted line and it should be discarded. 
                        if (!(pairs[open] == c))
                        {
                            corrupted = true;
                            break;
                        }
                    }
                }

                if (!corrupted)
                {
                    long score = 0;
                    while (characters.Count > 0)
                    {
                        char open = characters.Pop();
                        score = score * 5 + scores[pairs[open]];
                    }
                    points.Add(score);
                }
            }
            points.Sort();
            Console.WriteLine(points[points.Count / 2]);
        }
    }
}
