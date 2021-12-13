using System;
using System.Collections.Generic;

namespace Hydrothermal_Venture
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Day 5\Hydrothermal Venture\input.txt");

            // Dictionary that keeps track of the points that have been visited.
            // False means it has been visited once, True means it has been visited more than once.
            Dictionary<(int, int), bool> map = new Dictionary<(int, int), bool>();

            int counter = 0;

            foreach (string line in lines)
            {
                string[] points = line.Split(" -> ");

                string[] startPoints = points[0].Split(',');
                int startX = int.Parse(startPoints[0]);
                int startY = int.Parse(startPoints[1]);

                string[] endPoints = points[1].Split(',');
                int endX = int.Parse(endPoints[0]);
                int endY = int.Parse(endPoints[1]);

                // If it is a horizontal line
                if (startX != endX && startY == endY)
                    for (int i = Math.Min(startX, endX); i <= Math.Max(startX, endX); i++)
                    {
                        if (map.ContainsKey((i, startY)))
                        {
                            // If this point has been visited once, set the dictionary value to true and add one to the counter.
                            if (!map[(i, startY)])
                            {
                                map[(i, startY)] = true;
                                counter++;
                            }
                            // If the point has been visited more than once, it has already been counted and should be skipped
                        }
                        else
                            // If the point has not been visited before, mark it as visited once.
                            map.Add((i, startY), false);
                    }

                // If it is a vertical line
                else if (startY != endY && startX == endX)
                    for (int i = Math.Min(startY, endY); i <= Math.Max(startY, endY); i++)
                    {
                        if (map.ContainsKey((startX, i)))
                        {
                            if (!map[(startX, i)])
                            {
                                map[(startX, i)] = true;
                                counter++;
                            }
                        }
                        else
                            map.Add((startX, i), false);
                    }
                // If it is a diagonal line
                else
                {
                    // Determine whether the X is moving up (1) or down (-1)
                    int iDirection = Math.Sign(endX - startX);
                    // Determine whether the Y is moving up (1) or down (-1)
                    int jDirection = Math.Sign(endY - startY);

                    for (int i = startX, j = startY; checkEnd(i,j,iDirection,jDirection, startX, startY, endX, endY); i+= iDirection, j+= jDirection)
                        if (map.ContainsKey((i, j)))
                        {
                            if (!map[(i, j)])
                            {
                                map[(i, j)] = true;
                                counter++;
                            }
                        }
                        else
                            map.Add((i, j), false);
                }
            }
            Console.WriteLine(counter);
        }

        static bool checkEnd (int i, int j, int iDirection, int jDirection, int startX, int startY, int endX, int endY)
        {
            bool result = true;

            // If the X-loop is heading downwards, it has to remain greater or equal to the minimum X.
            if (iDirection == -1)
                result &= i >= Math.Min(startX, endX);
            // If the X-loop is heading upwards, it has to remain smaller or equal to the maximum X.
            else
                result &= i <= Math.Max(startX, endX);

            // If the Y-loop is heading downwards, it has to remain greater or equal to the minimum Y.
            if (jDirection == -1)
                result &= j >= Math.Min(startY, endY);            
            // If the Y-loop is heading upwards, it has to remain smaller or equal to the maximum Y.
            else
                result &= j <= Math.Max(startY, endY);

            return result;
        }
    }
}
