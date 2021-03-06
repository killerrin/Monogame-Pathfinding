﻿using MonogamePathfinding.AI.Pathfinding.Graph.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Heuristics
{
    public interface IPathfindingHeuristic
    {
        bool UsesDiagonalMovementCost { get; }
        float CalculateHeuristic(NodePosition currentNode, NodePosition destinationNode, int horizontalMovementCost, int diagnolMovementCost);
    }
}
