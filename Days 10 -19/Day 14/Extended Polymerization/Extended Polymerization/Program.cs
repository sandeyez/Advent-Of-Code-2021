using System;
using System.Collections.Generic;
using System.Linq;

namespace Extended_Polymerization
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Days 10 -19\Day 14\Extended Polymerization\input.txt");
            string polymer = lines[0];

            // Pair to insertion letter
            Dictionary<string, string> insertion = new Dictionary<string, string>();

            // Counts for all pairs
            Dictionary<string, long> pairs = new Dictionary<string, long>();


            // Counts for all single letters
            Dictionary<string, long> occurences = new Dictionary<string, long>();

            for (int i = 0; i < polymer.Length - 1; i++)
            {
                string pair = polymer[i].ToString() + polymer[i + 1];

                // Increment occurence of this pair
                safeIncrement(pairs, pair, 1);

                // Increment occurence of this letter
                safeIncrement(occurences, polymer[i].ToString(), 1);
            }
            // Increment the occurences of the last letter
            safeIncrement(occurences, polymer[polymer.Length -1].ToString(), 1);

            // Skip line 1, it's whitespace
            for (int i = 2; i < lines.Length; i++)
            {
                string[] split = lines[i].Split(" -> ");
                insertion.Add(split[0], split[1]);
            }

            for (int step = 0; step < 40; step++)
            {
                // Pairs for the next round
                Dictionary<string, long> newPairs = new Dictionary<string, long>();

                // List all keys of the dictionary: all pairs
                var pairsList = pairs.Keys;

                foreach(string pair in pairsList)
                {
                    // If there exists a rule for this pair
                    if (insertion.ContainsKey(pair))
                    {
                        string insert = insertion[pair];

                        long count = pairs[pair];

                        string string1 = pair[0].ToString() + insert;
                        string string2 = insert + pair[1];

                        safeIncrement(newPairs, string1, count);
                        safeIncrement(newPairs, string2, count);

                        safeIncrement(occurences, insert, count);
                    }
                }

                pairs = newPairs;
            }

            var values = occurences.Values.ToList();
            values.Sort();

            Console.WriteLine(values.Last() - values.First());
        }

        // Increments a value in a dictionary if it existed before, else it adds it
        static void safeIncrement(Dictionary<string, long> dict, string key, long value)
        {
            if (dict.ContainsKey(key))
                dict[key]+= value;
            else
                dict.Add(key, value);
        }
    }
}
