using System;
using System.Collections.Generic;

namespace Hydrothermal_Venture
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Day 5\Hydrothermal Venture\input.txt");
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

                if (startX != endX && startY == endY)
                    for (int i = Math.Min(startX, endX); i <= Math.Max(startX, endX); i++)
                    {
                        if (map.ContainsKey((i, startY)))
                        {
                            if (!map[(i, startY)])
                            {
                                map[(i, startY)] = true;
                                counter++;
                            }
                        }
                        else
                            map.Add((i, startY), false);
                    }
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
                else
                {
                    int iDirection = Math.Sign(endX - startX);
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

            if (iDirection == -1)
                result &= i >= Math.Min(startX, endX);
            else
                result &= i <= Math.Max(startX, endX);

            if (jDirection == -1)
                result &= j >= Math.Min(startY, endY);
            else
                result &= j <= Math.Max(startY, endY);

            return result;
        }
    }
}
