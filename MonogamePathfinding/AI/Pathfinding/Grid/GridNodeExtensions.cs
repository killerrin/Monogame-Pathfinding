﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Grid
{
    public static class GridNodeExtensions
    {
        public static UInt64 Key(this IGridNode node)
        {
            return (((UInt64)(UInt32)node.Position.X) << 32) | (UInt64)(UInt32)node.Position.Y;
        }
    }
}
