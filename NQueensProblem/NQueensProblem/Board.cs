using System;
using System.Collections.Generic;
using System.Text;

namespace IRAPuzzleSolver
{
    public class Board
    {
        private readonly int size;
        private readonly int[] queensPositions;
        private readonly int[] queensInCol;
        private readonly int[] queensInMainDiagonal;
        private readonly int[] queensInSecondaryDiagonal;
        private int queensConflicts;
        private readonly Random randomIndex;
       

        public Board(int size)
        {
            this.size = size;
            queensPositions = new int[size];
            queensInCol = new int[size];
            queensInMainDiagonal = new int[size * 2];
            queensInSecondaryDiagonal = new int[size * 2];
            queensConflicts = 0;
            randomIndex = new Random();
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

                int sum = queensConflicts;
                if (sum == 0)
                {
                    Console.WriteLine($"Restarts: {restarts}\nMoves: {moves}");
                    return;
                }

                int minConflictsQueenIndex = GetMinConflictsQueenIndex(maxConflictsQueenIndex);

                int oldPosition = queensPositions[maxConflictsQueenIndex];
                int newPosition = minConflictsQueenIndex;

                if (newPosition != oldPosition && newPosition != -1)
                {
                    queensPositions[maxConflictsQueenIndex] = newPosition;

                    queensInCol[oldPosition] -= 1;
                    queensInCol[newPosition] += 1;

                    MarkPositionMainDiagonal(oldPosition, maxConflictsQueenIndex, -1);
                    MarkPositionMainDiagonal(newPosition, maxConflictsQueenIndex, 1);

                    MarkPositionInSecondaryDiagonal(oldPosition, maxConflictsQueenIndex, -1);
                    MarkPositionInSecondaryDiagonal(newPosition, maxConflictsQueenIndex, 1);

                    moves++;
                }

                iterations++;
                if (iterations == 100 || newPosition == -1)
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

            for (int i = 0; i < size; i++)
            {
                queensPositions[i] = GetMinConflictsQueenIndex(i);
                queensInCol[queensPositions[i]] += 1;

                MarkPositionMainDiagonal(queensPositions[i], i, 1);
                MarkPositionInSecondaryDiagonal(queensPositions[i], i, 1);
            }
        }

        private void ClearBoard()
        {
            for (int i = 0; i < size * 2 - 1; i++)
            {
                queensInMainDiagonal[i] = 0;
                queensInSecondaryDiagonal[i] = 0;

                if (i < size) queensInCol[i] = 0;
            }
        }

        private void MarkPositionMainDiagonal(int queenColIndex, int queenRowIndex, int value)
        {
            if (queenRowIndex <= queenColIndex)
            {
                queensInMainDiagonal[size - 1 - Math.Abs(queenRowIndex - queenColIndex)] += value;
            }
            else
            {
                queensInMainDiagonal[size - 1 + Math.Abs(queenRowIndex - queenColIndex)] += value;
            }
        }

        private void MarkPositionInSecondaryDiagonal(int queenColIndex, int queenRowIndex, int value)
        {
            queensInSecondaryDiagonal[queenColIndex + queenRowIndex] += value;
        }


        private int CalculateNumberOfQueensConflicts(int col, int row)
        {
            int occupiedPositionConflicts = queensPositions[row] == col ? 3 : 0; // is position occupied by queen => remove 1 digit for every queen direction (3)

            int mainDiagonalIndex = 0;
            if (row <= col)
            {
                mainDiagonalIndex = size - 1 - Math.Abs(row - col);
            }
            else
            {
                mainDiagonalIndex = size - 1 + Math.Abs(row - col);
            }

            int secondaryDiagonalIndex = col + row;

            int result = queensInCol[col] + queensInMainDiagonal[mainDiagonalIndex] + queensInSecondaryDiagonal[secondaryDiagonalIndex];
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
            queensConflicts = 0;
            for (int row = 0; row < size; row++)
            {
                int numberOfConflicts = CalculateNumberOfQueensConflicts(queensPositions[row], row);
                if (numberOfConflicts == maxConflicts)
                {
                    maxCandidates.Add(row);
                }
                else if (numberOfConflicts > maxConflicts)
                {
                    maxConflicts = numberOfConflicts;
                    queensConflicts = maxConflicts;
                    maxCandidates.Clear();
                    maxCandidates.Add(row);
                }
            }

            return maxCandidates[randomIndex.Next(0, maxCandidates.Count)];
        }

        private int GetMinConflictsQueenIndex(int row)
        {
            List<int> minCandidates = new List<int>();
            int minConflicts = size;
            for (int col = 0; col < size; col++)
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

            //the row with max conflicts has no min conflicts => we have no option to move the queen => restart
            if(minCandidates.Count == size-1)
            {
                return -1;
            }
            
            return minCandidates[randomIndex.Next(0, minCandidates.Count)];
        }

        public override string ToString()
        {
            StringBuilder solution = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (queensPositions[i] != j)
                    {
                        solution.Append(j == size - 1 ? "_\n" : "_");
                    }
                    else
                    {
                        solution.Append(j == size - 1 ? "*\n" : "*");
                    }
                }
            }

            return solution.ToString();
        }

    }
}