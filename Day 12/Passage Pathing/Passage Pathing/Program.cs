using System;
using System.Collections.Generic;
using System.Linq;

namespace Passage_Pathing
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Day 12\Passage Pathing\input.txt");

            Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();

            // Fill graph with nodes and edges
            foreach (string line in lines)
            {
                string[] split = line.Split('-');
                addEdge(split[0], split[1], graph);
                addEdge(split[1], split[0], graph);
            }

            // Traverse graph to find paths
            Console.WriteLine(countAllPaths("start", "end", graph, new Stack<string>(), new List<string>(), new Dictionary<string, int>(), 2));
        }

        static void addEdge(string start, string end, Dictionary<string, List<string>> graph)
        {
            if (graph.ContainsKey(start))
                graph[start].Add(end);
            else
                graph.Add(start, new List<string> { end });
        }

        static int countAllPaths(string start, string end, Dictionary<string, List<string>> graph, Stack<string> path, List<string> paths, Dictionary<string, int> smallCavesVisited, int smallCavesMax)
        {
            int sum = 0;

            // Maximal number of visits for small caves for the next rounds
            int nextSmallCavesMax = smallCavesMax;

            // If this node is a small cave
            if (!start.All(c => char.IsUpper(c)))
            {
                if (smallCavesVisited.ContainsKey(start))
                {
                    // Increase the number of times this cave has been visited.
                    smallCavesVisited[start]++;

                    // If it's the 2nd time we're visiting this small cave, we should only visit small caves once, from now on.
                    if (smallCavesVisited[start] == 2)
                        nextSmallCavesMax = 1;
                }
                else
                    smallCavesVisited.Add(start, 1);
            }

            // add this node to the path
            path.Push(start);

            if (start == end)
            {
                string pathString = String.Join("-", path);
                paths.Add(pathString);
                path.Pop();
                smallCavesVisited[start]--;
                return 1;
            }

            foreach (string node in graph[start])
            {
                if (smallCavesVisited.ContainsKey(node))
                {
                    // If node is not the start or end node, and has been visited less than the max
                    if (node != "start" && node != "end" && smallCavesVisited[node] < nextSmallCavesMax)
                        sum += countAllPaths(node, end, graph, path, paths, smallCavesVisited, nextSmallCavesMax);

                    // If node is start or end and we haven't visited them
                    else if ((node == "start" || node == "end") && smallCavesVisited[node] < 1)
                        sum += countAllPaths(node, end, graph, path, paths, smallCavesVisited, nextSmallCavesMax);
                }

                else // If the node hasn't been visited before
                    sum += countAllPaths(node, end, graph, path, paths, smallCavesVisited, nextSmallCavesMax);
            }
            // If the cave was a small one, we decrement its visits by 1
            if (!start.All(c => char.IsUpper(c)))
                smallCavesVisited[start]--;

            // Delete this node from the path
            path.Pop();

            return sum;
        }
    }
}