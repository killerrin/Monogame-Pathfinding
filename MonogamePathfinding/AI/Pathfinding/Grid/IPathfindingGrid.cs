﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Grid
{
    public interface IPathfindingGrid
    {
        int Width { get; }
        int Height { get; }

        IGridNode FindNode(int x, int y);
        IGridNode FindNode(NodePosition position);

        bool WithinGrid(NodePosition position);
        bool WithinGrid(int x, int y);
    }
}
