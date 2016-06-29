using MonogamePathfinding.AI.Pathfinding.Events;
using MonogamePathfinding.AI.Pathfinding.Grid;
using MonogamePathfinding.AI.Pathfinding.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Engines
{
    public interface IHeuristicPathfindingEngine : IPathfindingEngine
    {
        int BaseMovementCost { get; set; }
        int BaseDiagonalMovementCost { get; set; }
        IPathfindingHeuristic Heuristic { get; set; }
    }
}
