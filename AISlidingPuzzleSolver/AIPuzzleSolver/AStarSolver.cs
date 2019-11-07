using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPuzzleSolver
{
    class AStarSolver
    {
        public int Level { get; set; } //gscore, moves count
        public int[] GoalEmptySpace { get; set; } //we save tha empty space position
        private int[,] StartState { get; set; } //
        public int RowsXCols { get; set; }

        private readonly int[,] goalState; //goal node to compare to
        private SimplePriorityQueue<StateNode> PQStateNodes; //all possible movement from tha last state => we choose the next move
        private readonly List<StateNode> solutionStateNodes; //colects the movement for achieving the goal state
        private readonly List<string> solutionStepsStringRepresentation;

        public AStarSolver(int[] goalEmptySpace, int[,] startState, int rowsXcols)
        {
            GoalEmptySpace = goalEmptySpace;
            StartState = startState;
            RowsXCols = rowsXcols;
            Level = 0;

            goalState = new int[rowsXcols, rowsXcols];
            PQStateNodes = new SimplePriorityQueue<StateNode>();
            solutionStateNodes = new List<StateNode>();
            solutionStepsStringRepresentation = new List<string>();

            InitializeGoalState();
            
        }

        private void InitializeGoalState()
        {
            int value = 1;
            for (int i = 0; i < RowsXCols; i++)
            {
                for (int j = 0; j < RowsXCols; j++)
                {
                    if (i == GoalEmptySpace[0] && j == GoalEmptySpace[1])
                    {
                        goalState[i, j] = 0;
                        continue;
                    }
                    goalState[i, j] = value;
                    value++;
                }
            }
        }

        public void ExecuteAStar()
        {
            int[,] startState = new int[RowsXCols, RowsXCols];
            Array.Copy(StartState, startState, StartState.Length);

            var rootNode = new StateNode(startState, RowsXCols,0,string.Empty, new List<string>(), goalState);

            //Debug.WriteLine("Level:\n{1} => startNode: {0}", rootNode.ToString(), rootNode.Level);

            rootNode.CalculateHeuristicOfTheState(goalState);
            var limit = rootNode.Cost;
            
            while (true)
            {
                Console.WriteLine($"Threshold: {limit}");
                PQStateNodes.Enqueue(rootNode, rootNode.Cost);
                while (PQStateNodes.Count != 0)
                {

                    StateNode bestStep = PQStateNodes.Dequeue();
                    
                    //Debug.WriteLine("solution step chosen: matrix => \n{0} , cost => {1}, move => {2}", bestStep.ToString(), bestStep.Heuristic, bestStep.Move);

                    foreach (var child in ContructStateNodesInCurrentLevel(bestStep))
                    {

                        if (child.Heuristic == 0)
                        {

                            Console.WriteLine(child.Level);
                            foreach (var move in child.Path)
                            {
                                Console.WriteLine(move);
                            }

                            return;
                        }
                        
                        if (child.Cost <= limit)
                        {
                            PQStateNodes.Enqueue(child, child.Cost);
                        }
                    }

                }

                limit += 2;
            }
            
        }

        private List<StateNode> ContructStateNodesInCurrentLevel(StateNode current)
        {

            StateNode newNode = null;
            List<StateNode> children = new List<StateNode>();
            for (int i = 0; i < 4; i++)
            {
                newNode = MakeMove(current, i);

                if (newNode != null)
                {
                    
                    //Debug.WriteLine("move: {0} => h = {1}", newNode.Move, newNode.Heuristic);

                    children.Add(newNode);
                }
            }

            return children;
        }


        private StateNode MakeMove(StateNode current, int direction)
        {
            int[,] node = new int[RowsXCols, RowsXCols];
            Array.Copy(current.CurrentState, node, current.CurrentState.Length);
            int[] emptySpace = current.CurrentEmptySpace;

            StateNode newNode = null;

            if (direction == 0 && emptySpace[0] > 0)
            {
                //up (down actually)
                SwapValues(new int[2] { (emptySpace[0] - 1), emptySpace[1] }, new int[2] { emptySpace[0], emptySpace[1] }, node);
                List<string> newRoad = current.Path.GetRange(0, current.Path.Count);
                newRoad.Add("Down");
                newNode = new StateNode(node, RowsXCols, current.Level + 1, "Down", newRoad, goalState);
            }
            else if (direction == 1 && emptySpace[1] < RowsXCols - 1)
            {
                //right (left actually)
                SwapValues(new int[2] { emptySpace[0], (emptySpace[1] + 1) }, new int[2] { emptySpace[0], emptySpace[1] }, node);
                List<string> newRoad = current.Path.GetRange(0, current.Path.Count);
                newRoad.Add("Left");
                newNode = new StateNode(node, RowsXCols, current.Level + 1, "Left", newRoad, goalState);
            }
            else if (direction == 2 && emptySpace[0] < RowsXCols - 1)
            {
                //down (up actually)
                SwapValues(new int[2] { (emptySpace[0] + 1), emptySpace[1] }, new int[2] { emptySpace[0], emptySpace[1] }, node);
                List<string> newRoad = current.Path.GetRange(0, current.Path.Count);
                newRoad.Add("Up");
                newNode = new StateNode(node, RowsXCols, current.Level + 1, "Up", newRoad, goalState);
            }
            else if (direction == 3 && emptySpace[1] > 0)
            {
                //left (right actually)
                SwapValues(new int[2] { emptySpace[0], (emptySpace[1] - 1) }, new int[2] { emptySpace[0], emptySpace[1] }, node);
                List<string> newRoad = current.Path.GetRange(0, current.Path.Count);
                newRoad.Add("Right");
                newNode = new StateNode(node, this.RowsXCols, current.Level + 1, "Right", newRoad, goalState);
            }

            return newNode;
        }

        private void SwapValues(int[] num, int[] blank, int[,] node)
        {
            //int temp = node[num[0], num[1]];
            node[blank[0], blank[1]] = node[num[0], num[1]];
            node[num[0], num[1]] = 0;
        }

        private void PrintSolution()
        {
            //print the number of steps
            Console.WriteLine(this.Level);
            foreach (var item in this.solutionStateNodes)
            {
                Console.WriteLine(item.Move + "\n");
            }
            
        }
    }
}
