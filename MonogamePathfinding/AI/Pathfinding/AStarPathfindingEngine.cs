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
        public bool AllowDiagonalMovement { get; set; }
        public int BaseMovementCost { get; set; }
        public int BaseDiagonalMovementCost { get; set; }
        public IPathfindingGrid Grid { get; }

        public IPathfindingHeuristic HeuristicCalculator { get; set; }

        public AStarPathfindingEngine(bool allowDiagonalMovement, int movementCost, int diagonalMovementCost, IPathfindingGrid grid, IPathfindingHeuristic heuristic)
        {
            BaseMovementCost = movementCost;
            BaseDiagonalMovementCost = diagonalMovementCost;
            Grid = grid;
            HeuristicCalculator = heuristic;
        }

        public PathfindingResult FindPath(NodePosition startPosition, NodePosition endPosition)
        {
            if (!Grid.WithinGrid(startPosition)) return null;
            if (!Grid.WithinGrid(endPosition)) return null;

            // Create the opened and closed lists
            List<IPathfindingNode> closedList = new List<IPathfindingNode>();
            PriorityQueue<PriorityQueueNode<IPathfindingNode>> openedList = new PriorityQueue<PriorityQueueNode<IPathfindingNode>>();

            // Cache the End Node
            IGridNode endingGridNode = Grid.FindNode(endPosition);
            IPathfindingNode endingPathfindingNode = new PathfindingNode(endingGridNode);

            // Get the starting node and add it to the opened list
            IGridNode startingGridNode = Grid.FindNode(startPosition);
            IPathfindingNode startingPathfindingNode = new PathfindingNode(startingGridNode);
            openedList.Enqueue(new PriorityQueueNode<IPathfindingNode>(0, startingPathfindingNode));

            // Set up the current node at our starting position
            IPathfindingNode currentNode = startingPathfindingNode;

            bool foundPath = false;
            while (!foundPath)
            {
                if (openedList.Count == 0) { break; }

                List<IPathfindingNode> currentNodeNeighbors = GetAdjacentNodes(currentNode);
                foreach (var node in currentNodeNeighbors)
                {
                    // Check if the node is passable
                    if (node.GridNode.Navigatable == TraversalSettings.Unpassable) continue;

                    // If the ending nodes position is equal to the current node we are searching
                    // parent the end node to the node we are currently iterating through and return
                    if (endingPathfindingNode.GridNode.Position == node.GridNode.Position)
                    {
                        endingPathfindingNode.Parent = currentNode;
                        return new PathfindingResult(this, endingPathfindingNode, closedList, openedList.Select(x => x.Data).ToList());
                    }

                    if (!ClosedListContains(closedList, node))
                    {
                        node.Parent = currentNode;
                        int newMovementCost = currentNode.GridNode.MovementCost + BaseMovementCost;

                        if (OpenedListContains(openedList, node))
                        {
                            if (newMovementCost < node.GridNode.MovementCost)
                            {
                                node.GridNode.MovementCost = newMovementCost;
                            }
                        }
                        else
                        {
                            node.GridNode.MovementCost = newMovementCost;
                            AddToOpenedList(openedList, node, endingPathfindingNode);
                        }
                    }

                }

                if (!foundPath)
                {
                    AddToClosedList(closedList, currentNode);
                    currentNode = openedList.Dequeue().Data;
                }
            }

            // Sadly, there is no path to our target so we return null
            return new PathfindingResult(this, null, closedList, new List<IPathfindingNode>());
        }

        private List<IPathfindingNode> GetAdjacentNodes(IPathfindingNode centerNode)
        {
            // Using the helper methods, grab all of the nodes from each direction
            List<NodePosition> positions = new List<NodePosition>();
            positions.Add(centerNode.GridNode.Position.North());
            positions.Add(centerNode.GridNode.Position.South());
            positions.Add(centerNode.GridNode.Position.East());
            positions.Add(centerNode.GridNode.Position.West());

            if (AllowDiagonalMovement)
            {
                positions.Add(centerNode.GridNode.Position.NorthEast());
                positions.Add(centerNode.GridNode.Position.SouthEast());
                positions.Add(centerNode.GridNode.Position.NorthWest());
                positions.Add(centerNode.GridNode.Position.SouthWest());
            }

            // Remove all of the positions not currently within the grid
            for (int i = positions.Count - 1; i >= 0; i--)
            {
                if (!Grid.WithinGrid(positions[i]))
                    positions.RemoveAt(i);
            }

            // Finally, convert all of the positions into Grid Nodes and return
            List<IPathfindingNode> nodes = new List<IPathfindingNode>();
            foreach (var pos in positions)
            {
                nodes.Add(new PathfindingNode(Grid.FindNode(pos), centerNode));
            }

            return nodes;
        }

        #region Opened/Closed List Helper Methods
        private void AddToOpenedList(PriorityQueue<PriorityQueueNode<IPathfindingNode>> openedList, IPathfindingNode node, IPathfindingNode endNode)
        {

            openedList.Enqueue(new PriorityQueueNode<IPathfindingNode>(
                (int)HeuristicCalculator.CalculateHeuristic(node.GridNode.Position, endNode.GridNode.Position, 
                BaseMovementCost + node.GridNode.MovementCost,
                BaseDiagonalMovementCost + node.GridNode.MovementCost),
                node));
        }
        private void AddToClosedList(List<IPathfindingNode> closedList, IPathfindingNode node)
        {
            if (!ClosedListContains(closedList, node))
                closedList.Add(node);
        }

        private bool OpenedListContains(PriorityQueue<PriorityQueueNode<IPathfindingNode>> openedList, IPathfindingNode node)
        {
            for (int i = 0; i < openedList.Count; i++)
            {
                if (openedList[i].Data.GridNode.Position == node.GridNode.Position)
                    return true;
            }
            return false;
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
        #endregion
    }
}
