using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPuzzleSolver
{
    class AStarSolver
    {
        public int Level { get; set; } //gscore, moves count
        public int[] GoalEmptySpace { get; set; } //we save tha empty space position
        public int[,] StartState { get; set; } //
        public int RowsXCols { get; set; }

        private readonly int[,] goalState; //goal node to compare to
        private SimplePriorityQueue<StateNode> stateNodesInCurrenLevel; //all possible movement from tha last state => we choose the next move
        private readonly List<StateNode> solutionStepsRepresentation; //colects the movement for achieving the goal state
        private readonly List<string> solutionSteps;

        public AStarSolver(int[] goalEmptySpace, int[,] startState, int rowsXcols)
        {
            GoalEmptySpace = goalEmptySpace;
            StartState = startState;
            RowsXCols = rowsXcols;
            InitializeGoalState();
            Level = 0;
        }

        private void InitializeGoalState()
        {
            int value = 1;
            for (int i = 0; i < this.RowsXCols; i++)
            {
                for (int j = 0; j < this.RowsXCols; j++)
                {
                    if (i == this.GoalEmptySpace[0] && j == this.GoalEmptySpace[1])
                    {
                        this.goalState[i, j] = 0;
                        continue;
                    }
                    this.goalState[i, j] = value;
                    value++;
                }
            }
        }

        public void ExecuteAStar()
        {
            var startNode = new StateNode(StartState, RowsXCols);
            solutionStepsRepresentation.Add(startNode);
            stateNodesInCurrenLevel = ContructStateNodesInCurrentLevel(startNode);

        }

        private SimplePriorityQueue<StateNode> ContructStateNodesInCurrentLevel(StateNode current)
        {
            StateNode newNode = null;
            SimplePriorityQueue<StateNode> childNodes = new SimplePriorityQueue<StateNode>();
            for (int i = 0; i < 4; i++)
            {
                newNode = MakeMove(current.CurrentState, i, current.CurrentEmptySpace);
                if (newNode != null)
                {
                    childNodes.Push(newNode);
                }
            }
        }

        private StateNode MakeMove(int[,] node, int direction, int[] emptySpace)
        {

        }

        private void SwapValues()
        { }
    }
}
