using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Heuristics
{
    public interface IPathfindingHeuristic
    {
        bool AllowsDiagonalMovement { get; }
        float CalculateHeuristic(NodePosition currentNode, NodePosition destinationNode, int horizontalTravelCost, int diagnolTravelCost);
    }
}
