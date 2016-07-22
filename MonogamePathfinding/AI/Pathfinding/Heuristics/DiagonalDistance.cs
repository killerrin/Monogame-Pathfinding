using MonogamePathfinding.AI.Pathfinding.Graph.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Heuristics
{
    public class DiagonalDistance : IPathfindingHeuristic
    {
        public bool UsesDiagonalMovementCost { get { return true; } }
        public float CalculateHeuristic(NodePosition currentNode, NodePosition destinationNode, int horizontalMovementCost, int diagnolMovementCost)
        {
            float dx = Math.Abs(currentNode.X - destinationNode.X);
            float dy = Math.Abs(currentNode.Y - destinationNode.Y);
            return horizontalMovementCost * (dx + dy) + (diagnolMovementCost - 2 * horizontalMovementCost) * Math.Min(dx, dy);
        }
    }
}
