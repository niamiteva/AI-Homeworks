using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPuzzleSolver
{
    class StateNode
    {
        public int Heuristic { get; set; } //hcrore for the state
        public int[,] CurrentState { get; set; } //state representation matrix
        //public int EmptySpaceCol { get; set; } //col index of the empty space
        //public int EmprySpaceRow { get; set; } // row index of the empty space
        public int[,] CurrentEmptySpace { get; set; }

        public StateNode()
        {
        }

        public StateNode(int[,] state, int[,] emptySpace)
        {
            Heuristic = 0;
            CurrentState = state;
            CurrentEmptySpace = emptySpace;
        }
    }
}
