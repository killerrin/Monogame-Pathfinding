﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding
{
    public class PathfindingNode : IPathfindingNode
    {
        public IPathfindingNode Parent { get; set; }
        public IGridNode GridNode { get; }

        public PathfindingNode(IGridNode gridNode)
        {
            GridNode = gridNode;
        }
        public PathfindingNode(IGridNode gridNode, IPathfindingNode parent)
        {
            GridNode = gridNode;
            Parent = parent;
        }
    }
}
