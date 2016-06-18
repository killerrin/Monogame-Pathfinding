using MonogamePathfinding.AI.Pathfinding.Grid;
using MonogamePathfinding.AI.Pathfinding.Heuristics;
using MonogamePathfinding.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding
{
    public class AStarPathfindingEngine : IPathfindingEngine
    {
        public IPathfindingHeuristic HeuristicCalculator { get; set; }
        public IPathfindingGrid Grid { get; }

        public AStarPathfindingEngine(IPathfindingHeuristic heuristic, IPathfindingGrid grid)
        {
            HeuristicCalculator = heuristic;
            Grid = grid;
        }

        public object FindPath(NodePosition startPosition, NodePosition endPosition)
        {
            // Create the opened and closed lists
            List<IPathfindingNode> closedList = new List<IPathfindingNode>();
            PriorityQueue<PriorityQueueNode<IPathfindingNode>> openedList = new PriorityQueue<PriorityQueueNode<IPathfindingNode>>();

            // Get the starting node and add it to the opened list
            IGridNode startingGridNode = Grid.FindNode(startPosition);
            IGridNode endingGridNode = Grid.FindNode(endPosition);

            IPathfindingNode startingPathfindingNode = new PathfindingNode(startingGridNode);
            openedList.Enqueue(new PriorityQueueNode<IPathfindingNode>(startingPathfindingNode, 0));

            bool foundPath = false;
            while (!foundPath)
            {
                if (openedList.Count == 0)
                {
                    break;
                }
            }

            return null;
        }
    }
}
