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
        private SimplePriorityQueue<StateNode> stateNodesInCurrenLevel; //all possible movement from tha last state => we choose the next move
        private readonly List<StateNode> solutionStateNodes; //colects the movement for achieving the goal state
        private readonly List<string> solutionStepsStringRepresentation;

        public AStarSolver(int[] goalEmptySpace, int[,] startState, int rowsXcols)
        {
            GoalEmptySpace = goalEmptySpace;
            StartState = startState;
            RowsXCols = rowsXcols;
            Level = 0;

            goalState = new int[rowsXcols, rowsXcols];
            stateNodesInCurrenLevel = new SimplePriorityQueue<StateNode>();
            solutionStateNodes = new List<StateNode>();
            solutionStepsStringRepresentation = new List<string>();

            InitializeGoalState();
            
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
            int[,] startState = new int[RowsXCols, RowsXCols];
            Array.Copy(this.StartState, startState, StartState.Length);
            var rootNode = new StateNode(startState, RowsXCols);

            Debug.WriteLine("Level:\n{1} => startNode: {0}", rootNode.ToString(), this.Level);

            rootNode.CalculateHeuristicOfTheState(this.goalState);
            stateNodesInCurrenLevel.Enqueue(rootNode, rootNode.Heuristic + this.Level);
            solutionStateNodes.Add(rootNode);
            solutionStepsStringRepresentation.Add(rootNode.ToString());
            

            //while (rootNode.Heuristic != 0 )
            while(stateNodesInCurrenLevel.Count != 0)
            {
                //if (stateNodesInCurrenLevel.Count != 0)
                //{
                //    stateNodesInCurrenLevel = null; //empty the queue to add only the elements of the current level
                //}

                this.Level++;
                StateNode current = rootNode;
                //stateNodesInCurrenLevel = 
                ContructStateNodesInCurrentLevel(current);

                //if (stateNodesInCurrenLevel.Count <= 0) break;

                rootNode = stateNodesInCurrenLevel.Dequeue();

                solutionStateNodes.Add(rootNode);
                solutionStepsStringRepresentation.Add(rootNode.ToString());

                Debug.WriteLine("solution step chosen: matrix => \n{0} , cost => {1}, move => {2}", rootNode.ToString(), rootNode.Heuristic, rootNode.Move);

                if (rootNode.Heuristic == 0)
                {
                    break;
                }
            }

            PrintSolution();
            
        }

        //private SimplePriorityQueue<StateNode> ContructStateNodesInCurrentLevel(StateNode current)
        private void ContructStateNodesInCurrentLevel(StateNode current)
        {
            ///Debug.WriteLine("root:\nmatrix => {0} , heuristic => {1}", current.ToString(), current.Heuristic);

            StateNode newNode = null;
            //SimplePriorityQueue<StateNode> childNodes = new SimplePriorityQueue<StateNode>();
            for (int i = 0; i < 4; i++)
            {
                newNode = MakeMove(current.CurrentState, i, current.CurrentEmptySpace);
                
                if (newNode != null && !solutionStepsStringRepresentation.Contains(newNode.ToString()))
                {

                    //Debug.WriteLine($"new move:\n{newNode.ToString()} => {newNode.Move}");
                    newNode.CalculateHeuristicOfTheState(this.goalState);
                    Debug.WriteLine("move: {0} => h = {1}", newNode.Move, newNode.Heuristic);
                    var cost = newNode.Heuristic + this.Level;
                    //childNodes.Enqueue(newNode, cost);
                    stateNodesInCurrenLevel.Enqueue(newNode, cost);
                }
            }

            //return childNodes;
        }

        private StateNode MakeMove(int[,] root, int direction, int[] emptySpace)
        {
            int[,] node = new int[RowsXCols,RowsXCols];
            Array.Copy(root, node, root.Length);
            StateNode resultNode = null;

            if(direction == 0 && emptySpace[0] != 0)
            {
                //up (down actually)
                SwapValues(new int[2] { (emptySpace[0] - 1), emptySpace[1] }, new int[2] { emptySpace[0], emptySpace[1] }, node);
                resultNode = new StateNode(node, this.RowsXCols);
                resultNode.SetMove("Down");
            }
            else if(direction == 1 && emptySpace[1] != this.RowsXCols-1)
            {
                //right (left actually)
                SwapValues(new int[2] { emptySpace[0], (emptySpace[1] + 1) }, new int[2] { emptySpace[0], emptySpace[1] }, node);
                resultNode = new StateNode(node, this.RowsXCols);
                resultNode.SetMove("Left");
            }
            else if (direction == 2 && emptySpace[0] != this.RowsXCols-1)
            {
                //down (up actually)
                SwapValues(new int[2] { (emptySpace[0] + 1), emptySpace[1] }, new int[2] { emptySpace[0], emptySpace[1] }, node);
                resultNode = new StateNode(node, this.RowsXCols);
                resultNode.SetMove("Up");
            }
            else if (direction == 3 && emptySpace[1] != 0)
            {
                //left (right actually)
                SwapValues(new int[2] { emptySpace[0], (emptySpace[1] -1 )}, new int[2] { emptySpace[0], emptySpace[1] }, node);
                resultNode = new StateNode(node, this.RowsXCols);
                resultNode.SetMove("Right");
            }

            return resultNode;
        }

        private void SwapValues(int[] num, int[] blank, int[,] node)
        {
            int temp = node[num[0], num[1]];
            node[num[0], num[1]] = node[blank[0], blank[1]];
            node[blank[0], blank[1]] = temp;
        }

        private void PrintSolution()
        {
            //print the number of steps
            Console.WriteLine(this.Level);
            foreach (var item in this.solutionStateNodes)
            {
                Console.WriteLine(item.Move + "\n");
            }

            foreach (var item in this.solutionStepsStringRepresentation)
            {
                Console.WriteLine(item);
                if(this.solutionStepsStringRepresentation.Last() != item)
                {
                    for (int i = 0; i < this.RowsXCols; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine("\n=>\n");
                }
            }
        }
    }
}
