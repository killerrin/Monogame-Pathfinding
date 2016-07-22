using MonogamePathfinding.AI.Pathfinding.Grid;
using MonogamePathfinding.AI.Pathfinding.Heuristics;
using MonogamePathfinding.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonogamePathfinding.AI.Pathfinding.Events;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using Priority_Queue;

namespace MonogamePathfinding.AI.Pathfinding.Engines
{
    public class AStarPathfindingEngine : IHeuristicPathfindingEngine
    {
        //IPathfindingEngine
        public IPathfindingGrid Grid { get; }
        public IPathfindingNodeFactory NodeFactory { get; }
        public bool AllowHorizontalVerticalMovement { get; set; }
        public bool AllowDiagonalMovement { get; set; }
        public event PathfindingEventHandler PathFound;
        public event PathfindingEventHandler PathInProgress;
        public event PathfindingEventHandler PathFailed;

        //IHeuristicPathfindingEngine
        public int BaseMovementCost { get; set; }
        public int BaseDiagonalMovementCost { get; set; }
        public IPathfindingHeuristic Heuristic { get; set; }

        public AStarPathfindingEngine(IPathfindingGrid grid, IPathfindingNodeFactory nodeFactory, bool allowHorizontalVerticalMovement, int movementCost, bool allowDiagonalMovement, int diagonalMovementCost, IPathfindingHeuristic heuristic)
        {
            Grid = grid;
            NodeFactory = nodeFactory;
            AllowHorizontalVerticalMovement = allowHorizontalVerticalMovement;
            AllowDiagonalMovement = allowDiagonalMovement;

            BaseMovementCost = movementCost;
            BaseDiagonalMovementCost = diagonalMovementCost;
            Heuristic = heuristic;
        }

        public PathfindingResult FindPath(NodePosition startPosition, NodePosition endPosition)
        {
            if (!Grid.NodeExists(startPosition)) return null;
            if (!Grid.NodeExists(endPosition)) return null;

            // Create the opened and closed lists
            Dictionary<UInt64, IPathfindingNode> closedList = new Dictionary<ulong, IPathfindingNode>();
            Dictionary<UInt64, IPathfindingNode> quickSearchOpenedList = new Dictionary<ulong, IPathfindingNode>();

            //PriorityQueue<PriorityQueueNode<IPathfindingNode>> openedList = new PriorityQueue<PriorityQueueNode<IPathfindingNode>>();
            //FastPriorityQueue<PriorityPathfindingNode> openedList = new FastPriorityQueue<PriorityPathfindingNode>(Grid.Width * Grid.Height);
            BinaryHeap<float, IPathfindingNode> openedList = BinaryHeap<float, IPathfindingNode>.MinBinaryHeap(4);

            // Cache the End Node
            IGridNode endingGridNode = Grid.FindNode(endPosition);
            IPathfindingNode endingPathfindingNode = NodeFactory.CreateNode(endingGridNode, null);

            // Get the starting node and add it to the opened list
            IGridNode startingGridNode = Grid.FindNode(startPosition);
            IPathfindingNode startingPathfindingNode = NodeFactory.CreateNode(startingGridNode, null);

            //openedList.Enqueue(new PriorityQueueNode<IPathfindingNode>(0, startingPathfindingNode));
            //openedList.Enqueue(new PriorityPathfindingNode(startingPathfindingNode), 0.0f);
            openedList.Insert(0.0f, startingPathfindingNode);
            quickSearchOpenedList[startingPathfindingNode.GridNode.Key()] = startingPathfindingNode;

            // Begin Pathfind
            IPathfindingNode currentNode = null;
            while (openedList.Count > 0)
            {
                //currentNode = openedList.Dequeue().Data;
                currentNode = openedList.Pop().Data;
                quickSearchOpenedList.Remove(currentNode.GridNode.Key());

                // Get all of the Adjacent Nodes to our Current Node
                var adjacentGridNodes = Grid.GetAdjacentNodes(currentNode.GridNode.Position, AllowHorizontalVerticalMovement, AllowDiagonalMovement);
                List<IPathfindingNode> currentNodeNeighbors = new List<IPathfindingNode>();
                foreach (var adjacentGridNode in adjacentGridNodes)
                {
                    currentNodeNeighbors.Add(NodeFactory.CreateNode(adjacentGridNode, currentNode));
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

                        var result = new PathfindingResult(endingPathfindingNode, closedList.Values, quickSearchOpenedList.Values);

                        if (PathFound != null)
                            PathFound(this, new PathfindingEventArgs(result));
                        return result;
                    }

                    if (!closedList.ContainsKey(node.GridNode.Key()))
                    {
                        node.Parent = currentNode;

                        var baseMovement = node.GridNode.Position.IsNextTo(currentNode.GridNode.Position) ? BaseMovementCost : BaseDiagonalMovementCost;
                        int newMovementCost = currentNode.CurrentMovementCost + currentNode.GridNode.MovementCost + baseMovement;

                        if (quickSearchOpenedList.ContainsKey(node.GridNode.Key()))
                        {
                            //if (newMovementCost < node.GridNode.MovementCost)
                            if (newMovementCost < node.CurrentMovementCost)
                            {
                                //node.GridNode.MovementCost = newMovementCost;
                                node.CurrentMovementCost = newMovementCost;
                            }
                        }
                        else
                        {
                            //node.GridNode.MovementCost = newMovementCost;
                            node.CurrentMovementCost = newMovementCost;

                            float heuristic = Heuristic.CalculateHeuristic(node.GridNode.Position, endingPathfindingNode.GridNode.Position,
                                node.GridNode.MovementCost + BaseMovementCost,
                                node.GridNode.MovementCost + BaseDiagonalMovementCost);
                            //Debug.WriteLine($"Heuristic: {heuristic} | MovementCost: {node.GridNode.MovementCost} | BaseMovement: {BaseMovementCost} | DiagonalMovement: {BaseDiagonalMovementCost}");

                            //openedList.Enqueue(new PriorityQueueNode<IPathfindingNode>((int)heuristic, node));
                            //openedList.Enqueue(new PriorityPathfindingNode(node), heuristic);
                            openedList.Insert(heuristic, node);
                            quickSearchOpenedList[node.GridNode.Key()] = node;
                        }
                    }
                }

                if (!closedList.ContainsKey(currentNode.GridNode.Key()))
                    closedList[currentNode.GridNode.Key()] = currentNode;

                if (PathInProgress != null)
                    PathInProgress(this, new PathfindingEventArgs(new PathfindingResult(null, closedList.Values.ToList(), quickSearchOpenedList.Values.ToList())));
            }

            // Sadly, there is no path to our target so we return null
            var failedResult = new PathfindingResult(null, closedList.Values, quickSearchOpenedList.Values.ToList());

            if (PathFailed != null)
                PathFailed(this, new PathfindingEventArgs(failedResult));
            return failedResult;
        }
    }

    public class PriorityPathfindingNode : FastPriorityQueueNode
    {
        public IPathfindingNode Data { get; }
        public PriorityPathfindingNode(IPathfindingNode data)
        {
            Data = data;
        }
    }
}
