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
        IPathfindingGrid Grid { get; }
        int BaseMovementCost { get; set; }

        IPathfindingNode FindPath(NodePosition startPosition, NodePosition endPosition);
    }
}
