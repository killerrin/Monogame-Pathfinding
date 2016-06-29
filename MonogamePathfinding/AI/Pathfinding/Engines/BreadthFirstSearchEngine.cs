using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonogamePathfinding.AI.Pathfinding.Events;
using MonogamePathfinding.AI.Pathfinding.Grid;

namespace MonogamePathfinding.AI.Pathfinding.Engines
{
    public class BreadthFirstSearchEngine : IPathfindingEngine
    {
        public bool AllowDiagonalMovement { get; set; }
        public bool AllowHorizontalVerticalMovement { get; set; }
        public IPathfindingGrid Grid { get; }
        public event EventHandler<PathfindingEventArgs> PathFound;

        public BreadthFirstSearchEngine(bool allowHorizontalVerticalMovement, bool allowDiagonalMovement, IPathfindingGrid grid)
        {
            AllowHorizontalVerticalMovement = allowHorizontalVerticalMovement;
            AllowDiagonalMovement = allowDiagonalMovement;
            Grid = grid;
        }

        public PathfindingResult FindPath(NodePosition startPosition, NodePosition endPosition)
        {
            if (!Grid.WithinGrid(startPosition)) return null;
            if (!Grid.WithinGrid(endPosition)) return null;

            List<IPathfindingNode> closedList = new List<IPathfindingNode>();
            Queue<IPathfindingNode> openedList = new Queue<IPathfindingNode>();

            // Cache the End Node
            IGridNode endingGridNode = Grid.FindNode(endPosition);
            IPathfindingNode endingPathfindingNode = new PathfindingNode(endingGridNode);

            // Get the starting node and add it to the opened list
            IGridNode startingGridNode = Grid.FindNode(startPosition);
            IPathfindingNode startingPathfindingNode = new PathfindingNode(startingGridNode);
            openedList.Enqueue(startingPathfindingNode);

            IPathfindingNode currentNode = null;
            while (openedList.Count > 0)
            {
                currentNode = openedList.Dequeue();

                // Get all of the Adjacent Nodes and convert them to pathfinding nodes
                var adjacentGridNodes = Grid.GetAdjacentNodes(currentNode.GridNode.Position, AllowHorizontalVerticalMovement, AllowDiagonalMovement);
                List<IPathfindingNode> currentNodeNeighbors = new List<IPathfindingNode>();
                foreach (var adjacentGridNode in adjacentGridNodes)
                {
                    currentNodeNeighbors.Add(new PathfindingNode(adjacentGridNode, currentNode));
                }

                // Go through all the adjacentNodes and preform Pathfinding
                foreach (var node in currentNodeNeighbors)
                {
                    // Check if the node is passable
                    if (node.GridNode.Navigatable == TraversalSettings.Unpassable) continue;

                    // If the ending nodes position is equal to the current node we are searching
                    // parent the end node to the node we are currently iterating through and return
                    if (endingPathfindingNode.GridNode.Position == node.GridNode.Position)
                    {
                        endingPathfindingNode.Parent = currentNode;

                        var result = new PathfindingResult(endingPathfindingNode, closedList, openedList);

                        if (PathFound != null)
                            PathFound(this, new PathfindingEventArgs(result));

                        return result;
                    }

                    // Otherwise, add it to the queue and continue to the next node
                    if (!ClosedListContains(closedList, node))
                        openedList.Enqueue(node);
                }

                closedList.Add(currentNode);
            }

            return new PathfindingResult(null, closedList, openedList);
        }

        private bool ClosedListContains(List<IPathfindingNode> closedList, IPathfindingNode node)
        {
            for (int i = 0; i < closedList.Count; i++)
            {
                if (closedList[i].GridNode.Position == node.GridNode.Position)
                    return true;
            }
            return false;
        }
    }
}
