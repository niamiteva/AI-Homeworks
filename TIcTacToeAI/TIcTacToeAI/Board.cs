using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeAI
{
    class Board
    {
        public Cell[,] Cells { get; set; }
        public int Score { get; private set; }
        public bool IsTurnForPlayerX { get; set; }
        public int BestScore { get; set;}
        public bool GameOver { get; private set; }

        //empty board
        public Board()
        {
            Score = 0;

            Cells = new Cell[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Cells[i, j] = new Cell(i, j, '.');
                }
            }
        }

        public Board(Cell[,] cells, bool turnForPlayerX)
        {
            IsTurnForPlayerX = turnForPlayerX;
            Cells = cells;
            //CalculateScore(); //heuristic
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

        public int CalculateScore()
        {
            int score = 0;

            //3 rows
            score += GetScoreForLine(new Cell[] { Cells[0, 0], Cells[0, 1], Cells[0, 2] });
            score += GetScoreForLine(new Cell[] { Cells[1, 0], Cells[1, 1], Cells[1, 2] });
            score += GetScoreForLine(new Cell[] { Cells[2, 0], Cells[2, 1], Cells[2, 2] });
            //3 cols
            score += GetScoreForLine(new Cell[] { Cells[0, 0], Cells[1, 0], Cells[2, 0] });
            score += GetScoreForLine(new Cell[] { Cells[0, 1], Cells[1, 1], Cells[2, 1] });
            score += GetScoreForLine(new Cell[] { Cells[0, 2], Cells[1, 2], Cells[2, 2] });
            //2 diagonals
            score += GetScoreForLine(new Cell[] { Cells[0, 0], Cells[1, 1], Cells[2, 2] });
            score += GetScoreForLine(new Cell[] { Cells[0, 2], Cells[1, 1], Cells[2, 0] });

            Score = score;

            return score;
        }

        private int GetScoreForLine(Cell[] cells)
        {
            int score = 0;

            // First cell
            if (cells[0].Content == 'X')
            {
                score = 1;
            }
            else if (cells[0].Content == 'O')
            {
                score = -1;
            }

            // Second cell
            if (cells[1].Content == 'X')
            {
                if (score == 1)
                {   // cell1 is mySeed
                    score = 10;
                }
                else if (score == -1)
                {  // cell1 is oppSeed
                    return 0;
                }
                else
                {  // cell1 is empty
                    score = 1;
                }
            }
            else if (cells[1].Content == 'O')
            {
                if (score == -1)
                { // cell1 is oppSeed
                    score = -10;
                }
                else if (score == 1)
                { // cell1 is mySeed
                    return 0;
                }
                else
                {  // cell1 is empty
                    score = -1;
                }
            }

            // Third cell
            if (cells[2].Content == 'X')
            {
                if (score > 0)
                {  // cell1 and/or cell2 is mySeed
                    score *= 10;
                }
                else if (score < 0)
                {  // cell1 and/or cell2 is oppSeed
                    return 0;
                }
                else
                {  // cell1 and cell2 are empty
                    score = 1;
                }
            }
            else if (cells[2].Content == 'O')
            {
                if (score < 0)
                {  // cell1 and/or cell2 is oppSeed
                    score *= 10;
                }
                else if (score > 1)
                {  // cell1 and/or cell2 is mySeed
                    return 0;
                }
                else
                {  // cell1 and cell2 are empty
                    score = -1;
                }
            }
            return score;
        }

        public bool IsGameOver()
        {
            //CalculateScore();

            if (HasWinner())
                return true;

            //leaf node and is full ==> tie
            foreach(Cell c in Cells)
            {
                if (c.Content == '.')
                    return false;
            }

            return true;
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

            if (AllValuesInPrimeDiagonalAreEqual() || AllValuesInPrimeDiagonalAreEqual())
                return true;

            return false;

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

        //public IEnumerable<Board> GetChildrenOfCurrentState()
        //{
        //    foreach(Cell c in Cells)
        //    { 
        //        if (c.Content == '.')
        //        {
        //            Cell[,] newValues = (Cell[,])Cells.Clone();
        //            newValues[c.X, c.Y].Content = IsTurnForPlayerX ? 'X' : 'O' ;
        //            yield return new Board(newValues, !IsTurnForPlayerX);
        //        }
        //    }
        //}
        
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
