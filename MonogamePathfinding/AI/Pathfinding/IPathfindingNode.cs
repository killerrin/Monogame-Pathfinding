﻿using MonogamePathfinding.AI.Pathfinding.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding
{
    public interface IPathfindingNode
    {
        IPathfindingNode Parent { get; set; }
        IGridNode GridNode { get; }
    }
}
