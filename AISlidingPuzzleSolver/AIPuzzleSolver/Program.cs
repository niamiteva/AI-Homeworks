using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPuzzleSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //todo validations

            Console.WriteLine(
            @"Sliding Puzzle inputs:
                 N - number of blocks (8, 15, 24)
                 I - the index of the position of the blank space in the goal state(0 - N), -1 - default position (bottom left corner)
                 Numbers, divided by space");

            Console.Write("Enter N: ");
            int blocks = int.Parse(Console.ReadLine());

            Console.Write("Enter I: ");
            int index = int.Parse(Console.ReadLine());
            while(index < -1 || index > blocks)
            {
                Console.WriteLine("Eneter valid index [-1, N]");
                index = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("Enter the puzzle:");
            int n = Convert.ToInt32(Math.Sqrt(blocks + 1));
            int[,] puzzle = new int[n, n];
            int[] blankSpace = new int[2];
            int counter = 0;
            for (int i = 0; i < n; i++)
            {
                var input = new string[n];
                input = Console.ReadLine().Split(' ');
                for (var j = 0; j < n; j++)
                {
                    puzzle[i, j] = Convert.ToInt32(input[j]);

                    if (counter == index)
                    {
                        blankSpace[0] = i;
                        blankSpace[1] = j;
                    }
                    counter++;
                }
            }

            if (index == -1)
            {
                blankSpace[0] = n - 1;
                blankSpace[1] = n - 1;
            }

            Console.WriteLine("\n");

            AStarSolver solver = new AStarSolver(blankSpace, puzzle, n);
            solver.ExecuteAStar();

            return;
        }
    }
}
