using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRAPuzzleSolver
{
    public class Board
    {
        private int Size { get; set; }

        private int[] QueensPositions { get; set; }
        private int[] QueensInCol { get; set; }
        private int[] QueensInMainDiagonal { get; set; }
        private int[] QueensInSecondaryDiagonal { get; set; }
        //private int[] QueensConflicts { get; set; }
        private int QueensConflicts { get; set; }
        //private int Restatrts { get; set; }
        //private int Moves { get; set; }


        public Board(int size)
        {
            Size = size;
            QueensPositions = new int[size];
            QueensInCol = new int[size];
            QueensInMainDiagonal = new int[size*2];
            QueensInSecondaryDiagonal = new int[size*2];
            QueensConflicts = 0;
            //Restatrts = 0;
            //Moves = 0;
            //QueensConflicts = new int[size];
        }

        public void ExecuteIterativeRepairAlgorithm()
        {
            GenerateRandomBoard();
            int iterations = 0;
            int moves = 0;
            int restarts = 0;

            while (true)
            {   
                int maxConflictsQueenIndex = GetMaxConflictsQueenIndex();

                int sum = QueensConflicts;
                //int sum = SumAllQueensConflicts();
                if (sum == 0)
                {
                    //QueensPositions.ToString();
                    Console.WriteLine($"Restarts: {restarts}\nMoves: {moves}");
                    return;
                }

                int minConflictsQueenIndex = GetMinConflictsQueenIndex(maxConflictsQueenIndex);

                int oldPosition = QueensPositions[maxConflictsQueenIndex];
                int newPosition = minConflictsQueenIndex;

                if (newPosition != oldPosition)
                {
                    QueensPositions[maxConflictsQueenIndex] = newPosition;

                    QueensInCol[oldPosition] -= 1;
                    QueensInCol[newPosition] += 1;

                    MarkPositionMainDiagonal(oldPosition, maxConflictsQueenIndex, -1);
                    MarkPositionMainDiagonal(newPosition, maxConflictsQueenIndex, 1);

                    MarkPositionInSecondaryDiagonal(oldPosition, maxConflictsQueenIndex, -1);
                    MarkPositionInSecondaryDiagonal(newPosition, maxConflictsQueenIndex, 1);

                    moves++;
                }

                iterations++;
                if(iterations == Size * 2)
                {
                    GenerateRandomBoard();
                    iterations = 0;
                    moves = 0;
                    restarts++;
                }
            }
        }

        private void GenerateRandomBoard()
        {
            ClearBoard();

            Random rnd = new Random();

            for (int i = 0; i < Size; i++)
            {
                //QueensPositions[i] = rnd.Next(0, Size);
                QueensPositions[i] = GetMinConflictsQueenIndex(i);
                QueensInCol[QueensPositions[i]] += 1;

                MarkPositionMainDiagonal(QueensPositions[i], i, 1);
                MarkPositionInSecondaryDiagonal(QueensPositions[i], i, 1);
            }
        }

        private void ClearBoard()
        {
            for (int i = 0; i < Size*2 -1; i++)
            {
                QueensInMainDiagonal[i] = 0;
                QueensInSecondaryDiagonal[i] = 0;

                if(i < Size) QueensInCol[i] = 0;
            }
        }

        private void MarkPositionMainDiagonal(int queenColIndex, int queenRowIndex, int value)
        {
            if (queenRowIndex <= queenColIndex)
            {
                QueensInMainDiagonal[Size -1 - Math.Abs(queenRowIndex - queenColIndex)] += value;
            }
            else
            {
                QueensInMainDiagonal[Size - 1 + Math.Abs(queenRowIndex - queenColIndex)] += value;
            }
        }

        private void MarkPositionInSecondaryDiagonal(int queenColIndex, int queenRowIndex, int value)
        {
            QueensInSecondaryDiagonal[queenColIndex + queenRowIndex] += value;
        }
        
        
        private int CalculateNumberOfQueensConflicts(int col, int row)
        {
            int occupiedPositionConflicts = QueensPositions[row] == col ? 3 : 0; // is position occupied by queen => remove 1 digit for every queen direction (3)

            int mainDiagonalIndex = 0;
            if(row <= col)
            {
                mainDiagonalIndex = Size - 1 - Math.Abs(row - col);
            }
            else
            {
                mainDiagonalIndex = Size - 1 + Math.Abs(row - col);
            }

            int secondaryDiagonalIndex = col + row;

            int result = QueensInCol[col] + QueensInMainDiagonal[mainDiagonalIndex] + QueensInSecondaryDiagonal[secondaryDiagonalIndex];
            if (result >= 3)
            {
                result -= occupiedPositionConflicts;
            }

            return result;
        }

        private int GetMaxConflictsQueenIndex()
        {
            List<int> maxCandidates = new List<int>();
            int maxConflicts = 0;
            QueensConflicts = 0;
            for (int row = 0; row < Size; row++)
            {
                int numberOfConflicts = CalculateNumberOfQueensConflicts(QueensPositions[row], row);
                if (numberOfConflicts == maxConflicts)
                {
                    maxCandidates.Add(row);
                }
                else if (numberOfConflicts > maxConflicts)
                {
                    maxConflicts = numberOfConflicts;
                    QueensConflicts = maxConflicts;
                    maxCandidates.Clear();
                    maxCandidates.Add(row);
                }
            }

            Random rnd = new Random();

            return maxCandidates.ElementAt(rnd.Next(0, maxCandidates.Count));
        }

        private int GetMinConflictsQueenIndex(int row)
        {
            List<int> minCandidates = new List<int>();
            int minConflicts = Size;
            for (int col = 0; col < Size; col++)
            {
                int numberOfConflicts = CalculateNumberOfQueensConflicts(col, row);

                if (numberOfConflicts == minConflicts)
                {
                    minCandidates.Add(col);
                }
                else if (numberOfConflicts < minConflicts)
                {
                    minConflicts = numberOfConflicts;
                    minCandidates.Clear();
                    minCandidates.Add(col);
                }
            }

            Random rnd = new Random();

            return minCandidates.ElementAt(rnd.Next(0, minCandidates.Count));
        }

        public override string ToString()
        {
            StringBuilder solution = new StringBuilder();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (QueensPositions[i] != j)
                    {
                       solution.Append(j == Size - 1 ? "_\n" : "_");
                    }
                    else
                    {
                        solution.Append(j == Size - 1 ? "*\n" : "*");
                    }
                }
            }

            return solution.ToString();
        }

    }
}
