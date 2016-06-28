using MonogamePathfinding.AI.Pathfinding.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding
{
    public interface IPathfindingEngine
    {
        bool AllowDiagonalMovement { get; set; }
        int BaseMovementCost { get; set; }
        int BaseDiagonalMovementCost { get; set; }
        IPathfindingGrid Grid { get; }

        PathfindingResult FindPath(NodePosition startPosition, NodePosition endPosition);
    }
}
