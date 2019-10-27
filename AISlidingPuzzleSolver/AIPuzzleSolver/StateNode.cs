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
        public int[,] CurrentState { get; set; } //state representation matrix
        //public int EmptySpaceCol { get; set; } //col index of the empty space
        //public int EmprySpaceRow { get; set; } // row index of the empty space
        public int[] CurrentEmptySpace { get; set; }
        public int Size { get; set; }
        public string Move { get; set; }

        public StateNode()
        {
        }

        public StateNode(int[,] state, int size)
        {
            Heuristic = 0;
            CurrentState = state;
            Size = size;
            Move = string.Empty;
            CurrentEmptySpace = new int[2];

            FindCurrentEmptySpace();
        }

        public void SetMove(string move)
        {
            Move = move;
        }

        private void FindCurrentEmptySpace()
        {
            for (int i = 0; i < this.Size; i++)
            {
                for (int j = 0; j < this.Size; j++)
                {
                    if (this.CurrentState[i, j] == 0)
                    {
                        this.CurrentEmptySpace[0] = i;
                        this.CurrentEmptySpace[1] = j;
                    }
                }
            }
        }

        public void CalculateHeuristicOfTheState(int[,] goalState)
        {
            //h = abs(curr.x - goal.x) + abc(curr.y -goal.y)
            int result = 0;
            for (int i = 0; i < this.Size; i++)
            {
                for (int j = 0; j < this.Size; j++)
                {
                    var goalIndexors = IndexOfIn3DArray(goalState, this.CurrentState[i, j]);
                    result += (Math.Abs(i - goalIndexors[0]) + Math.Abs(j - goalIndexors[1]));
                }
            }

            this.Heuristic = result;
            Debug.WriteLine("Current state heuristic {0}", this.Heuristic);
        }

        private int[] IndexOfIn3DArray(int[,] matrix, int value)
        {
            for (int i = 0; i < this.Size; i++)
            {
                for (int j = 0; j < this.Size; j++)
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

            for (int i = 0; i < this.Size; i++)
            {
                for (int j = 0; j < this.Size; j++)
                {
                    sb.Append(this.CurrentState[i,j]);
                    sb.Append(" ");
                }
                sb.Append("\n");
            }

            result = sb.ToString();

            return result;
        }
    }
}
