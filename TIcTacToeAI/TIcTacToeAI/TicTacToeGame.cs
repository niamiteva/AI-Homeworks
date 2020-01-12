using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeAI
{
    class TicTacToeGame
    {
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
            int[] bestState = FindBestMove(depth, board, turnForMax);
            
            board.Cells[bestState[1], bestState[2]].Content = 'O';
           
            Console.WriteLine(board.ToString());
        }

        private static int[] FindBestMove(int depth, Board board, bool needMax)
        {
            int[] bestMove = new int[3];
            bestMove[0] = needMax ? -1000 : 1000;
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board.Cells[i, j].Content == '.')
                    {
                        // Make the move
                        board.Cells[i, j].Content = needMax ? 'X' : 'O';

                        // Compute evaluation function for this move
                        int moveVal = MiniMax(0,board, !needMax, -100000, +100000);

                        // Undo the move
                        board.Cells[i, j].Content = '.';

                        if ((needMax && moveVal > bestMove[0]) ||
                            (!needMax && moveVal < bestMove[0]))
                        {
                            bestMove[1] = i;
                            bestMove[2] = j;
                            bestMove[0] = moveVal;
                        }
                    }
                }
            }
            return bestMove;
        }

        private static int MiniMax(int depth, Board board, bool needMax, int alpha, int beta)
        {

            //childNode = null;
            int score = 0;
            int bestScore = needMax ? -1000 : 1000;

            if (board.IsGameOver())
            {
                score = board.Evaluate(depth);
                //score = board.Evaluate2();
                return score;
            }
           
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(board.Cells[i,j].Content == '.')
                    {
                        board.Cells[i, j].Content = needMax ? 'X' : 'O';

                        if (needMax)
                        {
                            score = MiniMax(depth + 1, board, !needMax, alpha, beta);
                            bestScore = bestScore > score ? bestScore : score; //max
                            alpha = bestScore > alpha ? bestScore : alpha; //max
                        }
                        else
                        {
                            score = MiniMax(depth + 1, board, needMax, alpha, beta);
                            bestScore = bestScore < score ? bestScore : score; //min
                            beta = beta < bestScore ? beta : bestScore; //min                          
                        }

                        board.Cells[i, j].Content = '.';

                        if (beta <= alpha)
                            return bestScore;
                    
                    }
                }
            }

            return bestScore;
           
        }


        
    }
    

}
