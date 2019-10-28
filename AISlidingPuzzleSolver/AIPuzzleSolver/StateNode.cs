using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPuzzleSolver
{
    class StateNode
    {
        public int Heuristic { get; set; } //hcrore for the state
        public int Level { get; set; }
        public int[,] CurrentState { get; set; } //state representation matrix
        //public int EmptySpaceCol { get; set; } //col index of the empty space
        //public int EmprySpaceRow { get; set; } // row index of the empty space
        public int[] CurrentEmptySpace { get; set; }
        public int Size { get; set; }
        public string Move { get; set; }
        public List<string> Path { get; set; }
        public int Cost { get; set; }

        public StateNode()
        {
        }

        public StateNode(int[,] state, int size, int level,string move, List<string> path, int[,] goal)
        {
            Level = level;
            CurrentState = state;
            Size = size;
            Move = move;
            Path = path;
            CurrentEmptySpace = new int[2];
            Heuristic = 0;
            Cost = 0;

            FindCurrentEmptySpace();
            CalculateHeuristicOfTheState(goal);
            Cost = Heuristic + Level;
        }

        private void FindCurrentEmptySpace()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (CurrentState[i, j] == 0)
                    {
                        CurrentEmptySpace[0] = i;
                        CurrentEmptySpace[1] = j;
                    }
                }
            }
        }

        public void CalculateHeuristicOfTheState(int[,] goalState)
        {
            //h = abs(curr.x - goal.x) + abc(curr.y -goal.y)
            int result = 0;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    var goalIndexors = IndexOfIn3DArray(goalState, CurrentState[i, j]);
                    result += (Math.Abs(i - goalIndexors[0]) + Math.Abs(j - goalIndexors[1]));
                }
            }

            Heuristic = result;
            //Debug.WriteLine("Current state heuristic {0}", this.Heuristic);
        }

        private int[] IndexOfIn3DArray(int[,] matrix, int value)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (value == matrix[i, j])
                    {
                        return new int[2] { i, j };
                    }
                }
            }
            return null;
        }

        public override string ToString()
        {
            string result = string.Empty;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    sb.Append(CurrentState[i,j]);
                    sb.Append(" ");
                }
                sb.Append("\n");
            }

            result = sb.ToString();

            return result;
        }
    }
}
