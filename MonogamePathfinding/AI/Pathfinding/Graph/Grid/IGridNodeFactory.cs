﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Graph.Grid
{
    public interface IGridNodeFactory
    {
        IGridNode CreateNode(NodePosition position);
    }
}
