using System;
using System.Collections.Generic;
using System.Linq;

namespace Packet_Decoder
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Days 10 -19\Day 16\Packet Decoder\input.txt");

            string hex = lines[0];
            string binary = HexToBinary(hex);

            // Create a queue to easily keep track of the bits
            Queue<int> bitQueue = new Queue<int>();

            // Enqueue all the bits from the string
            foreach (char c in binary)
            {
                bitQueue.Enqueue(c - '0');
            }

            Console.WriteLine(parseString(bitQueue));

            // Converts a hexidecimal number to a bitstring.
            // Source: https://stackoverflow.com/questions/6617284/c-sharp-how-convert-large-hex-string-to-binary
            static string HexToBinary(string hexstring)
            {
                return String.Join(String.Empty, hexstring.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
            }

            // Dequeue multiple bits and convert them to an int
            static long dequeueChunk(Queue<int> queue, int n)
            {
                int result = 0;
                for (int i = 0; i < n; i++)
                {
                    result = result * 2 + queue.Dequeue();
                }
                return result;
            }

            static long parseString(Queue<int> queue)
            {
                int version = (int)dequeueChunk(queue, 3);
                int typeID = (int)dequeueChunk(queue, 3);

                if (typeID == 4)
                    return parseLiteralString(queue);
                else
                    return parseOperatorString(queue, typeID);

            }
            static long parseLiteralString(Queue<int> queue)
            {
                bool lastRun = false;

                long result = 0;
                while (!lastRun)
                {
                    int value = queue.Dequeue();

                    // The final packet of a string is the one starting with 0.
                    lastRun = value == 0;
                    
                    result = result * 16 + dequeueChunk(queue, 4);
                }
                return result;
            }

            static long parseOperatorString(Queue<int> queue, int type)
            {
                long result = 0;

                int length = queue.Dequeue();

                long count = length == 0 ? dequeueChunk(queue, 15) : dequeueChunk(queue, 11);

                List<long> children = new List<long>();

                // If length represents the total length of the subpackets
                if (length == 0)
                {
                    // Stop when the right number of bits is converted
                    long stopCount = queue.Count - count;
                    while (queue.Count > stopCount)
                    {
                        children.Add(parseString(queue));
                    }
                }

                // If length represents the number of subpackets
                else
                {
                    for (int i = 0; i < count; i++)
                        children.Add(parseString(queue));
                }

                switch (type)
                {
                    case 0:
                        {
                            result = children.Sum();
                            break;
                        }
                    case 1:
                        {
                            result = calculateProduct(children);
                            break;
                        }
                    case 2:
                        {
                            result = children.Min();
                            break;
                        }
                    case 3:
                        {
                            result = children.Max();
                            break;
                        }
                    case 5:
                        {
                            result = children[0] > children[1] ? 1 : 0;
                            break;
                        }
                    case 6:
                        {
                            result = children[0] < children[1] ? 1 : 0;
                            break;
                        }
                    case 7:
                        {
                            result = children[0] == children[1] ? 1 : 0;
                            break;
                        }
                    default:
                        break;
                }

                return result;
            }

            static long calculateProduct(List<long> list)
            {
                long result = 1;

                foreach (long n in list)
                    result *= n;

                return result;
            }
        }
    }
}