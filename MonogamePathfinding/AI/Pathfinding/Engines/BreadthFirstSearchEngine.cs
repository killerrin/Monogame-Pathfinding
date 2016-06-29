using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonogamePathfinding.AI.Pathfinding.Events;
using MonogamePathfinding.AI.Pathfinding.Grid;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace MonogamePathfinding.AI.Pathfinding.Engines
{
    public class BreadthFirstSearchEngine : IPathfindingEngine
    {
        public bool AllowDiagonalMovement { get; set; }
        public bool AllowHorizontalVerticalMovement { get; set; }
        public IPathfindingGrid Grid { get; }
        public event PathfindingEventHandler PathFound;

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

            Dictionary<UInt64, IPathfindingNode> closedList = new Dictionary<ulong, IPathfindingNode>();
            Queue<IPathfindingNode> openedQueue = new Queue<IPathfindingNode>();

            // Cache the End Node
            IGridNode endingGridNode = Grid.FindNode(endPosition);
            IPathfindingNode endingPathfindingNode = new PathfindingNode(endingGridNode);

            // Get the starting node and add it to the opened list
            IGridNode startingGridNode = Grid.FindNode(startPosition);
            IPathfindingNode startingPathfindingNode = new PathfindingNode(startingGridNode);
            openedQueue.Enqueue(startingPathfindingNode);

            IPathfindingNode currentNode = null;
            while (openedQueue.Count > 0)
            {
                currentNode = openedQueue.Dequeue();

                if (closedList.ContainsKey(currentNode.GridNode.Key())) continue;
                closedList[currentNode.GridNode.Key()] = currentNode;

                Debug.WriteLine($"OpenList Count: {openedQueue.Count} | Current Node Position: {currentNode.GridNode.Position}");

                // Get all of the Adjacent Nodes and convert them to pathfinding nodes
                var adjacentGridNodes = Grid.GetAdjacentNodes(currentNode.GridNode.Position, AllowHorizontalVerticalMovement, AllowDiagonalMovement);
                foreach (var adjacentGridNode in adjacentGridNodes)
                {
                    // Check if the node is passable
                    if (adjacentGridNode.Navigatable == TraversalSettings.Unpassable) continue;

                    // If the ending nodes position is equal to the current node we are searching
                    // parent the end node to the node we are currently iterating through and return
                    if (adjacentGridNode.Position == endingPathfindingNode.GridNode.Position)
                    {
                        Debug.WriteLine("Found Path");
                        endingPathfindingNode.Parent = currentNode;

                        var result = new PathfindingResult(endingPathfindingNode, closedList.Values, openedQueue);

                        if (PathFound != null)
                            PathFound(this, new PathfindingEventArgs(result));

                        return result;
                    }

                    // Otherwise, add it to the queue and continue to the next node
                    var node = new PathfindingNode(adjacentGridNode, currentNode);
                    openedQueue.Enqueue(node);
                }

            }

            return new PathfindingResult(null, closedList.Values, openedQueue);
        }
    }
}
