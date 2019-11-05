using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRAPuzzleSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter number of Queens: ");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine("Draw board with queens positions (y/n):");
            string drawIt = Console.ReadLine();

            Board board = new Board(n);

            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            board.ExecuteIterativeRepairAlgorithm();

            if(drawIt == "y")
            {
                Console.WriteLine(board.ToString());
            }

            watch.Stop();
            Console.WriteLine($"Time: {watch.Elapsed}\n");

            return;
        }

    }
}
