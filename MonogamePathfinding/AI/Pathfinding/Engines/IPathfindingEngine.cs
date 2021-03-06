﻿using MonogamePathfinding.AI.Pathfinding.Events;
using MonogamePathfinding.AI.Pathfinding.Graph.Grid;
using MonogamePathfinding.AI.Pathfinding.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Engines
{
    public interface IPathfindingEngine
    {
        IPathfindingGrid Grid { get; }
        IPathfindingNodeFactory NodeFactory { get; }

        bool AllowHorizontalVerticalMovement { get; set; }
        bool AllowDiagonalMovement { get; set; }

        PathfindingResult FindPath(IGridNode start, IGridNode end);

        event PathfindingEventHandler PathFound;
        event PathfindingEventHandler PathInProgress;
        event PathfindingEventHandler PathFailed;
    }
}
