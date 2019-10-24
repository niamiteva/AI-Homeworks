using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPuzzleSolver
{
    class AStarSolver
    {
        public int Level { get; set; } //gscore, moves count
        public int[,] GoalEmptySpace { get; set; } //we save tha empty space position
        public int[,] SatrtState { get; set; } //
        public int NumberOfBlocks { get; set; }

        private int[,] goalState; //goal node to compare to
        private readonly SimplePriorityQueue<StateNode> stateNodesInCurrenLevel; //all possible movement from tha last state => we choose the next move
        private readonly List<StateNode> solutionSteps; //colects the movement for achieving the goal state

        public AStarSolver(int[,] goalEmptySpace, int[,] satrtState, int numberOfBlocks)
        {
            GoalEmptySpace = goalEmptySpace;
            SatrtState = satrtState;
            NumberOfBlocks = numberOfBlocks;
        }
    }
}
