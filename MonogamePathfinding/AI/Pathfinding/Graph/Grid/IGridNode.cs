using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Graph.Grid
{
    public interface IGridNode
    {
        NodePosition Position { get; }

        int MovementCost { get; set; }
        TraversalSettings Navigatable { get; set; }
    }
}
