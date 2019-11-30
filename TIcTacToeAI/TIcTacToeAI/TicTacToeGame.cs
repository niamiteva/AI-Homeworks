using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeAI
{
    class TicTacToeGame
    {
        Board InitialState { get; set; }
        public Board CurrentState { get; private set; }

        public TicTacToeGame(bool nextPlayer)
        {
            Board initial = new Board();
            InitialState = new Board(initial.Cells, nextPlayer);
            CurrentState = InitialState;
        }

        public void ComputerMakeNextMove(int depth)
        {
            int[] bestState = FindBestMove(depth, CurrentState.IsTurnForPlayerX);

            if (bestState.Length > 0)
            {
                CurrentState.BestScore = bestState[0];
                CurrentState.Cells[bestState[1], bestState[2]].Content = 'O';
            }
                

            Console.WriteLine(CurrentState.ToString());
        }

        private int[] FindBestMove(int depth, bool needMax)
        {
            int[] bestMove = new int[3];
            bestMove[0] = needMax ? int.MinValue + 1 : int.MaxValue - 1;

            // Traverse all cells, evaluate minimax function for
            // all empty cells. And return the cell with optimal value.
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (CurrentState.Cells[i, j].Content == '.')
                    {
                        // Make the move
                        CurrentState.Cells[i, j].Content = needMax ? 'X' : 'O';

                        // Compute evaluation function for this move
                        int[] moveVal = MiniMax(0, !needMax, int.MinValue + 1, int.MaxValue - 1);

                        // Undo the move
                        CurrentState.Cells[i, j].Content = '.';

                        if ((needMax && moveVal[0] > bestMove[0]) ||
                            (!needMax && moveVal[0] < bestMove[0]))
                        {
                            bestMove[1] = i;
                            bestMove[2] = j;
                            bestMove[0] = moveVal[0];
                        }
                    }
                }
            }
            return bestMove;
        }

        private int[] MiniMax(int depth, bool needMax, int alpha, int beta)
        {
            List<int[]> nextMoves = GenerateMoves();

            //childNode = null;

            int score;
            int bestRow = -1;
            int bestCol = -1;

            if (nextMoves.Count == 0 || depth == 0)
            {
                //score = CurrentState.CalculateScore();
                //score = CurrentState.Evaluate(depth);
                score = CurrentState.Evaluate2();
                return new int[] { score, bestRow, bestCol };
            }
            else
            {
                foreach (int[] move in nextMoves)
                //foreach (Board cur in GetChildrenOfCurrentState())
                {
                    CurrentState.Cells[move[0], move[1]].Content = needMax ? 'X' : 'O';

                    if (!needMax)
                    {
                        score = MiniMax(depth + 1, !needMax, alpha, beta)[0];
                        if (beta > score)
                        {
                            beta = score;
                            //childNode = cur;
                            bestRow = move[0];
                            bestCol = move[1];
                        }
                    }
                    else
                    {
                        score = MiniMax(depth + 1, needMax, alpha, beta)[0];
                        if (alpha < score)
                        {
                            alpha = score;
                            bestRow = move[0];
                            bestCol = move[1];
                        }
                    }

                    // undo move
                    CurrentState.Cells[move[0], move[1]].Content = '.';

                    // cut-off
                    if (alpha >= beta) break;
                }

                return new int[] { needMax ? alpha : beta, bestRow, bestCol };
            }
        }

        private List<int[]> GenerateMoves()
        {
            List<int[]> nextMoves = new List<int[]>();

            if (CurrentState.IsGameOver())
            {
                return nextMoves;   // return empty list
            }

            for (int row = 0; row < 3; ++row)
            {
                for (int col = 0; col < 3; ++col)
                {
                    if (IsValidCell(row, col))
                    {
                        nextMoves.Add(new int[] { row, col });
                    }
                }
            }
            return nextMoves;
        }

        public void GetNextMoveFromUser()
        {
            if (CurrentState.IsGameOver()) return;
            
            while (true)
            {
                    Console.WriteLine("Please type in x:[0-2]");
                    int x = int.Parse(Console.ReadLine());
                    Console.WriteLine("Please type in y:[0-2]");
                    int y = int.Parse(Console.ReadLine());
                    
                    if(IsValidCell(x, y))
                    {
                        CurrentState.Cells[x, y].Content = 'X';
                        Console.WriteLine(CurrentState.ToString());

                        return;
                    }
            }
        }

        private bool IsValidCell(int x , int y)
        {
            if (x < 0 || x > 2 || y < 0 || y > 2)
                return false;

            if (CurrentState.Cells[x, y].Content != '.')
                return false;

            return true;
        }
    }
    

}
