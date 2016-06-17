using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Heuristics
{
    public class EuclideanDistance : IPathfindingHeuristic
    {
        public bool AllowsDiagonalMovement { get { return true; } }

        float IPathfindingHeuristic.CalculateHeuristic(NodePosition currentNode, NodePosition destinationNode, int horizontalTravelCost, int diagnolTravelCost)
        {
            return CalculateHeuristic(currentNode, destinationNode, horizontalTravelCost);
        }

        public float CalculateHeuristic(NodePosition currentNode, NodePosition destinationNode, int horizontalTravelCost)
        {
            float dx = Math.Abs(currentNode.X - destinationNode.X);
            float dy = Math.Abs(currentNode.Y - destinationNode.Y);
            return horizontalTravelCost * (float)Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
