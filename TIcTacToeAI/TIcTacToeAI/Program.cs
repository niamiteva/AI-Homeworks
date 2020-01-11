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
                //TicTacToeGame game = new TicTacToeGame(userFirst);
                int depth = 0;

                while (true)
                {
                    if (userFirst)
                    {
                        TicTacToeGame.GetNextMoveFromUser(boardGame);

                        if (boardGame.IsGameOver()) break;

                        TicTacToeGame.ComputerMakeNextMove(depth, boardGame, userFirst);
                    }
                    else
                    {
                        game.ComputerMakeNextMove(depth, userFirst);

                        if (game.CurrentState.IsGameOver()) break;

                        game.GetNextMoveFromUser();
                    }

                    if (game.CurrentState.IsGameOver()) break;
                }

                int finalScore = game.CurrentState.Evaluate2();
                //int finalScore = game.CurrentState.Evaluate(0);
                //int finalScore = game.CurrentState.CalculateScore();
                if (finalScore < 0)
                    Console.WriteLine("PlayerO has won.");
                else if (finalScore > 0)
                    Console.WriteLine("PlayerX has won.");
                else
                    Console.WriteLine("It is a tie.");

                Console.WriteLine($"The final result is: {finalScore} \n" + game.CurrentState.ToString());

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
