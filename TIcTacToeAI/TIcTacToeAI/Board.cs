using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeAI
{
    class Board
    {
        public Cell[,] Cells { get; set; }

        //empty board
        public Board()
        {
            Cells = new Cell[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Cells[i, j] = new Cell(i, j, '.');
                }
            }
        }

        public int Evaluate2()
        {
            int emptyCellsCount = CountEmptyCells();

            // Check rows for victory
            for (int row = 0; row < 3; row++)
            {
                if (Cells[row, 0].Content != '.' && AllValuesInRowAreEqual(row))
                {
                    char valueInRow = Cells[row, 0].Content;

                    if (valueInRow == 'X')
                        return 1 + emptyCellsCount;
                    else if (valueInRow == 'O')
                        return -1 - emptyCellsCount;
                }
            }

            // Check columns for victory
            for (int col = 0; col < 3; col++)
            {
                if (Cells[0, col].Content != '.' && AllValuesInColAreEqual(col))
                {
                    char valueInColumn = Cells[0, col].Content;

                    if (valueInColumn == 'X')
                        return 1 + emptyCellsCount;
                    else if (valueInColumn == 'O')
                        return -1 - emptyCellsCount;
                }
            }

            // Check diagonals for victory
            if (Cells[1, 1].Content != '.' && AllValuesInPrimeDiagonalAreEqual() || AllValuesInSecondDiagonalAreEqual())
            {
                int valueInDiagonal = Cells[1, 1].Content;
                if (valueInDiagonal == 'X')
                    return 1 + emptyCellsCount;
                else if (valueInDiagonal == 'O')
                    return -1 - emptyCellsCount;
            }

            // Tie
            return 0;
        }

        private int CountEmptyCells()
        {
            int result = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Cells[i, j].Content == '.')
                        result += 1;
                }
            }

            return result;
        }

        public int Evaluate(int depth)
        {
            // Check rows for victory
            for (int row = 0; row< 3; row++)
            {
                if (Cells[row, 0].Content != '.' && AllValuesInRowAreEqual(row))
                {
                    char valueInRow = Cells[row, 0].Content;

                    if (valueInRow == 'X')
                        return 100 - depth;
                    else if (valueInRow == 'O')
                        return -(100 - depth);
                }
            }

            // Check columns for victory
            for (int col = 0; col< 3; col++)
            {
                if (Cells[0, col].Content != '.' && AllValuesInColAreEqual(col))
                {
                    char valueInColumn = Cells[0, col].Content;

                    if (valueInColumn == 'X')
                        return 100 - depth;
                    else if (valueInColumn == 'O')
                        return -(100 - depth);
                }
            }

            // Check diagonals for victory
            if (Cells[1, 1].Content != '.' && AllValuesInPrimeDiagonalAreEqual() || AllValuesInSecondDiagonalAreEqual())
            {
                int valueInDiagonal = Cells[1, 1].Content;
                if (valueInDiagonal == 'X')
                    return 100 - depth;
                else if (valueInDiagonal == 'O')
                    return -(100 - depth);
            }

            // Tie
            return 0;
        }

        public bool IsGameOver()
        {
            if (CountEmptyCells() == 0)
                return true;

            if (HasWinner())
                return true;
        

            return false;
        }

        private bool HasWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (AllValuesInRowAreEqual(i))
                    return true;

                if (AllValuesInColAreEqual(i))
                    return true;
            }

            return AllValuesInPrimeDiagonalAreEqual() || AllValuesInPrimeDiagonalAreEqual();

        }

        private bool AllValuesInRowAreEqual(int row)
        {
            if ((Cells[row, 0].Content != '.' && 
                Cells[row, 0].Content == Cells[row, 1].Content && 
                Cells[row, 1].Content == Cells[row, 2].Content))
                return true;

            return false;
        }

        private bool AllValuesInColAreEqual(int col)
        {
            if (Cells[0, col].Content != '.' && 
                Cells[0, col].Content == Cells[1, col].Content && 
                Cells[1, col].Content == Cells[2, col].Content)
                return true;

            return false;
        }

        private bool AllValuesInPrimeDiagonalAreEqual()
        {
            if (Cells[0, 0].Content != '.' && 
                Cells[0, 0].Content == Cells[1, 1].Content && 
                Cells[1, 1].Content == Cells[2, 2].Content)
                return true;

            return false;
        }

        private bool AllValuesInSecondDiagonalAreEqual()
        {
            if ((Cells[0, 2].Content != '.' && 
                Cells[0, 2].Content == Cells[1, 1].Content && 
                Cells[1, 1].Content == Cells[2, 0].Content))
                return true;

            return false;
        }
        
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < 3; i++)
            {
                str.Append(" ");

                for (int j = 0; j < 3; j++)
                {
                    if (j == 2)
                        str.Append(Cells[i,j].Content + "\n");
                    else
                        str.Append(Cells[i,j].Content + " | ");
                }

                if(i != 2)
                    str.Append("--- --- ---");

                str.Append("\n");
            }

            return str.ToString();
        }
    }
}
