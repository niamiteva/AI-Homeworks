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
        public static void GetNextMoveFromUser(Board board)
        {
            if (board.IsGameOver()) return;

            while (true)
            {
                Console.WriteLine("Please type in x:[0-2]");
                int x = int.Parse(Console.ReadLine());
                Console.WriteLine("Please type in y:[0-2]");
                int y = int.Parse(Console.ReadLine());

                if (IsValidCell(x, y, board))
                {
                    board.Cells[x, y].Content = 'X';
                    Console.WriteLine(board.ToString());

                    return;
                }
            }
        }

        private static bool IsValidCell(int x, int y, Board board)
        {
            if (x < 0 || x > 2 || y < 0 || y > 2)
                return false;

            if (board.Cells[x, y].Content != '.')
                return false;

            return true;
        }

        public static void ComputerMakeNextMove(int depth, Board board, bool turnForMax)
        {
            int[] bestState = FindBestMove(depth, board, !turnForMax);
            
            board.Cells[bestState[1], bestState[2]].Content = 'O';
           
            Console.WriteLine(board.ToString());
        }

        private static int[] FindBestMove(int depth, Board board, bool needMax)
        {
            int[] bestMove = new int[3];
            bestMove[0] = needMax ? -1000 : 1000;

            // Traverse all cells, evaluate minimax function for
            // all empty cells. And return the cell with optimal value.
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board.Cells[i, j].Content == '.')
                    {
                        // Make the move
                        board.Cells[i, j].Content = needMax ? 'X' : 'O';

                        // Compute evaluation function for this move
                        int[] moveVal = MiniMax(0,board, !needMax, int.MinValue + 1, int.MaxValue - 1);

                        // Undo the move
                        board.Cells[i, j].Content = '.';

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

        private static int[] MiniMax(int depth, Board board, bool needMax, int alpha, int beta)
        {

            //childNode = null;

            int score;
            int bestRow = -1;
            int bestCol = -1;

            if (board.GameOver())
            {
                //score = CurrentState.CalculateScore();
                //score = CurrentState.Evaluate(depth);
                score = board.Evaluate2();
                return new int[] { score, bestRow, bestCol };
            }

            if (needMax)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        board.Cells[i, j].Content = needMax ? 'X' : 'O';

                        if (!needMax)
                        {
                            score = MiniMax(depth + 1,board, !needMax, alpha, beta)[0];
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
                            score = MiniMax(depth + 1, board, needMax, alpha, beta)[0];
                            if (alpha < score)
                            {
                                alpha = score;
                                bestRow = move[0];
                                bestCol = move[1];
                            }
                        }

                        // undo move
                        board.Cells[move[0], move[1]].Content = '.';

                        // cut-off
                        if (alpha >= beta) break;
                    }
                }

            }
            return new int[] { needMax ? alpha : beta, bestRow, bestCol };
           
        }


        
    }
    

}
