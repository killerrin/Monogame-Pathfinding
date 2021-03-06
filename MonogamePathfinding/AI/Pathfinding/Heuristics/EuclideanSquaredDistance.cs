﻿using MonogamePathfinding.AI.Pathfinding.Graph.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Heuristics
{
    public class EuclideanSquaredDistance : IPathfindingHeuristic
    {
        public bool UsesDiagonalMovementCost { get { return false; } }
        float IPathfindingHeuristic.CalculateHeuristic(NodePosition currentNode, NodePosition destinationNode, int horizontalMovementCost, int diagnolMovementCost)
        {
            return CalculateHeuristic(currentNode, destinationNode, horizontalMovementCost);
        }

        public float CalculateHeuristic(NodePosition currentNode, NodePosition destinationNode, int horizontalMovementCost)
        {
            float dx = Math.Abs(currentNode.X - destinationNode.X);
            float dy = Math.Abs(currentNode.Y - destinationNode.Y);
            return horizontalMovementCost * (dx * dx + dy * dy);
        }
    }
}
