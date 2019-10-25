using System;
using System.Collections.Generic;
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
            FindCurrentEmptySpace();
        }

        public void SetMove() { }

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

        public int CalculateHeuristicOfTheState(int[,] goalState)
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

            return result;
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
    }
}
