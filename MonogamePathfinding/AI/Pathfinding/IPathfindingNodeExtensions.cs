﻿using MonogamePathfinding.AI.Pathfinding.Graph.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding
{
    public static class IPathfindingNodeExtensions
    {
        public static IPathfindingNode Reverse(this IPathfindingNode root)
        {
            IPathfindingNode p = root, n = null;
            while (p != null)
            {
                IPathfindingNode tmp = p.Parent;
                p.Parent = n;
                n = p;
                p = tmp;
            }
            root = n;
            return root;
        }
    }
}
