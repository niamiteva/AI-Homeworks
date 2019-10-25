using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            //gets input data
            //creates instance of a node -> starting point
            //creates instance of a IDA -> passes tha data
            //prints the solution...

            Console.WriteLine(@"Sliding Puzzle inputs:
                                N - number of blocks (8, 15, 24)
                                I - the index of the position of the blank space in the goal state(0 - N), -1 - default position (bottom left corner)
                                Numbers, divided by space");

            Console.Write("Enter N: ");
            int blocks = int.Parse(Console.ReadLine());
            Console.Write("Enter I: ");
            int index = int.Parse(Console.ReadLine());
            
            Console.WriteLine("Enter the puzzle:");
            int n = Convert.ToInt32(Math.Sqrt(blocks + 1));
            int[,] puzzle = new int[n,n];
            int[] blankSpace = new int[2];
            int counter = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    puzzle[i,j] = int.Parse(Console.Read());
                    if(counter == index)
                    {
                        blankSpace[0] = i;
                        blankSpace[1] = j;
                    }
                    counter++;                   
                }
            }
            
            if(intex == -1)
            {
                blankSpace[0] = n-1;
                blankSpace[1] = n-1;
            }

            AStarSolver solver = new AStarSolver(blankSpace, puzzle, n);

        }
    }
}
