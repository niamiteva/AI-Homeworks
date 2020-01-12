using System;

namespace TicTacToeAI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool stop = false;
            while (!stop)
            {
                bool userFirst = false;
               
                Console.WriteLine("User play against computer, Do you place the first step?[y/n]");
                if (Console.ReadLine().StartsWith("y", StringComparison.InvariantCultureIgnoreCase))
                {
                    userFirst = true;
                }

                Board boardGame = new Board();

                if (!userFirst)
                {
                    boardGame.Cells[0,0].Content = 'O';
                }

                Console.WriteLine(boardGame.ToString());
                
                int depth = 0;
                bool isTurnForMin = false;
                while (true)
                {
                    TicTacToeGame.GetNextMoveFromUser(boardGame);
                    if (boardGame.IsGameOver()) break;
                    TicTacToeGame.ComputerMakeNextMove(depth, boardGame, isTurnForMin);
                    if (boardGame.IsGameOver()) break;
                }

                //int finalScore = boardGame.Evaluate2();
                int finalScore = boardGame.Evaluate(0);
                if (finalScore < 0)
                    Console.WriteLine("PlayerO has won.");
                else if (finalScore > 0)
                    Console.WriteLine("PlayerX has won.");
                else
                    Console.WriteLine("It is a tie.");

                //Console.WriteLine($"The final result is: {finalScore} \n" + boardGame.ToString());

                Console.WriteLine("Try again?[y/n]");
                if (!Console.ReadLine().StartsWith("y", StringComparison.InvariantCultureIgnoreCase))
                {
                    stop = true;
                }
            }

            Console.WriteLine("bye");
        }
    }
}
