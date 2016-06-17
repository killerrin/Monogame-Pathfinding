using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Heuristics
{
    public class DiagonalDistance : IPathfindingHeuristic
    {
        public bool AllowsDiagonalMovement { get { return true; } }
        public float CalculateHeuristic(NodePosition currentNode, NodePosition destinationNode, int horizontalTravelCost, int diagnolTravelCost)
        {
            float dx = Math.Abs(currentNode.X - destinationNode.X);
            float dy = Math.Abs(currentNode.Y - destinationNode.Y);
            return horizontalTravelCost * (dx + dy) + (diagnolTravelCost - 2 * horizontalTravelCost) * Math.Min(dx, dy);
        }
    }
}
