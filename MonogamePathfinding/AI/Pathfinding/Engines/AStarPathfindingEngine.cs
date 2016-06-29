﻿using MonogamePathfinding.AI.Pathfinding.Grid;
using MonogamePathfinding.AI.Pathfinding.Heuristics;
using MonogamePathfinding.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonogamePathfinding.AI.Pathfinding.Events;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace MonogamePathfinding.AI.Pathfinding.Engines
{
    public class AStarPathfindingEngine : IHeuristicPathfindingEngine
    {
        //IPathfindingEngine
        public bool AllowHorizontalVerticalMovement { get; set; }
        public bool AllowDiagonalMovement { get; set; }
        public IPathfindingGrid Grid { get; }
        public event PathfindingEventHandler PathFound;

        //IHeuristicPathfindingEngine
        public int BaseMovementCost { get; set; }
        public int BaseDiagonalMovementCost { get; set; }
        public IPathfindingHeuristic Heuristic { get; set; }

        public AStarPathfindingEngine(bool allowHorizontalVerticalMovement, int movementCost, bool allowDiagonalMovement, int diagonalMovementCost, IPathfindingGrid grid, IPathfindingHeuristic heuristic)
        {
            AllowHorizontalVerticalMovement = allowHorizontalVerticalMovement;
            AllowDiagonalMovement = allowDiagonalMovement;
            Grid = grid;

            BaseMovementCost = movementCost;
            BaseDiagonalMovementCost = diagonalMovementCost;
            Heuristic = heuristic;
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

            // Begin Pathfind
            IPathfindingNode currentNode = null;
            while (openedList.Count > 0)
            {
                currentNode = openedList.Dequeue().Data;

                // Get all of the Adjacent Nodes to our Current Node
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

                        var result = new PathfindingResult(endingPathfindingNode, closedList, openedList.Select(x => x.Data));

                        if (PathFound != null)
                            PathFound(this, new PathfindingEventArgs(result));

                        return result;
                    }

                    if (!ClosedListContains(closedList, node))
                    {
                        node.Parent = currentNode;

                        var baseMovement = node.GridNode.Position.IsNextTo(currentNode.GridNode.Position) ? BaseMovementCost : BaseDiagonalMovementCost;
                        int newMovementCost = currentNode.GridNode.MovementCost + baseMovement;

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

                AddToClosedList(closedList, currentNode);
            }

            // Sadly, there is no path to our target so we return null
            return new PathfindingResult(null, closedList, new List<IPathfindingNode>());
        }

        #region Opened/Closed List Helper Methods
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddToOpenedList(PriorityQueue<PriorityQueueNode<IPathfindingNode>> openedList, IPathfindingNode node, IPathfindingNode endNode)
        {
            float heuristic = Heuristic.CalculateHeuristic(node.GridNode.Position, endNode.GridNode.Position,
                node.GridNode.MovementCost + BaseMovementCost,
                node.GridNode.MovementCost + BaseDiagonalMovementCost);
            Debug.WriteLine($"Heuristic: {heuristic} | MovementCost: {node.GridNode.MovementCost} | BaseMovement: {BaseMovementCost} | DiagonalMovement: {BaseDiagonalMovementCost}");

            openedList.Enqueue(new PriorityQueueNode<IPathfindingNode>((int)heuristic, node));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddToClosedList(List<IPathfindingNode> closedList, IPathfindingNode node)
        {
            if (!ClosedListContains(closedList, node))
                closedList.Add(node);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool OpenedListContains(PriorityQueue<PriorityQueueNode<IPathfindingNode>> openedList, IPathfindingNode node)
        {
            for (int i = 0; i < openedList.Count; i++)
            {
                if (openedList[i].Data.GridNode.Position == node.GridNode.Position)
                    return true;
            }
            return false;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
