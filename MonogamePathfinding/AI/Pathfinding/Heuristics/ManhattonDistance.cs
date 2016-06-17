using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Heuristics
{
    public class ManhattonDistance : IPathfindingHeuristic
    {
        public bool AllowsDiagonalMovement { get { return false; } }

        float IPathfindingHeuristic.CalculateHeuristic(NodePosition currentNode, NodePosition destinationNode, int horizontalTravelCost, int diagnolTravelCost)
        {
            return CalculateHeuristic(currentNode, destinationNode, horizontalTravelCost);
        }

        public float CalculateHeuristic(NodePosition currentNode, NodePosition destinationNode, int horizontalTravelCost)
        {
            float dx = Math.Abs(currentNode.X - destinationNode.X);
            float dy = Math.Abs(currentNode.Y - destinationNode.Y);
            return horizontalTravelCost * (dx + dy);
        }
    }
}
