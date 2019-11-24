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

                TicTacToeGame game = new TicTacToeGame(userFirst);
                int depth = 3;

                while (!game.CurrentState.IsGameOver())
                {
                    if (userFirst)
                    {
                        game.GetNextMoveFromUser();
                        game.ComputerMakeNextMove(depth);
                    }
                    else
                    {
                        game.ComputerMakeNextMove(depth);
                        game.GetNextMoveFromUser();
                    }
                }

                Console.WriteLine("The final result is \n" + game.CurrentState.ToString());
                if (game.CurrentState.BestScore < -100)
                    Console.WriteLine("PlayerO has won.");
                else if (game.CurrentState.BestScore > 100)
                    Console.WriteLine("PlayerX has won.");
                else
                    Console.WriteLine("It is a tie.");

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
