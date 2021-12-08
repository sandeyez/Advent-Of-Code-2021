using System;
using System.Collections.Generic;
using System.Linq;

namespace Giant_Squid
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Day 4\Giant Squid\input.txt");

            int[] bingoInput = Array.ConvertAll(lines[0].Split(','), int.Parse);

            // Save which BingoBoards contain every number for faster lookup.
            Dictionary<int, List<BingoBoard>> whoHasThisNumber = new Dictionary<int, List<BingoBoard>>();

            List<BingoBoard> bingoBoards = new List<BingoBoard>();

            for (int i = 1; i < lines.Length; i += 6)
            {
                List<int[]> board = new List<int[]>();
                List<int> allNumbers = new List<int>();

                for(int j = i + 1; j< i + 6; j++)
                {
                    string[] line = lines[j].Split();
                    line = line.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                    int[] row = Array.ConvertAll(line, int.Parse);

                    board.Add(row);
                    allNumbers.AddRange(row);
                }

                BingoBoard bingoBoard = new BingoBoard(board.ToArray());
                bingoBoards.Add(bingoBoard);

                foreach (int n in allNumbers)
                {
                    if (!whoHasThisNumber.ContainsKey(n))
                        whoHasThisNumber.Add(n, new List<BingoBoard> { bingoBoard });
                    else
                        whoHasThisNumber[n].Add(bingoBoard);
                }
            }

            // List with all boards that have yet won
            List<BingoBoard> winnerBoards = new List<BingoBoard>();

            for (int i = 0; i < bingoInput.Length; i++)
            {
                // Number rolled from the bingo
                int number = bingoInput[i];

                foreach (BingoBoard bingoBoard in whoHasThisNumber[number])
                {
                    bingoBoard.mark(number);

                    // No board can have bingo before 5 numbers have been rolled
                    if (i >= 5)
                        if (bingoBoard.hasBingo() && !winnerBoards.Contains(bingoBoard))
                        {
                            // If there are no more bingoBoards left in the list, this one is the last one to
                            // get bingo and should output the final answer.
                            if (bingoBoards.Count == 1)
                            {
                                Console.Write(bingoBoard.outputWinner(number));
                                return;
                            }
                            // If there are more than 1 boards left, continue the rally without this winner.
                            else
                            {
                                bingoBoards.Remove(bingoBoard);
                                winnerBoards.Add(bingoBoard);
                            }
                        }
                }
            }
        }
    }

    class BingoBoard
    {
        int[,] numbers = new int[5, 5];

        // True if number has been marked, False othewise.
        bool[,] check = new bool[5, 5];

        // Save 2D indices for every number on the card for faster lookup.
        Dictionary<int, (int, int)> numberToIndex = new Dictionary<int, (int, int)>();

        public BingoBoard(int[][] numbers)
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    this.numbers[i, j] = numbers[i][j];
                    check[i, j] = false;
                    numberToIndex.Add(this.numbers[i, j], (i, j));
                }
        }

        public void mark(int n)
        {
            (int i, int j) = numberToIndex[n];
            check[i, j] = true;
        }

        public bool hasBingo()
        {
            // Horizontal bingo
            for (int i = 0; i < 5; i++)
            {
                bool acc = true;
                for (int j = 0; j < 5; j++)
                    acc = acc && check[i, j];
                if (acc)
                    return true;
            }

            // Vertical bingo
            for (int i = 0; i < 5; i++)
            {
                bool acc = true;
                for (int j = 0; j < 5; j++)
                    acc = acc && check[j, i];
                if (acc)
                    return true;
            }
            return false;
        }

        public int outputWinner(int n)
        {
            // Calculate the sum of all unmarked numbers
            int sum = 0;
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (!this.check[i, j])
                        sum += this.numbers[i, j];
            return sum * n;
        }
    }
}
